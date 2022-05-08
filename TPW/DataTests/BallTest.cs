using Data;
using NUnit.Framework;
using System.Numerics;

namespace DataTests
{
    internal class BallTest
    {
        Ball ball;

        [SetUp]
        public void Setup()
        {
            ball = new Ball(0, new Vector2(0, 0), 0, 5, new Vector2(1, 1));
        }

        [Test]
        public void StartMovingTest()
        {
            ball.StartMoving();
        }
    }
}
