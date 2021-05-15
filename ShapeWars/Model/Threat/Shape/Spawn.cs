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
using ShapesAndMirrors.Engine;
using System.Threading.Tasks;
using System.Threading;

namespace ShapesAndMirrors.Model
{
    public class Spawn : Threat.Threat
    {
        public Spawn() : base(new Position(GameWorld.Width / 2 - (50 / 2), GameWorld.Height / 3 - (50 / 2)), false)
        {
            Diamater = 50;
            CanBeDestroyed = false;
            Health.Current = 100;

            Task.Run(() =>
            {
                while(!IsDead())
                {
                    Thread.Sleep(500);
                    Colour = "MidnightBlue";
                    if(!CanBeDestroyed)
                    {
                        Thread.Sleep(500);
                        Colour = "Black";
                    }
                }
            });
        }
    }
}
