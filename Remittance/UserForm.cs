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
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
            teamComboBox.DataSource = Enum.GetValues(typeof(User.Team));
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            String userName = userNameTextBox.Text;
            if (userName == String.Empty)
            {
                Remittance.ShowMessageBox("用户名不能为空。");
                return;
            }
            if (Remittance.entities.Users.Any(u => u.Name == userName))
            {
                Remittance.ShowMessageBox("用户名已存在。");
                return;
            }
            User user = new User()
            {
                Name = userName,
                TeamID = (byte)(User.Team)teamComboBox.SelectedValue
            };
            Remittance.entities.Users.Add(user);
            Remittance.entities.SaveChanges();
            Remittance.ShowMessageBox("用户添加完毕。");
        }
    }
}
