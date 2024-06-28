//======================================
//	���o�[�V UI
//======================================
#include "Mode.h"
#include "Reversi.h"
#include "Vector2.h"
#include "Utility.h"
#include <stdio.h>  // printf()

Mode SelectMode()
{
	static const char* modeName[] = {
		"�P�o�@�f�`�l�d",
		"�Q�o�@�f�`�l�d",
		"�v�`�s�b�g"
	};
	int sel = 0;
	while (true) {
		ClearScreen();
		puts("���[�h���@�I������\n��������\n");
		for (int i = 0; i < MODE_MAX; i++) {
			const char* cur = (sel == i) ? "��" : "�@";
			printf("%s%s\n\n", cur, modeName[i]);
		}
		switch(GetKey()) {
		case ARROW_UP:
			sel--;
			if (sel < 0) {
				sel = MODE_MAX - 1;
			}
			break;
		case ARROW_DOWN:
			sel++;
			if (sel >= MODE_MAX) {
				sel = 0;
			}
			break;
		case DECIDE:
			return (Mode)sel;
		}
	}
}
// �ʒu����
Vector2 InputPosition(Reversi* reversi)
{
	Vector2 pos = { 3,3 };
	while (true) {
		DrawScreen(reversi, pos,IN_PLAY);
		switch (GetKey()) {
		case ARROW_UP:
			pos.y--;
			if (pos.y < 0) {
				pos.y = BOARD_HEI - 1;
			}
			break;
		case ARROW_DOWN:
			pos.y++;
			if (pos.y >=BOARD_HEI) {
				pos.y = 0;
			}
			break;
		case ARROW_LEFT:
			pos.x--;
			if (pos.x < 0) {
				pos.x = BOARD_WID - 1;
			}
			break;
		case ARROW_RIGHT:
			pos.x++;
			if (pos.x >= BOARD_WID) {
				pos.x = 0;
			}
			break;
		case DECIDE:
			if (CheckCanPlace(reversi, reversi->turn, pos) == false) {
				printf("�����ɂ́@�u���܂���\n");
				WaitKey();
				break;
			}
			return pos;
		}
	}
}