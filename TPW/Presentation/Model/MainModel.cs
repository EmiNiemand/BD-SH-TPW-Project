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

        private Vector2 _screenSize = new Vector2(800, 500);
        private LogicAbstractApi _logic;
        private int _ballsNumber;

        public MainModel()
        {
            _logic = LogicAbstractApi.CreateApi(_screenSize);
        }
        internal void StartSimulation(int ballsNumber)
        {
            _logic.ClearBalls();
            _logic.CreateBalls(ballsNumber);
            _logic.MoveBalls();
        }

        internal ObservableCollection<Ball> GetBalls()
        {
            return _logic.GetBalls();
        }

        internal void SetBallsNumber(int ballsNumber)
        {
            _ballsNumber = ballsNumber;
        }

        internal int GetBallsNumber()
        {
            return _ballsNumber;
        }
    }
}
