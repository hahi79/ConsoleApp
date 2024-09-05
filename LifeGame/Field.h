//========================================
//      ���C�t�Q�[��:�t�B�[���h
//========================================
#ifndef __FIELD_H
#define __FIELD_H
#include "Pattern.h"

typedef struct {
	int width;       // �t�B�[���h����
	int height;      // �t�B�[���h�c��
	bool* cells[2];  // �t�B�[���h�z��(width�~height) 2�Z�b�g
	int currentIdx;  // �J�����g�̃t�B�[���h�Z�b�g(0 or 1)
	bool isLoopCells; // �t�B�[���h�̍��E�A�㉺�����[�v���Ă��邩?
	int stepCount;    // �X�e�b�v��
} Field;

// ������
void InitializeField(Field* field, int width, int height, bool isLoopCells);
// �t�B�[���h�̕`��
void DrawField(Field* field);
// �V�~�����[�V����1�X�e�b�v�i�߂�
void StepSimulation(Field* field);
// �p�^�[���̓]��
void TransferPattern(Field* field,Pattern *pattern);
// ��n��
void FinalizeField(Field* field);

#endif // __FIELD_H

