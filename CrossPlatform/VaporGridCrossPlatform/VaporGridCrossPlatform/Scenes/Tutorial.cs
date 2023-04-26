using Microsoft.Xna.Framework;
using VaporGridCrossPlatform.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace VaporGridCrossPlatform.Scenes
{
    internal class Tutorial : Scene
    {
        enum TutorialStage { movement, score, doubleTime, enemies };
        TutorialStage tutorialStage;

        Player player;
        Camera camera;
        GridManager gridManager;
        ArrowIndicators arrowIndicators;

        Collectable continueTile;
        PowerUpSpawner scoreSpawner;
        EnemySpawner enemySpawner;

        TutorialUI display;
        ScoreUI scoreDisplay;

        MenuController menuInput;


        public Tutorial(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm) : base(game, manager, rm, sm)
        {
            menuInput= new MenuController();
        }

        public override void SetupScene()
        {
            setupTutorial();
            base.SetupScene();
        }

        private void setupTutorial()
        {
            tutorialStage = TutorialStage.movement;

            display = new TutorialUI(Game);
            Game.Components.Add(display);

            scoreDisplay = new ScoreUI(Game, scoreManager, 420);
            Game.Components.Add(scoreDisplay);

            camera = new Camera(Game);
            Game.Components.Add(camera);

            gridManager = new GridManager(Game, camera, rm);
            gridManager.loadLevel(0);
            Game.Components.Add(gridManager);

            player = new Player(Game, gridManager, rm, 1, camera, scoreManager);
            Game.Components.Add(player);

            continueTile = new Collectable(Game, gridManager, "ExitTutorial", camera, player);

            scoreSpawner = new PowerUpSpawner(Game, gridManager, rm,  camera, player, 1, 4);
            Game.Components.Add(scoreSpawner);
            enemySpawner = new EnemySpawner(Game, gridManager, rm, camera, player, 1, 4);
            Game.Components.Add(enemySpawner);

            arrowIndicators = new ArrowIndicators(Game, gridManager, rm, player, camera);
            Game.Components.Add(arrowIndicators);

            addCompsToList();

            stageChangedUpdated = false;
        }

        private void addCompsToList()
        {
            addComponentToScene(display);
            addComponentToScene(scoreDisplay);
            addComponentToScene(camera);
            addComponentToScene(gridManager);
            addComponentToScene(player);
            addComponentToScene(continueTile);
            addComponentToScene(scoreManager);
            addComponentToScene(arrowIndicators);
            addComponentToScene(rm);
            addComponentToScene(scoreSpawner);
            addComponentToScene(enemySpawner);
        }

        private void checkForQuitTutorial()
        {
            menuInput.Update();
            if (menuInput.PressedBack)
            {
                returnToMenu();
            }
        }
        protected override void SceneUpdate()
        {
            base.SceneUpdate();
            checkForQuitTutorial();
            if (!stageChangedUpdated)
            {
                changeStage();
            }
            switch (tutorialStage)
            {
                case TutorialStage.movement:
                    if (completedMovement())
                    {
                        tutorialStage = TutorialStage.score;
                        stageChangedUpdated = false;
                    }
                    break;
                case TutorialStage.score:
                    if (completedScore())
                    {
                        //idk if I need double time in the tutorial
                        tutorialStage = TutorialStage.enemies; 
                        stageChangedUpdated = false;
                    }
                    break;
                case TutorialStage.doubleTime:
                    if (completedDoubleTime())
                    {
                        tutorialStage = TutorialStage.enemies;
                        stageChangedUpdated = false;
                    }
                    break;
                case TutorialStage.enemies:
                    if (completedEnemies())
                    {
                        tutorialStage = TutorialStage.movement;
                        stageChangedUpdated = false;
                        returnToMenu();
                    }
                    break;
            }

        }

        bool stageChangedUpdated;
        private void changeStage()
        {
            display.Stage = (int)tutorialStage;
            switch (tutorialStage)
            {
                case TutorialStage.movement:
                    continueTile.Collected = false;
                    continueTile.Load();
                    enemySpawner.UnLoad();
                    scoreSpawner.UnLoad();
                    scoreManager.ShowScore = false;
                    scoreManager.ComboScore = 0;
                    break;
                case TutorialStage.score:
                    scoreManager.ComboScore = 0;
                    scoreSpawner.Load();
                    scoreManager.ShowScore = true;
                    scoreManager.ShowHighScore = false;
                    scoreSpawner.SpawnBuffer = 1;
                    break;
                case TutorialStage.doubleTime:
                    scoreSpawner.UnLoad();
                    scoreManager.ComboScore = player.SixteenthBuffer;
                    break;
                case TutorialStage.enemies:
                    scoreManager.ShowScore = false;
                    enemySpawner.Load();
                    enemySpawner.SpawnBuffer = 1;
                    break;
            }
            stageChangedUpdated = true;
        }

        private void resetTutorial()
        {
            tutorialStage = TutorialStage.movement;
            changeStage();
        }

        private bool completedEnemies()
        {
            if (player.State == PlayerState.Dead)
            {
                return true;
            }
            return false;
        }

        private bool completedDoubleTime()
        {
            //This is reliant on double time inputs giving +2
            //And 17 being the point at which you can do double time!!
            if (scoreManager.ComboScore == player.SixteenthBuffer + 2)
            {
                scoreManager.ComboScore = player.SixteenthBuffer + 1;
            }
            if (scoreManager.ComboScore < player.SixteenthBuffer + 1)
            {
                scoreManager.ComboScore = player.SixteenthBuffer + 1;
            }
            if (scoreManager.ComboScore == player.SixteenthBuffer + 3)
            {
                return true;
            }
            return false;
        }

        private bool completedScore()
        {
            if (scoreManager.Score > 5)
            {
                return true;
            }
            return false;
        }
        private bool completedMovement()
        {
            if (continueTile.Collected)
            {
                continueTile.Collected = false;
                continueTile.UnLoad();

                return true;
            }
            return false;
        }

        private void returnToMenu()
        {
            rm.SetVolume(0);
            //resetTutorial();
            //TODO: not sure why need to call Unload manually on arrowIndicators only. 
            arrowIndicators.UnLoad();
            sceneManager.ChangeScene(this, sceneManager.mainMenu);
        }

        public override void loadScene()
        {
            rm.SetVolume(1.0f);
            player.ResetPlayer(new Vector2(0,0));
            base.loadScene();
            rm.state = SongState.reset;
            scoreManager.ShowScore = false;
            scoreManager.WriteEnabled = false;
            continueTile.gridPos = new Vector2(gridManager.Grid.GetLength(0) - 1, gridManager.Grid.GetLength(1) - 1);
            resetTutorial();
        }

        public override void closeScene()
        {
            base.closeScene();
        }

    }
}
