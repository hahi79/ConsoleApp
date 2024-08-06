//======================================
//	�X�e�[�W
//======================================
#include "Stage.h"
#include "TurnBattle.h"
#include "UI.h"
#include "Utility.h"
#include <stdio.h>  // printf()
// �֐��v���b�g�^�C�v
static void ExecBattle(Stage* stage, Character* enemy);


void InitializeStage(Stage* stage, Character* player, Character* boss)
{
	stage->player = player;
	stage->boss = boss;
	StartCharacter(player);  // �v���[��������Ԃ�
	StartCharacter(boss);   // �{�X������Ԃ�
	stage->zakoPtr = 0;
}
// �U�R�G��o�^����
void RegistZako(Stage* stage, Character* zako)
{
	if (stage->zakoPtr < ZAKO_SIZE) {
		stage->zako[stage->zakoPtr++] = zako;
	}
}

void DrawMap(Stage* stage)
{
	ClearScreen();
	for (int y = stage->playerY - SCREEN_HEIGHT / 2; y < stage->playerY + SCREEN_HEIGHT / 2; y++) {
		for (int x = stage->playerX - SCREEN_WIDTH / 2; x < stage->playerX + SCREEN_WIDTH / 2; x++) {
			if (x == stage->playerX && y == stage->playerY) {
				printf("�E");
			}
			else {
				char c = GetMapData(stage->mapSpec, x, y);
				if (c == outOfMap) {
					c = stage->mapSpec->outData;
				}
				printf(GetMapAA(c));
			}
		}
		putchar('\n');
	}

	putchar('\n');
	IndicatePlayer(stage->player);
	putchar('\n');
}

// �}�b�v�`�F���W
void ChangeMap(Stage* stage, NextMap* p)
{
	MapSpec* spec = GetMapSpec(p->nextId);
	stage->mapSpec = spec;
	stage->playerX = p->nextX;
	stage->playerY = p->nextY;
}
// �X�^�[�g�}�b�v�ɃG���g���[
void ChangeStartMap(Stage* stage)
{
	NextMap next = { MAP_KING_CASTLE,4,6 };
	ChangeMap(stage, &next);
}
// �}�b�v�X�y�b�N�擾
MapSpec* GetMapSpec(Stage* stage)
{
	return stage->mapSpec;
}
int GetPlayerX(Stage* stage)
{
	return stage->playerX;
}
int GetPlayerY(Stage* stage)
{
	return stage->playerY;
}
void SetPlayerPosition(Stage* stage, int playerX, int playerY)
{
	stage->playerX = playerX;
	stage->playerY = playerY;
}
// �T�S�o�g�����s
void BattleZako(Stage* stage)
{
	int idx = GetRand(stage->zakoPtr);
	Character* zako = stage->zako[idx];
	ExecBattle(stage, zako);
}
// �{�X�o�g�����s
void BattleBoss(Stage* stage)
{
	ExecBattle(stage, stage->boss);
}
// �o�g�����s
static void ExecBattle(Stage* stage, Character* enemy)
{
	TurnBattle btl[1];

	SetTurnBattle(btl, stage->player, enemy);
	StartTurnBattle(btl);
	IntroTurnBattle(btl);
	bool isEnd = false;
	Command cmd;
	while (true) {
		cmd = GetPlayerCommand(btl);
		isEnd = ExecPlayerTurn(btl, cmd);
		if (isEnd) {
			break;
		}
		cmd = GetEnemyCommand();
		isEnd = ExecEnemyTurn(btl, cmd);
		if (isEnd) {
			break;
		}
		NextTurnBattle(btl);
	}
}
// �v���[�����S?
bool IsPlayerDead(Stage* stage)
{
	return IsDeadCharacter(stage->player);
}
// �{�X���S?
bool IsBossDead(Stage* stage)
{
	return IsDeadCharacter(stage->boss);
}