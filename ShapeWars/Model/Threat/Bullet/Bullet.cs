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

namespace ShapesAndMirrors.Model.Threat.Bullet
{
    public class Bullet : Threat
    {
        public bool PlayerBullet { get; set; } = false;
        /// <summary>
        /// This is a bullet, a kind of Threat that can not be destroyed
        /// </summary>
        /// <param name="BulletMaker">The Threat that created this Bullet</param>
        public Bullet(Position BulletMaker, Path path, bool playerBullet = false) : base(BulletMaker, path, false)
        {
            Colour = "Pink";
            Diamater = 5;
            Speed.CurrentSpeed = 4;
            PlayerBullet = playerBullet;
        }

        public override bool WorldBoundaryCollision(Position position)
        {
            if (position.X <= 0 || position.Y <= 0 || position.X + Diamater >= GameWorld.Width || position.Y + Diamater >= GameWorld.Height)
            {
                ExistsInGameScreen = false;
                return true;
            }
            return false;
        }
    }
}