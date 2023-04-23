using TMPro;
using UnityEngine;

namespace UnityUI
{
    public class KeyBindingErrorUi : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text text;

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ShowText(string text)
        {
            gameObject.SetActive(true);
            this.text.text = text;
        }
    }
}