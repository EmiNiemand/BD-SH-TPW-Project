using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Presentation.Model;

namespace Presentation.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private MainModel _model;
        public AsyncObservableCollection<NotifyBall> BallsList { get; set; }

        public int BallsNumber
        {
            get { return _model.GetBallsNumber(); }
            set
            {
                _model.SetBallsNumber(value);
                OnPropertyChanged();
            }
        }

        public ICommand CreateBallsAndStartSimulation { get; set; }

        public MainViewModel()
        {
            _model = new MainModel();
            BallsList = new AsyncObservableCollection<NotifyBall>();
            BallsNumber = 10;
            CreateBallsAndStartSimulation = new RelayCommand(() =>
            {
                BallsList.Clear();
                for (int i = 0; i < BallsNumber; i++)
				{
                    var notifyBall = new NotifyBall();
                    BallsList.Add(notifyBall);
				}

                _model.BallMoved += (sender, args) =>
                {
                    if (BallsList.Count > 0)
                    {
                        BallsList[args.id].ChangePosition(args.position);
                        BallsList[args.id].ChangeD(args.ballD);
                    }
                };
                ((RelayCommand)CreateBallsAndStartSimulation).ChangeIsEnable(false);
                _model.StartSimulation();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, args);
        }

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

        public void ChangeIsEnable(bool parameter)
        {
            IsEnabled = parameter;
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
