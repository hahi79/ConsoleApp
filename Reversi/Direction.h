//======================================
//	リバーシ  ８方向
//======================================
#ifndef __DIRECTION_H
#define __DIRECTION_H

#include "Vector2.h"
// ８方向
typedef enum {
	DIR_UP,
	DIR_UP_LEFT,
	DIR_LEFT,
	DIR_DOWN_LEFT,
	DIR_DOWN,
	DIR_DOWN_RIGHT,
	DIR_RIGHT,
	DIR_UP_RIGHT,
	DIR_MAX,
} DIRECTION;

// ８方向のベクター取得
Vector2 GetDirVector2(DIRECTION d);

#endif //  __DIRECTION_H
