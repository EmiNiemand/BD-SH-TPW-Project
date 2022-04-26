using NUnit.Framework;
using Data;

namespace DataTests
{
    public class DataAbstractApiTest
    {
        DataAbstractApi data;

        [SetUp]
        public void Setup()
        {
            data = DataAbstractApi.CreateApi();
        }

        [Test]
        public void CreateBallTest()
        {
            IBall ball1 = data.CreateBall(new System.Numerics.Vector2(2, 3), 5);
            Assert.IsNotNull(ball1);
            IBall ball2 = data.CreateBall(new System.Numerics.Vector2(2, 3), 5);
            Assert.AreEqual(ball1.GetPosition(), ball2.GetPosition());
            Assert.AreEqual(ball1.GetRadius(), ball2.GetRadius());
            IBall ball3 = data.CreateBall(new System.Numerics.Vector2(4, 8), 10);
            Assert.AreNotEqual(ball1.GetPosition(), ball3.GetPosition());
            Assert.AreNotEqual(ball1.GetRadius(), ball3.GetRadius());
        }
    }
}