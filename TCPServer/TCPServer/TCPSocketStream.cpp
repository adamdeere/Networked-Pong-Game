#include "stdafx.h"
#include "TCPSocketStream.h"


TCPSocketStream::TCPSocketStream(TCPSocket* listenSocket) : socket(listenSocket), socketIntilized(false)
{
	
}


TCPSocketStream::~TCPSocketStream()
{
}

bool TCPSocketStream::InitTCPStreamSocket()
{
	sockaddr_in client;
	int clientSize = sizeof(client);
	//this needs a thread to continue along with the rest of the program
	clientSocket = accept(socket->getSocket(), (SOCKADDR*)&client, &clientSize);

	if (clientSocket == INVALID_SOCKET)
	{
		std::cerr << "Accept failed with " << WSAGetLastError() << std::endl;
		return false;
	}
	
	char host[NI_MAXHOST];
	char service[NI_MAXHOST];

	ZeroMemory(host, NI_MAXHOST);
	ZeroMemory(service, NI_MAXHOST);

	if (getnameinfo((sockaddr*)&client, sizeof(client), host, NI_MAXHOST, service, NI_MAXSERV, 0) == 0)
	{
		std::cout << host << " connected on port " << service << std::endl;
	
	}
	else
	{
		inet_ntop(AF_INET, &client.sin_addr, host, NI_MAXHOST);
		std::cout << host << " connected on port " << ntohs(client.sin_port) << std::endl;
		
	}
	//close socket
	socket->closeSocket();
	socketIntilized = true;
	//end thread
	return true;

}

SOCKET TCPSocketStream::getClientSocket()
{
	return clientSocket;
}

void TCPSocketStream::closeSocket()
{
	closesocket(clientSocket);
}

bool TCPSocketStream::isSocketIntilized()
{
	return socketIntilized;
}
