using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using ProjectGameDev.UI.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.UI.Components
{
    public class MouseComponent : HitboxComponent
    {
        public event EventHandler OnHoverStartEvent;
        public event EventHandler OnHoverEndEvent;
        public event EventHandler OnClickEvent;

        private MouseService mouseService;

        private bool isHovering = false;
        private bool isPressed = false;

        private Point Size1 = new Point(1, 1);

        public MouseComponent() : base()
        {
            WantsTick = true;
        }

        public override void RegisterDependencies(DependencyManager dependencyManager)
        {
            base.RegisterDependencies(dependencyManager);

            dependencyManager.Inject(ref mouseService);
        }

        public override void Tick(GameTime gameTime)
        {
            base.Tick(gameTime);

            var hitbox = Hitbox.Rectangles[0];

            var state = Mouse.GetState();
            var mousePosition = new Rectangle(mouseService.GetMouseLocation(), Size1);

            if (hitbox.Intersects(mousePosition))
            {
                if (!isHovering)
                {
                    isHovering = true;
                    OnHoverStartEvent?.Invoke(this, new EventArgs());
                }

                if (state.LeftButton == ButtonState.Pressed)
                {
                    isPressed = true;
                }
                else if (state.LeftButton == ButtonState.Released && isPressed)
                {
                    isPressed = false;
                    OnClickEvent?.Invoke(this, new EventArgs());
                }
            }
            else if (isHovering)
            {
                isHovering = false;
                OnHoverEndEvent?.Invoke(this, new EventArgs());
            }
        }
    }
}
