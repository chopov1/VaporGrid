using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform
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
