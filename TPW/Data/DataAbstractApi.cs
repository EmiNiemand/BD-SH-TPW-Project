using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BallsEventArgs : EventArgs
    {
        public readonly IBall Ball;
        public readonly IList<IBall> Balls;
        public BallsEventArgs(IBall ball, IList<IBall> balls)
        {
            this.Ball = ball;
            this.Balls = balls;
        }
    }

    public abstract class DataAbstractApi
    {
        public event EventHandler<BallsEventArgs>? BallMoved;
        protected IList<IBall>? ballsList;
        protected Random random;
        public Vector2 screenSize { get; protected set; }
        protected DataAbstractApi(Vector2 boardSize)
        {
            this.screenSize = boardSize;
        }
        public abstract IList<IBall> GetBalls();
        public abstract void CreateBalls(int ballsNumber);
        public abstract void StartSimulation();
        public virtual void OnBallMoved(BallsEventArgs args)
        {
            BallMoved?.Invoke(this, args);
        }

        public static DataAbstractApi? CreateDataApi(Vector2 screenSize)
        {
            return new DataApi(screenSize);
        }

    }

    public class DataApi : DataAbstractApi
    {
        public DataApi(Vector2 screenSize) : base(screenSize)
        {
            this.ballsList = new List<IBall>();
            this.random = new Random();
        }

        public override void StartSimulation()
        {
            foreach (var ball in ballsList)
            {
                ball.Moved += (sender, argv) =>
                {
                    var args = new BallsEventArgs(argv.Ball, new List<IBall>(ballsList));
                    this.OnBallMoved(args);
                };
                Task.Factory.StartNew(ball.StartMoving);
            }
        }

        public override IList<IBall> GetBalls()
        {
            return ballsList;
        }

        public override void CreateBalls(int ballsNumber)
        {   
            for (int i = 0; i < ballsNumber; i++)
            {
                var ballD = this.GetRandomD();
                var isPositionFree = false;
                var position = new Vector2(0, 0);
                while (!isPositionFree)
                {
                    position = this.GetStartRandomPosition(ballD);
                    isPositionFree = this.IsPositionFree(position, ballD);

                }
                Ball ball = new Ball(ballsList.Count, position, ballD, this.GetRandomMass(), this.GenerateDirection());
                ballsList.Add(ball);
            }
        }

        private Vector2 GetStartRandomPosition(float ballsD)
		{
			Vector2 randomPoint;
			randomPoint.X = (float)(random.Next(Convert.ToInt32(screenSize.X - ballsD * 2))) + ballsD;
			randomPoint.Y = (float)(random.Next(Convert.ToInt32(screenSize.Y - ballsD * 2))) + ballsD;
			return randomPoint;
		}

        private bool IsPositionFree(Vector2 position, float ballD)
        {
            foreach (var ball in ballsList)
            {
                if(this.DoBallsCollide(position, ballD, ball.position, ball.ballD))
                {
                    return false;
                }
            }
            return true;
        }

        private bool DoBallsCollide(Vector2 pos1, float ballD1, Vector2 pos2, float ballD2)
        {
            var ballsDistance = (pos1.X - pos2.X) * (pos1.X - pos2.X) + (pos1.Y - pos2.Y) * (pos1.Y - pos2.Y);
            var ballsRDistance = (ballD1/2 + ballD2/2) * (ballD1/2 + ballD2/2);
            return ballsDistance <= ballsRDistance;
        }

        public float GetRandomD()
        {
            return random.Next(10, 40);
        }

        public float GetRandomMass()
        {
            return (float)(random.Next(1, 5)) / 2;
        }

        public Vector2 GenerateDirection()
        {
            Vector2 direction;
            direction.X = (float)(random.NextDouble() * 10 - 5);
            direction.Y = (float)(random.NextDouble() * 10 - 5);
            return direction;
        }

        public override void OnBallMoved(BallsEventArgs args)
        {
            base.OnBallMoved(args);
        }
    }
}
