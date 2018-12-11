// mkutil.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "mkutil.h"
#include <tchar.h>
#include <psapi.h>
#include <tlhelp32.h>
#include <string>
using namespace std;

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


//function declaration
void DeleteSmartUpdateFile(CString fileName);

#define FILENAME _T("C:\\ProgramData\\NEC\\w10ri.flg")
#define PARTIALFILE _T("C:\\Recovery\\OEM\\Partial-C.swm")
// The one and only application object

CWinApp theApp;

using namespace std;

typedef struct _MYDATA {
	TCHAR *szText;
	DWORD dwValue;
} MYDATA;

BOOL GetPidByName(LPCTSTR lpApplicationName, DWORD& dwPid)
{
	DWORD dwSessionId = WTSGetActiveConsoleSessionId();
	BOOL bRet = FALSE;
	HANDLE hSnap = ::CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
	if (INVALID_HANDLE_VALUE != hSnap)
	{
		PROCESSENTRY32 oProcEntry = { 0 };
		oProcEntry.dwSize = sizeof(PROCESSENTRY32);
		if (::Process32First(hSnap, &oProcEntry))
		{
			do
			{
				if (_tcsicmp(oProcEntry.szExeFile, lpApplicationName) == 0)
				{
					dwPid = oProcEntry.th32ProcessID;
					DWORD dwExplorerSessId = 0;
					if (ProcessIdToSessionId(dwPid, &dwExplorerSessId) && dwExplorerSessId == dwSessionId)
					{
						bRet = TRUE;
					}
				}
			} while (::Process32Next(hSnap, &oProcEntry));
		}
		::CloseHandle(hSnap);
		hSnap = NULL;
	}
	return bRet;
}


BOOL FindMKProcess(LPCTSTR lpApplicationName)
{
	DWORD dwSessionId = WTSGetActiveConsoleSessionId();
	BOOL bRet = FALSE;
	HANDLE hSnap = ::CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
	if (INVALID_HANDLE_VALUE != hSnap)
	{
		PROCESSENTRY32 oProcEntry = { 0 };
		oProcEntry.dwSize = sizeof(PROCESSENTRY32);
		if (::Process32First(hSnap, &oProcEntry))
		{
			do
			{
				if (_tcsicmp(oProcEntry.szExeFile, lpApplicationName) == 0)
				{
					return TRUE;
				}
			} while (::Process32Next(hSnap, &oProcEntry));
		}
		::CloseHandle(hSnap);
		hSnap = NULL;
	}
	return bRet;
}


void PrintProcessNameAndId(DWORD processID) {
	TCHAR szProcessName[MAX_PATH] = TEXT("<unknown>");
	TCHAR szMKName[20] = TEXT("baidunetdisk.exe");
	HANDLE hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_ALL_ACCESS, FALSE, processID);

	if (NULL != hProcess) {
		HMODULE hMod;
		DWORD cbNeeded;
		
		if (EnumProcessModules(hProcess, &hMod, sizeof(hMod), &cbNeeded)) {
			GetModuleBaseName(hProcess, hMod, szProcessName, sizeof(szProcessName) / sizeof(TCHAR));
		}

		/*if (CompareString(NULL, LINGUISTIC_IGNORECASE, szProcessName, -1, szMKName, -1) == CSTR_EQUAL) {
			_tprintf(TEXT("%s (PID: %u)\n"), szProcessName, processID);
		}*/

		_tprintf(TEXT("%s (PID: %u)\n"), szProcessName, processID);

		CloseHandle(hProcess);
	}
}

VOID CALLBACK TimerAPCProc(LPVOID lpArg, DWORD dwTimerLowValue, DWORD dwTimerHighValue) {
	MYDATA *pMyData = (MYDATA *)lpArg;
	UNREFERENCED_PARAMETER(dwTimerLowValue);
	UNREFERENCED_PARAMETER(dwTimerHighValue);

	//_tprintf(TEXT("Message: %s\nValue:%d\n\n"), pMyData->szText, pMyData->dwValue);
	BOOL bFoundMKProcess = FALSE;
	BOOL bPartialFileExist = FALSE;

	LPCTSTR mkprocessname = TEXT("mkrcvcd.exe");
	bFoundMKProcess = FindMKProcess(mkprocessname);

	CFileFind finder;
	bPartialFileExist = finder.FindFile(PARTIALFILE);

	if (TRUE == bFoundMKProcess || FALSE == bPartialFileExist) {
		//MessageBox(NULL, _T("CBD started"), _T("Test"), MB_OK);
		//KillTimer();
	}else {
		DeleteSmartUpdateFile(FILENAME);
	}

	
}

void DeleteSmartUpdateFile(CString fileName) {
	CFileFind finder;
	BOOL bExist = finder.FindFile(fileName);
	if (bExist) {
		DeleteFile(fileName);
		MessageBox(NULL, _T("w10ri.flg delete success."), _T("Test"), MB_OK);
	}
}

BOOL CheckRunCondition() {

	CFileFind finder;
	BOOL bExist = FALSE;
	bExist = finder.FindFile(PARTIALFILE);
	if (FALSE == bExist) {
		return FALSE;
	}

	return TRUE;
}

void WriteSystemTime() {
	time_t seconds;
	seconds = time(NULL);
	_tprintf(TEXT("%ld\n"), seconds);

	char buffer[32] = std::to_string((double)seconds);
	snprintf(buffer, sizeof(buffer), "%ld", seconds);
}

int main()
{
    int nRetCode = 0;

    HMODULE hModule = ::GetModuleHandle(nullptr);

    if (hModule != nullptr)
    {
        // initialize MFC and print and error on failure
        if (!AfxWinInit(hModule, nullptr, ::GetCommandLine(), 0))
        {
            // TODO: change error code to suit your needs
            wprintf(L"Fatal Error: MFC initialization failed\n");
            nRetCode = 1;
        }
        else
        {
            // TODO: code your application's behavior here.
			// check partial-c.swm exist or not.

			WriteSystemTime();
			return 0;

			BOOL bShouldRun = FALSE;
			bShouldRun = CheckRunCondition();
			if (FALSE == bShouldRun) {
				return 0;
			}

			HANDLE hTimer;
			__int64 qwDueTime;
			BOOL bSuccess;
			LARGE_INTEGER liDueTime;
			MYDATA MyData;

			MyData.szText = TEXT("This is my data.");
			MyData.dwValue = 100;

			hTimer = CreateWaitableTimer(NULL, FALSE, TEXT("MYTimer"));
			if (hTimer != NULL) {
				qwDueTime = -5 * 10000000;
				liDueTime.LowPart = (DWORD)(qwDueTime & 0xFFFFFFFF);
				liDueTime.HighPart = (LONG)(qwDueTime >> 32);

			}

			bSuccess = SetWaitableTimer(hTimer, &liDueTime, 2000, TimerAPCProc, &MyData, FALSE);
			
			if (bSuccess) {
				for (; MyData.dwValue < 10000000; MyData.dwValue += 100) {
					SleepEx(INFINITE, TRUE);
				}
				
			}

			return 0;
        }
    }
    else
    {
        // TODO: change error code to suit your needs
        wprintf(L"Fatal Error: GetModuleHandle failed\n");
        nRetCode = 1;
    }

    return nRetCode;
}

