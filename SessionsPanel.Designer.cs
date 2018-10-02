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
            this.ctxMenuSession = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteSessionCtxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSessionCtxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuSessionsRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newSessionCtxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.ctxMenuSession.SuspendLayout();
            this.ctxMenuSessionsRoot.SuspendLayout();
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
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewSession});
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
            this.tsbNewSession.Size = new System.Drawing.Size(44, 22);
            this.tsbNewSession.Text = "New...";
            this.tsbNewSession.Click += new System.EventHandler(this.tsbNewSession_Click);
            // 
            // ctxMenuSession
            // 
            this.ctxMenuSession.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteSessionCtxMenuItem,
            this.editSessionCtxMenuItem});
            this.ctxMenuSession.Name = "ctxMenuSession";
            this.ctxMenuSession.Size = new System.Drawing.Size(108, 48);
            // 
            // deleteSessionCtxMenuItem
            // 
            this.deleteSessionCtxMenuItem.Name = "deleteSessionCtxMenuItem";
            this.deleteSessionCtxMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteSessionCtxMenuItem.Text = "Delete";
            this.deleteSessionCtxMenuItem.Click += new System.EventHandler(this.deleteSessionCtxMenuItem_Click);
            // 
            // editSessionCtxMenuItem
            // 
            this.editSessionCtxMenuItem.Name = "editSessionCtxMenuItem";
            this.editSessionCtxMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editSessionCtxMenuItem.Text = "Edit...";
            this.editSessionCtxMenuItem.Click += new System.EventHandler(this.editSessionCtxMenuItem_Click);
            // 
            // ctxMenuSessionsRoot
            // 
            this.ctxMenuSessionsRoot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSessionCtxMenuItem});
            this.ctxMenuSessionsRoot.Name = "ctxMenuSessionsRoot";
            this.ctxMenuSessionsRoot.Size = new System.Drawing.Size(153, 48);
            // 
            // newSessionCtxMenuItem
            // 
            this.newSessionCtxMenuItem.Name = "newSessionCtxMenuItem";
            this.newSessionCtxMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newSessionCtxMenuItem.Text = "New...";
            this.newSessionCtxMenuItem.Click += new System.EventHandler(this.newSessionCtxMenuItem_Click);
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
            this.ctxMenuSessionsRoot.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip ctxMenuSessionsRoot;
        private System.Windows.Forms.ToolStripMenuItem newSessionCtxMenuItem;
    }
}
