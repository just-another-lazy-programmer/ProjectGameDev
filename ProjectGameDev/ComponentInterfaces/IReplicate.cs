using ProjectGameDev.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.ComponentInterfaces
{
    internal interface IReplicate
    {
        public NetworkComponent NetworkComponent { get; }
    }
}
