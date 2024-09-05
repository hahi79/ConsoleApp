<?php
//======================================
//	ターン制バトル  Main
//======================================
require_once "Command.php";
require_once "Utility.php";
require_once "Character.php";
require_once "TurnBattle.php";
require_once "UI.php";
require_once "AI.php";

main();

function main()
{
    Utility::initialize();
    $c=0;
    do{
        game();
        printf("もう一度(y/n)?");
        while(true){
            $c=Utility::GetKey();
            if( $c=="y" || $c=="n") break;
        }
    }while($c=="y");
    Utility::finalize();
    return 0;
}

function game()
{
    $player = new Character(
        100,        // HP
		15,         // MP
		30,         // 攻撃力
		"ゆうしゃ", // 名前
		""        // アスキーアート
    );
    $boss = new Character(
        255,      // HP
		0,        // MP
		50,       // 攻撃力
		"まおう", // 名前
		"　　Ａ＠Ａ\n". // アスキーアート
		"ψ（▼皿▼）ψ"   
    );
    $zako = new Character(
		3,          // HP
		0,          // MP
		2,          // 攻撃力
		"スライム", // 名前 
		"／・Д・＼\n". // アスキーアート
		"～～～～～"
    );

    $btl = new TurnBattle($player,$boss);
    $btl->start();
    $btl->intro();

    $isEnd=false;
    while(true){
        $cmd=UI::getPlayerCommand($btl);
        $isEnd=$btl->execPlayerTurn($cmd);
        if($isEnd){
            break;
        }
        $cmd=AI::getEnemyCommand();
        $isEnd=$btl->execEnemyTurn($cmd);
        if($isEnd){
            break;
        }
        $btl->nextTurn();
    }
}