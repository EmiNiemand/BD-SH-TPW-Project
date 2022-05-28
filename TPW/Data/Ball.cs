using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Data
{
    public interface IBall
    {
        public int id { get; }
        public Vector2 position { get; }
        public float ballD { get; }
        public float mass { get; }
        public Vector2 direction { get; set; }
        public void StartMoving();
        public event EventHandler<BallEventArgs>? Moved;
    }

    public class Ball : IBall
    {
        public int id { get; }
        public Vector2 position { get; private set; }
        public float ballD { get; }
        public float mass { get; }
        public Vector2 direction { get; set; }
        public event EventHandler<BallEventArgs>? Moved;

        public Ball(int id, Vector2 position, float ballD, Vector2 direction)
        {
            this.id = id;
            this.position = position;
            this.ballD = ballD;
            this.mass = ballD / 10;
            this.direction = direction;
        }

        public async void StartMoving()
        {
            while (true)
            {
                this.position += direction;
                BallEventArgs args = new BallEventArgs(this);
                Moved?.Invoke(this, args);
                await Task.Delay(1);
            }
        }
    }
}
