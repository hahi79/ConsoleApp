//======================================
//      �������o�Y�� �X�e�[�W
//======================================
#include "Stage.h"
#include "BlockShape.h"
#include "Utility.h"
#include <stdio.h>   // printf(),putchar()
#include <string.h>  // memcpy()

// �֐��v���g�^�C�v
static void writeFallBlockToField(Stage* stage, FallBlock* fallBlock, Block block);
static bool isInField(int x, int y);

const Block defaultField[FIELD_HEIGHT][FIELD_WIDTH] = {
#define W  BLK_WALL
#define _  BLK_NONE
	{W,_,_,_,_,_,_,_,_,_,_,W}, // 0
	{W,_,_,_,_,_,_,_,_,_,_,W}, // 1
	{W,_,_,_,_,_,_,_,_,_,_,W}, // 2
	{W,_,_,_,_,_,_,_,_,_,_,W}, // 3
	{W,_,_,_,_,_,_,_,_,_,_,W}, // 4
	{W,_,_,_,_,_,_,_,_,_,_,W}, // 5
	{W,_,_,_,_,_,_,_,_,_,_,W}, // 6
	{W,_,_,_,_,_,_,_,_,_,_,W}, // 7
	{W,_,_,_,_,_,_,_,_,_,_,W}, // 8
	{W,_,_,_,_,_,_,_,_,_,_,W}, // 9
	{W,_,_,_,_,_,_,_,_,_,_,W}, // 10
	{W,_,_,_,_,_,_,_,_,_,_,W}, // 11
	{W,W,_,_,_,_,_,_,_,_,W,W}, // 12
	{W,W,_,_,_,_,_,_,_,_,W,W}, // 13
	{W,W,_,_,_,_,_,_,_,_,W,W}, // 14
	{W,W,W,_,_,_,_,_,_,W,W,W}, // 15
	{W,W,W,W,_,_,_,_,W,W,W,W}, // 16
	{W,W,W,W,W,W,W,W,W,W,W,W}, // 17
#undef _
#undef W
};

const char* blockAA[] = {
	"�@", //BLK_NONE
	"�{", //BLK_WALL
	"��", //BLK_FIX
	"��", //BLK_FALL
};
// �X�e�[�W������
void InitializeStage(Stage* stage)
{
	memcpy(stage->field, defaultField, sizeof(stage->field));
	stage->isGameOver = false;
	SetupFallBlock(stage);
	DrawScreen(stage);
}
// �w����W�̃t�B�[���h�Z�b�g
void SetField(Stage* stage, int x, int y, Block block)
{
	if (isInField(x, y)) {
		stage->field[y][x] = block;
	}
}
// �w����W�̃t�B�[���h�擾
Block GetField(Stage* stage, int x, int y)
{
	if (isInField(x, y)) {
		return stage->field[y][x];
	}
	return BLK_WALL;
}
// �����u���b�N(+�ړ��I�t�Z�b�g)���t�B�[���h�Փ�?
bool BlockIntersectField(Stage* stage,FallBlock *fallBlock)
{
	BlockShape *shape = &fallBlock->shape;
	for (int y = 0; y<shape->size; y++) {
		int fieldY = fallBlock->y + y;
		for (int x = 0; x < shape->size; x++) {
			if (GetShapePattern(shape, x, y)) {
				int fieldX = fallBlock->x + x;
				if (GetField(stage, fieldX, fieldY) != BLK_NONE) {
					return true;
				}
			}
		}
	}
	return false;
}

// �������s�������āA�ォ��l�߂�
void EraseLine(Stage* stage)
{
	for (int y = 0; y < FIELD_HEIGHT; y++) {
		bool completed = true;
		// �s����������?
		for (int x = 0; x < FIELD_WIDTH; x++) {
			if (GetField(stage, x, y) == BLK_NONE) {
				completed = false;
				break;
			}
		}
		if (completed) {
			// �s������
			for (int x = 0; x < FIELD_WIDTH; x++) {
				if (GetField(stage, x, y) == BLK_FIX) {
					SetField(stage, x, y, BLK_NONE);
				}
			}
			// �ォ��l�߂�
			for (int x = 0; x < FIELD_WIDTH; x++) {
				for (int yy = y; yy >= 0; yy--) {
					if (GetField(stage, x, yy) == BLK_WALL) {
						break;
					}
					if (yy == 0) {
						// �ŏ�i�Ȃ�A�󔒂�
						SetField(stage, x, yy, BLK_NONE);
					}
					else {
						// �ŏ�i�łȂ���΁A1���
						Block blk = GetField(stage, x, yy - 1);
						if( blk!= BLK_WALL){
							SetField(stage, x, yy, blk);
						}
						else {
							SetField(stage, x, yy, BLK_NONE);
						}
					}
				} // for yy
			} // for x
		} // completed
	} // for y
}
// ��ʕ`��
void DrawScreen(Stage* stage)
{
	// �`��p���[�N
	Stage screen[1];
	memcpy(screen->field, stage->field, sizeof(stage->field));
	// �������̃u���b�N���L��
	writeFallBlockToField(screen, &stage->fallBlock, BLK_FALL);
	// �`��
	ClearScreen();
	for (int y = 0; y < FIELD_HEIGHT; y++) {
		for (int x = 0; x < FIELD_WIDTH; x++) {
			Block blk = GetField(screen, x, y);
			printf("%s", blockAA[blk]);
		}
		putchar('\n');
	}
	//PrintFallBlock(&stage->fallBlock);
}
// �u���b�N��1����������
void MoveDownFallBlock(Stage* stage)
{
	FallBlock fallBlock =GetFallBlock( stage);
	MoveFallBlock(&fallBlock, 0, 1);
	// y+1���Փ˂��邩?
	if (BlockIntersectField(stage,&fallBlock)) {
		// �����u���b�N�����݈ʒu��fix
		writeFallBlockToField(stage, &stage->fallBlock, BLK_FIX);
		EraseLine(stage);
		// �V���ȗ����u���b�N�Z�b�g�A�b�v
		SetupFallBlock(stage);
		if (BlockIntersectField(stage,&stage->fallBlock)) {
			stage->isGameOver = true;
		}
	}
	else {
		SetFallBlock(stage, &fallBlock);
	}
	DrawScreen(stage);
}
// �����u���b�N�̃Z�b�g�A�b�v
void SetupFallBlock(Stage* stage) 
{
	FallBlock* fallBlock = &stage->fallBlock;
	// ���W�́A��Ӓ���
	int x = FIELD_WIDTH / 2 - fallBlock->shape.size / 2;
	SetRandomFallBlock(fallBlock,x,0);
}
// �����u���b�N���擾
FallBlock GetFallBlock(Stage* stage)
{
	return stage->fallBlock;
}
// �����u���b�N���Z�b�g
void SetFallBlock(Stage* stage, FallBlock* fallBlock)
{
	stage->fallBlock = *fallBlock;
}
// �Q�[���I�[�o?
bool IsGameOver(Stage* stage)
{
	return stage->isGameOver;
}
// �����u���b�N���t�B�[���h�֋L������
static void writeFallBlockToField(Stage* stage, FallBlock* fallBlock, Block writeData)
{
	BlockShape* shape = &fallBlock->shape;
	for (int y = 0; y < shape->size; y++) {
		int fieldY = fallBlock->y + y;
		for (int x = 0; x < shape->size; x++) {
			int fieldX = fallBlock->x + x;
			if (GetShapePattern(shape, x, y)) {
				if (GetField(stage, fieldX, fieldY) == BLK_NONE) {
					SetField(stage, fieldX, fieldY, writeData);
				}
			}
		}
	}
}
// ���W�̓t�B�[���h��?
static bool isInField(int x, int y) 
{
	return 0 <= x && x < FIELD_WIDTH
		&& 0 <= y && y < FIELD_HEIGHT;
}