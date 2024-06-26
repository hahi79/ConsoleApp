//======================================
//	�^�[�����o�g�� ���C��
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
		30,         // �U����
		"�䂤����", // ���O
		"");        // �A�X�L�[�A�[�g
	SetCharacter(&boss,
		255,      // HP
		0,        // MP
		50,       // �U����
		"�܂���", // ���O
		"�@�@�`���`\n" // �A�X�L�[�A�[�g
		"�Ձi���M���j��"
		);
	SetCharacter(&zako,
		3,          // HP
		0,          // MP
		2,          // �U����
		"�X���C��", // ���O 
		"�^�E�D�E�_\n" // �A�X�L�[�A�[�g
		"�`�`�`�`�`"
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