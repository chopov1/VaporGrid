using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaporGridCrossPlatform.Scenes;
using VaporGridCrossPlatform.MapManagerClasses;
using VaporGridCrossPlatform.GameUtility;
using Microsoft.Xna.Framework.Graphics;

namespace VaporGridCrossPlatform
{
    public class SceneManager : GameComponent
    {
        public Scene mainMenu;
        public List<Scene> levels;
        public Scene gameOver;
        public LevelEnd levelEndScreen;
        public Scene tutorial;

        ScoreManager sm;
        RhythmManager rm;

        LevelLoader lvls;
        public int NumberOfLevels { get { return lvls.levelList.Count; } }
        public int LevelSelection;

        BackgroundStars bgs;
        public SceneManager(Game game) : base(game)
        {
            LevelSelection = 0;
            lvls = new LevelLoader();
            levels = new List<Scene>();
            rm = new RhythmManager(Game);
            sm = new ScoreManager(Game, rm);

            Camera c = new Camera(Game);
            bgs = new BackgroundStars(Game, c);
            Game.Components.Add(bgs);

            mainMenu = new MainMenu(game,this , rm, sm);
            gameOver = new GameOver(game, this, rm, sm);
            levelEndScreen = new LevelEnd(game, this, rm, sm);
            tutorial = new Tutorial(game,this , rm, sm);
            setupLevels();
            mainMenu.State = SceneState.loading;
            Game.Components.Add(mainMenu);
            Game.Components.Add(gameOver);
            Game.Components.Add(levelEndScreen);
            Game.Components.Add(tutorial);
            
        }

        private void setupLevels()
        {
            EndlessGamePlay endlessGameMode = new EndlessGamePlay(Game, this, rm, sm);
            levels.Add(endlessGameMode);
            levels.Add(tutorial);
            Game.Components.Add(endlessGameMode);
            for (int levelNumber = 1; levelNumber < NumberOfLevels; levelNumber++)
            {
                Level l = new Level(Game, this, rm, sm, levelNumber);
                levels.Add(l);
                Game.Components.Add(l);
            }
        }

        public void ChangeScene(Scene sceneToClose, Scene sceneToLoad)
        {
            sceneToClose.State= SceneState.readyToClose;
            sceneToLoad.State = SceneState.loading;
        }
    }
}
