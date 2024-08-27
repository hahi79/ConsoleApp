//======================================
//	�h�b�g�C�[�g�Q�[�� �f�B���N�V����
//======================================
#ifndef __DIRECTION_H
#define __DIRECTION_H

#include "Vector2.h"

// �S����
typedef enum {
	DIR_UP,
	DIR_LEFT,
	DIR_DOWN,
	DIR_RIGHT,
	DIR_MAX,
} DIRECTION;

// �S�����̃x�N�^�[�擾
Vector2 GetDirVector2(DIRECTION d);

#endif // __DIRECTION_H
