using NUnit.Framework;
using Logic;
using Data;
using System.Numerics;
using System.Collections.Generic;

namespace LogicTests
{
    public class CollisionHandlerTest
    {
        DataApi dataApi;
        LogicApi logic;

        [SetUp]
        public void Setup()
        {
            dataApi = new DataApi(new Vector2(800, 500));
            logic = new LogicApi(dataApi);
            logic.CreateBalls(3);

        }

        [Test]
        public void SimulationTest() {
            logic.StartSimulation();
        }
    }
}