//========================================
//      ライフゲーム:パターン
//========================================
#ifndef __PATTERN_H
#define __PATTERN_H

typedef struct {
	int width;
	int height;
	bool* data;
	const char* name;
} Pattern;

Pattern* SelectPattern();

#endif //  __PATTERN_H
