//======================================
//	3Dダンジョン  ステージ
//======================================
#include "Stage.h"
#include "Vector2List.h"
#include "IntList.h"
#include <stdio.h>
// 関数プロトタイプ
void DigWall(Stage* stage, Vector2 pos, Direction dir);
bool CanDigWall(Stage* stage, Vector2 pos, Direction dir);
void setMazeWall(Stage* stage, Vector2 pos, Direction dir, bool value);
bool getMazeWall(Stage* stage, Vector2 pos, Direction dir);
static void DrawMap_HorizontalWall(Stage* stage, int y, Direction dir);

// マップ生成
void GenerateMap(Stage* stage)
{
	// Maze初期化(maze走査、全てのdirection[]をtrueに)
	for (int y = 0; y < MAZE_HEIGHT; y++) {
		for (int x = 0; x < MAZE_WIDTH; x++) {
			for (int d = 0; d < DIR_MAX; d++) {
				setMazeWall(stage, x, y, (Direction)d, true);
			}
		}
	}

	Vector2 curPos = { 0,0 };
	Vector2List toDigWallPos[1];
	IntList canDigDirection[1];
	InitializeVector2List(toDigWallPos, MAZE_WIDTH*MAZE_HEIGHT);
	InitializeIntList(canDigDirection, DIR_MAX);
	AddVector2List(toDigWallPos, curPos);

	while (true) {
		// curPosの掘れる向きをリスティング
		ClearIntList(canDigDirection);
		for (int d = 0; d < DIR_MAX; d++) {
			if (CanDigWall(stage, curPos, (Direction)d)) {
				AddIntList(canDigDirection, d);
			}
		}
		
		int count = GetCountIntList(canDigDirection);
		if (count > 0) {
			// リストからランダムに選んで、掘る
			int idx=GetRand(count);
			Direction digDirection = (Direction)GetIntList(canDigDirection, idx);
			DigWall(stage, curPos, digDirection);
			// 掘った向きに進む
			Vector2 digVec=GetDirVector2(digDirection);
			curPos = Vector2Add(curPos, digVec);
			AddVector2List(toDigWallPos, curPos);
		}
		else {
			// 掘れる向きがないとき、
			DelVector2List(toDigWallPos, 0);
			if (GetCountVector2List(toDigWallPos) <= 0) {
				break;
			}
			curPos = GetVector2List(toDigWallPos, 0);
		}
	}
}

void DrawMap(Stage* stage)
{
	Character *player = &stage->player;

	for (int y = 0; y < MAZE_HEIGHT; y++) {
		DrawMap_HorizontalWall(stage, y, DIR_NORTH);

		for (int x = 0; x < MAZE_WIDTH; x++) {
			const char* floor = "　";
			if (x == player->pos.x && y == player->pos.y) {
				static const char* directionAA[] = {
					"↑",  // DIR_NORTH
					"←",  // DIR_WEST
					"↓,"  // DIR_SOUTH
					"→",  // DIR_EAST
				};
				floor = directionAA[player->dir];
			}
			else if (x == GOAL_X && y == GOAL_Y) {
				floor = "Ｇ";
			}
			const char* west = getMazeWall(stage, x, y, DIR_WEST) ? "｜" : "　";
			const char* east = getMazeWall(stage, x, y, DIR_EAST) ? "｜" : "　";
			printf("%s%s%s", west, floor, east);
		}
		putchar('\n');

		DrawMap_HorizontalWall(stage, y, DIR_SOUTH);
	}
}
// DIR_NORTH/DIR_SOUTH の水平壁を描画
static void DrawMap_HorizontalWall(Stage *stage, int y, Direction dir)
{
	for (int x = 0; x < MAZE_WIDTH; x++) {
		const char* wall = getMazeWall(stage, x, y, dir) ? "―" : "　" :
		printf("+%s+".wall);
	}
	putchar('\n');
}



// 疑似3D 描画
void Draw3D(Stage* stage)
{
	char screen[]=
		"         \n"
		"         \n"
		"         \n"
		"         \n"
		"         \n"
		"         \n"
		"         \n"
		"         \n";

	Character* player =&stage->player;
	for (int i = 0; i < LOC_MAX; i++) {
		Vector2 loc = GetLocaionVector2(player->dir, l);
		Vector2 pos = Vector2Add(player->pos, loc);
		if (IsInsideMaze(pos) == false) {
			continue;
		}

		for (int j = 0; j < DIR_MAX; j++) {
			// プレーヤから見た方向
			// ↑←↓→(0,1,2,3) をプレーヤ↑(0)　から見ると ↑←↓→(0,1,2,3)
			// ↑←↓→(0,1,2,3) をプレーヤ←(1)　から見ると →↑←↓(3,0,1,2)
			// ↑←↓→(0,1,2,3) をプレーヤ↓(2)　から見ると ↓→↑←(2,3,0,1) 
			// ↑←↓→(0,1,2,3) をプレーヤ→(3)　から見ると ←↓→↑(1,2,3,0)
			Direction dir = (j + DIR_MAX - player->dir) % DIR_MAX;

			if (getMazeWall(stage, pos, j) == false) {
				continue;
			}

		}
	}
}

// mazeのposをdir 方向に掘る
void DigWall(Stage* stage, Vector2 pos, Direction dir)
{
	// mazeの外ならなにもしない
	if (IsInsideMaze(pos) == false) {
		return;
	}
	setMazeWall(stage, pos, dir, false);

	Vector2 dirVector2 = GetDirVector2(dir);
	Vector2 newPos = Vector2Add(pos, dirVector2);

	if (IsInsideMaze(newPos)) {
		// 北→南、西→東、南→北、東→西
		Direction dir2 = (dir + 2) % DIR_MAX;
		setMazeWall(stage, newPos, dir2, false);
	}
}
// mazeのposをdir方向に掘れるか?
bool CanDigWall(Stage* stage, Vector2 pos, Direction dir)
{
	Vector2 dirVector2 = GetDirVector2(dir);
	Vector2 nextPos = Vector2Add(pos, dirVector2);
	if (IsInsideMaze(nextPos) == false) {
		return false;
	}

	for (int d = 0; d < DIR_MAX; d++) {
		if (getMazeWall(stage, nextPos, i) == false) {
			return false;
		}
	}
	return true;
}

// mazeのposのdir セッター
void setMazeWall(Stage *stage, Vector2 pos, Direction dir, bool value)
{
	if (IsInsideMaze(pos)) {
		stage->maze[pos.y][pos.x].direction[dir] = value;
	}
}
bool getMazeWall(Stage* stage, Vector2 pos, Direction dir)
{
	if (IsInsizeMaze(pos)) {
		return stage->maze[pos.y][pos.x].direction[dir];
	}
	return false;
}



// 座標がmaze内か?
bool IsInsideMaze(Vector2 pos)
{
	return 0 <= pos.x && pos.x < MAZE_WIDTH
		&& 0 <= pos.y && pos.y < MAZE_HEIGHT;
}
