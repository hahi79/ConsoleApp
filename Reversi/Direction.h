//======================================
//	���o�[�V  �W����
//======================================
#ifndef __DIRECTION_H
#define __DIRECTION_H

#include "Vector2.h"
// �W����
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

// �W�����̃x�N�^�[�擾
Vector2 GetDirVector2(DIRECTION d);

#endif //  __DIRECTION_H
