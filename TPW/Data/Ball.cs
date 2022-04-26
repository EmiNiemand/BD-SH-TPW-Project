using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data
{
    public interface IBall
    {
        public float ballX { get; set; }
        public float ballY { get; set; }
        public float ballR { get; }

        public void ChangePosition(Vector2 position);
        public Vector2 GetPosition();
        public float GetRadius();
    }

    internal class Ball : IBall
    {
        public float ballX { get; set; }
        public float ballY { get; set; }
        public float ballR { get; }

        public Ball(float ballx, float bally, float ballr)
        {
            ballX = ballx;
            ballY = bally;
            ballR = ballr;
        }

        public Ball(Vector2 position, float ballR) : this(position.X, position.Y, ballR)
        {
        }

        public void ChangePosition(Vector2 position)
        {
            ballX = position.X;
            ballY = position.Y;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(ballX, ballY);
        }

        public float GetRadius()
        {
            return ballR;
        }
    }
}
