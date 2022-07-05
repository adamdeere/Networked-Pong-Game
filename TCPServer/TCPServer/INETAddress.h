#pragma once
#include <winsock2.h>
#include <string>

class INETAddress
{
public:
	INETAddress(int &port, std::string &ip);
	~INETAddress();
	int getPortNumber();
	std::string getIP();
private:
	int portNumber;
	sockaddr_in _addr;
	std::string inIP;
};

