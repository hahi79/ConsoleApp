//====================================
//	3D�_���W����  �L�����N�^
//======================================
#ifndef __CHARACTER_H
#define __CHARACTER_H

#include "Vector2.h"
#include "Direction.h"

typedef struct {
	Vector2 pos;      // ���W
	DIRECTION dir;    // �����Ă������
}Character;

void InitCharacter(Character* ch, Vector2 pos, DIRECTION dir);


#endif __CHARACTER_H
