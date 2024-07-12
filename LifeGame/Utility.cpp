//======================================
//	ユーティリティ
//======================================
#include "Utility.h" // eKey
#include <stdlib.h> // srand()
#include <time.h>  // time()
#include <windows.h> // GetStdHandle(),SetConsoleMode()
#include <stdio.h> // printf()
#include <conio.h> // _getch()

const char* EscBLACK   = "\033[30m";
const char* EscRED     = "\033[31m";
const char* EscGREEN   = "\033[32m";
const char* EscYELLOW  = "\033[33m";
const char* EscBLUE    = "\033[34m";
const char* EscMAZENTA = "\033[35m";
const char* EscCYAN    = "\033[36m";
const char* EscWHITE   = "\033[37m";
const char* EscDEFAULT = "\033[39m";

// 乱数初期化
void InitRand()
{
	srand((unsigned int)time(NULL));
}
// 0～max-1 の一様乱数を得る
int GetRand(int max)
{
	int x = (rand() * max) / RAND_MAX;
	return x;
}
// キー入力を待つ
void WaitKey()
{
	_getch();
}

// キー取得
Key GetKey()
{
	int key=_getch();
	if (key == 0xe0) {
		int key2 = _getch();
		key = 0xe000 + key2;
	}
	return (Key)key;
}
// キー入力ありか?
bool KeyAvailable()
{
	return _kbhit();
}
// スクリーン消去
void ClearScreen()
{
#if false
	// cmd.exe を呼び出して cls を行う
	system("cls");
#else
	static bool s_modeSet = false;

	// 画面モードを切り替えてエスケープシーケンスが使えるようにする
	if (s_modeSet == false) {
		s_modeSet = true;
		HANDLE h = GetStdHandle(-11);
		SetConsoleMode(h, 0x07);
	}
	printf("\x1b[2J"	// 画面クリア
		"\x1b[0;0H");	// カーソルを0,0に
#endif
}
