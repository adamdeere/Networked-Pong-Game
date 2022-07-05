// TCPServer.cpp : Defines the entry point for the console application.
//
//#pragma comment(lib, "ws2_32.lib") //this is added via proerties->linker->input

#include "stdafx.h"
#include "WSA.h"
#include "INETAddress.h"
#include "TCPSocket.h"
#include "TCPSocketStream.h"
#include "UDPSocket.h"
// TCP SERVER
using namespace std;
int main()
{
	int port = 9171;
	WSA wsa;

	std::string receivedIP = "127.0.0.1";
	//if we get here, we have established a connection with a host
	//now we need to establish a TCP connection. this is done on the peer to aviod host relaince
	TCPSocket* listenSocket = new TCPSocket(INETAddress(port, receivedIP));

	//this needs a condition to say return true
	if (listenSocket->initSocket() == false)
		return -1;

	//wait for connection

	TCPSocketStream* streamSocket = new TCPSocketStream(listenSocket);

	//this needs a condition to return true
	if (streamSocket->InitTCPStreamSocket() == false)
		return -1;
	//create socket
	cout << "waiting for client to connect " << endl;
	//bind the ip address to an port and socket
	
	UDPSocket* udpSocket = new UDPSocket(port);
	if (udpSocket->InitUPDSocket() == false)
	{
		return -1;
	}

	sockaddr_in clientFromUDP;
	int clientLength = sizeof(clientFromUDP);
	ZeroMemory(&clientFromUDP, clientLength);
	//this is where the program gets hung up. would this go into the socket accept class?

	char buffer[256];
	char clientIP[256];
	cout << "server  listening > " << endl;
	//main loop
	while (true)
	{
		ZeroMemory(&buffer, clientLength);
		int bytesIn = recvfrom(udpSocket->getSocket(), buffer, 256, 0,(sockaddr*)&clientFromUDP, &clientLength);
		if (bytesIn == SOCKET_ERROR)
		{
			cout << "error > " << endl;
			continue;
		}
		
		ZeroMemory(clientIP, 256);

		inet_ntop(AF_INET, &clientFromUDP.sin_addr, clientIP, 256);

		cout << "message recived from " << clientIP << " : " << buffer << endl;

		break;
	}

	//set up a send socket  

	/*string receivedIP = (string)clientIP;
	sockaddr_in peerOut;
	peerOut.sin_family = AF_INET;
	peerOut.sin_port = htons(port);
	inet_pton(AF_INET, receivedIP.c_str(), &peerOut.sin_addr);

	string ak = "receive";
	int sentOk = sendto(udpSocket->getSocket(), ak.c_str(), ak.size() + 1, 0, (sockaddr*)&peerOut, sizeof(peerOut));
	if (sentOk == SOCKET_ERROR)
	{
		cout << "send error" << WSAGetLastError() << endl;
	}*/

	

	do
	{
		//i have the connecting ipadress here this is the tcp stuff

		ZeroMemory(buffer, 256);
		int bytesRecvived = recv(streamSocket->getClientSocket(), buffer, 256, 0);
		if (bytesRecvived == SOCKET_ERROR)
		{
		cerr << "Receive failed with " << WSAGetLastError() << endl;
		break;
		}
		else if (bytesRecvived == 0)
		{
		cout << "client disconnected " << endl;
		break;
		}
		else if (send(streamSocket->getClientSocket(), buffer, bytesRecvived + 1, 0) == SOCKET_ERROR)
		{
		cerr << "Send failed with " << WSAGetLastError() << endl;
		}
		cout << "message from client " << buffer << endl;
	} while (true);

	//close the socket
	streamSocket->closeSocket();
	udpSocket->closeSocket();
	// Delay
	cout << "Waiting..." << endl; 
	char ch; 
	cin >> ch;
	// Cleanup windows sockets
	
	
    return 0;
}

