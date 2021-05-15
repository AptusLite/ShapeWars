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
using System.Collections.Generic;

namespace ShapesAndMirrors.Model.Paths
{
    public abstract class Path
    {
        protected Random rand = new Random();

        //Current Position 
        public Position Position { get; set; } = new Position();
        public volatile int m_positionsCurrentIndex = 0;

        //Predicted positions (constructed path of position, with m_positionsCurrentIndex iterating through them)
        public volatile List<Position> m_positions = new List<Position>();
        public volatile List<Position> m_tmpPositions = new List<Position>();

        public abstract void CalculateNextPath(int speed);

        public bool NewPathRequired()
        {
            return m_positionsCurrentIndex >= m_tmpPositions.Count ? true : false;
        }

        public Position GetCurrentPosition()
        {
            return Position = m_tmpPositions[m_positionsCurrentIndex];
        }

        public void ReversePath()
        {
            m_positionsCurrentIndex = m_tmpPositions.Count - m_positionsCurrentIndex + 1;
            m_tmpPositions.Reverse();   //Collision
        }

        public void UpdatePositionsFromTemporaryPositions()
        {
            ++m_positionsCurrentIndex;
        }
        public void AddPositionToPath(Position p)
        {
            m_tmpPositions.Add(p);
        }
    }
}