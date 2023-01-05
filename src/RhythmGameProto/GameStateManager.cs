using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class GameStateManager : GameComponent
    {

        SceneManager sm;

        public GameStateManager(Game game) : base(game)
        {
            sm= new SceneManager(game);
            Game.Components.Add(sm);
            
        }

        public override void Initialize()
        {
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
