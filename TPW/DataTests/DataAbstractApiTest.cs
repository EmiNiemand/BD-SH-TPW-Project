using NUnit.Framework;
using Data;
using System.Collections.Generic;

namespace DataTests
{
    internal class BallsEventArgsTest
    {
        private readonly IBall ball;
        private readonly IList<IBall> balls;
        [Test]
        public void Setup()
        {
            BallsEventArgs test = new BallsEventArgs(ball, balls);
        }
    }

    internal class DataApiTest
    {
        DataApi dataApi;
        [SetUp]
        public void Setup()
        {
            dataApi = new DataApi(new System.Numerics.Vector2(900, 900));
            dataApi.CreateBalls(10);
        }

        [Test]
        public void GetBallsTest()
        {
            Assert.AreEqual(10, dataApi.GetBalls().Count);
        }   

        [Test]
        public void StartSimulationTest()
        {
            dataApi.StartSimulation();
        }
    }
}