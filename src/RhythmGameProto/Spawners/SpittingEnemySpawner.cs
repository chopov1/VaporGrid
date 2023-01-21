using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class SpittingEnemySpawner : EnemySpawner
    {
        public SpittingEnemySpawner(Game game, GridManager gm, RhythmManager rm, Camera camera, Player p, int numberOfObjects) : base(game, gm, rm, camera, p, numberOfObjects)
        {
        }

        public override GridSprite createSpawnableObject()
        {
            SpittingPathfinderEnemy e = new SpittingPathfinderEnemy(Game, gridManager, rhythmManager, camera, player);
            e.Enabled = false;
            e.Visible = false;
            return e;
        }

        public override void ResetObjects()
        {
            foreach (GridSprite s in objects)
            {
                DeSpawn(s);
            }
            foreach(SpittingPathfinderEnemy p in objects)
            {
                p.ResetProjectiles();
            }
        }
    }
}
