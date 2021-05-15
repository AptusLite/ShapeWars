//   AptusLite - Shape Wars
//   Copyright(C) 2021 - Brendan Price 
//
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with this program. If not, see<https://www.gnu.org/licenses/>.
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShapesAndMirrors.Model.Threat.Shape
{
    public class Rectangle : Shape
    {
        public int RateOfFireInMilliSeconds { get; set; } = 2000;
        public Action<Threat> AddThreatToCollection;

        public Rectangle(Action<Threat> t)
        {
            AddThreatToCollection = t;
            Speed.CurrentSpeed = 2;
            Colour = "Yellow";
            AddThreatToCollection  = t;
            FireThread();
        }

        public override void FireThread()
        {
                Task.Run(() =>
                {
                    while (!IsDead() && ExistsInGameScreen)
                    {
                        Bullet.Bullet bullet = new Bullet.Bullet(Position, new RandomStraightPath());
                        App.Current.Dispatcher.Invoke(() => AddThreatToCollection.Invoke(bullet));
                        Thread.Sleep(RateOfFireInMilliSeconds);
                    }
                });
        }
    }
}