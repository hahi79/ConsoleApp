//======================================
//	ターン制バトル メイン
//======================================
#include "Character.h"
#include "TurnBattle.h"
#include "Command.h"
#include "Utility.h"

int main()
{
	InitRand();
	Character player;
	Character boss;
	Character zako;
	TurnBattle btl;

	SetCharacter(&player,
		100,        // HP
		15,         // MP
		30,         // 攻撃力
		"ゆうしゃ", // 名前
		"");        // アスキーアート
	SetCharacter(&boss,
		255,      // HP
		0,        // MP
		50,       // 攻撃力
		"まおう", // 名前
		"　　Ａ＠Ａ\n" // アスキーアート
		"ψ（▼皿▼）ψ"
		);
	SetCharacter(&zako,
		3,          // HP
		0,          // MP
		2,          // 攻撃力
		"スライム", // 名前 
		"／・Д・＼\n" // アスキーアート
		"～～～～～"
		);

	SetTurnBattle(&btl, &player, &zako);// &boss);
	StartTurnBattle(&btl);
	IntroTurnBattle(&btl);
	bool isEnd = false;
	Command cmd;
	while (true) {
		cmd = GetPlayerCommand(&btl);
		isEnd = ExecPlayerTurn(&btl, cmd);
		if (isEnd) {
			break;
		}
		cmd = GetEnemyCommand();
		isEnd = ExecEnemyTurn(&btl, cmd);
		if (isEnd) {
			break;
		}
		NextTurnBattle(&btl);
	}
	return 0;
}