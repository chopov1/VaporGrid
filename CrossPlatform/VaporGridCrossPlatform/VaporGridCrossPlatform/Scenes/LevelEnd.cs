using Microsoft.Xna.Framework;
using VaporGridCrossPlatform.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform
{
    public class LevelEnd : Scene
    {
        MenuController input;
        PuzzleCompleteUI winDisplay;
        PuzzleFailUI loseDisplay;
        public LevelEnd(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm) : base(game, manager, rm, sm)
        {
            input = new MenuController();
            winDisplay = new PuzzleCompleteUI(Game, sm);
            loseDisplay = new PuzzleFailUI(Game);
            Game.Components.Add(winDisplay);
            Game.Components.Add(loseDisplay);
            addComponentToScene(loseDisplay);
            addComponentToScene(winDisplay);
        }

        public void SetDisplayType(bool hasWon)
        {
            if(hasWon)
            {
                loseDisplay.ShowDisplay = false;
                winDisplay.ShowDisplay = true;
            }
            else
            {
                winDisplay.ShowDisplay = false;
                loseDisplay.ShowDisplay= true;
            }
        }
        public void SetLevelNum(int levelNum)
        {
            winDisplay.setLevelNum(levelNum);
        }

        public override void loadScene()
        {
            base.loadScene();
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
