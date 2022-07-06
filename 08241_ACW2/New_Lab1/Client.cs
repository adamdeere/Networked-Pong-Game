//Demonstrate Sockets
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

internal class Client
{
    public StreamWriter writeToServer;
    public StreamReader readFromServer;
    public StreamWriter writeToMaster;
    public StreamReader readFromMaster;
    public TcpClient serverClientLookUp;
    public TcpClient masterClient;

    public string lineFromSlave;
    public string lineFromServer;
    public string lineFromMaster;
    public string serverAddress;
    public string masterAddress;
    public int port;

    public Client(string inServerAddress, int inPort, string inMasterAddress)
    {
        bool connected = false;
        serverAddress = inServerAddress;
        port = inPort;
        masterAddress = inMasterAddress;

        //serverClient = new TcpClient();
        masterClient = new TcpClient();
        //Thread t = new Thread(() =>
        //{
        //    try
        //    {
        //        serverClient.Connect(serverAddress, inPort);
        //        writeToServer = new StreamWriter(serverClient.GetStream());
        //        readFromServer = new StreamReader(serverClient.GetStream());
        //        writeToServer.WriteLine("check");
        //        writeToServer.Flush();
        //        lineFromServer = readFromServer.ReadLine();
        //        Console.WriteLine(lineFromServer);
        //        connected = true;
        //    }
        //    catch
        //    {
        //        Console.WriteLine("failed to connect");
        //    }
        //});
        //t.Start();

        Thread a = new Thread(() =>
        {
            try
            {
                masterClient.Connect(masterAddress, port);
                writeToMaster = new StreamWriter(masterClient.GetStream());
                readFromMaster = new StreamReader(masterClient.GetStream());
                Console.WriteLine("connected to master");
                connected = true;
            }
            catch
            {
                Console.WriteLine("failed to connect to master");
            }
        });
        a.Start();
    }

    public void startSever()
    {
    }

    /// <summary>
    /// this is a method that will ping a request to the server for the high scores
    /// and return names and scores in the form of a string that can be choipped
    /// up and returned to the game
    /// </summary>
    public void LookUp(string server, int port)
    {
        int inPort = port;
        string ipAddress = server;
        try
        {
            serverClientLookUp = new TcpClient();
            serverClientLookUp.Connect(ipAddress, inPort);
            writeToServer = new StreamWriter(serverClientLookUp.GetStream());
            readFromServer = new StreamReader(serverClientLookUp.GetStream());

            // serverClient.Connect(ipAddress, inPort);
            writeToServer.WriteLine("respond");
            writeToServer.Flush();
            lineFromServer = readFromServer.ReadLine();
            serverClientLookUp.Close();
        }
        catch
        {
            Console.WriteLine("Server not responding");
        }
    }

    /// <summary>
    /// this method will broadcast the positions to the slave
    /// </summary>

    //public void checkConnection(string master, int Port)
    //{
    //    do
    //    {
    //        writeToServer.WriteLine("confirm");
    //        writeToServer.Flush();
    //        serverClient.SendTimeout = 1000;
    //        lineFromMaster = readFromServer.ReadLine();

    //    } while (lineFromMaster != "ok");
    //}

    public void slaveMoveUp(string master, int Port)
    {
        writeToServer.WriteLine("up");
        writeToServer.Flush();
    }

    public void slaveMoveDown(string master, int Port)
    {
        writeToServer.WriteLine("down");
        writeToServer.Flush();
    }

    /// <summary>
    /// once the game has updated the new high score (if any) this method will send the infomation back
    /// to the server to be updated in the dictionary, written to a text file on the other
    /// side so as to act as storage space inside the server
    /// </summary>
    public void Update(int scoreOne, string nameOne, int scoreTwo, string nameTwo, int scoreThree, string nameThree, int scoreFour, string nameFour, int scoreFive, string nameFive)
    {
        string names = nameOne;
        string name2 = nameTwo;
        string name3 = nameThree;
        string name4 = nameFour;
        string name5 = nameFive;

        int scores = scoreOne;
        int score2 = scoreTwo;
        int scores3 = scoreThree;
        int score4 = scoreFour;
        int score5 = scoreFive;
        try
        {
            TcpClient serverUpdate = new TcpClient();
            serverUpdate.Connect(serverAddress, port);
            StreamWriter sw = new StreamWriter(serverUpdate.GetStream());

            sw.WriteLine(scores + " " + names + " " + score2 + " " + name2 + " " + scores3 + " " + name3 + " " + score4 + " " + name4 + " " + score5 + " " + name5);
            sw.Flush();
            serverUpdate.Close();
        }
        catch
        {
        }
    }
}