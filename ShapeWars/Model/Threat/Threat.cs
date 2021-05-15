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
using ShapesAndMirrors.Model.Paths;
using System;

namespace ShapesAndMirrors.Model.Threat
{
    public abstract class Threat : Item
    { 
        public Movement Movement { get; set; } = new Movement();
        public Speed Speed { get; set; } = new Speed();

        public Threat(Position startPosition, bool canbeDestroyed = true)
        {
            Position = Path.Position = startPosition;
            CanBeDestroyed = canbeDestroyed;
        }

        public Threat(Position startPosition, Path path,  bool canbeDestroyed = true)
        {
            Path = path;
            Position = Path.Position = startPosition;
            CanBeDestroyed = canbeDestroyed;
        }

        /// <summary>
        /// Threat takes a Step
        /// </summary>
        public override void Step()
        {
            try 
            {
                if (Path.NewPathRequired())
                {
                    Path.CalculateNextPath(Speed.CurrentSpeed);
                }
                if (WorldBoundaryCollision(Path.GetCurrentPosition()))
                {
                    if (this is Bullet.Bullet == false)
                    {
                        Path.ReversePath();
                        if (Path.NewPathRequired())
                        {
                            Path.CalculateNextPath(Speed.CurrentSpeed);
                        }
                    }
                }
                Position = Path.m_tmpPositions[Path.m_positionsCurrentIndex];
            }
            catch(Exception ex)
            {
                 Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Threat's should override this with their firing code
        /// </summary>
        public virtual void FireThread() {}
    }
}