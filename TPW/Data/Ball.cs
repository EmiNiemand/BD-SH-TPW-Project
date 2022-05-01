using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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

    public interface IBall
    {
        public int id { get; }
        public Vector2 position { get; }
        public float ballD { get; }
        public float mass { get; }
        public Vector2 direction { get; }
        public void StartMoving();
        public event EventHandler<BallEventArgs>? Moved;
    }

    public class Ball : IBall
    {
        public int id { get; }
        public Vector2 position { get; private set; }
        public float ballD { get; }
        public float mass { get; }
        public Vector2 direction { get; private set; }
        private Random random;
        public event EventHandler<BallEventArgs>? Moved;

        public Ball(int id, Vector2 position, float ballD)
        {
            this.id = id;
            this.position = position;
            this.ballD = ballD;
            random = new Random();
            this.mass = (float)(random.NextDouble() * 5 / 10);
            direction = GenerateDirection(random);
        }

        private static Vector2 GenerateDirection(Random random)
        {
            Vector2 direction;
            direction.X = (float)(random.NextDouble() * 2 - 1);
            direction.Y = (float)(random.NextDouble() * 2 - 1);
            return direction;
        }

        public async void StartMoving()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    this.position += direction * mass;
                    Moved?.Invoke(this, new BallEventArgs(this));
                    Task.Delay(10).Wait();
                }
            });
        }
    }
}
