<?php
//======================================
//	ターン制バトル  AI
//======================================
require_once "Command.php";

class AI
{
    // 敵のコマンドを返す
    public static function GetEnemyCommand()
    {
        return Command::Fight;
    }
}