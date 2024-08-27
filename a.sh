#F1=./DotEatGame/IntervalTimer.cpp
#F2=./FallingBlockPuzzle/IntervalTimer.cpp
#FC=./LifeGame/IntervalTimer.cpp
WINMERGE=/mnt/d/Program1/WinMerge/WinMergeU.exe

F0=./ClassicRPG/Utility.cpp
F2=./Sangokushi/Utility.cpp
F1=./3DDungeonGame/Utility.cpp

F3=./DotEatGame/Utility.cpp
F4=./FallingBlockPuzzle/Utility.cpp
F5=./LifeGame/Utility.cpp
F6=./Reversi/Utility.cpp
F7=./SengokuSimulation/Utility.cpp
F8=./TurnBattle/Utility.cpp

#$WINMERGE $F0 $F4
F0=./3DDungeonGame/Vector2.cpp
F1=./DotEatGame/Vector2.cpp
F2=./Reversi/Vector2.cpp
#$WINMERGE $F0 $F2

F0=./3DDungeonGame/Vector2List.cpp
F1=./DotEatGame/Vector2List.cpp
F2=./Reversi/Vector2List.cpp
$WINMERGE $F0 $F2
