//=============================================================
//	�I�[�f�B�I
// ------------------------------------------------------------
// ����: �u�v���W�F�N�g�v���u�v���p�e�B�v
// (1)C/C++�u�S�ʁv�ǉ��̃C���N���[�h�f�B���N�g���[��
//   c:\Program Files(x86)\Microsoft SDKs\Windows\v7.1A\Include
// (2)�����J�[�u�S�ʁv�ǉ��̃��C�u�����[�f�B���N�g���[��
//   c:\Program Files(x86)\Microsoft SDKs\Windows\v7.1A\Lib
// (3)�����J�[�u���́v�ǉ��̈ˑ��t�@�C����
//   WinMM.lib
//=============================================================
#undef UNICODE
#include <windows.h>
#include <MMSystem.h>
#include <stdio.h>

// wav�t�@�C�����Đ�
void PlayWav(const char* filename)
{
	bool ret = PlaySound(filename, nullptr, SND_ASYNC);
	if (ret == false) {
		printf("PlayWav(% s) fail.", filename);
	}
	
}

void PlayDamageSE()
{

}