using UnityEngine;
using UnityEngine.Assertions;

namespace UnityUI
{
    public class KeyBindingError
    {
        private string[] actions;
        private KeyBindingsConfig[] configs;
        private KeyCode keyCode;

        public KeyBindingError(KeyBindingsConfig config, string action1, string action2, KeyCode keyCode)
            : this(new[] { config }, new[] { action1, action2 }, keyCode)
        {
            Assert.IsNotNull(action1);
            Assert.IsNotNull(action2);
        }

        public KeyBindingError(KeyBindingsConfig config1, KeyBindingsConfig config2, string action1, string action2, KeyCode keyCode)
            : this(new[] { config1, config2 }, new[] { action1, action2 }, keyCode)
        {
            Assert.IsNotNull(action1);
            Assert.IsNotNull(action2);
        }

        public KeyBindingError(KeyBindingsConfig[] configs, string[] actions, KeyCode keyCode)
        {
            this.actions = actions;
            this.configs = configs;
            this.keyCode = keyCode;
        }

        public override string ToString()
        {
            return $"{keyCode} => {actions[0]} {actions[1]}";
        }
    }
}