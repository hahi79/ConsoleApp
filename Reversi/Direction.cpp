//======================================
//	リバーシ  ８方向
//======================================
#include "Direction.h"
#include "Vector2.h"
#include <assert.h>

// ８方向のベクター取得
static Vector2 dirVector2[] = {
	{0,-1}, //DIR_UP
	{-1,-1},//DIR_UP_LEFT
	{-1,0 },//DIR_LEFT
	{-1,1},	//DIR_DOWN_LEFT,
	{0,1},  //DIR_DOWN,
	{1,1},  //DIR_DOWN_RIGHT,
	{1,0},  //DIR_RIGHT,
	{1,-1}, //DIR_UP, RIGHT,
};

Vector2 GetDirVector2(DIRECTION d)
{
	assert(0 <= d && d < DIR_MAX);
	return dirVector2[d];
}