//====================================
//	3Dダンジョン  キャラクタ
//======================================
#ifndef __CHARACTER_H
#define __CHARACTER_H

#include "Vector2.h"
#include "Direction.h"

typedef struct {
	Vector2 pos;      // 座標
	DIRECTION dir;    // 向いている方位
}Character;

void InitCharacter(Character* ch, Vector2 pos, DIRECTION dir);


#endif __CHARACTER_H
