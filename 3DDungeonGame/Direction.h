//======================================
//	ディレクション(方向)
//======================================
#ifndef __DIRECTION_H
#define __DIRECTION_H

// ４方向
// マップ上では「北西南東」の意味だが
// プレーヤから「前左後右」の意味にも使われる
typedef enum {
	DIR_NORTH,  // 北(前方)
	DIR_WEST,   // 西(左方)
	DIR_SOUTH,  // 南(後方)
	DIR_EAST,   // 東(右方)
	DIR_MAX,
} Direction;

#endif // __DIRECTION_H
