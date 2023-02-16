using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.Objects
{
    internal class Portal : PowerUp
    {
        Vector2 portal1GridPos;
        Vector2 portal2GridPos;
        Animation portalAnim;
        Vector2 portal1DrawPos;
        Vector2 portal2DrawPos;
        public Portal(Game game, GridManager gm, string texturename, Camera camera, Player player, Spawner spawner) : base(game, gm, texturename, camera, player, spawner)
        {
           
        }

        public void SetPositions(Vector2 position1, Vector2 position2)
        {
            portal1GridPos = position1;
            portal2GridPos = position2;
            portal1DrawPos = gridManager.Grid[(int)portal1GridPos.X, (int)portal1GridPos.Y].Position;
            portal2DrawPos = gridManager.Grid[(int)portal2GridPos.X, (int)portal2GridPos.Y].Position;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            portalAnim.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            portalAnim = new Animation(spriteTexture,3, 0.2f );
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,null, SamplerState.PointClamp,null, null, null, camera.Transform);
            portalAnim.Draw(portal1DrawPos, spriteBatch, 1, scale);
            portalAnim.Draw(portal2DrawPos, spriteBatch, 1, scale);
            spriteBatch.End();
        }

        protected override void checkPlayerPos()
        {
            if(player.gridPos == portal1GridPos)
            {
                Activate();
            }
            else if(player.gridPos == portal2GridPos)
            {
                Activate();
            }
        }

        public override void Activate()
        {
            if(player.gridPos == portal1GridPos)
            {
                player.gridPos = portal2GridPos;
            }
            else if(player.gridPos == portal2GridPos)
            {
                player.gridPos = portal1GridPos;
            }
            spawner.BufferCount= 0;
            base.Activate();
        }

    }
}
