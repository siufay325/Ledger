using System;
using System.ComponentModel;
using System.Windows.Forms;
using RemittanceWindows.Models;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using RemittanceWindows.Helpers;

namespace RemittanceWindows
{
    public partial class DenominationControl : UserControl, INotifyPropertyChanged
    {
        private int currencyDenominationID = 0;
        private byte typeID;
        private short quantity10000 = 0, quantity1000 = 0, quantity100 = 0, quantity50 = 0, quantity10 = 0, quantity5 = 0, quantity2 = 0;
        private decimal coinAmount = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void NotifyPropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            LambdaExpression lambda = (LambdaExpression)property;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                UnaryExpression unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }
            NotifyPropertyChanged(memberExpression.Member.Name);
        }

        public CurrencyDenomination.Type Type
        {
            set
            {
                if (typeID != (byte)value)
                {
                    typeID = (byte)value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(() => TypeString);
                }
            }
        }

        public string TypeString
        {
            get
            {
                return ((CurrencyDenomination.Type)typeID).ToString();
            }
        }

        public short Quantity10000
        {
            get
            {
                return quantity10000;
            }
            set
            {
                if (quantity10000 != value)
                {
                    quantity10000 = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(() => SubTotal10000);
                    NotifyPropertyChanged(() => SubTotal10000Text);
                    NotifyPropertyChanged(() => Total);
                    //NotifyPropertyChanged(() => TotalText);
                }
            }
        }

        public short Quantity1000
        {
            get
            {
                return quantity1000;
            }
            set
            {
                if (quantity1000 != value)
                {
                    quantity1000 = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(() => SubTotal1000);
                    NotifyPropertyChanged(() => SubTotal1000Text);
                    NotifyPropertyChanged(() => Total);
                    //NotifyPropertyChanged(() => TotalText);
                }
            }
        }

        public short Quantity100
        {
            get
            {
                return quantity100;
            }
            set
            {
                if (quantity100 != value)
                {
                    quantity100 = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(() => SubTotal100);
                    NotifyPropertyChanged(() => SubTotal100Text);
                    NotifyPropertyChanged(() => Total);
                    //NotifyPropertyChanged(() => TotalText);
                }
            }
        }

        public short Quantity50
        {
            get
            {
                return quantity50;
            }
            set
            {
                if (quantity50 != value)
                {
                    quantity50 = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(() => SubTotal50);
                    NotifyPropertyChanged(() => SubTotal50Text);
                    NotifyPropertyChanged(() => Total);
                    //NotifyPropertyChanged(() => TotalText);
                }
            }
        }

        public short Quantity10
        {
            get
            {
                return quantity10;
            }
            set
            {
                if (quantity10 != value)
                {
                    quantity10 = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(() => SubTotal10);
                    NotifyPropertyChanged(() => SubTotal10Text);
                    NotifyPropertyChanged(() => Total);
                    //NotifyPropertyChanged(() => TotalText);
                }
            }
        }

        public short Quantity5
        {
            get
            {
                return quantity5;
            }
            set
            {
                if (quantity5 != value)
                {
                    quantity5 = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(() => SubTotal5);
                    NotifyPropertyChanged(() => SubTotal5Text);
                    NotifyPropertyChanged(() => Total);
                    //NotifyPropertyChanged(() => TotalText);
                }
            }
        }

        public short Quantity2
        {
            get
            {
                return quantity2;
            }
            set
            {
                if (quantity2 != value)
                {
                    quantity2 = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(() => SubTotal2);
                    NotifyPropertyChanged(() => SubTotal2Text);
                    NotifyPropertyChanged(() => Total);
                    //NotifyPropertyChanged(() => TotalText);
                }
            }
        }

        public decimal CoinAmount
        {
            get
            {
                return coinAmount;
            }
            set
            {
                if (coinAmount != value)
                {
                    coinAmount = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(() => Total);
                    //NotifyPropertyChanged(() => TotalText);
                }
            }
        }

        public int SubTotal10000 { get { return 10000 * Quantity10000; } }
        public string SubTotal10000Text { get { return SubTotal10000.ToString("N2"); } }
        public int SubTotal1000 { get { return 1000 * Quantity1000; } }
        public string SubTotal1000Text { get { return SubTotal1000.ToString("N2"); } }
        public int SubTotal100 { get { return 100 * Quantity100; } }
        public string SubTotal100Text { get { return SubTotal100.ToString("N2"); } }
        public int SubTotal50 { get { return 50 * Quantity50; } }
        public string SubTotal50Text { get { return SubTotal50.ToString("N2"); } }
        public int SubTotal10 { get { return 10 * Quantity10; } }
        public string SubTotal10Text { get { return SubTotal10.ToString("N2"); } }
        public int SubTotal5 { get { return 5 * Quantity5; } }
        public string SubTotal5Text { get { return SubTotal5.ToString("N2"); } }
        public int SubTotal2 { get { return 2 * Quantity2; } }
        public string SubTotal2Text { get { return SubTotal2.ToString("N2"); } }
        public decimal Total
        {
            get
            {
                return SubTotal10000 + SubTotal1000 + SubTotal100 + SubTotal50 + SubTotal10 + SubTotal5 + SubTotal2 + CoinAmount;
            }
        }
        //public string TotalText { get { return Total.ToString("N2"); } }

        public CurrencyDenomination ToDenomination(int ledgerID = 0)
        {
            CurrencyDenomination denomination = new CurrencyDenomination();
            denomination.CurrencyDenominationID = currencyDenominationID;
            denomination.LedgerID = ledgerID;
            denomination.CurrencyDenominationTypeID = typeID;
            denomination.Quantity10000 = Quantity10000;
            denomination.Quantity1000 = Quantity1000;
            denomination.Quantity100 = Quantity100;
            denomination.Quantity50 = Quantity50;
            denomination.Quantity10 = Quantity10;
            denomination.Quantity5 = Quantity5;
            denomination.Quantity2 = Quantity2;
            denomination.CoinAmount = CoinAmount;
            return denomination;
        }

        public DenominationControl()
        {
            InitializeComponent();
            quantity10000NumericUpDown.DataBindings.Add("Value", this, DataBindingHelper.GetPropertyName(() => Quantity10000));
            quantity1000NumericUpDown.DataBindings.Add("Value", this, DataBindingHelper.GetPropertyName(() => Quantity1000));
            quantity100NumericUpDown.DataBindings.Add("Value", this, DataBindingHelper.GetPropertyName(() => Quantity100));
            quantity50NumericUpDown.DataBindings.Add("Value", this, DataBindingHelper.GetPropertyName(() => Quantity50));
            quantity10NumericUpDown.DataBindings.Add("Value", this, DataBindingHelper.GetPropertyName(() => Quantity10));
            quantity5NumericUpDown.DataBindings.Add("Value", this, DataBindingHelper.GetPropertyName(() => Quantity5));
            quantity2NumericUpDown.DataBindings.Add("Value", this, DataBindingHelper.GetPropertyName(() => Quantity2));
            coinAmountNumericUpDown.DataBindings.Add("Value", this, DataBindingHelper.GetPropertyName(() => CoinAmount));
            subTotal10000TextBox.DataBindings.Add("Text", this, DataBindingHelper.GetPropertyName(() => SubTotal10000Text));
            subTotal1000TextBox.DataBindings.Add("Text", this, DataBindingHelper.GetPropertyName(() => SubTotal1000Text));
            subTotal100TextBox.DataBindings.Add("Text", this, DataBindingHelper.GetPropertyName(() => SubTotal100Text));
            subTotal50TextBox.DataBindings.Add("Text", this, DataBindingHelper.GetPropertyName(() => SubTotal50Text));
            subTotal10TextBox.DataBindings.Add("Text", this, DataBindingHelper.GetPropertyName(() => SubTotal10Text));
            subTotal5TextBox.DataBindings.Add("Text", this, DataBindingHelper.GetPropertyName(() => SubTotal5Text));
            subTotal2TextBox.DataBindings.Add("Text", this, DataBindingHelper.GetPropertyName(() => SubTotal2Text));
        }
    }
}
