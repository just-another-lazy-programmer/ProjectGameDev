using ProjectGameDev.Core;
using ProjectGameDev.Utility;
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

        protected CooldownManager cooldownManager;

        public override void RegisterDependencies(DependencyManager dependencyManager)
        {
            base.RegisterDependencies(dependencyManager);

            dependencyManager.Inject(ref cooldownManager);
        }

        public void ProcessHitEvent(WorldObject objectHit)
        {
            object key = IsCooldownGlobal ? null : objectHit;

            if (!cooldownManager.IsOnCooldown(this, key, CooldownTime))
            {
                OnCollisionEvent?.Invoke(this, new CollisionEventArgs
                {
                    ObjectHit = objectHit
                });

                cooldownManager.SetCooldown(this, key);
            }
        }
    }

    public class CollisionEventArgs : EventArgs
    {
        public WorldObject ObjectHit { get; set; }
    }
}
