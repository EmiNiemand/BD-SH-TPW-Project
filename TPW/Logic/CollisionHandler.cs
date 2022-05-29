using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Data;

namespace Logic
{
    internal class CollisionHandler
    {
        public static IBall? CheckBallsCollisions(IBall ball, IEnumerable<IBall> ballsList)
        {
            foreach (IBall ball2 in ballsList)
            {
                if (ReferenceEquals(ball, ball2))
                {
                    continue;
                }

                if (DoBallsCollide(ball, ball2))
                {
                    return ball2;
                }
            }

            return null;
        }

        private static bool DoBallsCollide(IBall ball1, IBall ball2)
        {
            Vector2 ball1NextPos = ball1.position + (Vector2.One * ball1.ballD / 2) + ball1.direction;
            Vector2 ball2NextPos = ball2.position + (Vector2.One * ball2.ballD / 2) + ball2.direction;
            float ballsDistance = (float)(Math.Pow(ball1NextPos.X - ball2NextPos.X, 2) + Math.Pow(ball1NextPos.Y - ball2NextPos.Y, 2));
            float ballsRDistance = (float)Math.Pow(ball1.ballD / 2 + ball2.ballD / 2, 2);
            return ballsDistance <= ballsRDistance;
        }

        public static void DoesBallCollideWithWalls(IBall ball, Vector2 screenSize)
        {
            Vector2 ballNextPos = ball.position + (Vector2.One * ball.ballD / 2) + ball.direction;
            if (ballNextPos.X <= ball.ballD / 2 || ballNextPos.X + ball.ballD / 2 >= screenSize.X)
            {
                ball.direction = new Vector2(-ball.direction.X, ball.direction.Y);
            }

            if (ballNextPos.Y <= ball.ballD / 2 || ballNextPos.Y + ball.ballD / 2 >= screenSize.Y)
            {
                ball.direction = new Vector2(ball.direction.X, -ball.direction.Y);
            }
        }

        public static void HandleBallsCollision(IBall ball1, IBall ball2)
        {
            Vector2 ball1center = ball1.position + (Vector2.One * ball1.ballD / 2);
            Vector2 ball2center = ball2.position + (Vector2.One * ball2.ballD / 2);

            float centerDistancePow1 = (float)(Math.Pow(ball1center.X - ball2center.X, 2) + Math.Pow(ball1center.Y - ball2center.Y, 2));
            float massCalculation1 = 2 * ball2.mass / (ball1.mass + ball2.mass);
            Vector2 Oi1 = ball1center - ball2center;
            float dotProduct1 = Vector2.Dot(ball1.direction - ball2.direction, Oi1);

            float centerDistancePow2 = (float)(Math.Pow(ball2center.X - ball1center.X, 2) + Math.Pow(ball2center.Y - ball1center.Y, 2));
            float massCalculation2 = 2 * ball1.mass / (ball1.mass + ball2.mass);
            Vector2 Oi2 = ball2center - ball1center;
            float dotProduct2 = Vector2.Dot(ball2.direction - ball1.direction, Oi2);

            ball1.direction -= massCalculation1 * dotProduct1 / centerDistancePow1 * Oi1;
            ball2.direction -= massCalculation2 * dotProduct2 / centerDistancePow2 * Oi2;
        }
    }
}
