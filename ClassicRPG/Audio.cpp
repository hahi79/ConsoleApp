//=============================================================
//	オーディオ
// ------------------------------------------------------------
// 準備: 「プロジェクト」→「プロパティ」
// (1)C/C++「全般」追加のインクルードディレクトリーに
//   c:\Program Files(x86)\Microsoft SDKs\Windows\v7.1A\Include
// (2)リンカー「全般」追加のライブラリーディレクトリーに
//   c:\Program Files(x86)\Microsoft SDKs\Windows\v7.1A\Lib
// (3)リンカー「入力」追加の依存ファイルに
//   WinMM.lib
//=============================================================
#undef UNICODE
#include <windows.h>
#include <MMSystem.h>
#include <stdio.h>

// wavファイルを再生
void PlayWav(const char* filename)
{
	bool ret = PlaySound(filename, nullptr, SND_ASYNC);
	if (ret == false) {
		printf("PlayWav(% s) fail.", filename);
	}
	
}

void PlayDamageSE()
{

}