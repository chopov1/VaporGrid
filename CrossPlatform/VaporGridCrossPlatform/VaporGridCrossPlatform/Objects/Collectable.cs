using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaporGridCrossPlatform;

namespace VaporGridCrossPlatform
{
    public class Collectable : GridSprite
    {
        public bool Collected;
        protected Player player;
        public Collectable(Game game, GridManager gm, string texturename, Camera camera, Player player) : base(game, gm, texturename, camera)
        {
            this.player = player;
            Collected = false;
            game.Components.Add(this);
        }

        protected virtual void checkPlayerPos()
        {
            if (player.gridPos == this.gridPos)
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
            Collected = true;
        }
    }
}
