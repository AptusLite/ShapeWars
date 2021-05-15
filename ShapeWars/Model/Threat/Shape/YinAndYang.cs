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
    public class YinAndYang : Shape
    {
        public int RateOfFireInMilliSeconds { get; set; } = 200;
        public Action<Threat> AddThreatToCollection;

        public YinAndYang(Action<Threat> t)
        {
            AddThreatToCollection = t;
            Colour = "Red";
            Diamater = 40;
            Health.Current = 60;
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
                    Bullet.Bullet bullet, bullet2, bullet3, bullet4, bullet5, bullet6, bullet7, bullet8;

                    bullet = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.UP));
                    App.Current.Dispatcher.Invoke(() => AddThreatToCollection.Invoke(bullet));
                    Thread.Sleep(RateOfFireInMilliSeconds);

                    bullet2 = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.RIGHT_UP));
                    App.Current.Dispatcher.Invoke(() => AddThreatToCollection.Invoke(bullet2));
                    Thread.Sleep(RateOfFireInMilliSeconds);

                    bullet3 = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.RIGHT));
                    App.Current.Dispatcher.Invoke(() => AddThreatToCollection.Invoke(bullet3));
                    Thread.Sleep(RateOfFireInMilliSeconds);

                    bullet4 = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.RIGHT_DOWN));
                    App.Current.Dispatcher.Invoke(() => AddThreatToCollection.Invoke(bullet4));
                    Thread.Sleep(RateOfFireInMilliSeconds);

                    bullet5 = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.DOWN));
                    App.Current.Dispatcher.Invoke(() => AddThreatToCollection.Invoke(bullet5));
                    Thread.Sleep(RateOfFireInMilliSeconds);

                    bullet6 = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.LEFT_DOWN));
                    App.Current.Dispatcher.Invoke(() => AddThreatToCollection.Invoke(bullet6));
                    Thread.Sleep(RateOfFireInMilliSeconds);

                    bullet7 = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.LEFT));
                    App.Current.Dispatcher.Invoke(() => AddThreatToCollection.Invoke(bullet7));
                    Thread.Sleep(RateOfFireInMilliSeconds);

                    bullet8 = new Bullet.Bullet(Position, new RandomStraightPath(RandomStraightPath.DIRECTION.LEFT_UP));
                    App.Current.Dispatcher.Invoke(() => AddThreatToCollection.Invoke(bullet8));
                    Thread.Sleep(RateOfFireInMilliSeconds);

                    Thread.Sleep(RateOfFireInMilliSeconds);
                }
            });
        }
    }
}