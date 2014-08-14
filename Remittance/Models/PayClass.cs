using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemittanceWindows.Models
{
    public partial class Pay
    {
        public enum Type
        {
            CIMB = 1,
            DBS = 2,
            HL入帐 = 3,
            NETS = 4,
            OCBC = 5,
            RHB = 6,
            SMJ = 7,
            撤单 = 8,
            Others = 0
        }
    }
}
