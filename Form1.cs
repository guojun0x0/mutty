using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MuTTY
{
    public partial class Form1 : Form
    {
        protected DockPanel dockPanel;
        protected Job ttyDisposalJob;
        uint numOpenTTYs;
        public string puttyPath { get; set; }

        public Form1()
        {
            InitializeComponent();

            puttyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\putty.exe";

            // Set window size
            Rectangle screenSz = Screen.FromControl(this).Bounds;
            Location = new Point((int)(0.5 * 0.1 * screenSz.Width), (int)(0.5 * 0.1 * screenSz.Height));
            Size = new Size((int)(screenSz.Width * 0.9), (int)(screenSz.Height * 0.9));

            // Add sessions form
            SessionsPanel sessionsPanel = new SessionsPanel(this);
            sessionsPanel.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(sessionsPanel);

            // Add dock panel
            dockPanel = new DockPanel();
            dockPanel.Dock = DockStyle.Fill;
            dockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            splitContainer1.Panel2.Controls.Add(dockPanel);

            ttyDisposalJob = new Job();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            while (!File.Exists(puttyPath))
            {
                DialogResult res = MessageBox.Show("PuTTY is not installed. Please put your putty.exe in the following path:\n" + puttyPath,
                    "PuTTY not installed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                if (res == DialogResult.Cancel)
                    break;
            }

            if (File.Exists(puttyPath))
                lblAppStatus.Text = "Using PuTTY @ " + puttyPath;
            else
                lblAppStatus.Text = "Warning: PuTTY executable not found (" + puttyPath + ")!";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void openTTY(SessionInfo session)
        {
            while (!File.Exists(puttyPath))
            {
                DialogResult res = MessageBox.Show("PuTTY is not installed. Please put your putty.exe in the following path:\n" + puttyPath,
                    "PuTTY not installed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                if (res == DialogResult.Cancel)
                    return;
            }

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = puttyPath;

            string args = "";
            switch (session.Type)
            {
                case SessionType.SSH:
                    args += "-ssh ";
                    break;
            }

            if (session.Username.Length > 0)
                args += session.Username + "@";

            args += session.Host;

            processStartInfo.Arguments = args;

            Process process = Process.Start(processStartInfo);
            ttyDisposalJob.AddProcess(process.Handle);
            
            while (!Win32.IsWindowVisible(process.MainWindowHandle))
            {
                Thread.Sleep(50);
            }

            // Wait another 100ms just to be sure putty has loaded
            Thread.Sleep(100);

            var tty = new TTYForm(process, session);
            tty.Dock = DockStyle.Fill;
            tty.DockAreas = DockAreas.Document;
            tty.Show(dockPanel, DockState.Document);

            tty.StartPosition = FormStartPosition.CenterParent;
            tty.FormBorderStyle = FormBorderStyle.None;
            tty.BackColor = Color.Aqua;
            tty.OnClose.Add(OnTTYFormClose);
            numOpenTTYs++;

            lblAppStatus.Text = "Opened new " + session.Type.ToString() + " session to " + session.Host;

            lblNoSessionsStarted.Hide();
        }

        private void OnTTYFormClose(TTYForm tty)
        {
            lblAppStatus.Text = "Closed " + tty.Session.Type.ToString() + " session to " + tty.Session.Host;

            numOpenTTYs--;
            if (numOpenTTYs < 0)
                numOpenTTYs = 0;

            if (numOpenTTYs == 0)
                lblNoSessionsStarted.Show();
        }

        private void connectSSHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Directly connect without stored session
            SessionOptionsDialog dlg = new SessionOptionsDialog();
            dlg.OKButtonText = "Connect";
            dlg.Text = "Direct connect";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                openTTY(dlg.Session);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDialog dlg = new AboutDialog();
            dlg.puttyPath = puttyPath;
            dlg.configPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\.mutty";
            dlg.ShowDialog();
        }
    }
}
