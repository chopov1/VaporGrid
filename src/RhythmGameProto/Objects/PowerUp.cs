using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RhythmGameProto
{
    public class PowerUp : Collectable
    {

        Spawner spawner;


        public PowerUp(Game game, GridManager gm, string texturename, Camera camera, Player player, Spawner spawner) : base(game, gm, texturename, camera, player)
        {
            this.spawner = spawner;
            game.Components.Add(this);
        }

        public override void Activate()
        {
            player.IncreaseScore();
            spawner.DeSpawn(this);
        }

    }
}
