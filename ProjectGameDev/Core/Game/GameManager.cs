using ProjectGameDev.Core.Game.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Core.Game
{
    internal class GameManager
    {
        public GameState State { get; protected set; }
        private readonly DependencyManager dependencyManager;
        public GameState PreviousState { get; protected set; }

        public GameManager(DependencyManager dependencyManager)
        {
            this.dependencyManager = dependencyManager;
        }

        public void TransitionTo(GameState state)
        {
            State?.OnStateLeft();
            PreviousState = State;

            state.SetContext(this, dependencyManager);

            State = state;
            State.OnStateEnter();
        }

        // Generic version
        public void TransitionTo<T>() where T : GameState, new()
        {
            TransitionTo(new T());
        }
    }
}
