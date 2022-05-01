using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public event EventHandler<BallEventArgs>? BallMoved;
        protected IList<IBall>? ballsList;

        public abstract void CreateBalls(Vector2 position, float ballD);
        public abstract void StartSimulation();
        public virtual void OnBallMoved(BallEventArgs args)
        {
            BallMoved?.Invoke(this, args);
        }
        public static DataAbstractApi CreateApi()
        {
            return new DataApi();
        }
    }

    public class DataApi : DataAbstractApi
    {
        public DataApi()
        {
            ballsList = new List<IBall>();
        }

        public override void StartSimulation()
        {
            foreach (var ball in ballsList)
            {
                ball.Moved += (sender, args) =>
                {
                    this.OnBallMoved(args);
                };
                Task.Factory.StartNew(ball.StartMoving);
            }
        }

        public override void CreateBalls(Vector2 position, float ballD)
        {
            Ball ball = new Ball(ballsList.Count, position, ballD);
            ballsList.Add(ball);
        }

        public override void OnBallMoved(BallEventArgs args)
        {
            base.OnBallMoved(args);
        }
    }
}
