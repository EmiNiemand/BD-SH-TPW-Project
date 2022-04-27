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
	public class BallEventArgs : EventArgs
	{
		public Vector2 position;
		public int id;
		public BallEventArgs(int id, Vector2 position)
		{
			this.id = id;
			this.position = position;
		}
	}


	public abstract class LogicAbstractApi
	{
		protected DataAbstractApi? data;
		protected IList<BallWrapper>? ballsList;
		public event EventHandler<BallEventArgs>? BallMoved;

		protected virtual void OnBallMoved(BallEventArgs args)
        {
			BallMoved?.Invoke(this, args);
        }

		public abstract Vector2 screenSize { get; }
		public abstract void CreateBalls(int ballsNumber, float ballsR);
		public abstract IList<BallWrapper> GetBalls();
		public abstract void ClearBalls();
		public abstract void MoveBalls(int interval_ms);
		public abstract void MoveBall(BallWrapper ball);
		public abstract void ChangeBallsPosition(int interval_ms, BallWrapper ball);
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
			ballsList = new List<BallWrapper>();
			random = new Random();
		}

		public override void CreateBalls(int ballsNumber, float ballsR)
		{
			for (int i = 0; i < ballsNumber; i++)
			{
				ballsList.Add(new BallWrapper(i, data.CreateBall(GetStartRandomPosition(ballsR), ballsR)));
			}
		}

		public override IList<BallWrapper> GetBalls()
		{
			return new List<BallWrapper>(ballsList);
		}

		public override void ClearBalls()
		{
			ballsList.Clear();
		}

		private Vector2 GetStartRandomPosition(float ballsR)
		{
			Vector2 randomPoint;
			randomPoint.X = (float)(random.Next(Convert.ToInt32(screenSize.X - ballsR * 2))) + ballsR;
			randomPoint.Y = (float)(random.Next(Convert.ToInt32(screenSize.Y - ballsR * 2))) + ballsR;
			return randomPoint;
		}

		private Vector2 GetNextRandomPosition(Vector2 position, float ballsR)
		{
			Vector2 randomPoint;
			randomPoint.X = (float)(random.NextDouble() - 0.5) * 5;
			randomPoint.Y = (float)(random.NextDouble() - 0.5) * 5;
			var temp = position + randomPoint;
			if (temp.X < ballsR || temp.X > (screenSize.X - ballsR)) randomPoint.X = -randomPoint.X;
			if (temp.Y < ballsR || temp.Y > (screenSize.Y - ballsR)) randomPoint.Y = -randomPoint.Y;
			return position + randomPoint;
		}

		public override void MoveBalls(int interval_ms)
		{
			foreach (var ball in ballsList)
            {
				ChangeBallsPosition(interval_ms, ball);
            }
		}

		public override void MoveBall(BallWrapper ball)
		{
				ball.ChangePosition(GetNextRandomPosition(ball.GetPosition(), ball.GetRadius()));
				OnBallMoved(new BallEventArgs(ball.id, ball.GetPosition()));
		}

		public override async void ChangeBallsPosition(int interval_ms, BallWrapper ball)
		{
				await Task.Run(() =>
				{
					while (true)
					{
						MoveBall(ball);
						Task.Delay(interval_ms).Wait();
					}
				});
		}

		protected override void OnBallMoved(BallEventArgs args)
		{
			base.OnBallMoved(args);
		}
	}
}