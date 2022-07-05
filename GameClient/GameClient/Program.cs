//Demonstrate Sockets
using System;
using System.Net.Sockets;
using System.IO;



public class Whois
{
    static StreamWriter sw;
    static StreamReader sr;
    /// <summary>
    /// this is a method that will ping a request to the server for the high scores
    /// and return names and scores in the form of a string that can be choipped 
    /// up and returned to the game
    /// </summary>
    static void LookUp()
    {

    }
    /// <summary>
    /// once the game has updated the new high score (if any) this method will send the infomation back
    /// to the server to be updated in the dictionary, written to a text file on the other
    /// side so as to act as storage space inside the server
    /// </summary>
    static void Update(string name, int score, string address)
    {
        sw.WriteLine(name + " " + score);
        sw.Flush();
        string response = sr.ReadLine();
        Console.WriteLine(response);
    }
    static void Main(string[] args)
    {
        int port = 43;
        string ipAddress = "localhost";
        string name = null;
        int score = 0;
        TcpClient client = new TcpClient();
        client.Connect(ipAddress, port);
        sw = new StreamWriter(client.GetStream());
        sr = new StreamReader(client.GetStream());

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {    case "-h":
                    ipAddress = args[++i]; 
                    break;
                case "-p":
                    port = int.Parse(args[++i]);
                    break;
                default:
                    if (name == null)
                    {
                        name = args[i];
                    }
                    else if (score == 0)
                    {
                        score = int.Parse(args[i]);
                    }
                    else
                    {
                        Console.WriteLine("too many arguments");
                        return;
                    }
                    break;
            }
        }
        Update(name, score, ipAddress);
        Console.WriteLine(sr.ReadToEnd());
    }
}
