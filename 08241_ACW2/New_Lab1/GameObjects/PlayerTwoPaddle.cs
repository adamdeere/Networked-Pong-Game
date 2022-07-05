using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongGame.GameObjects
{
    class PlayerTwoPaddle : Paddle
    {
        public PlayerTwoPaddle(int x, int y) : base(x, y)
        { 
        }

        public override void Update(float dt)
        {
        }

        public void Move(int dy)
        {
            position.Y += dy;
            if (position.Y < 0) position.Y = 0;
            else if (position.Y > SceneManager.WindowHeight) position.Y = SceneManager.WindowHeight;
        }
    }
}
