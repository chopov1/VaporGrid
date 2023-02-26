using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform
{

    public class EnemySpawner : Spawner
    {

        public EnemySpawner(Game game, GridManager gm, RhythmManager rm,  Camera camera, Player p, int numberOfObjects, int spawnBuffer) : base(game, gm, rm,  camera, p, numberOfObjects, spawnBuffer)
        {


        }


        public override GridSprite createSpawnableObject()
        {
            PathfindingEnemy e = new PathfindingEnemy(Game, gridManager, rhythmManager, camera, player);
            e.Enabled = false;
            e.Visible = false;
            return e;
        }

    }
}
