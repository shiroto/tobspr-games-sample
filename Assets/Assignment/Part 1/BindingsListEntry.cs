using System;
using TMPro;
using UnityEngine;

namespace UnityUI
{
    public class BindingsListEntry : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text actionText;

        [SerializeField]
        private TMP_Text key1Text;

        [SerializeField]
        private TMP_Text key2Text;

        public event EventHandler<int> OnChangeKey = delegate { };

        public InputAction InputAction { get; private set; }

        public void SetAction(InputAction action)
        {
            InputAction = action;
            actionText.text = action.ToString();
        }

        public void SetKey1(string key)
        {
            key1Text.text = key;
        }

        public void SetKey2(string key)
        {
            key2Text.text = key;
        }

        public void SetKeyTexts(string key1, string key2)
        {
            key1Text.text = key1;
            key2Text.text = key2;
        }

        public void TriggerChangeKey1()
        {
            OnChangeKey(this, 0);
        }

        public void TriggerChangeKey2()
        {
            OnChangeKey(this, 1);
        }
    }
}