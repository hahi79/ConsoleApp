//======================================
//	リバーシ UI
//======================================
#ifndef __UI_H
#define __UI_H
#include "Mode.h"
#include "Vector2.h"
#include "Reversi.h"

// モード選択
Mode SelectMode();
// 位置入力
Vector2 InputPosition(Reversi *reversi);

#endif // UI_H
