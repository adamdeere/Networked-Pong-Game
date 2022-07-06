using System;

namespace PongGame
{
    public class Start
    {
        private static int Port;
        private static string IPaddress;
        private static string masterIp;

        public static void Main(string[] args)
        {
            bool exit = false;
            while (exit == false)
            {
                try
                {
                    Console.WriteLine("please enter the server port address");
                    Port = int.Parse(Console.ReadLine());
                    exit = true;
                }
                catch
                {
                    Console.WriteLine("please enter a numerical value");
                }
            }

            Console.WriteLine("please enter the server ip address");
            IPaddress = Console.ReadLine();
            Console.WriteLine("please enter the ip address of the game you wish to connect to");
            masterIp = Console.ReadLine();
            MasterServer master = new MasterServer();
            Client client = new Client(IPaddress, Port, masterIp);

            using (SceneManager sceneManager = new SceneManager(client, master))
            {
                sceneManager.Run(60.0f);
            }
        }
    }
}