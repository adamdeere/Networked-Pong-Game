#pragma once
#include <string>
#include <winsock2.h>
#include <WS2tcpip.h>
#include <iostream>
class UDPSocket
{
public:
	UDPSocket(int &port);
	~UDPSocket();
	SOCKET getSocket();
	void closeSocket();
	bool InitUPDSocket();
	void initServer(std::string & inIp);
	sockaddr_in getServer();
	
private:
	int portNum;
	std::string ip;
	sockaddr_in server;
	SOCKET udpSocket;
};

