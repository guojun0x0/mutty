using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuTTY
{
    public class SessionInfo
    {
        public SessionType Type;
        public string Host;
        public string Username;
        public bool External;

        public SessionInfo()
        {
            Type = SessionType.SSH;
            Host = "localhost";
            External = false;
        }

        public SessionInfo(SessionInfo info)
        {
            CopyFrom(info);
        }

        public void CopyFrom(SessionInfo info)
        {
            Type = info.Type;
            Host = info.Host;
            Username = info.Username;
            External = info.External;
        }

        public string GetName()
        {
            string name = Host + " (" + Type.ToString() + ")";
            if (Username.Length > 0)
                name = Username + "@" + name;

            return name;
        }
    }
}
