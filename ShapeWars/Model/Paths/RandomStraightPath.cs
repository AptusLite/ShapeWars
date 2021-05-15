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
    public class RandomStraightPath : Path
    {
        public enum DIRECTION { LEFT, LEFT_UP, UP, RIGHT_UP, RIGHT, RIGHT_DOWN, DOWN, LEFT_DOWN };
        public DIRECTION m_direction;


        public RandomStraightPath()
        {
            m_direction = (DIRECTION) rand.Next(0, 8);
        }

        public RandomStraightPath(DIRECTION direction)
        {
            m_direction = direction;
        }

        public override void CalculateNextPath(int speed)
        {
            m_tmpPositions.Clear();
            m_positionsCurrentIndex = m_tmpPositions.Count;

            const int StepsInPath = 500; //Just get it to go out the boundary.

            m_tmpPositions.Add(new Position(Position.X, Position.Y));
            Position tmpPosition = m_tmpPositions[0];
            switch (m_direction)
            {
                case DIRECTION.RIGHT:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X + speed, tmpPosition.Y);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                case DIRECTION.LEFT:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X - speed, tmpPosition.Y);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                case DIRECTION.DOWN:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X, tmpPosition.Y + speed);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                case DIRECTION.UP:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X, tmpPosition.Y - speed);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                case DIRECTION.LEFT_UP:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X - speed, tmpPosition.Y - speed);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                case DIRECTION.RIGHT_DOWN:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X + speed, tmpPosition.Y + speed);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                case DIRECTION.RIGHT_UP:
                    {
                        for (int i = 0; i < StepsInPath; i++)
                        {
                            Position p = new Position(tmpPosition.X + speed, tmpPosition.Y - speed);
                            AddPositionToPath(p);
                            tmpPosition = p;
                        }
                        break;
                    }
                case DIRECTION.LEFT_DOWN:
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