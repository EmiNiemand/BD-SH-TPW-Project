using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
                    BallsList.Add(new NotifyBall(_model.screenSize/2, _model.ballsR));
				}

                _model.BallMoved += (sender, argv) =>
                {
                    if (BallsList.Count > 0)
                        BallsList[argv.id].ChangePosition(argv.position);
                };
                ((RelayCommand)CreateBallsAndStartSimulation).ChangeIsEnable(false);
                _model.StartSimulation();
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
            X = (double)position.X;
            Y = (double)position.Y;
            this.R = (double)R;
        }

        public void ChangePosition(Vector2 position)
		{
            X = position.X;
            Y = position.Y;
		}

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }

    public class AsyncObservableCollection<T> : ObservableCollection<T>
    {
        private SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        public AsyncObservableCollection()
        {
        }

        public AsyncObservableCollection(IEnumerable<T> list)
            : base(list)
        {
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (SynchronizationContext.Current == _synchronizationContext)
            {
                // Execute the CollectionChanged event on the current thread
                RaiseCollectionChanged(e);
            }
            else
            {
                // Raises the CollectionChanged event on the creator thread
                _synchronizationContext.Send(RaiseCollectionChanged, e);
            }
        }

        private void RaiseCollectionChanged(object param)
        {
            // We are in the creator thread, call the base implementation directly
            base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (SynchronizationContext.Current == _synchronizationContext)
            {
                // Execute the PropertyChanged event on the current thread
                RaisePropertyChanged(e);
            }
            else
            {
                // Raises the PropertyChanged event on the creator thread
                _synchronizationContext.Send(RaisePropertyChanged, e);
            }
        }

        private void RaisePropertyChanged(object param)
        {
            // We are in the creator thread, call the base implementation directly
            base.OnPropertyChanged((PropertyChangedEventArgs)param);
        }
    }
}
