//======================================
//      �^��3D�_���W�����@���P�[�V����
//======================================
using Vector2 = GP2.Vector2;
using System.Diagnostics;   // Debug

namespace _3DDungeonGame_CS
{
    enum Location
    {
        FrontLeft,  // ���O
        FrontRight, // �E�O
        Front,      // �O
        Left,       // ��
        Right,      // �E
        Center,     // ���S
        Max,        // (�ő�)
    }

    static partial class Misc
    {
        static Vector2[,] locations = new Vector2[,]
        {
            // Direction.North
            {                       //                            x:-1 + 0 + 1
		            new Vector2(-1,-1), // LOC_FRONT_LEFT  ���O(A)     +--+--+--+
		            new Vector2( 1,-1), // LOC_FRONT_RIGHT �E�O(B)     |�`|�b|�a|-1 
		            new Vector2( 0,-1), // LOC_FRONT       �O(C)       +--+--+--+
		            new Vector2(-1, 0), // LOC_LEFT        ��(D)       |�c|��|�d|+0
		            new Vector2( 1, 0), // LOC_RIGHT       �E(E)       +--+--+--+
		            new Vector2( 0, 0), // LOC_CENTER      ���S        |�@|�@|�@|+1
                                        //                             +--+--+--+
            },
            // Direction.West
            	{                       //                            x:-1 + 0 + 1
                    new Vector2(-1, 1), // LOC_FRONT_LEFT  ���O(A)     +--+--+--+
                    new Vector2(-1,-1), // LOC_FRONT_RIGHT �E�O(B)     |�a|�d|�@|-1 
                    new Vector2(-1, 0), // LOC_FRONT       �O(C)       +--+--+--+
                    new Vector2( 0,+1), // LOC_LEFT        ��(D)       |�b|��|�@|+0
                    new Vector2( 0,-1), // LOC_RIGHT       �E(E)       +--+--+--+
                    new Vector2( 0, 0), // LOC_CENTER      ���S        |�`|�c|�@|+1
                                        //                             +--+--+--+
	        },
	        // DIR_SOUTH
	        {                           //                            x:-1 + 0 + 1
                    new Vector2( 1, 1), // LOC_FRONT_LEFT  ���O(A)     +--+--+--+
                    new Vector2(-1, 1), // LOC_FRONT_RIGHT �E�O(B)     |�@|�@|�@|-1 
                    new Vector2( 0, 1), // LOC_FRONT       �O(C)       +--+--+--+
                    new Vector2( 1, 0), // LOC_LEFT        ��(D)       |�d|��|�c|+0
                    new Vector2(-1, 0), // LOC_RIGHT       �E(E)       +--+--+--+
                    new Vector2( 0, 0), // LOC_CENTER      ���S        |�a|�b|�`|+1
	                                    //                             +--+--+--+
	        },
	        // DIR_EAST
	        {                          //                            x:-1 + 0 + 1
                    new Vector2(1,-1), // LOC_FRONT_LEFT  ���O(A)     +--+--+--+
                    new Vector2(1, 1), // LOC_FRONT_RIGHT �E�O(B)     |�@|�c|�`|-1 
                    new Vector2(1, 0), // LOC_FRONT       �O(C)       +--+--+--+
                    new Vector2(0,-1), // LOC_LEFT        ��(D)       |�@|��|�b|+0
                    new Vector2(0, 1), // LOC_RIGHT       �E(E)       +--+--+--+
                    new Vector2(0, 0), // LOC_CENTER      ���S        |�@|�d|�a|+1
	                                   //                             +--+--+--+
        	},
        };

        // �����ƃ��P�[�V��������I�t�Z�b�g�x�N�^�[�擾
        public static Vector2 GetLocationVector2(Direction dir, Location loc)
        {
            int idx1 = (int)dir;
            int idx2 = (int)loc;
            Debug.Assert(0 <= idx1 && idx1 < locations.GetLength(0));
            Debug.Assert(0 <= idx2 && idx2 < locations.GetLength(1));
            return locations[idx1, idx2];
        }


        // [5-2]��ƂȂ�A�X�L�[�A�[�g��錾����
        // +..+==+..+
        // .  |  |  .
        // +..+..+..+
        // .  |��|  .
        // +..+..+..+
        const string all =
            "L       /\n" +
	        "#L     /#\n" +
	        "#|L _ /|#\n" +
	        "#|#|#|#|#\n" +
	        "#|#|_|#|#\n" +
	        "#|/   L|#\n" +
	        "#/     L#\n" +
	        "/       L\n";

        // [5-3]���O���O�̕ǂ̃A�X�L�[�A�[�g��錾����
        // +==+..+..+ (���̕ǂ�`��)
        // .  .  .  .
        // +..+..+..+
        // .  .��.  .
        // +..+..+..+
        const string frontLeftNorth =
            "         \n" +
	        "         \n" +
	        "  _      \n" +
	        " |#|     \n" +
	        " |_|     \n" +
	        "         \n" +
	        "         \n" +
	        "         \n";

        // [5-4]�E�O���O�̕ǂ̃A�X�L�[�A�[�g��錾����
        // +..+..+==+ (���̕ǂ�`��)
        // .  .  .  .
        // +..+..+..+
        // .  .��.  .
        // +..+..+..+
        static string frontRightNorth =
            "         \n" +
	        "         \n" +
	        "      _  \n" +
	        "     |#| \n" +
	        "     |_| \n" +
	        "         \n" +
	        "         \n" +
	        "         \n";

        // [5-5]�O���O�̕ǂ̃A�X�L�[�A�[�g��錾����
        // +..+==+..+ (���̕ǂ�`��)
        // .  .  .  .
        // +..+..+..+
        // .  .��.  .
        // +..+..+..+
        static string frontNorth =
            "         \n" +
	        "         \n" +
	        "    _    \n" +
	        "   |#|   \n" +
	        "   |_|   \n" +
	        "         \n" +
	        "         \n" +
	        "         \n";

        // [5-6]�O�����̕ǂ̃A�X�L�[�A�[�g��錾����
        // +..+..+..+ 
        // .  |  .  . (���̕ǂ�`��)
        // +..+..+..+
        // .  .��.  .
        // +..+..+..+
        static string frontWest =
            "         \n" +
	        "         \n" +
	        " |L      \n" +
	        " |#|     \n" +
	        " |#|     \n" +
	        " |/      \n" +
	        "         \n" +
	        "         \n";

        // [5-7]�O���E�̕ǂ̃A�X�L�[�A�[�g��錾����
        // +..+..+..+ 
        // .  .  |  . (���̕ǂ�`��)
        // +..+..+..+
        // .  .��.  .
        // +..+..+..+
        static string frontEast =
            "         \n" +
	        "         \n" +
	        "      /| \n" +
	        "     |#| \n" +
	        "     |#| \n" +
	        "      L| \n" +
	        "         \n" +
	        "         \n";

        // [5-8]�����O�̕ǂ̃A�X�L�[�A�[�g��錾����
        // +..+..+..+ 
        // .  .  .  .
        // +==+..+..+ (���̕ǂ�`��)
        // .  .��.  .
        // +..+..+..+
        static string leftNorth =
            "         \n" +
	        "_        \n" +
	        "#|       \n" +
	        "#|       \n" +
	        "#|       \n" +
	        "_|       \n" +
	        "         \n" +
	        "         \n";

        // [5-9]�E���O�̕ǂ̃A�X�L�[�A�[�g��錾����
        // +..+..+..+ 
        // .  .  .  .
        // +..+..+==+ (���̕ǂ�`��)
        // .  .��.  .
        // +..+..+..+
        static string rightNorth =
            "         \n" +
	        "        _\n" +
	        "       |#\n" +
	        "       |#\n" +
	        "       |#\n" +
	        "       |_\n" +
	        "         \n" +
	        "         \n";

        // [5-10]�O�̕ǂ̃A�X�L�[�A�[�g��錾����
        // +..+..+..+ 
        // .  .  .  .
        // +..+==+..+ (���̕ǂ�`��)
        // .  .��.  .
        // +..+..+..+
        static string north =
            "         \n" +
	        "  _____  \n" +
	        " |#####| \n" +
	        " |#####| \n" +
	        " |#####| \n" +
	        " |_____| \n" +
	        "         \n" +
	        "         \n";

        // [5-11]���̕ǂ̃A�X�L�[�A�[�g��錾����
        // +..+..+..+ 
        // .  .  .  .
        // +..+..+..+
        // .  |��.  . (���̕ǂ�`��)
        // +..+..+..+
        static string west =
            "L        \n" +
	        "#L       \n" +
	        "#|       \n" +
	        "#|       \n" +
	        "#|       \n" +
	        "#|       \n" +
	        "#/       \n" +
	        "/        \n";

        // [5-12]�E�̕ǂ̃A�X�L�[�A�[�g��錾����
        // +..+..+..+ 
        // .  .  .  .
        // +..+..+..+
        // .  .��|  . (���̕ǂ�`��)
        // +..+..+..+
        static string east = 
            "        /\n" +
	        "       /#\n" +
	        "       |#\n" +
	        "       |#\n" +
	        "       |#\n" +
	        "       |#\n" +
	        "       L#\n" +
	        "        L\n";

        static string[,] aaTable = new string[,]
        {
	        // LOC_FRONT_LEFT ���O
	        {
                frontLeftNorth,  // DIR_NORTH(�O��)
		        null,         // DIR_WEST (����)
		        null,         // DIR_SOUTH(���)
		        null,         // DIR_EAST (�E��)
	        },
	        // LOC_FRONT_RIGHT �E�O
	        {
                frontRightNorth, // DIR_NORTH(�O��)
		        null,         // DIR_WEST (����)
		        null,         // DIR_SOUTH(���)
		        null,         // DIR_EAST (�E��)
	        },
	        // LOC_FRONT
	        {
                frontNorth,      // DIR_NORTH(�O��)
		        frontWest,       // DIR_WEST (����)
		        null,         // DIR_SOUTH(���)
		        frontEast,       // DIR_EAST (�E��)
	        },
	        // LOC_LEFT
	        {
                leftNorth,       // DIR_NORTH(�O��)
		        null,         // DIR_WEST (����)
		        null,         // DIR_SOUTH(���)
		        null,         // DIR_EAST (�E��)
	        },
	        // LOC_RIGHT
	        {
                rightNorth,      // DIR_NORTH(�O��)
		        null,         // DIR_WEST (����)
		        null,         // DIR_SOUTH(���)
		        null,         // DIR_EAST (�E��)
	        },
	        // LOC_CENTER
	        {
                north,          // DIR_NORTH(�O��)
		        west,           // DIR_WEST (����)
		        null,        // DIR_SOUTH(���)
		        east,           // DIR_EAST (�E��)
	        },
        };

        public static string GetLocationAA(Location loc, Direction dir)
        {
            int idx1 = (int)loc;
            int idx2 = (int)dir;
            Debug.Assert(0 <= idx1 && idx1 < aaTable.GetLength(0));
            Debug.Assert(0 <= idx2 && idx2 < aaTable.GetLength(1));
            return aaTable[idx1, idx2];
        }
    } // class
} // namespace
