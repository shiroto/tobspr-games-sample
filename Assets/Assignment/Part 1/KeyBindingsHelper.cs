using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace UnityUI
{
    public static class KeyBindingsHelper
    {
        /// <summary>
        /// Check if any 2 actions use the same key.
        /// Return true when an error is detected.
        /// </summary>
        public static bool IntegrityCheck(out KeyBindingError error, params KeyBindingsConfig[] bindings)
        {
            Assert.IsNotNull(bindings);
            error = null;
            Dictionary<KeyCode, UsedBinding> usedBindings = new();
            foreach (KeyBindingsConfig currentConfig in bindings)
            {
                error = CheckKeys(usedBindings, currentConfig);
                if (error != null)
                {
                    return true;
                }
            }
            return false;
        }

        private static KeyBindingError CheckKeys(Dictionary<KeyCode, UsedBinding> usedBindings, KeyBindingsConfig currentConfig)
        {
            foreach (InputAction action in Enum.GetValues(typeof(InputAction)))
            {
                KeyBinding binding = currentConfig.GetBinding(action);
                if (binding.keyCode == KeyCode.None)
                {
                    continue;
                }
                UsedBinding ub = new(binding.keyCode, action.ToString(), currentConfig);
                if (usedBindings.ContainsKey(binding.keyCode))
                {
                    UsedBinding prevUsedBinding = usedBindings[binding.keyCode];
                    if (prevUsedBinding.config.Equals(currentConfig))
                    {
                        return new KeyBindingError(currentConfig, ub.action, prevUsedBinding.action, binding.keyCode);
                    }
                    else
                    {
                        return new KeyBindingError(currentConfig, prevUsedBinding.config, ub.action, prevUsedBinding.action, binding.keyCode);
                    }
                }
                else
                {
                    usedBindings[binding.keyCode] = ub;
                }
            }
            return null;
        }

        private class UsedBinding
        {
            public string action;
            public KeyBindingsConfig config;
            public KeyCode keyCode;

            public UsedBinding(KeyCode keyCode, string action, KeyBindingsConfig config)
            {
                this.keyCode = keyCode;
                this.action = action;
                this.config = config;
            }
        }
    }
}