using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VaporGridCrossPlatform.GameUtility;
using VaporGridCrossPlatform.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.Scenes
{
    public class MainMenu : Scene
    {
        MenuController input;
        MainMenuUI display;
        int levelSelection { get { return display.LevelSelection; } set { sceneManager.LevelSelection = display.LevelSelection = value; } }
        public MainMenu(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm) : base(game, manager, rm, sm)
        {
            input = new MenuController();
            display = new MainMenuUI(game, sm);
            levelSelection = 0;
            Game.Components.Add(display);
            addComponentToScene(display);
            
        }

        protected override void SceneUpdate()
        {
            input.Update();
            base.SceneUpdate();
            if (input.PressedContinue)
            {
                sceneManager.ChangeScene(this, sceneManager.levels[levelSelection]);
            }
            updateLevelSelection();
        }

        private void updateLevelSelection()
        {
            if (input.PressedRight)
            {
                if(levelSelection + 1 > sceneManager.NumberOfLevels)
                {
                    levelSelection = 0;
                }
                else
                {
                    levelSelection++;
                }
            }
            if(input.PressedLeft)
            {
                if(levelSelection - 1 < 0)
                {
                    levelSelection = sceneManager.NumberOfLevels;
                }
                else
                {
                    levelSelection--;
                }
            }
        }



    }
}
