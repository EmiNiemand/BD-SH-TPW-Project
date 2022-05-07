using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Data;

namespace Logic
{
    public interface ILogicBall
    {
        int id { get; }
        Vector2 position { get; }
        float ballD { get; }
        float mass { get; }
    }

    internal class BallStripper : ILogicBall
    {
        private readonly IBall ball;

        public BallStripper(IBall ball)
        {
            this.ball = ball;
        }

        public int id { get => ball.id; }
        public Vector2 position { get => ball.position; }
        public float ballD { get => ball.ballD; }
        public float mass { get => ball.mass; }
    }
}
