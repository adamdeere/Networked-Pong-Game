#pragma once
#include <winsock2.h>
#include "INETAddress.h"
#include <iostream>

class TCPSocket
{
public:
	TCPSocket(INETAddress &iAdd);
	~TCPSocket();

	bool initSocket();
	void closeSocket();
	SOCKET getSocket();

private:
	SOCKET listening;
	INETAddress inetAdress;
};

