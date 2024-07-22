//======================================
//	�h�b�g�C�[�g�Q�[���@Character
//======================================
#include "Character.h"
#include "Vector2.h"

// �L�����N�^�[������
void InitCharacter(Character* ch, Chara id, Vector2 initPos)
{
	ch->id = id;
	ch->pos = initPos;
	ch->lastPos = initPos;
}

// �L�����N�^�[�ړ�
void MoveCharacter(Character* ch, Vector2 newPos)
{
	ch->lastPos = ch->pos;
	ch->pos = newPos;
}
