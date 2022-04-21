using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private String ballsNumber;

        public String BallsNumber
        {
            get { return this.ballsNumber; }
            set
            {
                this.ballsNumber = Convert.ToInt32(value).ToString();
                OnPropertyChanged(nameof(BallsNumber));
            }
        }

        public ICommand CreateBalls { get; set; }

        public ViewModel()
        {
            BallsNumber = "0";
            CreateBalls = new RelayCommand(() => {
                BallsNumber = Convert.ToInt32(BallsNumber).ToString(); 
                });
        }


        class RelayCommand : ICommand
        {
            private readonly Action _handler;
            private bool _isEnabled;

            public RelayCommand(Action handler)
            {
                _handler = handler;
                _isEnabled = true;
            }

            public bool IsEnabled
            {
                get { return _isEnabled; }
                set
                {
                    if (value != _isEnabled)
                    {
                        _isEnabled = value;
                        if (CanExecuteChanged != null)
                        {
                            CanExecuteChanged(this, EventArgs.Empty);
                        }
                    }
                }
            }

            public bool CanExecute(object parameter)
            {
                return IsEnabled;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                _handler();
            }
        }
    }
}
