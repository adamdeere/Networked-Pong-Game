using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using PongGame.Utils;

namespace PongGame
{
    internal class MainMenuScene : Scene, IScene
    {
        public static string serverIp;
        public static string masterIp;
        public static int portNo;

        private Client client; //= new Client(serverIp, portNo, masterIp);
        private MasterServer master;

        private RenderText mTextRender;

        public MainMenuScene(SceneManager sceneManager, Client inClient, MasterServer inMaster)
            : base(sceneManager)
        {
            serverIp = inClient.serverAddress;
            masterIp = inClient.masterAddress;
            portNo = inClient.port;
            client = inClient;
            master = inMaster;
            // Set the title of the window
            sceneManager.Title = "Pong - Main Menu";
            // Set the Render and Update delegates to the Update and Render methods of this class
            sceneManager.renderer = Render;
            sceneManager.updater = Update;
            mTextRender = new RenderText(sceneManager.Width, sceneManager.Height);
        }

        public void Update(FrameEventArgs e)
        {
            SelectMenu(new KeyboardKeyEventArgs());
        }

        public void Render(FrameEventArgs e)
        {
            GL.Viewport(0, 0, sceneManager.Width, sceneManager.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, sceneManager.Width, 0, sceneManager.Height, -1, 1);

            mTextRender.renderTextOnScreen("welcome to pong", 0f, 0f);
            mTextRender.renderTextOnScreen("1. single player game", 0f, 40f);
            mTextRender.renderTextOnScreen("2. multyplayer game", 0f, 80f);
            mTextRender.renderTextOnScreen("3. host networked game", 0f, 140f);
            mTextRender.renderTextOnScreen("4 connect to networked game", 0f, 180f);
            mTextRender.renderTextOnScreen("5. display high scores", 0, 220f);
           
        }
        public void SelectMenu(KeyboardKeyEventArgs e)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Key.Number1))
            {
                sceneManager.StartNewGame();
            }
            if (keyState.IsKeyDown(Key.Number2))
            {
                sceneManager.MultiPLayerGame();
            }
            if (keyState.IsKeyDown(Key.Number3))// host networked game
            {
                mTextRender.renderTextOnScreen("waiting for a connection...", 0, 260f);
                master.confirmConnection();
                if (client.lineFromSlave.Contains("confrim"))
                {
                    sceneManager.HostAgame();
                }
            }
            if (keyState.IsKeyDown(Key.Number4))// connect networked game
            {
                //client.checkConnection(masterIp, portNo);
                if (client.lineFromMaster.Contains("ok"))
                {
                    sceneManager.ConnectToAgame();
                }
            }
            if (keyState.IsKeyDown(Key.Number5))
            {
                sceneManager.GameOver();
            }
        }
    }
}