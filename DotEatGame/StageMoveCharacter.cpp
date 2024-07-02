//======================================
//	�h�b�g�C�[�g�Q�[�� �L�����N�^�ړ�
//======================================
#include "Stage.h"
#include "Character.h"
#include "Utility.h"  // GetKey(),ARROW_XX
#include <stdio.h>


// �֐��v���g�^�C�v
static void MoveMonsterRandom(Stage* stage, Character* ch);
static void MoveMonsterChase(Stage* stage, Character* ch);
static void MoveMonsterAmbush(Stage* stage, Character* ch);
static void MoveMonsterSiege(Stage* stage, Character* ch);
static Vector2 GetRandomPosition(Stage* stage, Character* ch);
static Vector2 GetChasePosition(Stage* stage, Character* ch, Vector2 targetPos);
static void ScanDistance(Stage* stage, Vector2 pos, int dist);
static void SetDistance(Stage* stage, int x, int y, int dist);
static int GetDistance(Stage* stage, int x, int y);
static void PrintDistance(Stage* stage);

const int INIT_DISTANCE = -1;
const int LARGE_DISTANCE = 100;

// �v���[���ړ�
void MovePlayer(Stage *stage)
{
	Character* player = stage->player;
	Vector2 newPos = player->pos;
	switch (GetKey())
	{
	case ARROW_UP: newPos.y--; break;
	case ARROW_LEFT:newPos.x--; break;
	case ARROW_DOWN: newPos.y++; break;
	case ARROW_RIGHT: newPos.x++; break;
	default:
		return;
	}
	newPos = GetLoopPosition(newPos);
	char maze = GetMaze(stage, newPos.x, newPos.y);
	if (maze != MAZE_WALL) {
		MoveCharacter(player, newPos);
		if (maze == MAZE_DOT) {
			SetMaze(stage, newPos.x, newPos.y, MAZE_NONE);
		}
	}
}
// �����X�^�[�̈ړ�
void MoveMonsters(Stage* stage)
{
	for (int i = 0; i < stage->monsterNum; i++) {
		Character* monster = stage->monster[i];
		switch (monster->id) {
		case CHARA_RANDOM: MoveMonsterRandom(stage,monster); break;
		case CHARA_CHASE:  MoveMonsterChase(stage,monster); break;
		case CHARA_AMBUSH: MoveMonsterAmbush(stage,monster); break;
		case CHARA_SIEGE:  MoveMonsterSiege(stage,monster); break;
		}
	}
}

// �����_�������X�^�[�̈ړ�
static void MoveMonsterRandom(Stage*stage,Character* ch)
{
	Vector2 newPos = GetRandomPosition(stage, ch);
	MoveCharacter(ch, newPos);
}
// �ǂ����������X�^�[�̈ړ�
static void MoveMonsterChase(Stage*stage,Character* ch)
{
	Vector2 targetPos = stage->player->pos;
	Vector2 newPos = GetChasePosition(stage, ch, targetPos);
	MoveCharacter(ch, newPos);
}
// ���胂���X�^�[�̈ړ�
static void MoveMonsterAmbush(Stage* stage, Character* ch)
{
	Character* player = stage->player;
	Vector2 playerDir = Vector2Sub(player->pos, player->lastPos);
	Vector2 targetPos;
	targetPos.x = playerDir.x * 3 + player->pos.x;
	targetPos.y = playerDir.y * 3 + player->pos.y;
	targetPos = GetLoopPosition(targetPos);
	Vector2 newPos = GetChasePosition(stage, ch, targetPos);
	MoveCharacter(ch, newPos);
}
// ���݌��������X�^�[�̈ړ�
static void MoveMonsterSiege(Stage *stage,Character* ch)
{
	Vector2 newPos;
	Character* player = stage->player;
	Character* chaser = stage->chaser;
	if (chaser == nullptr) {
		newPos = GetRandomPosition(stage, ch);
	}
	else {
		Vector2 chaseToPlayer = Vector2Sub(player->pos, chaser->pos);
		Vector2 targetPos = Vector2Add(player->pos, chaseToPlayer);
		targetPos = GetLoopPosition(targetPos);
		newPos = GetChasePosition(stage, ch, targetPos);
	}
	MoveCharacter(ch, newPos);
}
// �L�����̃����_���Ȉړ�����擾
static Vector2 GetRandomPosition(Stage* stage, Character* ch)
{
	ClearVector2List(&stage->v2list);
	// �S�����ōs������W�����e�B���O(��ނ͂��Ȃ�)
	for (int d = 0; d < DIR_MAX; d++) {
		Vector2 dir = GetDirVector2((DIRECTION)d);
		Vector2 newPos = Vector2Add(ch->pos, dir);
		newPos = GetLoopPosition(newPos);
		if (GetMaze(stage, newPos.x, newPos.y) != MAZE_WALL
			&& Vector2Equ(newPos, ch->lastPos) == false) {
			AddVector2List(&stage->v2list, newPos);
		}
	}
	// ���X�g���烉���_���ɑI��
	int idx = GetRand(GetCountVector2List(&stage->v2list));
	return GetVector2List(&stage->v2list, idx);
}
// �L�������^�[�Q�b�g�ֈړ����邽�߂̈ړ�����擾
static Vector2 GetChasePosition(Stage* stage, Character* ch, Vector2 targetPos)
{
	for (int y = 0; y < MAZE_HEIGHT; y++) {
		for (int x = 0; x < MAZE_WIDTH; x++) {
			SetDistance(stage, x, y, INIT_DISTANCE);
		}
	}
	// ScanDistance 1st
	Vector2 dir;
	Vector2 newPos;
	SetDistance(stage, ch->pos.x, ch->pos.y, 0);
	for (int d = 0; d < DIR_MAX; d++) {
		dir = GetDirVector2((DIRECTION)d);
		newPos = Vector2Add(ch->pos, dir);
		newPos= GetLoopPosition(newPos);
		if (Vector2Equ(newPos, ch->lastPos) == false) {
			ScanDistance(stage, newPos, 1);
		}
	}
//	PrintDistance(stage);
	int dist = GetDistance(stage, targetPos.x, targetPos.y);
	if (dist != INIT_DISTANCE) {
		// targetPos���炳���̂ڂ��� dist=1 �܂ł��ǂ�
		ClearVector2List(&stage->v2list);
		AddVector2List(&stage->v2list, targetPos);
		while (dist > 1) {
			ClearVector2List(&stage->v2temp);
			int count = GetCountVector2List(&stage->v2list);
			for (int i = 0; i <count ; i++) {
				Vector2 route = GetVector2List(&stage->v2list, i);
				for (int d = 0; d < DIR_MAX; d++) {
					Vector2 dir = GetDirVector2((DIRECTION)d);
					Vector2 pos = Vector2Add(route, dir);
					pos = GetLoopPosition(pos);
					if (GetDistance(stage, pos.x, pos.y) == dist - 1) {
						AddVector2List(&stage->v2temp, pos);
					}
				}
			}
			int cnt = GetCountVector2List(&stage->v2temp);
			if (cnt == 0) {
				printf("dist:%d\n", dist);
				PrintVector2List(&stage->v2list);
				PrintDistance(stage);
			}
			CopyVector2List(&stage->v2list, &stage->v2temp);
			dist--;
		}
		// dist=1 ���ړ���
		int count = GetCountVector2List(&stage->v2list);
		int idx = GetRand(count);
		Vector2 routePos = GetVector2List(&stage->v2list, idx);
		return routePos;
	}
	// targetPos�ɂȂ���o�H���Ȃ���΁A�����_���ȍs���
	return GetRandomPosition(stage, ch);
}
// Maze���X�L��������distance[][]�ɋ�������������(�ċA)
static void ScanDistance(Stage* stage, Vector2 pos, int dist)
{
	Vector2 dir;
	Vector2 newPos;
	pos = GetLoopPosition(pos);
	if (GetMaze(stage, pos.x, pos.y) != MAZE_WALL) {
		int tmp = GetDistance(stage, pos.x, pos.y);
		if (tmp == INIT_DISTANCE || tmp > dist) {
			SetDistance(stage, pos.x, pos.y, dist);
			for (int d = 0; d < DIR_MAX; d++) {
				dir = GetDirVector2((DIRECTION)d);
				newPos = Vector2Add(pos, dir);
				ScanDistance(stage, newPos, dist + 1);
			}
		}
	}
}
static int GetDistance(Stage* stage, int x, int y)
{
	if (IsInMaze(x, y)) {
		return stage->distance[y][x];
	}
	return LARGE_DISTANCE;
}
static void SetDistance(Stage* stage, int x, int y, int dist)
{
	if (IsInMaze(x, y)) {
		stage->distance[y][x] = dist;
	}
}


static void PrintDistance(Stage* stage)
{
	for (int y = 0; y < MAZE_HEIGHT; y++) {
		for (int x = 0; x < MAZE_WIDTH; x++) {
			int dist = GetDistance(stage, x, y);
			if (dist == INIT_DISTANCE) {
				printf("��");
			}
			else {
				printf("%2d", dist);
			}
		}
		putchar('\n');
	}
	WaitKey();
	WaitKey();
	WaitKey();
	WaitKey();
	WaitKey();
}