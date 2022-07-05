#include "stdafx.h"
#include "WSA.h"


WSA::WSA()
{
	WORD wVersionRequested = MAKEWORD(2, 0);

	// Startup windows sockets
	_status = !WSAStartup(wVersionRequested, &_wsaData);
}


WSA::~WSA()
{
	WSACleanup();
}
