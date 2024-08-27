//======================================
//	�h�b�g�C�[�g�Q�[�� �f�B���N�V����
//======================================
#include "Direction.h"
#include "Vector2.h"
#include <assert.h>

static Vector2 dirVector2[] = {
	{ 0,-1},  //DIR_UP,
	{-1, 0},  //DIR_LEFT,
	{ 0, 1},  //DIR_DOWN,
	{ 1, 0},  //DIR_RIGHT,
};

// �S�����̃x�N�^�[�擾
Vector2 GetDirVector2(DIRECTION d)
{
	assert(0 <= d && d < DIR_MAX);
	return dirVector2[d];
}
