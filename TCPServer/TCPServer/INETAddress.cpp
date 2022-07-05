#include "stdafx.h"
#include "INETAddress.h"


INETAddress::INETAddress(int &port, std::string &ip) : portNumber(port), inIP(ip)
{
}


INETAddress::~INETAddress()
{
}

int INETAddress::getPortNumber()
{
	return portNumber;
}

std::string INETAddress::getIP()
{
	return inIP;
}
