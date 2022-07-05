using System;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Collections.Generic;
public class Whois
{
    public static Dictionary<int,string> ScoresOnTheDoors;
    public static string name;
    public static int scores;
    static void runServer()
    {
        TcpListener listener;
        Socket connection;
        NetworkStream socketStream;
        try
        {
            listener = new TcpListener(IPAddress.Any, 43);
            listener.Start();
            Console.WriteLine("server started listening");
            while (true)
            {
                connection = listener.AcceptSocket();
                socketStream = new NetworkStream(connection);
                doRequest(socketStream);
                socketStream.Close();
                connection.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

    }
    static void doRequest(NetworkStream socketStream)
     {
        
        StreamWriter sw = new StreamWriter(socketStream);
        StreamReader sr = new StreamReader(socketStream);

        ScoresOnTheDoors = new Dictionary<int,string>();

        try
        {
            socketStream.WriteTimeout = 2000;
            socketStream.ReadTimeout = 2000;
            Console.WriteLine("connection recived");
            string line = null;
            
            while (sr.Peek() != -1)
            {
                line += sr.ReadLine() + Environment.NewLine;
            }
            Console.WriteLine("response received " + line);
            string[] sections = line.Split(new string[] { " " ,"\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (line.Contains("respond"))
            {
                StreamReader readFromFile = new StreamReader("myFile.txt");
                string lineFromFile = readFromFile.ReadLine();

                sw.WriteLine(lineFromFile);
                sw.Flush();
                readFromFile.Close();
            }
            else if (sections.Length == 10)
            {
                StreamWriter writeToFile = new StreamWriter("myFile.txt");
                //if we get here name and location have been found, set and stored in the dictionary

                    writeToFile.Write(line);
                    writeToFile.Close();
                
            }
            // Console.WriteLine(sr.ReadToEnd());
        }
        catch (IOException e)
        {

            Console.WriteLine(e.Message);
        }
    }
    static void Main(string[] args)
    {
        runServer();
    }
}
