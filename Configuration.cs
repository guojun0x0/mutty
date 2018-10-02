using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MuTTY
{
    public class Configuration
    {
        public static void Save(string xmlFilePath, List<SessionInfo> sessions)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement sessionsElement = doc.CreateElement("sessions");
            foreach (SessionInfo session in sessions)
            {
                XmlElement sessionElement = doc.CreateElement("session");
                sessionElement.SetAttribute("type", ((int)session.Type).ToString());
                sessionElement.SetAttribute("host", session.Host);
                sessionElement.SetAttribute("username", session.Username);
                sessionsElement.AppendChild(sessionElement);
            }

            doc.AppendChild(sessionsElement);
            doc.Save(xmlFilePath);
        }

        public static List<SessionInfo> Load(string xmlFilePath)
        {
            List<SessionInfo> sessions = new List<SessionInfo>();
            if (!File.Exists(xmlFilePath))
                return sessions;

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);

            XmlNodeList sessionNodes = doc.SelectNodes("//sessions/descendant::session");
            foreach (XmlNode node in sessionNodes)
            {
                SessionInfo session = new SessionInfo();
                session.Type = (SessionType)int.Parse(node.Attributes["type"].Value);
                session.Host = node.Attributes["host"].Value;
                session.Username = node.Attributes["username"].Value;

                sessions.Add(session);
            }

            return sessions;
        }
    }
}
