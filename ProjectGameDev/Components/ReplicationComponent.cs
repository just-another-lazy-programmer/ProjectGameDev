using Microsoft.Xna.Framework;
using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Components
{
    internal class ReplicationComponent : Component
    {
        public ReplicatedMovement ReplicatedMovement { get; set; }
        public bool ShouldReplicateMovement { get; set; }
        public ulong ReplicationId { get; private set; }

        public void Pull()
        {

        }

        public void Push()
        {

        }
    }

    struct ReplicatedMovement
    {
        public Vector2 Location { get; set; }
        public Vector2 Velocity { get; set; }
    }
}
