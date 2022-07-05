#include "stdafx.h"
#include "UDPSocket.h"


UDPSocket::UDPSocket(int &port) : portNum(port)
{
}


UDPSocket::~UDPSocket()
{
}

SOCKET UDPSocket::getSocket()
{
	return udpSocket;
}

void UDPSocket::closeSocket()
{
	closesocket(udpSocket);
}

bool UDPSocket::InitUPDSocket()
{
	udpSocket = socket(AF_INET, SOCK_DGRAM, 0);
	sockaddr_in hint;
	hint.sin_addr.S_un.S_addr = ADDR_ANY;
	hint.sin_family = AF_INET;
	hint.sin_port = htons(portNum);

	if (bind(udpSocket, (sockaddr*)&hint, sizeof(hint)) == SOCKET_ERROR)
	{
		return false;
	}
	//lets make a send socket 

	return true;
}

void UDPSocket::initServer(std::string & inIp)
{
	server.sin_family = AF_INET;
	server.sin_port = htons(portNum);
	inet_pton(AF_INET, inIp.c_str(), &server.sin_addr);
}

sockaddr_in UDPSocket::getServer()
{
	return server;
}


