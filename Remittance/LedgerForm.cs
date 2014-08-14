using RemittanceWindows.Helpers;
using RemittanceWindows.Models;
using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RemittanceWindows
{
    public partial class LedgerForm : Form, INotifyPropertyChanged
    {
        public decimal CashTotal
        {
            get
            {
                return cashDenominationControl.Total + submittedDenominationControl.Total;
            }
        }

        public decimal CiscoTotal
        {
            get
            {
                return ciscoDenominationControl.Total;
            }
        }

        public decimal NetsTotal
        {
            get
            {
                return ((EntityCollection<Pay>)payDataGridView.DataSource).Where(x=> x.PayTypeID == (byte)Pay.Type.NETS).Sum(x => x.Amount);
            }
        }

        public LedgerForm()
        {
            InitializeComponent();
            cashDenominationControl.Type = Models.CurrencyDenomination.Type.Cash;
            submittedDenominationControl.Type = Models.CurrencyDenomination.Type.Submitted;
            ciscoDenominationControl.Type = Models.CurrencyDenomination.Type.Cisco;

            cashDenominationControl.PropertyChanged += cashDenominationControl_PropertyChanged;
            submittedDenominationControl.PropertyChanged += submittedDenominationControl_PropertyChanged;
            ciscoDenominationControl.PropertyChanged += ciscoDenominationControl_PropertyChanged;

            payDataGridView.AutoGenerateColumns = false;
            payDataGridView.Columns.Add(new DataGridViewComboBoxColumn()
            {
                Name = "PayTypeID",
                HeaderText = "Pay Type",
                DataPropertyName = "PayTypeID",
                DisplayMember = "Name",
                ValueMember = "PayTypeID",
                DataSource = Enum.GetValues(typeof(Pay.Type)).Cast<Pay.Type>().Select(e => new
                {
                    PayTypeID = (byte)e,
                    Name = e.ToString()
                }).ToList()
            });
            payDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Amount",
                HeaderText = "Amount",
                DataPropertyName = "Amount",
                DefaultCellStyle =
                {
                    Format = "N2"
                }
            });
            payDataGridView.DataSource = new EntityCollection<Pay>();

            chequeDataGridView.DataSource = new EntityCollection<Cheque>();
            chequeDataGridView.Columns["ChequeID"].Visible = false;
            chequeDataGridView.Columns["LedgerID"].Visible = false;
            chequeDataGridView.Columns["Number"].HeaderText = "编号";
            chequeDataGridView.Columns["Bank"].HeaderText = "银行";
            chequeDataGridView.Columns["Amount"].HeaderText = "Amount($)";
            chequeDataGridView.Columns["Amount"].DefaultCellStyle.Format = "N2";
            chequeDataGridView.Columns["Ledger"].Visible = false;

            ecDataGridView.DataSource = new EntityCollection<EC>();
            ecDataGridView.Columns["ECID"].Visible = false;
            ecDataGridView.Columns["LedgerID"].Visible = false;
            ecDataGridView.Columns["PurchaseDate"].HeaderText = "出钱日期";
            ecDataGridView.Columns["Name"].HeaderText = "名字";
            ecDataGridView.Columns["ICNumber"].HeaderText = "证件号";
            ecDataGridView.Columns["Telephone"].HeaderText = "电话";
            ecDataGridView.Columns["IDType"].HeaderText = "证件类型";
            ecDataGridView.Columns["Detail"].HeaderText = "收款帐号";
            ecDataGridView.Columns["ReceivedDate"].HeaderText = "入帐日期";
            ecDataGridView.Columns["Currency"].Visible = false;
            ecDataGridView.Columns["AmountForeignCurrency"].HeaderText = "人民币";
            ecDataGridView.Columns["AmountForeignCurrency"].DefaultCellStyle.Format = "N2";
            ecDataGridView.Columns["AmountSingaporeDollar"].DisplayIndex = 11;
            ecDataGridView.Columns["AmountSingaporeDollar"].HeaderText = "新币";
            ecDataGridView.Columns["AmountSingaporeDollar"].DefaultCellStyle.Format = "N2";
            ecDataGridView.Columns["ExchangeRate"].HeaderText = "汇率";
            ecDataGridView.Columns["ExchangeRate"].DefaultCellStyle.Format = "N4";
            ecDataGridView.Columns["Ledger"].Visible = false;

            cashTextBox.DataBindings.Add("Text", this, DataBindingHelper.GetPropertyName(() => CashTotal), formattingEnabled: true, updateMode: DataSourceUpdateMode.Never, nullValue: 0, formatString: "N2");
            ciscoTextBox.DataBindings.Add("Text", this, DataBindingHelper.GetPropertyName(() => CiscoTotal), formattingEnabled: true, updateMode: DataSourceUpdateMode.Never, nullValue: 0, formatString: "N2");
            netsTextBox.DataBindings.Add("Text", this, DataBindingHelper.GetPropertyName(() => NetsTotal), formattingEnabled: true, updateMode: DataSourceUpdateMode.Never, nullValue: 0, formatString: "N2");
        }

        private void cashDenominationControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Total")
            {
                NotifyPropertyChanged(() => CashTotal);
            }
        }

        private void submittedDenominationControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Total")
            {
                NotifyPropertyChanged(() => CashTotal);
            }
        }

        private void ciscoDenominationControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Total")
            {
                NotifyPropertyChanged(() => CiscoTotal);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void NotifyPropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            NotifyPropertyChanged(DataBindingHelper.GetPropertyName(property));
        }

        private bool validateForm()
        {
            return false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            EntityCollection<Pay> payCollection = (EntityCollection<Pay>)payDataGridView.DataSource;
            EntityCollection<Cheque> chequeCollection = (EntityCollection<Cheque>)chequeDataGridView.DataSource;
            EntityCollection<EC> ecCollection = (EntityCollection<EC>)ecDataGridView.DataSource;
            decimal paySubTotal = ((EntityCollection<Pay>)payDataGridView.DataSource).Sum(x => x.Amount);

            return;

            Ledger ledger = new Ledger();
            ledger.Date = DateTime.Now.Date;
            ledger.UserID = Remittance.userID;
            ledger.ReportAmount = reportAmountNumericUpDown.Value;
            Remittance.entities.Ledgers.Add(ledger);
            Remittance.entities.SaveChanges();
            Remittance.entities.CurrencyDenominations.Add(cashDenominationControl.ToDenomination(ledger.LedgerID));
            Remittance.entities.CurrencyDenominations.Add(submittedDenominationControl.ToDenomination(ledger.LedgerID));
            Remittance.entities.CurrencyDenominations.Add(ciscoDenominationControl.ToDenomination(ledger.LedgerID));
            Remittance.entities.SaveChanges();
            MessageBox.Show("Saved.");
        }

        private void payDataGridView_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            NotifyPropertyChanged(() => NetsTotal);
        }

        private void payDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            payDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            NotifyPropertyChanged(() => NetsTotal);
        }

        private void dataGridView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            DataGridViewTextBoxEditingControl textBox;
            string text = "";
            int selectionStart = 0;
            if (dataGridView.CurrentCell != null && dataGridView.CurrentCell is DataGridViewTextBoxCell)
            {
                textBox = (DataGridViewTextBoxEditingControl)dataGridView.EditingControl;
                text = textBox.Text;
                selectionStart = textBox.SelectionStart;
                Trace.WriteLine("Text: " + text);
            }
            Validate();
            if (dataGridView.CurrentCell != null && dataGridView.CurrentCell is DataGridViewTextBoxCell)
            {
                dataGridView.BeginEdit(selectAll: false);
                textBox = (DataGridViewTextBoxEditingControl)dataGridView.EditingControl;
                textBox.Text = text;
                textBox.Select(selectionStart, 0);
            }
        }

        private void ecDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            ecDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            if (ecDataGridView.CurrentCell != null)
            {
                string columnName = ecDataGridView.Columns[ecDataGridView.CurrentCell.ColumnIndex].Name;
                if (columnName == "AmountForeignCurrency" || columnName == "ExchangeRate")
                {
                    ecDataGridView.UpdateCellValue(ecDataGridView.Columns["AmountSingaporeDollar"].Index, ecDataGridView.CurrentCell.RowIndex);
                }
            }
        }

        private void LedgerForm_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }
    }
}
