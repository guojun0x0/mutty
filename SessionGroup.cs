using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuTTY
{
    public class SessionGroup
    {
        public string Name;
        public List<SessionInfo> Sessions = new List<SessionInfo>();

        public SessionGroup(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
