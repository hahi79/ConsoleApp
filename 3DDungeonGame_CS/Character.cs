//======================================
//      �^��3D�_���W�����@�L�����N�^�[
//======================================
using Vector2 = GP2.Vector2;
using System.Diagnostics;   // Debug

namespace _3DDungeonGame_CS
{
    internal class Character
    {
        Vector2 m_pos;    // ���W
        Direction m_dir;  // �����Ă������

        // �R���X�g���N�^
        public Character(Vector2 pos, Direction dir)
        {
            m_pos = pos;
            m_dir = dir;
        }
        // �v���p�e�Bpos
        public Vector2 pos
        {
            get { return m_pos; }
            set { m_pos = value; }
        }
        // �v���p�e�Bdir
        public Direction dir
        {
            get { return m_dir; }
            set { m_dir = value; }
        }
        // ��������
        public void TurnBack()
        {
            m_dir = Misc.DirectionAdd(m_dir, 2);
        }
        // ��������
        public void TurnLeft()
        {
            // �k�������쁨��
            m_dir = Misc.DirectionAdd(m_dir, 1);
        }
        // �E������
        public void TurnRight()
        {
            // �k�������쁨��
            m_dir = Misc.DirectionAdd(m_dir, -1);
        }
    } // class
} // namespace