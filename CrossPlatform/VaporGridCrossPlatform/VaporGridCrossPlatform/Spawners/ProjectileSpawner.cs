using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform
{
    public class ProjectileSpawner : Spawner
    {
        public ProjectileSpawner(Game game, GridManager gm, RhythmManager rm, Camera camera, Player p, int numberOfObjects, int spawnBuffer) : base(game, gm, rm, camera, p, numberOfObjects, spawnBuffer)
        {
            spawnState = SpawnState.manualSpawner;
        }

        public override GridSprite createSpawnableObject()
        {
            Projectile p = new Projectile(Game, gridManager, "ShitFire", rhythmManager,camera, player);
            p.Enabled = false;
            p.Visible = false;
            return p;
        }


    }
}
