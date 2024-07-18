//======================================
//	3Dダンジョン  ステージ
//======================================
#ifndef __STAGE_H
#define __STAGE_H
#include "Direction.h"
#include "Character.h"

const int MAZE_WIDTH = 8;
const int MAZE_HEIGHT = 8;

typedef struct {
	bool walls[DIR_MAX];
} TILE;

typedef struct {
	TILE maze[MAZE_HEIGHT][MAZE_WIDTH];
	Character player:
} Stage;

bool IsInsideMaze(int x, int y);


#endif //  __STAGE_H
