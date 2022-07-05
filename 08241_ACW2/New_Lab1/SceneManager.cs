using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using PongGame.Scenes;
using System.Collections.Generic;

namespace PongGame
{
    class SceneManager : GameWindow
    {
        Scene scene;
        static int width = 0;
        static int height = 0;
       // static double menuTime = 10;
       
        Client client;
        MasterServer master;
        public delegate void SceneDelegate(FrameEventArgs e);
        public SceneDelegate renderer;
        public SceneDelegate updater;
        public static Dictionary<int, string> myDictionary = new Dictionary<int, string>();
        public string ServerIp;
        public string masterIp;
        public int Port;
        public SceneManager(Client inClient, MasterServer inMaster)
        {
            client = inClient;
            master = inMaster;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);

            base.Width = 1024;
            base.Height = 512;
            SceneManager.width = Width;
            SceneManager.height = Height;

            scene = new MainMenuScene(this, client, master);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            updater(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            renderer(e);

            GL.Flush();
            SwapBuffers();
        }
        public void StartNewGame()
        {
            scene = new GameScene(this);
        }

        public void StartMenu()
        {
            scene = new MainMenuScene(this, client, master);
        }
        public void GameOver()
        {
            scene = new GameOverScene(this, client);
            
        }
        public void HighScore(int scorePlayer)
        {
            scene = new HighScoreScene(this, scorePlayer, client);
        }
        public void MultiPLayerGame()
        {
            scene = new MultiGameScene(this);
        }
        public void HostAgame()
        {
            scene = new MasterGameClass(this, client, master);
        }
        public void ConnectToAgame()
        {
            scene = new SlaveGameClass(this, client);
        }

        public static int WindowWidth
        {
            get { return width; }
        }

        public static int WindowHeight
        {
            get { return height; }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            SceneManager.width = Width;
            SceneManager.height = Height;
        }
    }

}

