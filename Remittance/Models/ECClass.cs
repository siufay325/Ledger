using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemittanceWindows.Models
{
    public partial class EC
    {
        public decimal AmountSingaporeDollar
        {
            get
            {
                return ExchangeRate == 0 ? 0 : Math.Round(AmountForeignCurrency / ExchangeRate, 2);
            }
        }
    }
}
