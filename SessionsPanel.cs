using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MuTTY
{
    public partial class SessionsPanel : UserControl
    {
        TreeNode sessionsNode; // Root TreeView node for session groups
        TreeNode puttySessionsNode;
        Form1 mainForm;
        public string ConfigFile;

        public SessionsPanel(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void SessionsPanel_Load(object sender, EventArgs e)
        {
            // Load MuTTY sessions from configuration
            sessionsNode = new TreeNode("Sessions");
            tvSessions.Nodes.Add(sessionsNode);

            List<SessionGroup> sessionGroups = Configuration.Load(ConfigFile);
            foreach (SessionGroup group in sessionGroups)
            {
                TreeNode groupNode = new TreeNode(group.Name);
                groupNode.Tag = group;

                foreach (SessionInfo session in group.Sessions)
                {
                    TreeNode sessionNode = new TreeNode(session.GetName());
                    sessionNode.Tag = session;
                    groupNode.Nodes.Add(sessionNode);
                }

                sessionsNode.Nodes.Add(groupNode);
            }

            // Add default session group if none exist
            if (sessionGroups.Count == 0)
            {
                SessionGroup defaultGroup = new SessionGroup("Default Group");

                TreeNode defaultGroupNode = new TreeNode(defaultGroup.Name);
                defaultGroupNode.Tag = defaultGroup;

                sessionsNode.Nodes.Add(defaultGroupNode);
            }

            sessionsNode.ExpandAll();

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

        private void SaveGroupsAndSessions()
        {
            Configuration.Save(ConfigFile, GetGroupsAndSessions());
        }

        private List<SessionGroup> GetGroupsAndSessions()
        {
            List<SessionGroup> sessionGroups = new List<SessionGroup>();
            foreach (TreeNode groupNode in sessionsNode.Nodes)
            {
                if (!(groupNode.Tag is SessionGroup))
                    continue;

                SessionGroup group = (SessionGroup)groupNode.Tag;
                group.Sessions.Clear();

                foreach (TreeNode sessionNode in groupNode.Nodes)
                {
                    if (!(sessionNode.Tag is SessionInfo))
                        continue;

                    group.Sessions.Add((SessionInfo)sessionNode.Tag);
                }

                sessionGroups.Add(group);
            }

            return sessionGroups;
        }

        private TreeNode GetSessionGroupTreeNode(SessionGroup group)
        {
            foreach (TreeNode node in sessionsNode.Nodes)
            {
                if (node.Tag == group)
                    return node;
            }

            return null;
        }

        private void tsbNewSession_Click(object sender, EventArgs e)
        {
            ShowNewSessionDialog();
        }

        // If sessionGroup is not given, the first group node found will be pre-selected
        private void ShowNewSessionDialog(SessionGroup sessionGroup = null)
        {
            SessionOptionsDialog dlg = new SessionOptionsDialog();
            dlg.OKButtonText = "Add";
            dlg.SessionGroups = GetGroupsAndSessions();
            dlg.SelectedGroup = (sessionGroup != null ? sessionGroup : dlg.SessionGroups.First());

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SessionInfo session = dlg.Session;
                TreeNode node = new TreeNode(session.GetName());
                node.Tag = session;

                TreeNode groupNode = GetSessionGroupTreeNode(dlg.SelectedGroup);
                groupNode.Nodes.Add(node);
                groupNode.Expand();

                SaveGroupsAndSessions();
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
                else if (e.Node.Tag is SessionGroup)
                {
                    newGroupToolStripMenuItem.Visible = false;
                    deleteGroupToolStripMenuItem.Visible = true;
                    editGroupToolStripMenuItem.Visible = true;

                    ctxMenuSessionGroup.Tag = e.Node;
                    ctxMenuSessionGroup.Show(tvSessions, e.Location);
                }
                else if (e.Node == sessionsNode) // Root node
                {
                    newGroupToolStripMenuItem.Visible = true;
                    deleteGroupToolStripMenuItem.Visible = false;
                    editGroupToolStripMenuItem.Visible = false;

                    ctxMenuSessionGroup.Tag = e.Node;
                    ctxMenuSessionGroup.Show(tvSessions, e.Location);
                }
            }
        }

        private void deleteSessionCtxMenuItem_Click(object sender, EventArgs e)
        {
            if (ctxMenuSession.Tag is TreeNode)
            {
                TreeNode node = (TreeNode)ctxMenuSession.Tag;
                SessionInfo session = (SessionInfo)node.Tag;

                if (MessageBox.Show("Do you really want to delete Session '" + session.GetName() + "'?",
                        "Delete session", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                node.Remove();
                SaveGroupsAndSessions();
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

                SessionGroup group = (SessionGroup)node.Parent.Tag;

                SessionOptionsDialog dlg = new SessionOptionsDialog();
                dlg.OKButtonText = "Save";
                dlg.SessionGroups = GetGroupsAndSessions();
                dlg.SelectedGroup = group;
                dlg.Session = new SessionInfo(session);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    session.CopyFrom(dlg.Session);
                    node.Text = session.GetName();

                    if (dlg.SelectedGroup != group)
                    {
                        node.Remove();

                        TreeNode newGroupNode = GetSessionGroupTreeNode(dlg.SelectedGroup);
                        newGroupNode.Nodes.Add(node);
                    }

                    SaveGroupsAndSessions();
                }
            }
        }

        private void newSessionCtxMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode groupNode = (TreeNode)ctxMenuSessionGroup.Tag;
            ShowNewSessionDialog((SessionGroup)groupNode.Tag);
        }

        private void tvSessions_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tvSessions.SelectedNode == null)
                return;

            // Handle "Enter" Key to open a selected session
            TreeNode selectedNode = tvSessions.SelectedNode;
            if (selectedNode.Tag is SessionInfo)
            {
                SessionInfo sessionInfo = (SessionInfo)selectedNode.Tag;
                mainForm.openTTY(sessionInfo);
            }
        }

        private void DeleteGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctxMenuSessionGroup.Tag is TreeNode && ctxMenuSessionGroup.Tag != sessionsNode)
            {
                TreeNode node = (TreeNode)ctxMenuSessionGroup.Tag;
                SessionGroup group = (SessionGroup)node.Tag;

                if (MessageBox.Show("Do you really want to delete group '" + group.Name + "'?",
                        "Delete group", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                node.Remove();
                SaveGroupsAndSessions();
            }
        }

        private void ShowNewGroupDialog()
        {
            SessionGroupOptionsDialog dlg = new SessionGroupOptionsDialog();
            dlg.DialogTitle = "New Group";
            dlg.OKButtonText = "Add Group";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SessionGroup group = new SessionGroup(dlg.GroupName);
                TreeNode node = new TreeNode(group.Name);
                node.Tag = group;
                sessionsNode.Nodes.Add(node);
                SaveGroupsAndSessions();
            }
        }

        private void EditGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctxMenuSessionGroup.Tag is TreeNode && ctxMenuSessionGroup.Tag != sessionsNode)
            {
                TreeNode groupNode = (TreeNode)ctxMenuSessionGroup.Tag;
                SessionGroup group = (SessionGroup)groupNode.Tag;

                SessionGroupOptionsDialog dlg = new SessionGroupOptionsDialog();
                dlg.DialogTitle = "Edit Group";
                dlg.OKButtonText = "Ok";
                dlg.GroupName = group.Name;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    group.Name = dlg.GroupName;
                    groupNode.Text = dlg.GroupName;
                    SaveGroupsAndSessions();
                }
            }
        }

        private void NewGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewGroupDialog();
        }
        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            ShowNewGroupDialog();
        }
    }
}
