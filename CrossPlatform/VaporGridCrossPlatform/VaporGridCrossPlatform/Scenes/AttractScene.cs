using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.Scenes
{
    public class AttractScene : EndlessGamePlay
    {
        InputHandler inputHandler;
        public AttractScene(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm) : base(game, manager, rm, sm)
        {
            inputHandler = new InputHandler();
            
        }



        protected override void SceneUpdate()
        {
            inputHandler.Update();
            base.SceneUpdate();
            if (inputHandler.IsPressingAnyKey())
            {
                ResetGamePlay();
                arrowIndicators.UnLoad();
                scoreManager.WriteEnabled = true;
                sceneManager.ChangeScene(this, sceneManager.mainMenu);
            }
        }
        
        protected override void setupPlayer()
        {
            player = new NonPlayer(Game, gridManager, rm, 1, camera, scoreManager);
            Game.Components.Add(player);
        }


        protected override void checkPlayerState()
        {
            switch (player.State)
            {
                case PlayerState.Dead:
                    ResetGamePlay();
                    arrowIndicators.UnLoad();
                    sceneManager.ChangeScene(this, sceneManager.mainMenu);
                    break;
                case PlayerState.Alive:
                    break;
            }
        }
    }
}
