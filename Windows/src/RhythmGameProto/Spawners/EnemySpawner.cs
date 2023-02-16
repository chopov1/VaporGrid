using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{

    public class EnemySpawner : Spawner
    {

        public EnemySpawner(Game game, GridManager gm, RhythmManager rm,  Camera camera, Player p, int numberOfObjects) : base(game, gm, rm,  camera, p, numberOfObjects)
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
