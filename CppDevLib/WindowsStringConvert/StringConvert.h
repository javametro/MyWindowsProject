#pragma once
#include "stdafx.h"
#include <string>
using namespace std;

class StringConvert {
private:

public:
	CString string2CString(string str);
	string CString2string(CString str);
	char* string2charpointer(string str);
	string charpointer2string(char*);
	char* CString2charpointer(CString);
	CString charpointer2CString(char*);
};