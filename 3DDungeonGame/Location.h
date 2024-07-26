//======================================
//	���P�[�V����
//======================================
#ifndef __LOCATION_H
#define __LOCATION_H

#include "Direction.h"
#include "Vector2.h"

// �v���[������̑��Έʒu
typedef enum {
	LOC_FRONT_LEFT,  // ���O
	LOC_FRONT_RIGHT, // �E�O
	LOC_FRONT,       // �O
	LOC_LEFT,        // ��
	LOC_RIGHT,       // �E
	LOC_CENTER,      // ���S
} Location;

// �����ƃ��P�[�V��������A�I�t�Z�b�g�x�N�^�[�擾
Vector2 GetLocationVector2(Direction dir, Location loc);
// �����ƃ��P�[�V��������A�A�X�L�[�A�[�g��������擾
const char* GetLocationAA(Direction dir,Location loc);

#endif // __LOCATION_H
