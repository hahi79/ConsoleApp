//======================================
//	2D�x�N�^�[
//======================================
#ifndef __VECTOR2_H
#define __VECTOR2_H

typedef struct {
	int x;
	int y;
} Vector2;



// �x�N�^�[���Z
void AddVector2(Vector2* a, Vector2* b);
// �S�����̃x�N�^�[�擾
Vector2 GetDirVector2(DIRECTION d);

// 2D�x�N�^�[�̉��Z
Vector2 Vector2Add(Vector2 a, Vector2 b);
// 2D�x�N�^�[�̌��Z
Vector2 Vector2Sub(Vector2 a, Vector2 b);
// 2D�x�N�^�[�̓���?
bool Vector2Equ(Vector2 a, Vector2 b);

#endif // __VECTOR2_H