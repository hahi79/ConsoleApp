//======================================
//      �^��3D�_���W�����@�X�e�[�W
//======================================
using Vector2 = GP2.Vector2;
using Utility = GP2.Utility;
using System.Collections.Generic;   // List<T>
using System.Diagnostics;   // Debug
using System; // Console

namespace _3DDungeonGame_CS
{
    internal class Stage
    {
        const int MAZE_WIDTH = 8;
        const int MAZE_HEIGHT = 8;
        const int GOAL_X = MAZE_WIDTH - 1;
        const int GOAL_Y = MAZE_HEIGHT - 1;

        static string[] FloorPlayer = new string[]
        {
            "��",  // North
            "��",  // West
            "��",  // South
            "��",  // East
        };

        struct TILE
        {
            private bool[] m_walls;
            // �R���X�g���N�^�[
            public TILE()
            {
                m_walls = new bool[(int)Direction.Max];
            }
            // �v���p�e�B
            public bool[] walls
            {
                get { return m_walls; }
            }
            // ���Z�b�g
            public void Reset()
            {
                for(int i=0; i<m_walls.Length; i++)
                {
                    m_walls[i] = true;
                }
            }
        }

        TILE[,] m_maze;   // �}�b�v
        Character m_player; // �v���[���̈ʒu�ƌ���
        Vector2 m_goal;   // �S�[���̈ʒu
        bool m_isForMap;  // UI forMap or for3D

        // �v���p�e�B
        public bool isForMap
        {
            get { return m_isForMap; }
            set { m_isForMap = value; }
        }
        // �v���p�e�B
        public Character player
        {
            get { return m_player; }
        }

        // �R���X�g���N�^�[
        public Stage()
        {
            m_maze = new TILE[MAZE_HEIGHT, MAZE_WIDTH];
            for(int y=0; y<MAZE_HEIGHT; y++)
            {
                for(int x=0; x<MAZE_WIDTH; x++)
                {
                    m_maze[y, x] = new TILE();
                }
            }
            m_player = new Character(new Vector2(0, 0), Direction.North);
            m_goal=new Vector2(GOAL_X, GOAL_Y);
            m_isForMap = false;
            GenerateMap();
        }
    

        // ���H�}�b�v�`��
        public void DrawMap()
        {
            Vector2 pos = new Vector2(0, 0);
            for(int y=0; y<MAZE_HEIGHT; y++)
            {
                DrawMap_HorizontalWall(y, Direction.North);
                pos.y = y;
                for(int x=0; x<MAZE_WIDTH; x++)
                {
                    pos.x = x;
                    string floor = "�@";
                    if(pos == m_player.pos)
                    {
                        floor = FloorPlayer[(int)m_player.dir];
                    }else if (pos == m_goal)
                    {
                        floor = "�f";
                    }
                    string west = GetMazeWall(pos, Direction.West) ? "|" : " ";
                    string east = GetMazeWall(pos, Direction.East) ? "|" : " ";
                    Utility.Printf("{0}{1}{2}", west, floor, east);
                }
                Utility.Putchar('\n');
                DrawMap_HorizontalWall(y, Direction.South);
            }
        }
        // �k�A��̕ǂ�\��
        protected void DrawMap_HorizontalWall(int y,Direction dir)
        {
            Vector2 pos = new Vector2(0, y);
            for(int x=0; x<MAZE_WIDTH; x++)
            {
                pos.x = x;
                string wall = GetMazeWall(pos, dir) ? "�\" : "�@";
                Utility.Printf("+{0}+", wall);
            }
            Utility.Putchar('\n');
        }
        // 3D�`��
        public void Draw3D()
        {
            string _screen = 
                "         \n" +
                "         \n" +
                "         \n" +
                "         \n" +
                "         \n" +
                "         \n" +
                "         \n" +
                "         \n";
            char[] screen = _screen.ToCharArray();

            // 
            //  �v���[���̎���(A�`E)�ɂ���
            //	+--+--+--+
            //  |�`|�b|�a|
            //  +--+--+--+
            //  |�c|��|�d|
            //  +--+--+--+
            for(int i=0; i<(int)Location.Max; i++)
            {
                Vector2 loc = Misc.GetLocationVector2(m_player.dir, (Location)i);
                Vector2 pos = m_player.pos + loc;
                if (IsInsideMaze(pos) == false)
                {
                    continue;
                }

                for(int j=0; j<(int)Direction.Max; j++)
                {
                    // �v���[�����猩������
                    // ��������(0,1,2,3) ���v���[����(0)�@���猩��� ��������(0,1,2,3)
                    // ��������(0,1,2,3) ���v���[����(1)�@���猩��� ��������(3,0,1,2)
                    // ��������(0,1,2,3) ���v���[����(2)�@���猩��� ��������(2,3,0,1) 
                    // ��������(0,1,2,3) ���v���[����(3)�@���猩��� ��������(1,2,3,0)
                    const int dirMax = (int)Direction.Max;
                    Direction dir = (Direction)((dirMax + j - (int)player.dir) % dirMax);

                    if (GetMazeWall(pos, (Direction)j) == false)
                    {
                        continue;
                    }
                    string aa = Misc.GetLocationAA((Location)i, dir);
                    if ( aa==null)
                    {
                        continue;
                    }
                    // screen�ɃR�s�[
                    for(int k=0; k<screen.Length; k++)
                    {
                        char c = aa[k];
                        if (c != ' ')
                        {
                            screen[k] = c;
                        }
                    }
                }
            }
            //
            // screen[]��`��
            //
            for(int i=0; i<screen.Length; i++)
            {
                char c = screen[i];
                switch (c)
                {
                    case ' ': Utility.Printf("�@"); break;
                    case '#': Utility.Printf("�@"); break;
                    case '_': Utility.Printf("�Q"); break;
                    case '|': Utility.Printf("�b"); break;
                    case '/': Utility.Printf("�^"); break;
                    case 'L': Utility.Printf("�_"); break;
                    case '\n':
                        Utility.Putchar(c);
                        break;
                    default:
                        Console.Write(string.Format("{0:X}", (int)c));
                        Debug.Assert(false);
                        break;
                }
            }
        }
        // �v���[�����S�[��������?
        public bool IsGoalPlayer()
        {
            return m_player.pos == m_goal;
        }
        protected void SetMazeWall(Vector2 pos, Direction dir, bool value)
        {
            if (IsInsideMaze(pos) && Misc.IsInDirection(dir))
            {
                m_maze[pos.y, pos.x].walls[(int)dir] = value;
            }
        }
        public bool GetMazeWall(Vector2 pos, Direction dir)
        {
            if(IsInsideMaze(pos) && Misc.IsInDirection(dir))
            {
                return m_maze[pos.y, pos.x].walls[(int)dir];
            }
            return true;
        }
        // ���W��Maze����?
        public bool IsInsideMaze(Vector2 pos)
        {
            return 0 <= pos.x && pos.x < MAZE_WIDTH
                && 0 <= pos.y && pos.y < MAZE_HEIGHT;
        }
        // �}�b�v����
        protected void GenerateMap()
        {
            ResetMap();

            Vector2 curPos = new Vector2(0, 0);
            List<Vector2> toDigWallPos = new List<Vector2>();
            List<int> canDigDirection = new List<int>();
            toDigWallPos.Add(curPos);

            while (true)
            {
                // curPos�̌@�����������X�g
                canDigDirection.Clear();
                for(int d=0; d<(int)Direction.Max; d++)
                {
                    if(CanDigWall(curPos, (Direction)d))
                    {
                        canDigDirection.Add(d);
                    }
                }
                int count = canDigDirection.Count;
                if (count > 0)
                {
                    // ���X�g���烉���_���ɑI��Ō@��
                    int idx = Utility.GetRand(count);
                    Direction digDirection=(Direction)canDigDirection[idx];
                    DigWall(curPos, digDirection);
                    // �@���������ɐi��
                    Vector2 digVec = Misc.GetDirVector2(digDirection);
                    curPos += digVec;
                    toDigWallPos.Add(curPos);
                }
                else
                {
                    toDigWallPos.RemoveAt(0);
                    // �@�������������Ƃ�
                    if(toDigWallPos.Count == 0)
                    {
                        break;
                    }
                    curPos = toDigWallPos[0];
                }
            }
        }
        // �}�b�v�����Z�b�g(�S���̕ǂ�on�ɂ���)
        protected void ResetMap()
        {
            for(int y=0; y<MAZE_HEIGHT; y++)
            {
                for(int x=0; x<MAZE_WIDTH; x++)
                {
                    m_maze[y, x].Reset();
                }
            }
        }
        // �w����W�̎w������͌@��邩?
        protected bool CanDigWall(Vector2 pos, Direction dir)
        {
            Vector2 dirVector2 = Misc.GetDirVector2(dir);
            Vector2 nextPos = pos + dirVector2;
            if (IsInsideMaze(nextPos) == false)
            {
                return false;
            }
            // ���Ɍ@���Ă�����NG
            for (int d = 0; d < (int)Direction.Max; d++)
            {
                if (GetMazeWall(nextPos, (Direction)d) == false)
                {
                    return false;
                }
            }
            return true;
        }
        // �ǂ��@��
        protected void DigWall(Vector2 pos, Direction dir)
        {
            // maze�̊O�Ȃ�Ȃɂ����Ȃ�
            if (IsInsideMaze(pos) == false)
            {
                return;
            }
            // pos��dir�̕ǂ���蕥��
            SetMazeWall(pos, dir, false);

            Vector2 dirVector2 = Misc.GetDirVector2(dir);
            Vector2 newPos = pos + dirVector2;
            if (IsInsideMaze(newPos))
            {
                // �k������A��������
                Direction dir2 = Misc.DirectionAdd(dir, 2);
                // newPos��dir2�̕ǂ���蕥��
                SetMazeWall(newPos, dir2, false);
            }
        }
    } // class
} // namespace 
