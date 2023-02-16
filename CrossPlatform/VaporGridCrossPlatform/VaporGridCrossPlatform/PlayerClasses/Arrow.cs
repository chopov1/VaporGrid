using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform
{
    public class Arrow : RhythmSprite
    {
        protected Player player;
        Texture2D error;
        Texture2D onBeat;
        Texture2D regular;
        string errorName;
        string onBeatName;
        public Arrow(Game game, GridManager gm, RhythmManager rm,string texturename, string errortextureName,string onBeatTextureName ,Camera camera, Player p) : base(game, gm, rm,texturename, camera)
        {
            player = p;
            errorName= errortextureName;
            onBeatName = onBeatTextureName;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            error = Game.Content.Load<Texture2D>(errorName);
            onBeat = Game.Content.Load<Texture2D>(onBeatName);
            regular = spriteTexture;
        }

        private void changeTexture()
        {


            if (player.lastInputState == InputOutcome.Missed)
            {
                if (spriteTexture != error)
                {
                    spriteTexture = error;
                }
            }
            else if (rhythmManager.RhythmState == RhythmState.Quarter)
            {
                if(spriteTexture != onBeat)
                {
                    spriteTexture = onBeat;
                }
            }
            else if (player.CanInputOnSixteenth())
            {
                if (spriteTexture != onBeat)
                {
                    spriteTexture = onBeat;
                }
            }
            
            else
            {
                if(spriteTexture != regular)
                { 
                spriteTexture = regular;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            changeTexture();
        }
    }
}
