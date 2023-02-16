using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform
{
    public class SpittingPathfinderEnemy : PathfindingEnemy
    {
        ProjectileSpawner projSpawner;
        public SpittingPathfinderEnemy(Game game, GridManager gm, RhythmManager rm, Camera camera, Player p) : base(game, gm, rm, camera, p)
        {
            projSpawner = new ProjectileSpawner(game, gm, rm, camera, p, 8);
            Game.Components.Add(projSpawner);
        }

        protected override void moveEnemy()
        {
            base.moveEnemy();
            checkSpitFire();
        }

        public void ResetProjectiles()
        {
            projSpawner.ResetObjects();
        }

        private void checkSpitFire()
        {
            if(player.gridPos.Y == this.gridPos.Y)
            {
                if(player.gridPos.X < this.gridPos.X)
                {
                    spitFire(new Vector2(-1, 0));
                }
                else if(player.gridPos.X > this.gridPos.X)
                {
                    spitFire(new Vector2(1, 0));
                }
            }
            else if(player.gridPos.X == this.gridPos.X)
            {
                if(player.gridPos.Y < gridPos.Y)
                {
                    spitFire(new Vector2(0, -1));
                }
                else if (player.gridPos.Y > gridPos.Y)
                {
                    spitFire(new Vector2(0, 1));
                }
            }
        }

        private bool hasClearPath(Vector2 dir)
        {
            switch (dir)
            {
                case Vector2(0,1):
                    for (int y = (int)gridPos.Y; y < player.gridPos.Y; y++)
                    {
                        if (!gridManager.Grid[(int)gridPos.X, y].IsWalkable)
                        {
                            return false;
                        }
                    }
                    break; 
                case Vector2(0,-1):
                    for (int y = (int)gridPos.Y; y > player.gridPos.Y; y-- )
                    {
                        if (!gridManager.Grid[(int)gridPos.X, y].IsWalkable)
                        {
                            return false;
                        }
                    }
                        break;
                case Vector2(1, 0):
                    for (int x = (int)gridPos.X; x < player.gridPos.X; x++)
                    {
                        if (!gridManager.Grid[x, (int)gridPos.Y].IsWalkable)
                        {
                            return false;
                        }
                    }
                    break;
                case Vector2(-1, 0):
                    for (int x = (int)gridPos.X; x > player.gridPos.X; x--)
                    {
                        if (!gridManager.Grid[x, (int)gridPos.Y].IsWalkable)
                        {
                            return false;
                        }
                    }
                    break;
            }
            return true;
            
        }

        private void spitFire(Vector2 direction)
        {
            if (hasClearPath(direction))
            {
                Projectile fire = (Projectile)projSpawner.SpawnObject(gridPos);
                if (fire != null)
                {
                    fire.Dir = direction;
                }
            }
        }
    }
}
