using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto.Spawners
{
    public class KeySpawner : Spawner
    {
        public List<Collectable> Keys;
        public KeySpawner(Game game, GridManager gm, RhythmManager rm, Player p, Camera camera, int numberOfObjects) : base(game, gm, rm, p, camera, numberOfObjects)
        {
            Keys = new List<Collectable>();
        }

        public override GridSprite createSpawnableObject()
        {
            Collectable k = new Collectable(Game, gridManager, "Apple", camera, player );
            k.Enabled = false;
            k.Visible = false;
            Keys.Add( k );
            return k;
        }

    }
}
