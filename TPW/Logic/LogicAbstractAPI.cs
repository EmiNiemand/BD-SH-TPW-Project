using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public ObservableCollection<Ball> balls;
        public float ballsR;
        public abstract Vector2 screenSize { get; }
        public abstract void CreateBalls(int ballsNumber);
        public abstract void ClearBalls();
        public abstract ObservableCollection<Ball> GetBalls();
        public abstract void MoveBalls();
        public abstract void UpdateBallPosition(int interval_ms);
        public static LogicAbstractApi CreateApi(Vector2 screenSize)
        {
            return new LogicApi(screenSize);
        }
    }

    public class LogicApi : LogicAbstractApi
    {
        public override Vector2 screenSize { get; }
        private Random random;

        public LogicApi(Vector2 screenSize, float ballsR = 10)
        {
            this.ballsR = ballsR;
            this.balls = new ObservableCollection<Ball>();
            this.screenSize = screenSize;
            random = new Random();
        }

        public override void CreateBalls(int ballsNumber)
        {
            for (int i = 0; i < ballsNumber; i++)
            {
                balls.Add(new Ball(GetStartRandomPosition(), 10));
            }
        }

        public override void ClearBalls()
        {
            balls.Clear();
        }

        private Vector2 GetStartRandomPosition()
        {
            Vector2 randomPoint;
            randomPoint.X = (float)(random.Next(Convert.ToInt32(screenSize.X - ballsR * 2))) + ballsR;
            randomPoint.Y = (float)(random.Next(Convert.ToInt32(screenSize.Y - ballsR * 2))) + ballsR;
            return randomPoint;
        }
        
        private Vector2 GetNextRandomPosition(Vector2 position)
        {
            Vector2 randomPoint;
            randomPoint.X = (float)(random.NextDouble() - 0.5) * 5;
            randomPoint.Y = (float)(random.NextDouble() - 0.5) * 5;
            return position+randomPoint;
        }

        public override ObservableCollection<Ball> GetBalls()
        {
            return balls;
        }

        public override void MoveBalls()
        {
            foreach (var ball in balls)
            {
                ball.ChangePosition(GetNextRandomPosition(ball.GetPosition()));
            }
        }

        public override async void UpdateBallPosition(int interval_ms)
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    MoveBalls();
                    Task.Delay(interval_ms).Wait();
                }
            });
        }
    }
}
