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

namespace ShapesAndMirrors.Model
{
    public class RandomChangeStraightPath : Path
    {
        public override void CalculateNextPath(int speed)
        {
            m_tmpPositions.Clear();
            m_positionsCurrentIndex = m_tmpPositions.Count;

            const int StepsInPath = 100;

            m_tmpPositions.Add(new Position(Position.X, Position.Y));
            Position tmpPosition = m_tmpPositions[0];
            switch (rand.Next(1, 9))
            {
                case 1:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X + speed, tmpPosition.Y);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X - speed, tmpPosition.Y);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                case 3:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X, tmpPosition.Y + speed);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                case 4:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X, tmpPosition.Y - speed);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                case 5:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X - speed, tmpPosition.Y - speed);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                case 6:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X + speed, tmpPosition.Y - speed);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                case 7:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X + speed, tmpPosition.Y + speed);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                case 8:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X - speed, tmpPosition.Y + speed);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }

            m_positions = m_tmpPositions;
        }
    }
}