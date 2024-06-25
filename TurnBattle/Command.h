//======================================
//	コマンド
//======================================
#ifndef __COMMAND_H
#define __COMMAND_H
#include "Command.def"
#include "TurnBattle.h"

// プレーヤのコマンド取得
Command GetPlayerCommand(TurnBattle* btl);
// 敵のコマンド取得
Command GetEnemyCommand();

#endif __COMMAND_H
