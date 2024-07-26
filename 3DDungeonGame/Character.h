//====================================
//	3Dダンジョン  キャラクタ
//======================================
#ifndef __CHARACTER_H
#define __CHARACTER_H

#include "Vector2.h"
#include "Direction.h"

typedef struct {
	Vector2 pos;      // 座標
	Direction dir;    // 向いている方位
}Character;

// キャラ初期化
void InitCharacter(Character* ch, Vector2 pos, Direction dir);


#endif __CHARACTER_H
