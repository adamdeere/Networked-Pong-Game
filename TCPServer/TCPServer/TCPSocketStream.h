#pragma once
#include <winsock2.h>
#include "TCPSocket.h"
#include <iostream>
#include <WS2tcpip.h>

class TCPSocketStream
{
public:
	TCPSocketStream(TCPSocket* listenSocket);
	~TCPSocketStream();
	bool InitTCPStreamSocket();
	SOCKET getClientSocket();
	void closeSocket();
	bool isSocketIntilized();
private:
	TCPSocket* socket;
	SOCKET clientSocket;
	sockaddr_in client;
	int clientSize;
	bool socketIntilized;
};

