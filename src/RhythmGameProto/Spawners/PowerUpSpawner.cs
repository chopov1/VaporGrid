﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class PowerUpSpawner : Spawner
    {
        public PowerUpSpawner(Game game, GridManager gm, RhythmManager rm,  Camera camera, Player p, int numberOfObjects) : base(game, gm, rm,  camera, p, numberOfObjects)
        {

        }

        public override GridSprite createSpawnableObject()
        {
            PowerUp p = new PowerUp(Game, gridManager, "Apple", camera, player, this);
            p.Enabled = false;
            p.Visible = false;
            return p;
        }
    }
}
