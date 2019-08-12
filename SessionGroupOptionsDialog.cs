using System;
using System.Windows.Forms;

namespace MuTTY
{
    public partial class SessionGroupOptionsDialog : Form
    {
        public string DialogTitle = "";
        public string OKButtonText = "Ok";
        public string GroupName;

        public SessionGroupOptionsDialog()
        {
            InitializeComponent();
        }

        private void SessionGroupOptionsDialog_Load(object sender, EventArgs e)
        {
            Text = DialogTitle;
            btnOk.Text = OKButtonText;
            txtName.Text = GroupName;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("Please enter a group name", "No group name given", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GroupName = txtName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
