using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Numerics;
using System.Text;
using System.Threading;
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
		public event EventHandler<LogicBallEventArgs>? BallMoved;
		public virtual void OnBallMoved(LogicBallEventArgs args)
		{
			BallMoved?.Invoke(this, args);
		}
		public abstract void StartSimulation();
		public abstract void CreateBalls(int ballsNumber);
		public static LogicAbstractApi CreateApi(Vector2 screenSize, DataAbstractApi? data = default(DataAbstractApi))
		{
			if(data == null)
            {
				data = DataAbstractApi.CreateDataApi(screenSize);
            }
			return new LogicApi(data);
		}
	}

	public class LogicApi : LogicAbstractApi
	{
		protected readonly DataAbstractApi? data;
		public LogicApi(DataAbstractApi data)
		{
			this.data = data;
		}

        public override void StartSimulation()
        {
			data.BallMoved += OnDataBallMoved;
			data.StartSimulation();
        }

        public override void CreateBalls(int ballsNumber)
		{
			data.CreateBalls(ballsNumber);
		}

		private void OnDataBallMoved(object _, Data.BallsEventArgs args)
		{
			this.OnBallMoved(new LogicBallEventArgs(new BallStripper(args.Ball)));
			CollisionHandler.DoesBallCollideWithWalls(args.Ball, data.screenSize);
			//tutaj dopisać jakieś ogarnianie odbić kulek ale to nie na moją głowę póki co 
		}
	}
}