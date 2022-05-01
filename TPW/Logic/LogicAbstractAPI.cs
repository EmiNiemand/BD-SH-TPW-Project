using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Data;

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


	public abstract class LogicAbstractApi
	{
		protected DataAbstractApi data;
		public event EventHandler<LogicBallEventArgs>? BallMoved;
		public abstract Vector2 screenSize { get; }
		public virtual void OnBallMoved(LogicBallEventArgs args)
		{
			BallMoved?.Invoke(this, args);
		}
		public abstract void StartSimulation();
		public abstract void CreateBalls(int ballsNumber, float ballsD);
		public static LogicAbstractApi CreateApi(Vector2 screenSize, DataAbstractApi data = default(DataAbstractApi))
		{
			if(data == null)
            {
				data = DataAbstractApi.CreateApi();
            }
			return new LogicApi(screenSize, data);
		}
	}

	public class LogicApi : LogicAbstractApi
	{
		public override Vector2 screenSize { get; }
		private Random random;

		public LogicApi(Vector2 screenSize, DataAbstractApi data)
		{
			this.data = data;
			this.screenSize = screenSize;
			random = new Random();
		}

        public override void StartSimulation()
        {
			data.BallMoved += (sender, args) =>
			{
				this.OnBallMoved(new LogicBallEventArgs(new BallStripper(args.Ball)));
			};
			data.StartSimulation();
        }

        public override void CreateBalls(int ballsNumber, float ballsD)
		{
			for(int i = 0; i < ballsNumber; i++)
            {
				data.CreateBalls(GetStartRandomPosition(ballsD), ballsD);
			}
		}

		private Vector2 GetStartRandomPosition(float ballsD)
		{
			Vector2 randomPoint;
			randomPoint.X = (float)(random.Next(Convert.ToInt32(screenSize.X - ballsD * 2))) + ballsD;
			randomPoint.Y = (float)(random.Next(Convert.ToInt32(screenSize.Y - ballsD * 2))) + ballsD;
			return randomPoint;
		}

		public override void OnBallMoved(LogicBallEventArgs args)
		{
			base.OnBallMoved(args);
		}
	}
}