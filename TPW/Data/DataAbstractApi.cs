using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract IBall CreateBall(Vector2 position, float ballR);
        public static DataAbstractApi CreateApi()
        {
            return new DataApi();
        }
    }

    public class DataApi : DataAbstractApi
    {
        public DataApi()
        {
        }

        public override IBall CreateBall(Vector2 position, float ballR)
        {
            return new Ball(position, ballR);
        }
    }
}
