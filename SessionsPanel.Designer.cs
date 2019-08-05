namespace MuTTY
{
    partial class SessionsPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SessionsPanel));
            this.tvSessions = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNewSession = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ctxMenuSession = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editSessionCtxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSessionCtxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuSessionGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newSessionCtxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.ctxMenuSession.SuspendLayout();
            this.ctxMenuSessionGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvSessions
            // 
            this.tvSessions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSessions.Location = new System.Drawing.Point(0, 25);
            this.tvSessions.Name = "tvSessions";
            this.tvSessions.Size = new System.Drawing.Size(263, 485);
            this.tvSessions.TabIndex = 12;
            this.tvSessions.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSessions_NodeMouseClick);
            this.tvSessions.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSessions_NodeMouseDoubleClick);
            this.tvSessions.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tvSessions_KeyPress);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewSession,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(263, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbNewSession
            // 
            this.tsbNewSession.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbNewSession.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewSession.Image")));
            this.tsbNewSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewSession.Name = "tsbNewSession";
            this.tsbNewSession.Size = new System.Drawing.Size(86, 22);
            this.tsbNewSession.Text = "New Session...";
            this.tsbNewSession.Click += new System.EventHandler(this.tsbNewSession_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(80, 22);
            this.toolStripButton1.Text = "New Group...";
            this.toolStripButton1.Click += new System.EventHandler(this.ToolStripButton1_Click);
            // 
            // ctxMenuSession
            // 
            this.ctxMenuSession.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editSessionCtxMenuItem,
            this.deleteSessionCtxMenuItem});
            this.ctxMenuSession.Name = "ctxMenuSession";
            this.ctxMenuSession.Size = new System.Drawing.Size(181, 70);
            // 
            // editSessionCtxMenuItem
            // 
            this.editSessionCtxMenuItem.Name = "editSessionCtxMenuItem";
            this.editSessionCtxMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editSessionCtxMenuItem.Text = "&Edit Session...";
            this.editSessionCtxMenuItem.Click += new System.EventHandler(this.editSessionCtxMenuItem_Click);
            // 
            // deleteSessionCtxMenuItem
            // 
            this.deleteSessionCtxMenuItem.Name = "deleteSessionCtxMenuItem";
            this.deleteSessionCtxMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteSessionCtxMenuItem.Text = "&Delete Session";
            this.deleteSessionCtxMenuItem.Click += new System.EventHandler(this.deleteSessionCtxMenuItem_Click);
            // 
            // ctxMenuSessionGroup
            // 
            this.ctxMenuSessionGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSessionCtxMenuItem,
            this.newGroupToolStripMenuItem,
            this.editGroupToolStripMenuItem,
            this.deleteGroupToolStripMenuItem});
            this.ctxMenuSessionGroup.Name = "ctxMenuSessionsRoot";
            this.ctxMenuSessionGroup.Size = new System.Drawing.Size(150, 92);
            // 
            // newSessionCtxMenuItem
            // 
            this.newSessionCtxMenuItem.Name = "newSessionCtxMenuItem";
            this.newSessionCtxMenuItem.Size = new System.Drawing.Size(149, 22);
            this.newSessionCtxMenuItem.Text = "New &Session...";
            this.newSessionCtxMenuItem.Click += new System.EventHandler(this.newSessionCtxMenuItem_Click);
            // 
            // newGroupToolStripMenuItem
            // 
            this.newGroupToolStripMenuItem.Name = "newGroupToolStripMenuItem";
            this.newGroupToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.newGroupToolStripMenuItem.Text = "New &Group...";
            this.newGroupToolStripMenuItem.Click += new System.EventHandler(this.NewGroupToolStripMenuItem_Click);
            // 
            // editGroupToolStripMenuItem
            // 
            this.editGroupToolStripMenuItem.Name = "editGroupToolStripMenuItem";
            this.editGroupToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.editGroupToolStripMenuItem.Text = "&Edit Group...";
            this.editGroupToolStripMenuItem.Click += new System.EventHandler(this.EditGroupToolStripMenuItem_Click);
            // 
            // deleteGroupToolStripMenuItem
            // 
            this.deleteGroupToolStripMenuItem.Name = "deleteGroupToolStripMenuItem";
            this.deleteGroupToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.deleteGroupToolStripMenuItem.Text = "&Delete Group";
            this.deleteGroupToolStripMenuItem.Click += new System.EventHandler(this.DeleteGroupToolStripMenuItem_Click);
            // 
            // SessionsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvSessions);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SessionsPanel";
            this.Size = new System.Drawing.Size(263, 510);
            this.Load += new System.EventHandler(this.SessionsPanel_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ctxMenuSession.ResumeLayout(false);
            this.ctxMenuSessionGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvSessions;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNewSession;
        private System.Windows.Forms.ContextMenuStrip ctxMenuSession;
        private System.Windows.Forms.ToolStripMenuItem deleteSessionCtxMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSessionCtxMenuItem;
        private System.Windows.Forms.ContextMenuStrip ctxMenuSessionGroup;
        private System.Windows.Forms.ToolStripMenuItem newSessionCtxMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}
