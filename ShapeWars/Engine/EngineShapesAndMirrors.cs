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
using ShapesAndMirrors.Model;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ShapesAndMirrors.Model.Threat.Bullet;
using ShapesAndMirrors.Model.Player;
using System.Windows.Input;
using ShapesAndMirrors.Commands;

namespace ShapesAndMirrors.Engine
{
    public class EngineShapesAndMirrors : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public enum STATE { START, PAUSE, STOP, RESET };                    //Game states 
        public static STATE m_state { get; set; } = STATE.START;            //Default start state
        public string Time { get; set; }
        public string Message { get; set; } = String.Empty;
        public DateTime startTime = DateTime.Now;
        public DateTime startLevelTime = DateTime.Now;

        public GameWorld GameWorld { get; set; } = new GameWorld();

        public EngineShapesAndMirrors() 
        {
            Task.Run(GameLoop);
        }

        private void GameLoop()
        {
            DateTime startFrameTime;
            while (true)
            {
                startFrameTime = DateTime.Now;                              //Time at start of this frame
                switch (m_state)
                {
                    case STATE.START:
                        {
                            try
                            {
                                CalculateNextStep();
                                Time = GetTime(startTime);//Update Timer
                                startLevelTime = NextLevelCalculation(startLevelTime);
                                NotifyPropertyChanged("Time");
                                break;
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                break;
                            }
                        }
                    case STATE.STOP:
                        {
                            break;
                        }
                    case STATE.PAUSE:
                        {
                            break;
                        }
                    case STATE.RESET:
                        {
                            break;
                        }
                }
                FrameSleep(startFrameTime); //Sleep between frames.
            }
        }

        /// <summary>
        /// Frame should last TimeBetweenGenerationsInMilliseconds in total.
        /// </summary>
        /// <param name="startFrameTime"></param>
        private void FrameSleep(DateTime startFrameTime)
        {
            //todo check to make this 60 fps
            int timeRunning = Convert.ToInt32(DateTime.Now.Subtract(startFrameTime).TotalMilliseconds);
            timeRunning = 16 - timeRunning < 0 ? 0 : 16 - timeRunning;
            Console.WriteLine(timeRunning);
            Thread.Sleep(timeRunning);
        }

        /// <summary>
        /// Calculate next step for each threat.
        /// </summary>
        private void CalculateNextStep()
        {
            try
            {




                //Detect Collision - Between Player's bullet and Threats
                foreach (var playerBullet in GameWorld.Items.Where(t => t is Bullet && ((Bullet)t).PlayerBullet).ToList())
                {
                    foreach (var threat in GameWorld.Items.Where(t => !(t is Bullet) && !(t is Player)).ToList())
                    {
                        if (threat.CanBeDestroyed && Item.HasCollided(playerBullet, threat))
                        {
                            playerBullet.ExistsInGameScreen = false;
                            threat.Health.Current -= 10;
                        }
                    }
                }

                //Detect Collision - Between Player and Threat's bullets.
                foreach (var threatBullet in GameWorld.Items.Where(t => t is Bullet && ((Bullet)t).PlayerBullet == false).ToList())
                {
                    if (threatBullet.ExistsInGameScreen && Item.HasCollided(threatBullet, GameWorld.Player))
                    {
                        threatBullet.ExistsInGameScreen = false;
                        GameWorld.Player.Health.Current -= 10;
                    }
                }

                //Remove any threats if they are dead or no longer exist in game screen (i.e. a bullet)
                foreach (var threat in GameWorld.Items.Where(t => t.IsDead() || !t.ExistsInGameScreen).ToList())
                {
                    App.Current.Dispatcher.Invoke(() => GameWorld.Items.Remove(threat));
                    if(threat is Player)
                    {
                        PlayerDied();
                    }
                }

                //Check if Player has Won the game by killing special Spawner Enemy
                if (GameWorld.Spawner.IsDead())
                {
                    PlayerWon();
                }

                Parallel.ForEach(GameWorld.Items, t =>
                {
                    t.Step();
                });
                Parallel.ForEach(GameWorld.Items, t =>
                {
                    t.UpdatePositionsFromTemp();
                });

                foreach (Item t in GameWorld.Items)
                {
                    t.Step();
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
            }
        }

        private DateTime NextLevelCalculation(DateTime startLevelTime )
        {
            if((DateTime.Now - startLevelTime).Seconds > 40)
            {
                GameWorld.StartNextLevel();
                return DateTime.Now;
            }
            return startLevelTime;
        }

        private void PlayerDied()
        {
            m_state = STATE.PAUSE;
            Message = "You Died";
            NotifyPropertyChanged("Message");
        }

        private void PlayerAlive()
        {
            Message = String.Empty;
            NotifyPropertyChanged("Message");
        }

        private void PlayerWon()
        {
            m_state = STATE.PAUSE;
            Message = "You are Winner!";
            NotifyPropertyChanged("Message");
        }

        private string GetTime(DateTime fromDt)
        {
            return string.Format(@"{0:mm\:ss}", DateTime.Now - fromDt);
        }

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Actions
        private ICommand _exitCmd = null;
        public ICommand ExitCommand
        {
            get
            {
                if (_exitCmd == null)
                {
                    _exitCmd = new RelayCommand(
                        p => Exit(),
                        p => true);
                }
                return _exitCmd;
            }
        }

        private void Exit()
        {
            if(m_state == STATE.START)
            {
                System.Windows.Application.Current.Shutdown();
            }
            else if(m_state == STATE.STOP)
            {
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                if(m_state == STATE.PAUSE && (Message.Equals("You Died") || Message.Equals("You are Winner!")))
                {
                    m_state = STATE.START;
                    GameWorld.ResetLevels();
                    GameWorld.StartNextLevel();
                    PlayerAlive(); 
                    startLevelTime = startTime = DateTime.Now;
                }
                else
                {
                    m_state = STATE.START;
                }
            }
        }
    }
}