//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RemittanceWindows.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cheque
    {
        public int ChequeID { get; set; }
        public int LedgerID { get; set; }
        public string Number { get; set; }
        public string Bank { get; set; }
        public decimal Amount { get; set; }
    
        public virtual Ledger Ledger { get; set; }
    }
}