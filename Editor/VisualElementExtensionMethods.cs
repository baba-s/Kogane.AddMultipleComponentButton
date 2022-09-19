using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
    internal static class VisualElementExtensionMethods
    {
        public static Vector2 GetPosition( this VisualElement self )
        {
            var element  = self;
            var position = Vector2.zero;

            while ( element is { parent: { } } )
            {
                position += element.layout.position;
                element  =  element.parent;
            }

            return position;
        }
    }
}