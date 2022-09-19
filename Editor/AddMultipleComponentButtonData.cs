using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [Serializable]
    internal sealed class AddMultipleComponentButtonData
    {
        [SerializeField] private string       m_name;
        [SerializeField] private MonoScript[] m_monoScriptArray;

        public string                    Name           => m_name;
        public IReadOnlyList<MonoScript> MonoScriptList => m_monoScriptArray;
    }
}