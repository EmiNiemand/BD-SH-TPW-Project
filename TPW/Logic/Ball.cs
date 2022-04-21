using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class Ball : INotifyPropertyChanged
    {
        private float _ballX { get; set; }
        private float _ballY { get; set; }
        private float _ballR { get; }

        public Ball(float ballX, float ballY, float ballR)
        {
            _ballX = ballX;
            _ballY = ballY;
            _ballR = ballR;
        }

        public Ball(Vector2 position, float ballR)
        {
            _ballX = position.X;
            _ballY = position.Y;
            _ballR = ballR;
        }

        public void ChangePosition(Vector2 position)
        {
            _ballX = position.X;
            _ballY = position.Y;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(_ballX, _ballY);
        }

        public float GetRadius()
        {
            return _ballR;
        }
		
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
