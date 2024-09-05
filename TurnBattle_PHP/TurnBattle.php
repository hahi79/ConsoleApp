<?php
//======================================
//	ターン制バトル  TurnBattle
//======================================
require_once "Command.php";

class TurnBattle
{
    var $player;  // プレーヤ(参照)
    var $enemy;   // エネミー(参照)
    var $turn;    // ターン数
    // コンストラクター
    public function __construct(&$player, &$enemy)
    {
        $this->player=$player;
        $this->enemy=$enemy;
    }
    // イントロ「～が現れた!!」表示
    public function intro()
    {
        $this->drawBattleScreen();
        printf("%sが　あらわれた!!\n", $this->enemy->GetName());
        Utility::waitKey();
    }
    // バトル開始
    public function start()
    {
        $this->turn=1;
        $this->player->Start();
        $this->enemy->Start();
    }
    public function drawBattleScreen()
    {
        Utility::clearScreen();
        $this->player->indicatePlayer();
        printf("\n");
        $this->enemy->indicateEnemy();
        printf("\n");
    }
    // プレーヤのターン実行
    public function execPlayerTurn(Command $cmd)
    {
        $this->execCommand($cmd,$this->player,$this->enemy);
        if($this->enemy->isDead()){
            $this->enemy->setEraseAa();
            $this->drawBattleScreen();
            printf("%sを　たおした!\n", $this->enemy->GetName());
            Utility::waitKey();
            return true;
        }
        return $this->player->isEscape();
    }
    // 敵のターン実行
    public function execEnemyTurn(Command $cmd)
    {
        $this->execCommand($cmd,$this->enemy,$this->player);
        if($this->player->isDead()){
            $this->drawBattleScreen();
            printf("あなたは　しにました\n");
            Utility::waitKey();
            return true;
        }
        return false;
    }
    // コマンド実行
    function execCommand(Command $cmd,&$offense,&$defense)
    {
        $dmg=0;
        switch($cmd){
            case Command::Fight:
                $this->drawBattleScreen();
                printf("%sの　こうげき!\n", $offense->GetName());
                Utility::waitKey();
           
                $dmg=$offense->calcDamage();
                $defense->damage($dmg);

                $this->drawBattleScreen();
                printf("%sに　%dの　ダメージ!\n", $defense->GetName(),$dmg);
                Utility::waitKey();
                break;

            case Command::Spell:
                if($offense->canSpell()==false){
                    $this->drawBattleScreen();
                    printf("ＭＰが　たりない!\n");
                    Utility::waitKey();
                    break;    
                }
                $offense->useSpell();
                $this->drawBattleScreen();
                printf("%sは　ヒールを　となえた!\n", $offense->GetName());
                Utility::waitKey();
                    
                $offense->recover();
                $this->drawBattleScreen();
                printf("%sのきずが　かいふくした!\n", $offense->GetName());
                Utility::waitKey();
                break;
            case Command::Escape:
                $this->drawBattleScreen();
                printf("%sは　にげだした!\n", $offense->GetName());
                Utility::waitKey();
                $offense->setEscape();
                break;
        }
    }
    // 次のターンへ
    public function nextTurn()
    {
        $this->turn ++;
    }
}