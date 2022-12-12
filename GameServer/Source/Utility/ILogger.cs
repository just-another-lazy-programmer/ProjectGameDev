using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Source.Utility
{
    public interface ILogger
    {
        // Append data to the output stream
        public void Write(object message);
        
        // Append data + newline to the output stream
        public void WriteLine(object message);
    }
}
