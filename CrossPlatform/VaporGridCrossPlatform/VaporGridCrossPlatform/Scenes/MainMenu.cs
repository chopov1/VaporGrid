using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VaporGridCrossPlatform.GameUtility;
using VaporGridCrossPlatform.UI;
using System.Timers;

namespace VaporGridCrossPlatform.Scenes
{
    public class MainMenu : Scene
    {
        MenuController input;
        MainMenuUI display;
        Timer attractTimer;

        int levelSelection { get { return display.LevelSelection; } set { sceneManager.LevelSelection = display.LevelSelection = value; } }
        public MainMenu(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm) : base(game, manager, rm, sm)
        {
            input = new MenuController();
            display = new MainMenuUI(game, sm);
            levelSelection = 0;
            Game.Components.Add(display);
            addComponentToScene(display);
            
        }
        void startAttractTimer()
        {
            attractTimer = new Timer(60000);
            attractTimer.Elapsed += startAttractMode;
            attractTimer.AutoReset = false;
            attractTimer.Start();
        }

        void stopAttractTimer()
        {
            if(attractTimer == null)
            {
                return;
            }
            attractTimer.Stop();
            attractTimer.Elapsed -= startAttractMode;
            attractTimer.Dispose();
        }

        void startAttractMode(object o, ElapsedEventArgs e)
        {
            scoreManager.WriteEnabled = false;
            sceneManager.ChangeScene(this, sceneManager.attractScene);
        }

        public override void loadScene()
        {
            rm.SetVolume(0);
            base.loadScene();
            startAttractTimer();
        }

        protected override void SceneUpdate()
        {
            if (input.IsPressingAnyKey())
            {
                stopAttractTimer();
                startAttractTimer();
            }
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
            if (input.PressedDown)
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
            if(input.PressedUp)
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

        public override void closeScene()
        {
            stopAttractTimer();
            base.closeScene();
        }

    }
}
