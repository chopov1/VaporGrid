using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RhythmGameProto
{
    public class GamePlay : Scene
    {
        GridManager gridManager;
        Player player;
        ArrowIndicators arrowIndicators;
        EnemySpawner enemySpawner;
        PowerUpSpawner powerUpSpawner;
        Camera camera;
        FileReader fileReader;
        

        public GamePlay(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm) : base(game, manager, rm, sm)
        {

        }

        public override void SetupScene()
        {
            setupGame();
            base.SetupScene();
        }

        private void setupGame()
        {
            fileReader = new FileReader();
            camera = new Camera(Game);
            Game.Components.Add(camera);
            
            gridManager = new GridManager(Game, camera, rm);
            Game.Components.Add(gridManager);
            
            player = new Player(Game, gridManager, rm, 1, camera, scoreManager);
            Game.Components.Add(player);
            enemySpawner = new EnemySpawner(Game, gridManager, rm, player, camera);
            Game.Components.Add(enemySpawner);
            powerUpSpawner = new PowerUpSpawner(Game, gridManager, rm, player, camera);
            Game.Components.Add(powerUpSpawner);
            arrowIndicators = new ArrowIndicators(Game, gridManager, player, camera);
            Game.Components.Add(arrowIndicators);
            addCompsToList();
        }

        private void addCompsToList()
        {
            addComponentToScene(camera);
            addComponentToScene(gridManager);
            addComponentToScene(enemySpawner);
            addComponentToScene(powerUpSpawner);
            addComponentToScene(player);
            addComponentToScene(scoreManager);
            addComponentToScene(arrowIndicators);
            addComponentToScene(rm);
        }


        protected override void SceneUpdate()
        {
            base.SceneUpdate();
            checkPlayerState();
        }

        public override void loadScene()
        {
            base.loadScene();
            rm.state = SongState.reset;
        }

        private void ResetGamePlay()
        {
            player.ResetPlayer();
            gridManager.ResetGrid(player.gridPos);
            enemySpawner.spawnState = SpawnState.reset;
            powerUpSpawner.spawnState = SpawnState.reset;
            //rm.state = SongState.reset;
        }
        private void checkPlayerState()
        {
            switch (player.State)
            {
                case PlayerState.Dead:
                    ResetGamePlay();
                    State = SceneState.readyToClose;
                    sceneManager.gameOver.State = SceneState.loading;
                    break;
                case PlayerState.Alive:
                    break;
            }
        }
    }
}
