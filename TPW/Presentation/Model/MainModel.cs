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
        private Vector2 _screenSize;
        private LogicAbstractApi _logic;
        public event EventHandler<BallEventArgs> BallMoved;
        private int _ballNumber;
        private float _ballsR;

        public MainModel(LogicAbstractApi logic = default(LogicAbstractApi))
        {
            _ballsR = 10;
            _screenSize = new Vector2(800, 500);
            if (logic == null)
            {
                logic = LogicAbstractApi.CreateApi(_screenSize);
            }
            this._logic = logic;
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
            _logic.CreateBalls(_ballNumber, _ballsR);
            _logic.MoveBalls(33);
        }

        public Vector2 GetScreenSize()
        {
            return _screenSize;
        }

        public float GetBallsR()
        {
            return _ballsR;
        }
    }
}
