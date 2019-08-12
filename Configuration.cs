using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MuTTY
{
    public class Configuration
    {
        public static void Save(string xmlFilePath, List<SessionGroup> sessionGroups)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement groupsElement = doc.CreateElement("groups");
            foreach (SessionGroup group in sessionGroups)
            {
                XmlElement groupElement = doc.CreateElement("group");
                groupElement.SetAttribute("name", group.Name);

                foreach (SessionInfo session in group.Sessions)
                {
                    XmlElement sessionElement = doc.CreateElement("session");
                    sessionElement.SetAttribute("type", ((int)session.Type).ToString());
                    sessionElement.SetAttribute("host", session.Host);
                    sessionElement.SetAttribute("username", session.Username);
                    groupElement.AppendChild(sessionElement);
                }

                groupsElement.AppendChild(groupElement);
            }

            doc.AppendChild(groupsElement);
            doc.Save(xmlFilePath);
        }

        public static List<SessionGroup> Load(string xmlFilePath)
        {
            List<SessionGroup> sessionGroups = new List<SessionGroup>();
            if (!File.Exists(xmlFilePath))
                return sessionGroups;

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);

            XmlNodeList groupNodes = doc.SelectNodes("//groups/descendant::group");
            foreach (XmlNode groupNode in groupNodes)
            {
                SessionGroup group = new SessionGroup(groupNode.Attributes["name"].Value);
                foreach (XmlNode sessionNode in groupNode.SelectNodes("session"))
                {
                    SessionInfo session = new SessionInfo();
                    session.Type = (SessionType)int.Parse(sessionNode.Attributes["type"].Value);
                    session.Host = sessionNode.Attributes["host"].Value;
                    session.Username = sessionNode.Attributes["username"].Value;

                    group.Sessions.Add(session);
                }

                sessionGroups.Add(group);
            }

            return sessionGroups;
        }
    }
}
