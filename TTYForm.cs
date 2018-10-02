using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MuTTY
{
    public partial class TTYForm : DockContent
    {
        Process process;
        public SessionInfo Session { get; private set; }
        IntPtr hWndPuTTY;
        Win32.RECT ttyRectClient;
        Win32.RECT ttyRectClientInner;
        uint ttyWndStyle;
        public List<Action<TTYForm>> OnClose { get; private set; }

        public TTYForm(Process ttyProcess, SessionInfo session)
        {
            InitializeComponent();
            this.Session = session;

            this.process = ttyProcess;
            this.process.EnableRaisingEvents = true;
            this.process.Exited += new EventHandler(process_Exited);

            this.hWndPuTTY = ttyProcess.MainWindowHandle;

            OnClose = new List<Action<TTYForm>>();

            Text = session.GetName();
        }

        private void process_Exited(object sender, EventArgs e)
        {
            // Close docker content on windows thread
            Invoke((MethodInvoker)delegate
            {
                Close();
            });
        }

        private void UpdateTTYWindowRect()
        {
            Win32.RECT rect;
            rect.Left = 0;
            rect.Top = 0;
            rect.Right = Width;
            rect.Bottom = Height;

            Win32.AdjustWindowRect(ref rect, ttyWndStyle, false);

            Win32.SetWindowPos(hWndPuTTY, IntPtr.Zero,
                rect.Left, rect.Top + 21,
                rect.Right - rect.Left, rect.Bottom - rect.Top - 21,
                Win32.SetWindowPosFlags.NoActivate);
        }

        private void TTYForm_Load(object sender, EventArgs e)
        {
            Win32.SetParent(hWndPuTTY, Handle);

            // Position PuTTY window so that window border is not visible
            Win32.GetClientRect(hWndPuTTY, out ttyRectClient);
            Win32.GetClientRect(hWndPuTTY, out ttyRectClientInner);
            ttyWndStyle = Win32.GetWindowStyle(hWndPuTTY);
            Win32.AdjustWindowRect(ref ttyRectClientInner, ttyWndStyle, false);

            Win32.RECT dockRect;
            Win32.GetClientRect(Handle, out dockRect);

            Win32.SetWindowPos(hWndPuTTY, IntPtr.Zero, 0, 0, 0, 0, 0);
            Win32.UpdateWindow(hWndPuTTY);
        }

        private void TTYForm_Resize(object sender, EventArgs e)
        {
            UpdateTTYWindowRect();
        }

        private void TTYForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (process != null)
            {
                process.CloseMainWindow();
                process.Close();
                process = null;
            }

            // Notify handlers
            foreach (var handler in OnClose)
                handler.Invoke(this);
        }
    }
}
