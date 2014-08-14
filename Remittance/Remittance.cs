using RemittanceWindows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemittanceWindows
{
    static class Remittance
    {
        public static RemittanceEntities entities = new RemittanceEntities();
        public static short userID;
        public static string userName;

        public static void ShowMessageBox(string message)
        {
            MessageBox.Show(message);
        }
    }
}
