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
using ShapesAndMirrors.Model.Paths;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShapesAndMirrors.Model
{
    public abstract class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Diamater { get; set; } = 20;

        //Current Position 
        public Position Position { get; set; } = new Position();

        public Path Path { get; set; } = new RandomChangeStraightPath();

        public Health Health { get; set; } = new Health();

        public bool ExistsInGameScreen { get; set; } = true;

        //Some threats cannot be destroyed
        public bool CanBeDestroyed { get; set; } = true;

        private string _colour = "Black";
        public string Colour 
        { 
            get
            {
                return _colour;
            }
            set
            {
                if(_colour != value)
                {
                    _colour = value;
                    NotifyPropertyChanged("Colour");
                }
                
            }
        } 

        public bool IsDead()
        {
            if(CanBeDestroyed)
            {
                return Health.Current <= 0 ? true : false;
            }
            return false; 
        }

        public abstract void Step();

        public void UpdatePositionsFromTemp()
        {
            try
            {
                Path.UpdatePositionsFromTemporaryPositions();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            NotifyPropertyChanged("Position");
        }
        
        public static bool HasCollided(Item item1, Item item2)
        {
            if (item1.Position.X < item2.Position.X + item2.Diamater &&
                   item1.Position.X + item1.Diamater > item2.Position.X &&
                   item1.Position.Y < item2.Position.Y + item2.Diamater &&
                   item1.Position.Y + item1.Diamater > item2.Position.Y)
            {
                return true;
            }
            return false;
        }

        public void UpdatePosition()
        {
            Position = Path.m_tmpPositions[Path.m_positionsCurrentIndex];
        }

        public virtual bool WorldBoundaryCollision(Position position)
        {
            if (position.X < 0 || position.Y < 0 || position.X + Diamater > GameWorld.Width || position.Y + Diamater > GameWorld.Height)
            {
                return true;
            }
            return false;
        }

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}