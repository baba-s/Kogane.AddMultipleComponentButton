using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal static class AddMultipleComponentButton
    {
        private static EditorWindow  m_inspectorWindow;
        private static VisualElement m_addComponentButton;
        private static Button        m_addMultipleComponentButton;

        static AddMultipleComponentButton()
        {
            EditorApplication.update   -= OnUpdate;
            Selection.selectionChanged -= OnChangedSelection;

            // 1 フレーム遅らせないと Inspector に Button を追加できない
            EditorApplication.delayCall += () =>
            {
                EditorApplication.update   += OnUpdate;
                Selection.selectionChanged += OnChangedSelection;

                OnUpdate();
                OnChangedSelection();
            };
        }

        private static void UpdateInspectorWindow()
        {
            if ( m_inspectorWindow != null ) return;

            var windowType = typeof( Editor ).Assembly.GetType( "UnityEditor.InspectorWindow" );

            m_inspectorWindow = ( EditorWindow )Resources
                    .FindObjectsOfTypeAll( windowType )
                    .FirstOrDefault()
                ;
        }

        private static void UpdateAddComponentButton()
        {
            if ( m_addComponentButton != null ) return;

            m_addComponentButton = m_inspectorWindow.rootVisualElement
                .Q( className: "unity-inspector-add-component-button" );
        }

        private static void OnUpdate()
        {
            if ( !AddMultipleComponentButtonSetting.instance.IsEnable ) return;

            UpdateInspectorWindow();

            if ( m_inspectorWindow == null ) return;

            UpdateAddComponentButton();

            if ( m_addComponentButton == null ) return;

            var children = ( List<VisualElement> )m_addComponentButton.hierarchy.Children();

            if ( children.Contains( m_addMultipleComponentButton ) ) return;

            m_addMultipleComponentButton ??= new()
            {
                text = "Add Multiple Component",
                style =
                {
                    width     = 230,
                    height    = 24,
                    alignSelf = Align.Center,
                },
            };

            m_addMultipleComponentButton.clicked += () =>
            {
                var rect     = m_addMultipleComponentButton.contentRect;
                var position = m_addMultipleComponentButton.GetPosition();

                rect.x     =  position.x;
                rect.y     =  position.y + 3;
                rect.width += 14;

                var state    = new AdvancedDropdownState();
                var dropdown = new AddMultipleComponentAdvancedDropdown( state );

                dropdown.Show( rect );
            };

            m_addComponentButton.Add( m_addMultipleComponentButton );
        }

        private static void OnChangedSelection()
        {
            if ( !AddMultipleComponentButtonSetting.instance.IsEnable ) return;
            if ( m_addMultipleComponentButton == null ) return;

            var objects = Selection.objects;

            var isAllGameObject =
                    0 < objects.Length &&
                    objects.All( x => !EditorUtility.IsPersistent( x ) )
                ;

            m_addMultipleComponentButton.visible = isAllGameObject;
        }
    }
}