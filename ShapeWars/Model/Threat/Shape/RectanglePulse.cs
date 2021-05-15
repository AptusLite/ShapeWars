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
    public class RectanglePulse : Shape
    {
        public int RateOfFireInMilliSeconds { get; set; } = 2000;
        public Action<Threat> AddThreatToCollection;

        public RectanglePulse(Action<Threat> t)
        {
            AddThreatToCollection = t;
            Colour = "Purple";
            Diamater = 40;
            Health.Current = 40;
            Speed.CurrentSpeed = 1;
            FireThread();
        }

        public override void FireThread()
        {
            Random rand = new Random();

            Task.Run(() =>
            {
                while (!IsDead() && ExistsInGameScreen)
                {
                    int val = rand.Next(0, 2);
                    Bullet.Bullet bullet, bullet2, bullet3, bullet4;
                    if (val == 0)
                    {
                        bullet = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.LEFT));
                        bullet2 = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.RIGHT));
                        bullet3 = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.UP));
                        bullet4 = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.DOWN));
                    }
                    else
                    {
                        bullet = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.LEFT_UP));
                        bullet2 = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.LEFT_DOWN));
                        bullet3 = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.RIGHT_UP));
                        bullet4 = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.RIGHT_DOWN));
                    }
                    App.Current.Dispatcher.Invoke(() => AddThreatToCollection.Invoke(bullet));
                    App.Current.Dispatcher.Invoke(() => AddThreatToCollection.Invoke(bullet2));
                    App.Current.Dispatcher.Invoke(() => AddThreatToCollection.Invoke(bullet3));
                    App.Current.Dispatcher.Invoke(() => AddThreatToCollection.Invoke(bullet4));
                    Thread.Sleep(RateOfFireInMilliSeconds);
                }
            });
        }
    }
}