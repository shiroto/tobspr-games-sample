using UnityEngine;

namespace UnityUI
{
    public class UiPanel : MonoBehaviour
    {
        private const string SAVE_KEY = "my bindings";

        [SerializeField]
        private BindingsList bindingsList;

        [SerializeField]
        private DefaultKeyBindingsConfig defaultConfig;

        public void Load()
        {
            LoadConfigs();
        }

        public void ResetAll()
        {
            InitPlayerPrefs();
            LoadConfigs();
        }

        public void Save()
        {
            SaveConfigs(bindingsList.PrimaryBinding, bindingsList.SecondaryBinding);
        }

        [ContextMenu("Clear Saved Bindings")]
        private void ClearSave()
        {
            PlayerPrefs.DeleteKey(SAVE_KEY);
        }

        private void InitPlayerPrefs()
        {
            SaveConfigs(defaultConfig.DefaultKeyBindings, defaultConfig.SecondaryDefaultKeyBindings);
        }

        private void LoadConfigs()
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            (KeyBindingsConfig, KeyBindingsConfig) configs = JsonUtility.FromJson<(KeyBindingsConfig, KeyBindingsConfig)>(json);
            bindingsList.SetKeyBindings(configs.Item1, configs.Item2);
        }

        private void SaveConfigs(KeyBindingsConfig c1, KeyBindingsConfig c2)
        {
            (KeyBindingsConfig, KeyBindingsConfig) configs = (c1, c2);
            string json = JsonUtility.ToJson(configs);
            PlayerPrefs.SetString(SAVE_KEY, json);
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey(SAVE_KEY))
            {
                InitPlayerPrefs();
            }
            LoadConfigs();
        }
    }
}