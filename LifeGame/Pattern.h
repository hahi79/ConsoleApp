//========================================
//      ���C�t�Q�[��:�p�^�[��
//========================================
#ifndef __PATTERN_H
#define __PATTERN_H

typedef struct {
	int width;         // �p�^�[���̉���
	int height;        // �p�^�[���̏c��
	bool* data;        // �p�^�[��
	const char* name;  // �p�^�[���̖��O
	bool isLoopCells;  // �t�B�[���h�̍��E�A�㉺�����[�v���Ă��邩?
} Pattern;

// �p�^�[���I�����s��
Pattern* SelectPattern();

#endif //  __PATTERN_H
