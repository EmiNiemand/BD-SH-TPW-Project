using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Data
{
    public interface IBall : ISerializable
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", id);
            info.AddValue("Diameter", ballD);
            info.AddValue("Mass", mass);
            info.AddValue("Position", position);
            info.AddValue("Direction", direction);
        }
    }
}
