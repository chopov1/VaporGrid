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
    public class PowerUp : GridSprite
    {
        Player player;
        Spawner spawner;

       
        public PowerUp(Game game, GridManager gm, string texturename, Camera camera, Player player, Spawner spawner) : base(game, gm, texturename, camera)
        {
            this.player = player;
            this.spawner = spawner;
            game.Components.Add(this);
        }

        private void checkPlayerPos()
        {
            if(player.gridPos == this.gridPos)
            {
                Activate();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            checkPlayerPos();
        }

        public virtual void Activate()
        {
            player.IncreaseScore();
            spawner.DeSpawn(this);
        }

    }
}
