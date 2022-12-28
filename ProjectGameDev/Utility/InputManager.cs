using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Utility
{
    internal class InputManager
    {
        private readonly Dictionary<InputAction, Keys> actions = new();

        public InputManager()
        {
            SetupDefaults();
        }

        private void SetupDefaults()
        {
            actions.Add(InputAction.MoveLeft, Keys.Left);
            actions.Add(InputAction.MoveRight, Keys.Right);
            actions.Add(InputAction.Jump, Keys.Space);
        }

        public Keys GetKeyForAction(InputAction action)
        {
            if (actions.ContainsKey(action))
                return actions[action];

            return Keys.None;
        }
    }

    enum InputAction
    {
        MoveLeft,
        MoveRight,
        Jump
    }
}
