using Microsoft.Xna.Framework;
using VaporGridCrossPlatform.UI;
using System.Timers;
namespace VaporGridCrossPlatform.Scenes
{
    public class GameOver : Scene
    {
        MenuController input;
        GameOverMenuUI display;
        Timer menuTimer;
        public GameOver(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm) : base(game, manager, rm, sm)
        {
            input = new MenuController();
            display = new GameOverMenuUI(Game, sm);
            Game.Components.Add(display);
            addComponentToScene(display);
            menuTimer = new Timer(60000);
            menuTimer.Elapsed += returnToMenu;
        }
        void returnToMenu(object o, ElapsedEventArgs e)
        {
            menuTimer.Stop();
            sceneManager.ChangeScene(this, sceneManager.mainMenu);
        }

        public override void loadScene()
        {
            base.loadScene();
            menuTimer.Start();
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
