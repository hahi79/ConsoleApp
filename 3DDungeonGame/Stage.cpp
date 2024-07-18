//======================================
//	3Dダンジョン  ステージ
//======================================
#include "Stage.h"

void Draw3D(Stage* stage)
{

}

void GenerateMap(Stage* stage)
{
	// Maze初期化
	for (int y = 0; y < MAZE_HEIGHT; y++) {
		for (int x = 0; x < MAZE_WIDTH; x++) {
			for (int d = 0; d < DIR_MAX; d++) {
				SetMazeWall(stage, x, y, d, true);
			}
		}
	}

	Vector2 curPos = { 0,0 };
	Vector2List toDigWallPos;
	InitializeVector2List(&toDigWallPos, 100);
	AddVector2List(toDigWallPos, curPos);

	while (true) {

	}

}





bool IsInsideMaze(Vector2 pos)
{
	return 0 <= pos.x && pos.x < MAZE_WIDTH
		&& 0 <= pos.y && pos.y < MAZE_HEIGHT;
}
