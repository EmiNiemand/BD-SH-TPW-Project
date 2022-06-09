using System;
using System.Collections.Generic;
using System.Numerics;
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
        protected IList<IBall>? list;
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
        private readonly Logger logger;

        public DataApi(Vector2 screenSize) : base(screenSize)
        {
            this.list = new List<IBall>();
            this.random = new Random();
            this.logger = new Logger();
        }

        public override void StartSimulation()
        {
            Task.Factory.StartNew(logger.LogToFile);
            foreach (IBall? ball in list)
            {
                ball.Moved += (sender, argv) =>
                {
                    BallsEventArgs args = new BallsEventArgs(argv.Ball, new List<IBall>(list));
                    this.OnBallMoved(args);
                };
                Task.Factory.StartNew(ball.StartMoving);
            }
        }

        public override IList<IBall>? GetBalls() => list;

        public override void CreateBalls(int ballsNumber)
        {
            Random rand = new Random();

            for (int i = 0; i < ballsNumber; i++)
            {
                float ballD = this.GetRandomD();
                bool isPositionFree = false;
                Vector2 position = new Vector2(0, 0);
                while (!isPositionFree)
                {
                    position = this.GetStartRandomPosition(ballD);
                    isPositionFree = this.IsPositionFree(position, ballD);
                }
                Ball ball = new Ball(list.Count, position, ballD, this.GenerateDirection());
                list.Add(ball);
            }
        }

        private Vector2 GetStartRandomPosition(float ballsD)
		{
            Vector2 randomPoint;
			randomPoint.X = (float)(random.Next(Convert.ToInt32(screenSize.X - ballsD * 2)) + ballsD);
			randomPoint.Y = (float)(random.Next(Convert.ToInt32(screenSize.Y - ballsD * 2)) + ballsD);
			return randomPoint;
		}

        private bool IsPositionFree(Vector2 position, float ballD)
        {
            foreach (IBall? ball in list)
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
            float ballsDistance = (pos1.X - pos2.X) * (pos1.X - pos2.X) + (pos1.Y - pos2.Y) * (pos1.Y - pos2.Y);
            float ballsRDistance = (ballD1/2 + ballD2/2) * (ballD1/2 + ballD2/2);
            return ballsDistance <= ballsRDistance;
        }

        public float GetRandomD()
        {
            return random.Next(20, 40);
        }

        public Vector2 GenerateDirection()
        {
            Vector2 direction;
            direction.X = (float)((random.NextDouble() * 2 - 1) * 2);
            direction.Y = (float)((random.NextDouble() * 2 - 1) * 2);
            return direction;
        }

        public override void OnBallMoved(BallsEventArgs args)
        {
            Ball ballCopy = new Ball(args.Ball.id, new Vector2(args.Ball.position.X, args.Ball.position.Y), args.Ball.ballD, new Vector2(args.Ball.direction.X, args.Ball.direction.Y));
            logger.AddToLogQueue(ballCopy);
            base.OnBallMoved(args);
        }

    }
}
