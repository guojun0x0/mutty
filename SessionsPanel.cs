using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MuTTY
{
    public partial class SessionsPanel : UserControl
    {
        TreeNode sessionsNode; // Root TreeView node for sessions
        TreeNode puttySessionsNode;
        Form1 mainForm;
        string configFile;

        public SessionsPanel(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            configFile = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\.mutty";
        }

        private List<SessionInfo> TryLoadExistingPuTTYSessions()
        {
            List<SessionInfo> sessions = new List<SessionInfo>();

            var simonTathamKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("SimonTatham");
            if (simonTathamKey == null)
                return sessions;

            var puttyKey = simonTathamKey.OpenSubKey("PuTTY");
            if (puttyKey == null)
                return sessions;

            var sessionsKey = puttyKey.OpenSubKey("Sessions");
            if (sessionsKey == null)
                return sessions;

            foreach (string sessionName in sessionsKey.GetSubKeyNames())
            {
                var sessionKey = sessionsKey.OpenSubKey(sessionName);
                if (sessionKey == null)
                    continue;

                SessionInfo session = new SessionInfo();
                session.External = true;
                session.Host = (string)sessionKey.GetValue("HostName");
                if (session.Host == null || session.Host.Length == 0)
                    continue;

                session.Username = (string)sessionKey.GetValue("UserName");

                string protocol = (string)sessionKey.GetValue("protocol");
                switch (protocol)
                {
                    case "ssh":
                        session.Type = SessionType.SSH;
                        break;
                    default:
                        continue;
                }

                sessions.Add(session);
            }

            return sessions;
        }

        private void SessionsPanel_Load(object sender, EventArgs e)
        {
            // Load MuTTY sessions from configuration
            sessionsNode = new TreeNode("Sessions");
            tvSessions.Nodes.Add(sessionsNode);

            List<SessionInfo> sessions = Configuration.Load(configFile);
            foreach (SessionInfo session in sessions)
            {
                TreeNode node = new TreeNode(session.GetName());
                node.Tag = session;
                sessionsNode.Nodes.Add(node);
            }

            sessionsNode.Expand();

            // Try to load existing PuTTY sessions from registry
            puttySessionsNode = new TreeNode("PuTTY Sessions");
            tvSessions.Nodes.Add(puttySessionsNode);

            List<SessionInfo> existingSessions = TryLoadExistingPuTTYSessions();
            foreach (SessionInfo session in existingSessions)
            {
                TreeNode node = new TreeNode(session.GetName());
                node.Tag = session;
                puttySessionsNode.Nodes.Add(node);
            }

            puttySessionsNode.Expand();
        }

        private void SaveSessions()
        {
            List<SessionInfo> sessions = new List<SessionInfo>();
            foreach (TreeNode node in sessionsNode.Nodes)
            {
                if (!(node.Tag is SessionInfo))
                    continue;

                sessions.Add((SessionInfo)node.Tag);
            }

            Configuration.Save(configFile, sessions);
        }

        private void tsbNewSession_Click(object sender, EventArgs e)
        {
            ShowNewSessionDialog();
        }

        private void ShowNewSessionDialog()
        {
            SessionOptionsDialog dlg = new SessionOptionsDialog();
            dlg.OKButtonText = "Add";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SessionInfo session = dlg.Session;
                TreeNode node = new TreeNode(session.GetName());
                node.Tag = session;
                sessionsNode.Nodes.Add(node);
                sessionsNode.Expand();
                SaveSessions();
            }
        }

        private void tvSessions_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!(e.Node.Tag is SessionInfo))
                return;

            SessionInfo session = (SessionInfo)e.Node.Tag;
            mainForm.openTTY(session);
        }

        private void tvSessions_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node.Tag is SessionInfo)
                {
                    SessionInfo session = (SessionInfo)e.Node.Tag;
                    if (session.External)
                    {
                        // Cant modify external sessions
                        return;
                    }

                    ctxMenuSession.Tag = e.Node;
                    ctxMenuSession.Show(tvSessions, e.Location);
                }
                else if (e.Node == sessionsNode)
                {
                    ctxMenuSessionsRoot.Show(tvSessions, e.Location);
                }
            }
        }

        private void deleteSessionCtxMenuItem_Click(object sender, EventArgs e)
        {
            if (ctxMenuSession.Tag is TreeNode)
            {
                TreeNode node = (TreeNode)ctxMenuSession.Tag;
                node.Remove();
                SaveSessions();
            }
        }

        private void editSessionCtxMenuItem_Click(object sender, EventArgs e)
        {
            if (ctxMenuSession.Tag is TreeNode)
            {
                TreeNode node = (TreeNode)ctxMenuSession.Tag;
                if (!(node.Tag is SessionInfo))
                    return;

                SessionInfo session = (SessionInfo)node.Tag;
                if (session.External)
                    return;

                SessionOptionsDialog dlg = new SessionOptionsDialog();
                dlg.OKButtonText = "Save";
                dlg.Session = new SessionInfo(session);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    session.CopyFrom(dlg.Session);
                    node.Text = session.GetName();
                    SaveSessions();
                }
            }
        }

        private void newSessionCtxMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewSessionDialog();
        }
    }
}
