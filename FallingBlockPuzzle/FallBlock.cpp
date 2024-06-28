//======================================
//      �������o�Y�� �����u���b�N
//======================================
#include "FallBlock.h"
#include "Utility.h"  // GetRand()
#include <stdio.h>

// �����u���b�N�̈ړ�
void MoveFallBlock(FallBlock* fallBlock, int ofsx, int ofsy)
{
	fallBlock->x += ofsx;
	fallBlock->y += ofsy;
}
// �����u���b�N�̉�]
void RotateFallBlock(FallBlock* fallBlock)
{
	RotateShape(&fallBlock->shape);
}
// �����_���ȗ����u���b�N���Z�b�g
void SetRandomFallBlock(FallBlock* fallBlock,int x,int y)
{
	int idx = GetRand(blockShapesSize);
	SetShape(&fallBlock->shape, idx);
	// 0�`3��A��]����
	int rotateCount = GetRand(4);
	for (int i = 0; i < rotateCount; i++) {
		RotateShape(&fallBlock->shape);
	}
	fallBlock->x = x;
	fallBlock->y = y;
}
// �����u���O���v�����g
void PrintFallBlock(FallBlock* fallBlock)
{
	printf("(%d,%d)\n", fallBlock->x, fallBlock->y);
	PrintShape(&fallBlock->shape);
}
