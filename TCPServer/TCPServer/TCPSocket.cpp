#include "stdafx.h"
#include "TCPSocket.h"


TCPSocket::TCPSocket(INETAddress &iAdd) : inetAdress(iAdd)
{
}


TCPSocket::~TCPSocket()
{
}

bool TCPSocket::initSocket()
{
	listening = socket(AF_INET, SOCK_STREAM, 0);
	if (listening == INVALID_SOCKET)
	{
		std::cerr << "Create socket failed" << std::endl;
		return false;
	}
	sockaddr_in peer;
	peer.sin_family = AF_INET;
	peer.sin_port = htons(inetAdress.getPortNumber()); // port 9171
	peer.sin_addr.S_un.S_addr = htonl(INADDR_ANY);// Create listening socket

	if (bind(listening, (sockaddr *)&peer, sizeof(peer)) == SOCKET_ERROR)
	{
		std::cerr << "Bind failed with " << WSAGetLastError() << std::endl;
		return false;
	}
	//teel winsock to listen

	else if (listen(listening, SOMAXCONN) == SOCKET_ERROR)
	{
		std::cerr << "Listen failed with " << WSAGetLastError() << std::endl;
		return false;
	}
	return true;
}

void TCPSocket::closeSocket()
{
	closesocket(listening);
}

SOCKET TCPSocket::getSocket()
{
	return listening;
}
