using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuTTY
{
    public enum SessionType
    {
        SSH,
    }

    public static class SessionTypeExtensions
    {
        public static string ToString(this SessionType type)
        {
            switch (type)
            {
                case SessionType.SSH:
                    return "SSH";
                default:
                    return "Unknown";
            }
        }
    }
}
