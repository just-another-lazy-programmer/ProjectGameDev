using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Utility
{
    public class CooldownManager
    {
        private readonly Dictionary<CooldownRequester, DateTime> cooldowns = new();

        public bool IsOnCooldown(CooldownRequester requester, float cooldownLength, bool isInitiallyOnCooldown= false)
        {
            if (cooldowns.TryGetValue(requester, out DateTime cooldownStart))
            {
                return (DateTime.Now - cooldownStart).TotalSeconds <= cooldownLength;
            }

            cooldowns.Add(requester, DateTime.Now);

            return isInitiallyOnCooldown;
        }

        public bool IsOnCooldown(object self, object key, float cooldownLength, bool isInitiallyOnCooldown=false)
        {
            return IsOnCooldown(new CooldownRequester(self, key), cooldownLength, isInitiallyOnCooldown);
        }

        public void SetCooldown(object self, object key)
        {
            cooldowns[new CooldownRequester(self, key)] = DateTime.Now;
        }

        public void SetCooldown(CooldownRequester requester)
        {
            cooldowns[requester] = DateTime.Now;
        }
    }

    public struct CooldownRequester : IEquatable<CooldownRequester>
    {
        public object Self { get; set; }
        public object Key { get; set; }

        public CooldownRequester(object self, object key)
        {
            Self = self;
            Key = key;
        }

        public bool Equals(CooldownRequester other)
        {
            return Self == other.Self && Key == other.Key;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Self, Key);
        }

        public override bool Equals(object obj)
        {
            return obj is CooldownRequester requester && Equals(requester);
        }
    }
}
