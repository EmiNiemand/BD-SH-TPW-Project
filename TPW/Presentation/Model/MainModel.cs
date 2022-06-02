using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;
using Logic;

namespace Presentation.Model
{
    public class MainModel
    {
        private Vector2 _screenSize;
        private LogicAbstractApi _logic;
        public event EventHandler<ModelBallEventArgs> BallMoved;
        private int _ballNumber;

        public MainModel(LogicAbstractApi logic = default(LogicAbstractApi))
        {
            _screenSize = new Vector2(800, 500);
            if (logic == null)
            {
                logic = LogicAbstractApi.CreateApi(_screenSize);
            }
            this._logic = logic;
            logic.BallMoved += (sender, argv) =>
            {
                ModelBallEventArgs args = new ModelBallEventArgs(argv.Ball.id, argv.Ball.position, argv.Ball.ballD);
                BallMoved?.Invoke(this, args);
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
            _logic.CreateBalls(_ballNumber);
            _logic.StartSimulation();
        }

        public Vector2 GetScreenSize()
        {
            return _screenSize;
        }

        public void OnBallMoved(ModelBallEventArgs args)
        {
            BallMoved?.Invoke(this, args);
        }
    }
}
