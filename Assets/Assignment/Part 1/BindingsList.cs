using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUI
{
    public class BindingsList : MonoBehaviour
    {
        [SerializeField]
        private GameObject anyKeyPopup;

        private List<BindingsListEntry> entries;

        [SerializeField]
        private BindingsListEntry entryPrefab;

        [SerializeField]
        private KeyBindingErrorUi errorUi;

        private KeyBindingsConfig primBinding, secBinding;

        public KeyBindingsConfig PrimaryBinding => primBinding;

        public KeyBindingsConfig SecondaryBinding => secBinding;

        public void SetKeyBindings(KeyBindingsConfig primBinding, KeyBindingsConfig secBinding)
        {
            this.primBinding = primBinding;
            this.secBinding = secBinding;
            for (int i = 0; i < entries.Count; i++)
            {
                KeyBinding b1 = primBinding.GetBinding((InputAction)i);
                KeyBinding b2 = secBinding.GetBinding((InputAction)i);
                entries[i].SetKeyTexts(b1.keyCode.ToString(), b2.keyCode.ToString());
            }
            errorUi.Hide();
        }

        private void Awake()
        {
            entries = new();
            foreach (InputAction action in Enum.GetValues(typeof(InputAction)))
            {
                BindingsListEntry newEntry = GameObject.Instantiate(entryPrefab, transform);
                newEntry.SetAction(action);
                newEntry.OnChangeKey += OnChangeKey;
                entries.Add(newEntry);
            }
        }

        private IEnumerator KeyPressCoroutine(BindingsListEntry entry, int keyIndex)
        {
            anyKeyPopup.SetActive(true);
            while (!Input.anyKey)
            {
                yield return null;
            }
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(key))
                {
                    if (keyIndex == 0)
                    {
                        entry.SetKey1(key.ToString());
                        primBinding.UpdateBinding(entry.InputAction, key);
                    }
                    else if (keyIndex == 1)
                    {
                        entry.SetKey2(key.ToString());
                        secBinding.UpdateBinding(entry.InputAction, key);
                    }
                    break;
                }
            }
            if (KeyBindingsHelper.IntegrityCheck(out KeyBindingError error, primBinding, secBinding))
            {
                errorUi.ShowText(error.ToString());
            }
            else
            {
                errorUi.Hide();
            }
            anyKeyPopup.SetActive(false);
        }

        private void OnChangeKey(object sender, int keyIndex)
        {
            StartCoroutine(KeyPressCoroutine((BindingsListEntry)sender, keyIndex));
        }
    }
}