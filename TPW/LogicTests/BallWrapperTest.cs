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
            logic.CreateBalls(1, 10);
            ballWrapper = logic.GetBalls()[0];
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
            Assert.IsNotNull(ballWrapper.GetPosition());
        }

        [Test]
        public void GetRadiusTest()
        {
            Assert.AreEqual(ballWrapper.GetRadius(), 10);
        }
    }
}