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
    public partial class AboutDialog : Form
    {
        public string puttyPath = "";
        public string configPath = "";

        public AboutDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void AboutDialog_Load(object sender, EventArgs e)
        {
            txtPuttyPath.Text = puttyPath;
            txtConfigPath.Text = configPath;
        }
    }
}
