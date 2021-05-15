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
using ShapesAndMirrors.Model.Player;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace ShapesAndMirrors.Engine
{
    public class GameWorld
    {
        public static int Height { get; set; } = 800;
        public static int Width { get; set; } = 800;

        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

        public Action<Item> AddItemAction;

        public Player Player { get; set; }

        public static Spawn Spawner { get; set; } = new Spawn();

        public enum LEVEL { ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN};
        public volatile LEVEL m_level;
        public int LevelIndex { get; set; } = -1;

        public GameWorld()
        {
            AddItemAction = new Action<Item>(AddItem);
            Player = new Player(AddItemAction);
            Items.Add(Player);
            Items.Add(Spawner);
            StartNextLevel();
        }

        public void ResetLevels()
        {
            LevelIndex = -1;
            Parallel.ForEach(Items, t =>
            {
                t.ExistsInGameScreen = false;
            });
            Items.Clear();
            Player.Health.Current = Health.MAX_HEALTH;
            Player.ExistsInGameScreen = true;
            Spawner = new Spawn();
            Items.Add(Player);
            Items.Add(Spawner);
        }

        public void StartNextLevel()
        {
            LevelIndex++;
            m_level = (LEVEL)LevelIndex;
            StartLevel(m_level);
        }

        private void StartLevel(LEVEL lvl)
        {
            switch(lvl)
            {
                case LEVEL.ONE:
                    {
                        Task.Run(() => Level1());
                        break;
                    }
                case LEVEL.TWO:
                    {
                        Task.Run(() => Level2());
                        break;
                    }
                case LEVEL.THREE:
                    {
                        Task.Run(() => Level3());
                        break;
                    }
                case LEVEL.FOUR:
                    {
                        Task.Run(() => Level4());
                        break;
                    }
                case LEVEL.FIVE:
                    {
                        Task.Run(() => Level5());
                        break;
                    }
                case LEVEL.SIX:
                    {
                        Task.Run(() => Level6());
                        break;
                    }
                case LEVEL.SEVEN:
                    {
                        Task.Run(() => Level7());
                        break;
                    }
                default:
                    {
                        Spawner.CanBeDestroyed = true;
                        break;
                    }
            }
        }

        public void Level1()
        {

            while(m_level == LEVEL.ONE)
            {
                for (int i = 0; i < 2; i++)
                {
                    AddItem(ThreatFactory.GetThreat(ThreatFactory.ThreatType.RECTANGLE, AddItemAction));
                }

                Thread.Sleep(8000);
            }
        }

        public void Level2()
        {
            Player.Health.SetHealth(Health.MAX_HEALTH);

            while (m_level == LEVEL.TWO)
            {
                for (int i = 0; i < 5; i++)
                {
                    AddItem(ThreatFactory.GetThreat(ThreatFactory.ThreatType.RECTANGLE, AddItemAction));
                }
                Thread.Sleep(14000);
            }
        }

        public void Level3()
        {
            Player.Health.SetHealth(Health.MAX_HEALTH);

            while (m_level == LEVEL.THREE)
            {
                for (int i = 0; i < 2; i++)
                {
                    AddItem(ThreatFactory.GetThreat(ThreatFactory.ThreatType.RECTANGLE, AddItemAction));
                }
                for (int i = 0; i < 1; i++)
                {
                    AddItem(ThreatFactory.GetThreat(ThreatFactory.ThreatType.PULSE, AddItemAction));
                }
                Thread.Sleep(15000);
            }
        }

        public void Level4()
        {
            Player.Health.SetHealth(Health.MAX_HEALTH);

            while (m_level == LEVEL.FOUR)
            {
                for (int i = 0; i < 2; i++)
                {
                    AddItem(ThreatFactory.GetThreat(ThreatFactory.ThreatType.RECTANGLE, AddItemAction));
                }
                for (int i = 0; i < 2; i++)
                {
                    AddItem(ThreatFactory.GetThreat(ThreatFactory.ThreatType.PULSE, AddItemAction));
                }
                Thread.Sleep(12000);
            }
        }

        public void Level5()
        {
            Player.Health.SetHealth(Health.MAX_HEALTH);

            while (m_level == LEVEL.FIVE)
            {
                for (int i = 0; i < 2; i++)
                {
                    AddItem(ThreatFactory.GetThreat(ThreatFactory.ThreatType.RECTANGLE, AddItemAction));
                }
                for (int i = 0; i < 1; i++)
                {
                    AddItem(ThreatFactory.GetThreat(ThreatFactory.ThreatType.YIN_AND_YANG, AddItemAction));
                }
                Thread.Sleep(12000);
            }
        }

        public void Level6()
        {
            Player.Health.SetHealth(Health.MAX_HEALTH);

            while (m_level == LEVEL.SIX)
            {
                for (int i = 0; i < 5; i++)
                {
                    AddItem(ThreatFactory.GetThreat(ThreatFactory.ThreatType.RECTANGLE, AddItemAction));
                }
                for (int i = 0; i < 1; i++)
                {
                    AddItem(ThreatFactory.GetThreat(ThreatFactory.ThreatType.YIN_AND_YANG, AddItemAction));
                }
                Thread.Sleep(15000);
            }
        }

        public void Level7()
        {
            Player.Health.SetHealth(Health.MAX_HEALTH);

            while (m_level == LEVEL.SEVEN)
            {
                for (int i = 0; i < 2; i++)
                {
                    AddItem(ThreatFactory.GetThreat(ThreatFactory.ThreatType.YIN_AND_YANG, AddItemAction));
                }
                Thread.Sleep(15000);
            }
        }


        public void AddItem(Item t)
        {
            App.Current.Dispatcher.Invoke(() => Items.Add(t));
        }
    }
}