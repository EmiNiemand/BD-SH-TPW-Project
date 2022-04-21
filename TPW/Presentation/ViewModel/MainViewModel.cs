using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Presentation.Model;

namespace Presentation.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private MainModel _model;
        public ObservableCollection<NotifyBall> BallsList { get; set; } 

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
            BallsList = new ObservableCollection<NotifyBall>();
            BallsNumber = 10;
            CreateBallsAndStartSimulation = new RelayCommand(() => {
                BallsList.Clear();
                _model.StartSimulation(_model.GetBallsNumber());
                foreach (var ball in _model.GetBalls())
                {
                    BallsList.Add(new NotifyBall(ball.GetPosition(), ball.GetRadius()));
                }
                });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
    
    public class NotifyBall : INotifyPropertyChanged
    {
        private double _X;
        public double X
        {
            get { return _X; }
            set { _X = value; OnPropertyChanged(); }
        }

        private double _Y;
        public double Y
        {
            get { return _Y; }
            set { _Y = value; OnPropertyChanged(); }
        }

        private double _R;
        public double R
        {
            get { return _R; }
            set { _R = value; OnPropertyChanged(); }
        }

        public NotifyBall(Vector2 position, float R)
        {
            X = (double) position.X;
            Y = (double) position.Y;
            this.R = (double) R;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
