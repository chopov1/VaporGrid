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

        public EnemySpawner(Game game, GridManager gm, RhythmManager rm, Player p, Camera camera, int numberOfObjects) : base(game, gm, rm, p, camera, numberOfObjects)
        {


        }


        public override GridSprite createSpawnableObject()
        {
            Enemy e = new Enemy(Game, gridManager, rhythmManager, player, camera);
            e.Enabled = false;
            e.Visible = false;
            return e;
        }

    }
}
