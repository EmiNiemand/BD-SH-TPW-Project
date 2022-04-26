using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Data;

namespace Logic
{
    public class BallWrapper
    {
        public int id { get; }
        private IBall ball;

        public BallWrapper(int id, IBall ball)
        {
            this.id = id;
            this.ball = ball;
        }

        public void ChangePosition(Vector2 position)
        {
            ball.ChangePosition(position);
        }

        public Vector2 GetPosition()
        {
            return ball.GetPosition();
        }

        public float GetRadius()
        {
            return ball.GetRadius();
        }
    }
}
