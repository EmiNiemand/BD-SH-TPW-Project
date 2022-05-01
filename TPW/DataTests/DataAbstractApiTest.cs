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
            
        }
    }
}