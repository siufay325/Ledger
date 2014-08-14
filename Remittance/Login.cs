using RemittanceWindows.Models;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            User user = Remittance.entities.Users.FirstOrDefault(u => u.Name == userNameTextBox.Text);
            if (user != null)
            {
                Remittance.userID = user.UserID;
                Remittance.userName = user.Name;
                DialogResult = DialogResult.OK;
            }
            else
            {
                Remittance.ShowMessageBox("用户名不存在。");
            }
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            userNameTextBox.Focus();
        }
    }
}
