using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
	public class LogicBallEventArgs : EventArgs
	{
		public readonly ILogicBall Ball;
		public LogicBallEventArgs(ILogicBall ball)
		{
			this.Ball = ball;
		}
	}
}
