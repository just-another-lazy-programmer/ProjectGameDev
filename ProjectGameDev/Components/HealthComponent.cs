using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Components
{
    public class HealthComponent : Component
    {
        public event EventHandler<DeathEventArgs> OnDeathEvent;
        public event EventHandler<HealthChangeEventArgs> OnHealthChangedEvent;

        public float Health { get; protected set; } = 0;

        public float MaxHealth { get; set; } = 100;

        public override void Activate()
        {
            base.Activate();

            Health = MaxHealth;
        }

        public void TakeDamage(WorldObject damageCauser, float amount)
        {
            Health -= amount;
            Health = Math.Max(0, Health); // Clamp health

            OnHealthChangedEvent?.Invoke(this, new HealthChangeEventArgs(damageCauser, true, amount, Health));

            if (Health <= 0)
            {
                OnDeathEvent?.Invoke(this, new DeathEventArgs(damageCauser));
            }
        }

        public void Heal(WorldObject causer, float amount)
        {
            Health += amount;

            OnHealthChangedEvent?.Invoke(this, new HealthChangeEventArgs(causer, false, amount, Health));
        }
    }

    public class HealthChangeEventArgs : EventArgs
    {
        public WorldObject Causer { get; set; }
        public bool IsDamage { get; set; }
        public float Amount { get; set; }
        public float CurrentHealth { get; set; }

        public HealthChangeEventArgs(WorldObject causer, bool isDamage, float amount, float health)
        {
            Causer = causer;
            IsDamage = isDamage;
            Amount = amount;
            CurrentHealth = health;
        }
    }

    public class DeathEventArgs : EventArgs
    {
        public WorldObject Killer { get; set; }

        public DeathEventArgs(WorldObject killer)
        {
            Killer = killer;
        }
    }
}
