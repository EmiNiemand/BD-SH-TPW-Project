using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Presentation.ViewModel
{
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

        private double _D;
        public double D
        {
            get { return _D; }
            set { _D = value; OnPropertyChanged(); }
        }

        public NotifyBall()
        {
            Vector2 position = new Vector2(0, 0);
            X = (double)position.X;
            Y = (double)position.Y;
            this.D = 0;
        }

        public void ChangePosition(Vector2 position)
        {
            X = position.X;
            Y = position.Y;
        }

        public void ChangeD(float D)
        {
            this.D = D;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
