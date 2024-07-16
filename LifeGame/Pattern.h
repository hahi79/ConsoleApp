//========================================
//      ライフゲーム:パターン
//========================================
#ifndef __PATTERN_H
#define __PATTERN_H

typedef struct {
	int width;         // パターンの横幅
	int height;        // パターンの縦幅
	bool* data;        // パターン
	const char* name;  // パターンの名前
	bool isLoopCells;  // フィールドの左右、上下がループしているか?
} Pattern;

// パターン選択を行う
Pattern* SelectPattern();

#endif //  __PATTERN_H
