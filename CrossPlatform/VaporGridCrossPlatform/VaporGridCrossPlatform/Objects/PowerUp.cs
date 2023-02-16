using Microsoft.Xna.Framework;

namespace VaporGridCrossPlatform
{
    public class PowerUp : Collectable
    {

        protected Spawner spawner;


        public PowerUp(Game game, GridManager gm, string texturename, Camera camera, Player player, Spawner spawner) : base(game, gm, texturename, camera, player)
        {
            this.spawner = spawner;
        }

        public override void Activate()
        {
            player.IncreaseScore();
            spawner.DeSpawn(this);
        }

    }
}
