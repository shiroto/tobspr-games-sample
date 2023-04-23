using System;
using System.Linq;
using UnityEngine;

namespace UnityUI
{
    [Serializable]
    public struct KeyBindingsConfig : ICloneable
    {
        [SerializeField]
        private KeyBinding[] bindings;

        public object Clone()
        {
            string json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<KeyBindingsConfig>(json);
        }

        public KeyBinding GetBinding(InputAction action)
        {
            return bindings[(int)action];
        }

        public void SortBindings()
        {
            bindings = bindings.OrderBy(b => b.action).ToArray();
        }

        public void UpdateBinding(InputAction action, KeyCode key)
        {
            bindings[(int)action].keyCode = key;
        }
    }
}