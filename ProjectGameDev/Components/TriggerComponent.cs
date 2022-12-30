using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Components
{
    public class TriggerComponent : HitboxComponent
    {
        public event EventHandler<CollisionEventArgs> OnCollisionEvent;
        public float CooldownTime { get; set; } = 5;
        public bool IsCooldownGlobal { get; set; } = true;

        

        public void ProcessHitEvent(WorldObject objectHit)
        {
            OnCollisionEvent?.Invoke(this, new CollisionEventArgs
            {
                ObjectHit = objectHit
            });
        }
    }

    public class CollisionEventArgs : EventArgs
    {
        public WorldObject ObjectHit { get; set; }
    }
}
