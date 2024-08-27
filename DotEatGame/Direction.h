//======================================
//	ドットイートゲーム ディレクション
//======================================
#ifndef __DIRECTION_H
#define __DIRECTION_H

#include "Vector2.h"

// ４方向
typedef enum {
	DIR_UP,
	DIR_LEFT,
	DIR_DOWN,
	DIR_RIGHT,
	DIR_MAX,
} DIRECTION;

// ４方向のベクター取得
Vector2 GetDirVector2(DIRECTION d);

#endif // __DIRECTION_H
