using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class BallEventArgs : EventArgs
    {
        public readonly IBall Ball;
        public BallEventArgs(IBall ball)
        {
            this.Ball = ball;
        }
    }
}
