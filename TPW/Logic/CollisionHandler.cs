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
            foreach (var ball2 in ballsList)
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
            var ballsDistance = (ball1.position.X - ball2.position.X) * (ball1.position.X - ball2.position.X) + (ball1.position.Y - ball2.position.Y) * (ball1.position.Y - ball2.position.Y);
            var ballsRDistance = (ball1.ballD / 2 + ball2.ballD / 2) * (ball1.ballD / 2 + ball2.ballD / 2);
            return ballsDistance <= ballsRDistance;
        }

        public static void DoesBallCollideWithWalls(IBall ball, Vector2 screenSize)
        {
            if (ball.position.X <= 0 || ball.position.X + ball.ballD + 0 >= screenSize.X)
            {
                ball.direction = new Vector2(-ball.direction.X, ball.direction.Y);
            }

            if (ball.position.Y <= 0 || ball.position.Y + ball.ballD + 0 >= screenSize.Y)
            {
                ball.direction = new Vector2(ball.direction.X, -ball.direction.Y);
            }
        }

        public static void HandleBallsCollision(IBall ball1, IBall ball2)
        {
            //wzorki na odbicie i zmiane vectora ruchu przeskalowanego o mase
        }
    }
}
