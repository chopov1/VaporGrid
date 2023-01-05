using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class GameOver : Scene
    {
        MenuController input;
        GameOverMenuUI display;
        public GameOver(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm) : base(game, manager, rm, sm)
        {
            input = new MenuController();
            display = new GameOverMenuUI(Game, sm);
            Game.Components.Add(display);
            addComponentToScene(display);
        }


        protected override void SceneUpdate()
        {
            input.Update();
            base.SceneUpdate();
            if (input.PressedEnter)
            {
                State = SceneState.readyToClose;
                sceneManager.mainMenu.State = SceneState.loading;
            }
            else if (input.PressedSpace)
            {
                State = SceneState.readyToClose;
                sceneManager.gamePlay.State = SceneState.loading;
            }
        }


    }
}
