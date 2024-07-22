//======================================
//	ドットイートゲーム　Character
//======================================
#include "Character.h"
#include "Vector2.h"

// キャラクター初期化
void InitCharacter(Character* ch, Chara id, Vector2 initPos)
{
	ch->id = id;
	ch->pos = initPos;
	ch->lastPos = initPos;
}

// キャラクター移動
void MoveCharacter(Character* ch, Vector2 newPos)
{
	ch->lastPos = ch->pos;
	ch->pos = newPos;
}
