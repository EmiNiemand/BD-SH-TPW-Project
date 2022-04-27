using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;
using Logic;

namespace Presentation.Model
{
    public class BallEventArgs : EventArgs
    {
        public Vector2 position;
        public int id;
        public BallEventArgs(int id, Vector2 position)
        {
            this.id = id;
            this.position = position;
        }
    }

    public class MainModel
    {
        public Vector2 screenSize;
        LogicAbstractApi logic;
        public event EventHandler<BallEventArgs> BallMoved;
        private int _ballNumber;
        public float ballsR;

        public MainModel(LogicAbstractApi logic = default(LogicAbstractApi))
        {
            ballsR = 10;
            screenSize = new Vector2(800, 500);
            if (logic == null)
            {
                logic = LogicAbstractApi.CreateApi(screenSize);
            }
            this.logic = logic;
            logic.BallMoved += (sender, args) =>
            {
                BallMoved?.Invoke(this, new BallEventArgs(args.id, args.position));
            };
        }

        public int GetBallsNumber()
        {
            return _ballNumber;
        }

        public void SetBallsNumber(int number)
        {
            _ballNumber = number;
        }

        public void StartSimulation()
        {
            logic.CreateBalls(_ballNumber, ballsR);
            logic.MoveBalls(33);
        }
    }
}
