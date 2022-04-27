using NUnit.Framework;
using Logic;
using System;
using System.Numerics;
using System.Collections.Generic;

namespace LogicTests
{
    public class LogicAbstractApiTest
    {
        LogicAbstractApi logic;

        [SetUp]
        public void Setup()
        {
            logic = LogicAbstractApi.CreateApi(new System.Numerics.Vector2(800, 500));
            logic.CreateBalls(3, 3);
        }

        [Test]
        public void CreateApiTest()
        {
            Assert.IsNotNull(logic);
        }

        [Test]
        public void CreateAndGetBallsTest()
        {
            Assert.AreEqual(logic.GetBalls().Count, 3);
        }

        [Test]
        public void MoveBallTest()
        {
            Vector2 ball = logic.GetBalls()[0].GetPosition();
            logic.MoveBall(logic.GetBalls()[0]);
            Assert.AreNotEqual(ball, logic.GetBalls()[0].GetPosition());
            foreach (var bal in logic.GetBalls())
            {
                Assert.AreNotEqual(bal.GetPosition(), ball);
                Assert.Greater(bal.GetPosition().X, 0);
                Assert.Greater(bal.GetPosition().Y, 0);
                Assert.Greater(logic.screenSize.X, bal.GetPosition().X);
                Assert.Greater(logic.screenSize.Y, bal.GetPosition().Y);
            }
        }

        [Test]
        public void ClearBallsTest()
        {
            Assert.IsNotNull(logic);
            logic.ClearBalls();
            Assert.AreEqual(logic.GetBalls().Count, 0);
        }
    }
}