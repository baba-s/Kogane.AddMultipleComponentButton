using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [FilePath( "ProjectSettings/Kogane/AddMultipleComponentButtonSetting.asset", FilePathAttribute.Location.ProjectFolder )]
    internal sealed class AddMultipleComponentButtonSetting : ScriptableSingleton<AddMultipleComponentButtonSetting>
    {
        [SerializeField] private bool                             m_isEnable;
        [SerializeField] private AddMultipleComponentButtonData[] m_array;

        public bool                                          IsEnable => m_isEnable;
        public IReadOnlyList<AddMultipleComponentButtonData> List     => m_array;

        public void Save()
        {
            Save( true );
        }
    }
}