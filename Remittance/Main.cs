using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemittanceWindows
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (new Login().ShowDialog() != DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Remittance.entities.Dispose();
        }

        private void 添加结账表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new LedgerForm().ShowDialog();
        }

        private void 添加用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UserForm().ShowDialog();
        }
    }
}
