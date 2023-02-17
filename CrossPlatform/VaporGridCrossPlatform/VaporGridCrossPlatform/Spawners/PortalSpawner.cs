using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaporGridCrossPlatform.Objects;

namespace VaporGridCrossPlatform
{
    public class PortalSpawner : Spawner
    {
        Vector2 minPortalDist;
        public PortalSpawner(Game game, GridManager gm, RhythmManager rm, Camera camera, Player p, int numberOfObjects) : base(game, gm, rm, camera, p, numberOfObjects)
        {
            setDistance();
        }

        protected void setDistance()
        {
            minPortalDist = new Vector2(gridManager.WalkableRange.X / 2, gridManager.WalkableRange.Y / 2);
            minPortalDist -= Vector2.One;
        }

        public override GridSprite createSpawnableObject()
        {
            Portal p = new Portal(Game, gridManager, "Portal-Sheet", camera, player, this);
            p.Visible = false;
            p.Enabled = false;
            return p;
        }

        private bool isAwayFromFirstPortal(Vector2 pos1, Vector2 pos2)
        {
            setDistance();
            if (pos1 == pos2)
            {
                return false;
            }
            if(distance(pos1, pos2) < distance(pos1, pos1+ minPortalDist))
            {
                return false;
            }
            return true;
        }


        private Double distance(Vector2 pos1, Vector2 pos2)
        {
            double d = Math.Abs(Math.Sqrt(Math.Pow(pos1.X - pos2.X, 2) + Math.Pow(pos1.Y - pos2.Y,2)));
            return d;
        }

        Vector2 temp2;
        protected override void spawnObjectInRandomPos()
        {
            if (checkSpawnBuffer() && objects.Count > 0)
            {
                if (objects.Peek().Enabled == false)
                {
                    Portal objToSpawn = (Portal)objects.Dequeue();
                    objects.Enqueue(objToSpawn);
                    temp = getRandomPos();
                    temp2 = getRandomPos();
                    while (!canSpawn(temp))
                    {
                        temp = getRandomPos();
                    }
                    while(!isValidSecondPos(temp, temp2))
                    {
                        temp2 = getRandomPos();
                    }
                    objToSpawn.SetPositions(temp, temp2);
                    objToSpawn.SpriteState = SpriteState.Active;
                    objToSpawn.Enabled = true;
                    objToSpawn.Visible = true;
                }
            }
        }

        private bool isValidSecondPos(Vector2 temp, Vector2 temp2)
        {
            if(canSpawn(temp2) && isAwayFromFirstPortal(temp, temp2))
            {
                return true;
            }
            return false;
        }

    }
}
