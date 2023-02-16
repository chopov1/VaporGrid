using Microsoft.Xna.Framework;
using VaporGridCrossPlatform.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.Scenes
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
            if (input.PressedBack)
            {
                sceneManager.ChangeScene(this, sceneManager.mainMenu);
            }
            else if (input.PressedContinue)
            {
                sceneManager.ChangeScene(this, sceneManager.levels[sceneManager.LevelSelection]);
            }
        }


    }
}
