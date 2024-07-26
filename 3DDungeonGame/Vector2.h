//======================================
//	2Dベクター
//======================================
#ifndef __VECTOR2_H
#define __VECTOR2_H

typedef struct {
	int x;
	int y;
} Vector2;

typedef enum {
	DIR_NORTH,  // 北
	DIR_WEST,   // 西
	DIR_SOUTH,	// 南
	DIR_EAST,   // 東
	DIR_MAX,
} Direction;

// ベクター加算
void AddVector2(Vector2* a, Vector2* b);
// ４方向のベクター取得
Vector2 GetDirVector2(Direction d);

// 2Dベクターの加算
Vector2 Vector2Add(Vector2 a, Vector2 b);
// 2Dベクターの減算
Vector2 Vector2Sub(Vector2 a, Vector2 b);
// 2Dベクターの等価?
bool Vector2Equ(Vector2 a, Vector2 b);

#endif // __VECTOR2_H
