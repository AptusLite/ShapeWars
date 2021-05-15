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

using ShapesAndMirrors.Commands;
using ShapesAndMirrors.Engine;
using ShapesAndMirrors.Model.Threat.Bullet;
using System;
using System.Windows.Input;
using static ShapesAndMirrors.Model.RandomStraightPath;

namespace ShapesAndMirrors.Model.Player
{
    public class Player : Item
    {
        private const int MOVEMENT_SPEED = 10;
        public DIRECTION m_direction = DIRECTION.LEFT;
        public int m_directionIndex = 0; 
        public Action<Item> AddThreatToCollection;

        public Player(Action<Item> t)
        {
            Position = new Position(GameWorld.Width / 2, GameWorld.Height / 2 + 200);
            Health.Current = Health.MAX_HEALTH;
            Diamater = 30;
            Colour = "Lime";
            AddThreatToCollection = t;
            UpdateAimLine();
        }

        //Actions
        private ICommand _leftCmd = null;
        public ICommand LeftCommand
        {
            get
            {
                if (_leftCmd == null)
                {
                    _leftCmd = new RelayCommand(
                        p => Left(),
                        p => CanLeft());
                }
                return _leftCmd;
            }
        }

        //Actions
        private ICommand _rightCmd = null;
        public ICommand RightCommand
        {
            get
            {
                if (_rightCmd == null)
                {
                    _rightCmd = new RelayCommand(
                        p => Right(),
                        p => CanRight());
                }
                return _rightCmd;
            }
        }

        private ICommand _upCmd = null;
        public ICommand UpCommand
        {
            get
            {
                if (_upCmd == null)
                {
                    _upCmd = new RelayCommand(
                        p => Up(),
                        p => CanUp());
                }
                return _upCmd;
            }
        }

        private ICommand _downCmd = null;
        public ICommand DownCommand
        {
            get
            {
                if (_downCmd == null)
                {
                    _downCmd = new RelayCommand(
                        p => Down(),
                        p => CanDown());
                }
                return _downCmd;
            }
        }

        private ICommand _leftClickCmd = null;
        public ICommand LeftClickCommand
        {
            get
            {
                if (_leftClickCmd == null)
                {
                    _leftClickCmd = new RelayCommand(
                        p => LeftClick(),
                        p => true);
                }
                return _leftClickCmd;
            }
        }

        private ICommand _leftAimCmd = null;
        public ICommand LeftAimCommand
        {
            get
            {
                if (_leftAimCmd == null)
                {
                    _leftAimCmd = new RelayCommand(
                        p => LeftAim(),
                        p => true);
                }
                return _leftAimCmd;
            }
        }

        private ICommand _rightAimCmd = null;
        public ICommand RightAimCommand
        {
            get
            {
                if (_rightAimCmd == null)
                {
                    _rightAimCmd = new RelayCommand(
                        p => RightAim(),
                        p => true);
                }
                return _rightAimCmd;
            }
        }

        private bool CanLeft()
        {
            return !WorldBoundaryCollision(new Position(Position.X - MOVEMENT_SPEED, Position.Y));
        }

        private bool CanRight()
        {
            return !WorldBoundaryCollision(new Position(Position.X + MOVEMENT_SPEED, Position.Y));
        }

        private bool CanUp()
        {
            return !WorldBoundaryCollision(new Position(Position.X, Position.Y - MOVEMENT_SPEED));
        }

        private bool CanDown()
        {
            return !WorldBoundaryCollision(new Position(Position.X, Position.Y + MOVEMENT_SPEED));
        }

        private void Left()
        {
            
            Position.X -= MOVEMENT_SPEED;
            NotifyPropertyChanged("Position");
            UpdateAimLine();
        }

        private void Right()
        {
            Position.X += MOVEMENT_SPEED;
            NotifyPropertyChanged("Position");
            UpdateAimLine();
        }

        private void Up()
        {
            Position.Y -= MOVEMENT_SPEED;
            NotifyPropertyChanged("Position");
            UpdateAimLine();
        }

        private void Down()
        {
            Position.Y += MOVEMENT_SPEED;
            NotifyPropertyChanged("Position");
            UpdateAimLine();
        }

        public Position AimFin { get; set; }  = new Position(0, 0);

        private void UpdateAimLine()
        {
            RandomStraightPath aimPath = new RandomStraightPath(m_direction);
            aimPath.Position = Position;
            aimPath.CalculateNextPath(1);
            AimFin = aimPath.m_tmpPositions[50];
            NotifyPropertyChanged("AimFin");
        }

        private ICommand _downAimCmd = null;
        public ICommand DownAimCommand
        {
            get
            {
                if (_downAimCmd == null)
                {
                    _downAimCmd = new RelayCommand(
                        p => DownAim(),
                        p => true);
                }
                return _downAimCmd;
            }
        }

        private ICommand _upAimCmd = null;
        public ICommand UpAimCommand
        {
            get
            {
                if (_upAimCmd == null)
                {
                    _upAimCmd = new RelayCommand(
                        p => UpAim(),
                        p => true);
                }
                return _upAimCmd;
            }
        }

        private void DownAim()
        {
            switch((DIRECTION)m_directionIndex)
            {
                case DIRECTION.DOWN:
                case DIRECTION.LEFT_DOWN:
                case DIRECTION.RIGHT_DOWN:
                case DIRECTION.UP:
                    {
                        m_direction = DIRECTION.DOWN;
                        break;
                    }
                case DIRECTION.LEFT_UP:
                    {
                        m_direction = DIRECTION.LEFT;
                        break;
                    }
                case DIRECTION.LEFT:
                    {
                        m_direction = DIRECTION.LEFT_DOWN;
                        break;
                    }
                case DIRECTION.RIGHT_UP:
                    {
                        m_direction = DIRECTION.RIGHT;
                        break;
                    }
                case DIRECTION.RIGHT:
                    {
                        m_direction = DIRECTION.RIGHT_DOWN;
                        break;
                    }
            }
            m_directionIndex = (int)m_direction;
            UpdateAimLine();
        }

        private void UpAim()
        {
            switch ((DIRECTION)m_directionIndex)
            {
                case DIRECTION.UP:
                case DIRECTION.LEFT_UP:
                case DIRECTION.RIGHT_UP:
                case DIRECTION.DOWN:
                    {
                        m_direction = DIRECTION.UP;
                        break;
                    }
                case DIRECTION.LEFT_DOWN:
                    {
                        m_direction = DIRECTION.LEFT;
                        break;
                    }
                case DIRECTION.LEFT:
                    {
                        m_direction = DIRECTION.LEFT_UP;
                        break;
                    }
                case DIRECTION.RIGHT_DOWN:
                    {
                        m_direction = DIRECTION.RIGHT;
                        break;
                    }
                case DIRECTION.RIGHT:
                    {
                        m_direction = DIRECTION.RIGHT_UP;
                        break;
                    }
            }
            m_directionIndex = (int) m_direction;
            UpdateAimLine();
        }

        private void LeftAim()
        {
            switch ((DIRECTION)m_directionIndex)
            {
                case DIRECTION.DOWN:
                    {
                        m_direction = DIRECTION.LEFT_DOWN;
                        break;
                    }
                case DIRECTION.LEFT_DOWN:
                    {
                        m_direction = DIRECTION.LEFT;
                        break;
                    }
                case DIRECTION.UP:
                    {
                        m_direction = DIRECTION.LEFT_UP;
                        break;
                    }
                case DIRECTION.LEFT_UP:
                    {
                        m_direction = DIRECTION.LEFT;
                        break;
                    }
                case DIRECTION.RIGHT_DOWN:
                    {
                        m_direction = DIRECTION.DOWN;
                        break;
                    }
                case DIRECTION.RIGHT:
                    {
                        m_direction = DIRECTION.RIGHT_DOWN;
                        break;
                    }
                case DIRECTION.RIGHT_UP:
                    {
                        m_direction = DIRECTION.UP;
                        break;
                    }
            }
            m_directionIndex = (int)m_direction;
            UpdateAimLine();
        }

        private void RightAim()
        {
            switch ((DIRECTION)m_directionIndex)
            {
                case DIRECTION.DOWN:
                    {
                        m_direction = DIRECTION.RIGHT_DOWN;
                        break;
                    }
                case DIRECTION.RIGHT_DOWN:
                    {
                        m_direction = DIRECTION.RIGHT;
                        break;
                    }
                case DIRECTION.UP:
                    {
                        m_direction = DIRECTION.RIGHT_UP;
                        break;
                    }
                case DIRECTION.RIGHT_UP:
                    {
                        m_direction = DIRECTION.RIGHT;
                        break;
                    }
                case DIRECTION.LEFT_DOWN:
                    {
                        m_direction = DIRECTION.DOWN;
                        break;
                    }
                case DIRECTION.LEFT:
                    {
                        m_direction = DIRECTION.LEFT_UP;
                        break;
                    }
                case DIRECTION.LEFT_UP:
                    {
                        m_direction = DIRECTION.UP;
                        break;
                    }
            }
            m_directionIndex = (int)m_direction;
            UpdateAimLine();
        }

        private void LeftClick()
        {
            Bullet bullet = new Bullet(Position, new RandomStraightPath(m_direction), true);
            bullet.Speed.CurrentSpeed = 8;
            bullet.Diamater = 10;
            bullet.Colour = "HotPink";
            App.Current.Dispatcher.Invoke(() => AddThreatToCollection.Invoke(bullet));
        }

        public override void Step() { }
    }
}