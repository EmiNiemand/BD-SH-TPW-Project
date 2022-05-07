using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Presentation.Model
{
    public class ModelBallEventArgs : EventArgs
    {
        public int id;
        public Vector2 position;
        public float ballD;
        public ModelBallEventArgs(int id, Vector2 position, float ballD)
        {
            this.id = id;
            this.position = position;
            this.ballD = ballD;
        }
    }
}
