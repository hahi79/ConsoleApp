<?php
//======================================
//	ターン制バトル  Main
//======================================
require_once "Command.php";
require_once "TurnBattle.php";
require_once "Utility.php";

class UI
{
    public static function GetPlayerCommand(&$btl)
    {
        $cmdMax=Command::Max->value;
        $cmd=0;
        while(true){
            $btl->drawBattleScreen();
            for($i=0; $i<$cmdMax; $i++){
                $cmd_i = Command::from($i);
                $cur= ($i==$cmd)?"＞" : "　";
                printf("%s%s\n",$cur, $cmd_i->commandName());
            }

            $c=Utility::GetKey();
            switch($c){
                case Utility::ARROW_UP:
                    $cmd--;
                    if($cmd<0){
                        $cmd = $cmdMax-1;
                    }
                    break;
                case Utility::ARROW_DOWN:
                    $cmd++;
                    if($cmd >= $cmdMax){
                        $cmd=0;
                    }
                    break;
                case "\n":
                    return Command::from($cmd);
            }
        }
    }
}