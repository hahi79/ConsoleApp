//======================================
//	ドットイートゲーム　Character
//======================================
#ifndef __CHARACTER_H
#define __CHARACTER_H

#include "Vector2.h"

typedef enum {
	CHARA_PLAYER, // プレーヤ
	CHARA_RANDOM, // 気まぐれモンスター
	CHARA_CHASE,  // 追いかけモンスター
	CHARA_AMBUSH, // 先回りモンスター
	CHARA_SIEGE,  // 挟み撃ちモンスター
} Chara;

typedef struct {
	Chara    id;      // 誰?
	Vector2  pos;     // 座標
	Vector2  lastPos; // 前回の座標
} Character;

// キャラクター初期化
void InitCharacter(Character* ch, Chara id, Vector2 initPos);
// キャラクター移動
void MoveCharacter(Character* ch, Vector2 pos);

#endif // __CHARACTER_H
