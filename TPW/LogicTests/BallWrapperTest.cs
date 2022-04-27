using NUnit.Framework;
using Logic;

namespace LogicTests
{
    public class BallWrapperTest
    {
        LogicAbstractApi logic;
        BallWrapper ballWrapper;

        [SetUp]
        public void Setup()
        {
            logic = LogicAbstractApi.CreateApi(new System.Numerics.Vector2(800, 500));
            ballWrapper = new BallWrapper(1, logic.data.CreateBall(new System.Numerics.Vector2(4, 3), 10));
        }

        [Test]
        public void ChangePositionTest()
        {
            ballWrapper.ChangePosition(new System.Numerics.Vector2(69, 20));
            Assert.AreEqual(new System.Numerics.Vector2(69, 20), ballWrapper.GetPosition());
        }

        [Test]
        public void GetPositionTest()
        {
            Assert.AreEqual(ballWrapper.GetPosition(), new System.Numerics.Vector2(4, 3));
        }

        [Test]
        public void GetRadiusTest()
        {
            Assert.AreEqual(ballWrapper.GetRadius(), 10);
        }
    }
}