using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using PongGame.GameObjects;
using System;
using System.Drawing;

namespace PongGame.Scenes
{
    internal class SlaveGameClass : Scene, IScene
    {
        private Matrix4 projectionMatrix;

        private PlayerPaddle playerPaddle;
        private PlayerTwoPaddle playerTwoPaddle;
        private Ball ball;
        private int scorePlayer = 0;
        private int scorePlayerTwo = 0;
        private double gameTime = 30;
        private static string serverIp;
        private static string masterIP;
        private static int portNumber;
        private Client client = new Client(serverIp, portNumber, masterIP);

        public SlaveGameClass(SceneManager sceneManager, Client inClient)
            : base(sceneManager)
        {
            masterIP = inClient.masterAddress;
            portNumber = inClient.port;
            serverIp = inClient.serverAddress;
            // Set the Render and Update delegates to the Update and Render methods of this class
            sceneManager.renderer = Render;
            sceneManager.updater = Update;
            // Set Keyboard events to go to a method in this class
            sceneManager.Keyboard.KeyDown += Keyboard_KeyDown;
            ResetGame();

            GL.ClearColor(Color.Black);
        }

        private void ResetGame()
        {
            playerPaddle = new PlayerPaddle(40, (int)(SceneManager.WindowHeight * 0.5));
            playerPaddle.Init();
            playerTwoPaddle = new PlayerTwoPaddle(SceneManager.WindowWidth - 40, (int)(SceneManager.WindowHeight * 0.5));
            playerTwoPaddle.Init();
            ball = new Ball((int)(SceneManager.WindowWidth * 0.5), (int)(SceneManager.WindowHeight * 0.5));
            ball.Init();
        }

        public void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    //  client.slaveMoveUp(masterIP,portNumber);
                    playerTwoPaddle.Move(20);
                    break;

                case Key.Down:
                    // client.slaveMoveDown(masterIP, portNumber);
                    playerTwoPaddle.Move(-20);
                    break;
                    //case Key.A:
                    //    playerPaddle.Move(20);
                    //    break;
                    //case Key.Z:
                    //    playerPaddle.Move(-20);
                    //    break;
            }
        }

        //here
        public void Update(FrameEventArgs e)
        {
            // Set the title of the window
            if (gameTime > 0)
            {
                sceneManager.Title = "Pong - Player Score: " + scorePlayer + " - Player 2 Score: " + scorePlayerTwo + " time remaining " + Math.Truncate(gameTime);

                //playerTwoPaddle.Move(ball.Position);

                ball.Update((float)e.Time);
                playerTwoPaddle.Update((float)e.Time);

                CollisionDetection();
                if (GoalDetection())
                {
                    ResetGame();
                }
                // gameTime -= (1.0 * e.Time);
            }
            else
            {
                sceneManager.GameOver();
            }
        }

        private bool GoalDetection()
        {
            if (ball.Position.X < 0)
            {
                scorePlayerTwo++;
                return true;
            }
            else if (ball.Position.X > SceneManager.WindowWidth)
            {
                scorePlayer++;
                return true;
            }

            return false;
        }

        private void CollisionDetection()
        {
            // AI
            if ((playerTwoPaddle.Position.X - ball.Position.X) < ball.Radius &&
               ball.Position.Y > (playerTwoPaddle.Position.Y - 35.0f) && ball.Position.Y < (playerTwoPaddle.Position.Y + 35.0f))
            {
                ball.Position = new Vector2(playerTwoPaddle.Position.X - ball.Radius, ball.Position.Y);
                ball.Velocity = new Vector2(ball.Velocity.X * -1.0f, ball.Velocity.Y) * 2.0f;
            }
            // Player
            if ((ball.Position.X - playerPaddle.Position.X) < ball.Radius &&
               ball.Position.Y > (playerPaddle.Position.Y - 35.0f) && ball.Position.Y < (playerPaddle.Position.Y + 35.0f))
            {
                ball.Position = new Vector2(playerPaddle.Position.X + ball.Radius, ball.Position.Y);
                ball.Velocity = new Vector2(ball.Velocity.X * -1.0f, ball.Velocity.Y) * 2.0f;
            }
        }

        public void Render(FrameEventArgs e)
        {
            GL.Viewport(0, 0, sceneManager.Width, sceneManager.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, sceneManager.Width, 0, sceneManager.Height, -1.0f, +1.0f);

            ball.Render(projectionMatrix);
            playerPaddle.Render(projectionMatrix);
            playerTwoPaddle.Render(projectionMatrix);
        }
    }
}