using System;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace Kogane.Internal
{
    internal sealed class AddMultipleComponentAdvancedDropdown : AdvancedDropdown
    {
        private AdvancedDropdownItem[] m_array;

        public AddMultipleComponentAdvancedDropdown( AdvancedDropdownState state ) : base( state )
        {
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            m_array = AddMultipleComponentButtonSetting.instance.List
                    .Select( x => new AdvancedDropdownItem( x.Name ) )
                    .ToArray()
                ;

            var root = new AdvancedDropdownItem( "Component" );

            foreach ( var item in m_array )
            {
                root.AddChild( item );
            }

            return root;
        }

        protected override void ItemSelected( AdvancedDropdownItem item )
        {
            var index = Array.IndexOf( m_array, item );
            var data  = AddMultipleComponentButtonSetting.instance.List[ index ];

            foreach ( var gameObject in Selection.gameObjects )
            {
                foreach ( var monoScript in data.MonoScriptList )
                {
                    Undo.AddComponent( gameObject, monoScript.GetClass() );
                }
            }
        }
    }
}