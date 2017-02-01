using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calc
{
    class CalcViewModel : INotifyPropertyChanged
    {
        string inputString = "";
        string displayText = "";
        public event PropertyChangedEventHandler PropertyChanged;

        public CalcViewModel()
        {
            this.AddCharCommand = new Command<string>((key) =>
            {
                this.InputString += key;
            });

            this.DeleteCharCommand = new Command((nothing) =>
            {
                this.InputString = this.InputString.Substring(0, this.InputString.Length - 1);
            },
            (nothing) =>
            {
                return this.InputString.Length > 0;
            });

            this.ClearCommand = new Command((nohing) =>
            {
                this.InputString = "";
            });

            this.OperationCommand = new Command<string>((key) =>
            {
                this.Op = key;
                this.Op1 = Convert.ToDouble(this.InputString);
                this.InputString = "";
            });

            this.CalcCommand = new Command<string>((nothing) =>
            {
                this.Op2 = Convert.ToDouble(this.InputString);

                switch (this.Op)
                {
                    case "+":
                        this.InputString = (this.Op1 + this.Op2).ToString();
                        break;
                    case "-":
                        this.InputString = (this.Op1 - this.Op2).ToString();
                        break;
                    case "*":
                        this.InputString = (this.Op1 * this.Op2).ToString();
                        break;
                    case "/":
                        this.InputString = (this.Op1 / this.Op2).ToString();
                        break;
                }
            });
        }

        public string InputString
        {
            protected set
            {
                if (inputString != value)
                {
                    inputString = value;
                    OnPropertyChanged("InputString");
                    this.displayText = inputString;

                    ((Command)this.DeleteCharCommand).ChangeCanExecute();
                }
            }
            get { return inputString; }
        }

        public string DisplayText
        {
            protected set
            {
                if (displayText != value)
                {
                    displayText = value;
                    OnPropertyChanged("DisplayText");
                }
            }
            get { return displayText; }
        }

        public string Op { get; set; }
        public string Op1 { get; set; }
        public string Op2 { get; set; }

        public ICommand AddCharCommand { protected set; get; }
        public ICommand DeleteCharCommand { protected set; get; }
        public ICommand ClearCommand { protected set; get; }
        public ICommand OperationCommand { protected set; get; }
        public ICommand CalcCommand { protected set; get; }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangingEventArgs(propertyName));
            }
        }
    }
}
