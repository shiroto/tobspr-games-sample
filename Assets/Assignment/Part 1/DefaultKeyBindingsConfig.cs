using UnityEditor;
using UnityEngine;

namespace UnityUI
{
    [CreateAssetMenu(fileName = "New Key Bindings", menuName = "tobspr/Key Bindings")]
    public class DefaultKeyBindingsConfig : ScriptableObject
    {
        [SerializeField]
        private KeyBindingsConfig defaultKeyBinding;

        [SerializeField]
        private KeyBindingsConfig defaultKeyBinding_Secondary;

        public KeyBindingsConfig DefaultKeyBindings => defaultKeyBinding;

        public KeyBindingsConfig SecondaryDefaultKeyBindings => defaultKeyBinding_Secondary;

        [ContextMenu("Sort")]
        private void SortBindings()
        {
            defaultKeyBinding.SortBindings();
            defaultKeyBinding_Secondary.SortBindings();
            EditorUtility.SetDirty(this);
        }

        private void OnValidate()
        {
            if (KeyBindingsHelper.IntegrityCheck(out KeyBindingError error, defaultKeyBinding, defaultKeyBinding_Secondary))
            {
                Debug.LogError(error);
            }
        }
    }
}