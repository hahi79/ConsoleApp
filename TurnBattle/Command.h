//======================================
//	�R�}���h
//======================================
#ifndef __COMMAND_H
#define __COMMAND_H
#include "Command.def"
#include "TurnBattle.h"

// �v���[���̃R�}���h�擾
Command GetPlayerCommand(TurnBattle* btl);
// �G�̃R�}���h�擾
Command GetEnemyCommand();

#endif __COMMAND_H
