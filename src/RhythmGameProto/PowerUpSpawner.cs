using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class PowerUpSpawner : Spawner
    {
        public PowerUpSpawner(Game game, GridManager gm, RhythmManager rm, Player p, Camera camera) : base(game, gm, rm, p, camera)
        {

        }

        public override GridSprite createSpawnableObject()
        {
            PowerUp p = new PowerUp(Game, gridManager, "PowerUp", camera, player, this);
            p.Enabled = false;
            p.Visible = false;
            return p;
        }
    }
}
