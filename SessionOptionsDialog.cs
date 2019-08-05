using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuTTY
{
    public partial class SessionOptionsDialog : Form
    {
        public SessionInfo Session;
        public String OKButtonText = "Ok";
        public List<SessionGroup> SessionGroups;
        public SessionGroup SelectedGroup;
        public bool AllowGroupSelect = true;

        public SessionOptionsDialog()
        {
            InitializeComponent();

            cbSessionType.DropDownStyle = ComboBoxStyle.DropDownList;

            // Add all types to session type dropdown
            foreach (SessionType type in Enum.GetValues(typeof(SessionType)))
                cbSessionType.Items.Add(type);
        }

        private void SessionOptionsDialog_Load(object sender, EventArgs e)
        {
            if (Session == null)
                Session = new SessionInfo();

            groupComboBox.Items.Clear();
            foreach (SessionGroup group in SessionGroups)
                groupComboBox.Items.Add(group);

            groupComboBox.SelectedItem = SelectedGroup;
            groupComboBox.Enabled = AllowGroupSelect;

            cbSessionType.SelectedItem = Session.Type;
            txtHost.Text = Session.Host;
            txtUsername.Text = Session.Username;

            btnConnect.Text = OKButtonText;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (txtHost.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter a hostname.", "No hostname", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            Session.Type = (SessionType)cbSessionType.SelectedItem;
            Session.Host = txtHost.Text;
            Session.Username = txtUsername.Text;

            SelectedGroup = (SessionGroup)groupComboBox.SelectedItem;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
