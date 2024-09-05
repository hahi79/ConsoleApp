<?php
//======================================
//	ターン制バトル  Character
//======================================

class Character
{
    const SPELL_COST = 3;
    var $hp;     // HP
	var $maxHp;  // 最大HP
	var $mp;     // MP
	var $maxMp;  // 最大MP
	var $attack; // 攻撃力
	var $name;   // 名前
	var $aa;     // アスキーアート
	var $isEscape;    // 逃げ出したフラグ
	var $isEraseAa;   // アスキーアートを消すフラグ
    // コンストラクター
    public function __construct($hp,$mp,$attack,$name,$aa)
    {
        $this->maxHp=$hp;
        $this->maxMp=$mp;
        $this->attack=$attack;
        $this->name=$name;
        $this->aa=$aa;
    }
    // バトル開始
    public function start()
    {
        $this->hp=$this->maxHp;
        $this->mp=$this->maxMp;
        $this->isEscape=false;
        $this->isEraseAa=false;
    }
    // 死亡か?
    public function isDead()
    {
        return $this->hp==0;
    }
    // ダメージを受ける
    public function damage($dmg)
    {
        $this->hp -= $dmg;
        if($this->hp<0){
            $this->hp=0;
        }   
    }
    // 回復する
    public function recover()
    {
        $this->hp=$this->maxHp;
    }
    //  魔法が使えるか?
    public function canSpell()
    {
        return $this->mp >= self::SPELL_COST;
    }
    // 魔法を使う
    public function useSpell()
    {
        $this->mp -= self::SPELL_COST;
        if($this->mp < 0){
            $this->mp=0;
        }
    }
    // プレーヤ表示を行う
    public function indicatePlayer()
    {
        printf("%s\n",$this->name);
        printf("ＨＰ：%3d／%d　ＭＰ：%2d／%d\n", $this->hp, $this->maxHp, $this->mp, $this->maxMp);
    }
    // エネミー表示を行う
    public function indicateEnemy()
    {
        if ($this->isEraseAa == false) {
            printf("%s", $this->aa);
        }
        printf("（ＨＰ：%3d／%d）\n", $this->hp, $this->maxHp);
    }
    // 攻撃力から与えるダメージを計算
    public function calcDamage()
    {
	    $dmg = Utility::getRand($this->attack) + 1;
	    return $dmg;
    }
    public function getName()
    {
        return $this->name;
    }
    // 逃げ出したをセット
    public function setEscape()
    {
	    $this->isEscape = true;
    }
    // 逃げ出したか?
    public function isEscape()
    {
	    return $this->isEscape;
    }
    // アスキーアート消すをセット
    public function setEraseAa()
    {
	    $this->isEraseAa = true;
    }
}