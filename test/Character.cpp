//======================================
//	キャラクター
//======================================
#include "Character.h"
#include "Utility.h" // GetRand()
#include <string.h>  // strcpy()
#include <stdio.h>   // printf()
#include <stdlib.h>  // rand(), RAND_MAX

// 関数プロトタイプ
static void setColor(Character* ch);
// 呪文の消費MP
static const int SPELL_COST = 3;

// キャラクターセット
void SetCharacter(Character* ch, int hp, int mp, int attack, const char* name, const char* aa)
{
	ch->maxHp = hp;
	ch->maxMp = mp;
	ch->attack = attack;
	ch->name = name;
	ch->aa = aa;
}
// キャラの戦闘開始
void StartCharacter(Character* ch)
{
	ch->hp = ch->maxHp;
	ch->mp = ch->maxMp;
	ch->isEscape = false;
	ch->isEraseAa = false;
}
// 死亡か?
bool IsDeadCharacter(Character* ch)
{
	return ch->hp == 0;
}
// ダメージを受ける
void DamageCharacter(Character* ch, int damage)
{
	ch->hp -= damage;
	if (ch->hp < 0) {
		ch->hp = 0;
	}
}
// 回復する
void RecoverCharacter(Character* ch)
{
	ch->hp = ch->maxHp;
}
//  魔法が使えるか?
bool CanSpellCharacter(Character* ch)
{
	return ch->mp >= SPELL_COST;
}
// 魔法を使う
void UseSpellCharacter(Character* ch)
{
	ch->mp -= SPELL_COST;
	if (ch->mp < 0) {
		ch->mp = 0;
	}
}
// プレーヤ表示を行う
void IndicatePlayer(Character* ch)
{
	setColor(ch);
	printf("%s\n", ch->name);
	printf("ＨＰ：%3d／%d　ＭＰ：%2d／%d\n", ch->hp, ch->maxHp, ch->mp, ch->maxMp);
	printf(EscDEFAULT);
}
// エネミー表示を行う
void IndicateEnemy(Character* ch)
{
	setColor(ch);
	if (ch->isEraseAa == false) {
		printf("%s", ch->aa);
	}
	printf("（ＨＰ：%3d／%d）\n", ch->hp, ch->maxHp);
	printf(EscDEFAULT);
}
// 攻撃力から与えるダメージを計算
int CalcDamage(Character* ch)
{
	int dmg = GetRand(ch->attack) + 1;
	return dmg;
}
// 名前を取得
const char *GetName(Character* ch)
{
	return ch->name;
}
// 逃げ出したをセット
void SetEscapeCharacter(Character* ch)
{
	ch->isEscape = true;
}
// 逃げ出したか?
bool IsEscapeCharacter(Character* ch)
{
	return ch->isEscape;
}
// アスキーアート消すをセット
void SetEraseAa(Character* ch)
{
	ch->isEraseAa = true;
}
// HP/maxHPによって表示色を変える
static void setColor(Character* ch)
{
	float rate = (float)ch->hp / ch->maxHp;
	if (rate < 0.3f) {
		printf(EscRED);
	}
	else if (rate < 0.5f) {
		printf(EscYELLOW);
	}
	else {
		printf(EscWHITE);
	}
}