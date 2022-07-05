using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PongGame
{
    class MasterServer
    {
        public StreamReader readFromSlave;
        public StreamWriter writeToSlave;
        public Socket connection;
        public NetworkStream socketStream;
        public string lineFromSlave;
        public MasterServer()
        {

            Thread a = new Thread(() =>
            {
                TcpListener listener = new TcpListener(IPAddress.Any, 43);
                listener.Start();
                connection = listener.AcceptSocket();
                socketStream = new NetworkStream(connection);
                writeToSlave = new StreamWriter(socketStream);
                readFromSlave = new StreamReader(socketStream);
                Console.WriteLine("listening to slave");

            });
            a.Start();
        }
        public void confirmConnection()
        {
            do
            {
                socketStream.WriteTimeout = 1000;
                socketStream.ReadTimeout = 1000;
                lineFromSlave = readFromSlave.ReadLine();
                if (lineFromSlave == "confirm")
                {
                    writeToSlave.WriteLine("ok");
                    writeToSlave.Flush();

                }

            } while (lineFromSlave != "confirm");

        }
    }
}
