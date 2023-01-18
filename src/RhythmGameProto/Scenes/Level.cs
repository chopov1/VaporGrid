using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto.Scenes
{
    public class Level : GamePlay
    {
        int numOfKeys;
        int curKeys;
        public Level(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm, int level) : base(game, manager, rm, sm, level)
        {

        }

        protected override void checkPlayerState()
        {
            switch (player.State)
            {
                case PlayerState.Dead:
                    ResetGamePlay();
                    arrowIndicators.UnLoad();
                    sceneManager.ChangeScene(this, sceneManager.gameOver);
                    break;
                case PlayerState.Alive:

                    break;
            }
        }
    }
}
