using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class SceneManager : GameComponent
    {
        public Scene mainMenu;
        public Scene gamePlay;
        public Scene gameOver;

        ScoreManager sm;
        RhythmManager rm;
        public SceneManager(Game game) : base(game)
        {
            rm = new RhythmManager(Game);
            sm = new ScoreManager(Game, rm);

            mainMenu = new MainMenu(game,this , rm, sm);
            gamePlay = new GamePlay(game, this, rm, sm);
            gameOver = new GameOver(game, this, rm, sm);
            mainMenu.State = SceneState.loading;
            Game.Components.Add(mainMenu);
            Game.Components.Add(gamePlay);
            Game.Components.Add(gameOver);
        }
    }
}
