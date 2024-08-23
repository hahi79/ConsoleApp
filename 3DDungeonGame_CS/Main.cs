//======================================
//      �^��3D�_���W�����@���C��
//======================================
using System;  // ConsoleKey
using Utility = GP2.Utility;

namespace _3DDungeonGame_CS
{
    internal class _3DDungeonGameMain
    {
        static int Main()
        {
            Utility.InitRand();

            ConsoleKey c;
            do
            {
                game();
                Utility.Printf("������x(y/n)?");
                Utility.PrintOut();
                while (true)
                {
                    c = Utility.GetKey();
                    if (c == ConsoleKey.Y || c == ConsoleKey.N)
                    {
                        break;
                    }
                }
            } while (c == ConsoleKey.Y);

            return 0;
        }

        static void game()
        {
            Stage stage = new Stage();

            while (true)
            {
                Utility.ClearScreen();
                stage.Draw3D();
                stage.DrawMap();
                UI.DrawOperation(stage);
                Utility.PrintOut();
                // �v���[���ړ�
                bool isEsc = UI.MovePlayer(stage);
                if (isEsc)
                {
                    break;
                }
                if (stage.IsGoalPlayer())
                {
                    DrawEnding();
                    Utility.WaitKey();
                    break;
                }
            }
        }
        static void DrawEnding()
        {
            Utility.ClearScreen();
            Utility.Printf(
            "�@���@���@�b�n�m�f�q�`�s�t�k�`�s�h�n�m�r�@���@��\n" +
            "\n" +
            "�@���Ȃ��͂��Ɂ@�ł񂹂̂܂悯���@�Ăɂ��ꂽ�I\n" +
            "\n" +
            "�@�������A���炭���Ƃ��ɂ����@�u�Ȃ��܁v�Ƃ���\n" +
            "���������̂Ȃ��@��������Ăɂ����@���Ȃ��ɂƂ��āA\n" +
            "�܂悯�̂����₫���@���날���ā@�݂���̂ł������c\n" +
            "\n" +
            "�@�@�@�@�@�@�@�`�@�s�g�d�@�d�m�c�@�`\n" +
            "\n");
            Utility.PrintOut();
        }
    } // class
} // namespace 
