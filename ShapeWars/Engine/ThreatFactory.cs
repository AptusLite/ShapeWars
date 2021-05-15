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
using ShapesAndMirrors.Model.Threat;
using ShapesAndMirrors.Model.Threat.Shape;
using System;

namespace ShapesAndMirrors.Engine
{
    public static class ThreatFactory
    {
        public enum ThreatType { RECTANGLE, PULSE, YIN_AND_YANG };

        public static Threat GetThreat(ThreatType threatType, Action<Threat> action)
        {
            switch(threatType)
            {
                case ThreatType.PULSE:
                    {
                        return new RectanglePulse(action);
                    }
                case ThreatType.YIN_AND_YANG:
                    {
                        return new YinAndYang(action);
                    }
                case ThreatType.RECTANGLE:
                default:
                {
                    return new Rectangle(action);
                }
            }
        }
    }
}