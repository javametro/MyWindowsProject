#include "stdafx.h"
#include <string>
#include <Windows.h>
#include "StringConvert.h"
using namespace std;

CString StringConvert::string2CString(string str) {
	CString strTemp = _T("");
	strTemp.Format(_T("%s"), str.c_str());
	return strTemp;
}
