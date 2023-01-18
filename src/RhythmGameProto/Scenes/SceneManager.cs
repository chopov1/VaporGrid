using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RhythmGameProto.Scenes;
using RhythmGameProto.MapManagerClasses;

namespace RhythmGameProto
{
    public class SceneManager : GameComponent
    {
        public Scene mainMenu;
        public List<Scene> levels;
        public Scene gameOver;
        public Scene tutorial;

        ScoreManager sm;
        RhythmManager rm;

        LevelLoader lvls;
        public int NumberOfLevels { get { return lvls.levelList.Count; } }
        public int LevelSelection;
        public SceneManager(Game game) : base(game)
        {
            LevelSelection = 1;
            lvls = new LevelLoader();
            levels = new List<Scene>();
            rm = new RhythmManager(Game);
            sm = new ScoreManager(Game, rm);


            mainMenu = new MainMenu(game,this , rm, sm);
            setupLevels();
            gameOver = new GameOver(game, this, rm, sm);
            tutorial = new Tutorial(game,this , rm, sm);
            mainMenu.State = SceneState.loading;
            Game.Components.Add(mainMenu);
            Game.Components.Add(gameOver);
            Game.Components.Add(tutorial);
        }

        private void setupLevels()
        {
            for(int levelNumber =0; levelNumber < NumberOfLevels; levelNumber++)
            {
                GamePlay g = new GamePlay(Game, this, rm, sm, levelNumber);
                levels.Add(g);
                Game.Components.Add(g);
            }
        }

        public void ChangeScene(Scene sceneToClose, Scene sceneToLoad)
        {
            sceneToClose.State= SceneState.readyToClose;
            sceneToLoad.State = SceneState.loading;
        }
    }
}
