<?php
//======================================
//	ユーティリティ
//======================================

class Utility
{
    static $savedKey="";

    const ESC_BLACK   = "\x1b[30m";
    const ESC_RED     = "\x1b[31m";
    const ESC_GREEN   = "\x1b[32m";
    const ESC_YELLOW  = "\x1b[33m";
    const ESC_BLUE    = "\x1b[34m";
    const ESC_MAZENTA = "\x1b[35m";
    const ESC_CYAN    = "\x1b[36m";
    const ESC_WHITE   = "\x1b[37m";
    const ESC_DEFAULT = "\x1b[39m";

    const ESC_BG_BLACK   = "\x1b[40m";
    const ESC_BG_RED     = "\x1b[41m";
    const ESC_BG_GREEN   = "\x1b[42m";
    const ESC_BG_YELLOW  = "\x1b[43m";
    const ESC_BG_BLUE    = "\x1b[44m";
    const ESC_BG_MAZENTA = "\x1b[45m";
    const ESC_BG_CYAN    = "\x1b[46m";
    const ESC_BG_WHITE   = "\x1b[47m";
    const ESC_BG_DEFAULT = "\x1b[49m";

    const ARROW_UP    = "\x1b\x5b\x41";
    const ARROW_DOWN = "\x1b\x5b\x42";
    const ARROW_RIGHT= "\x1b\x5b\x43";
    const ARROW_LEFT = "\x1b\x5b\x44";
//define('Return',"\x0a");

    public static function initRand()
    {
        srand();
    }
    public static function getRand($max)
    {
        return rand(0,$max);
    }
    public static function waitKey()
    {
        self::getKey();
    }
    // ゲーム開始、初期化
    public static function initialize()
    {
        self::initRand();
        // ターミナルの設定を変更して非同期入力を可能にする
        system('stty cbreak -echo');
    }
    // ゲーム終了、あとしまつ
    public static function finalize()
    {
        // ターミナルの設定を元に戻す
        system('stty -cbreak echo');
    }
    // キー入力があったか? 
    public static function keyAvailable()
    {
        stream_set_blocking(STDIN,false);
        $c=fgetc(STDIN);
        stream_set_blocking(STDIN,true);
        if($c!==false){
            self::$savedKey=$c;
            return true;
        }
        return false;
    }
    public static function getKey()
    {
        // ターミナルの設定を変更して非同期入力を可能にする
    //  system('stty cbreak -echo');
        if(self::$savedKey!=""){
            $c = self::$savedKey;
            self::$savedKey="";
        }else{
            $c = fgetc(STDIN);
        }
        if($c == "\x1b"){
            $c .= fgetc(STDIN);
            if($c == "\x1b\x5b"){
                $c .= fgetc(STDIN);
            }
        }
        // ターミナルの設定を元に戻す
    //  system('stty -cbreak echo');
        return $c;
    }
    // 画面クリア
    public static function clearScreen()
    {
    //    printf("\x1b[2J"."\x1b[1;1H");
        system("clear");
    }
    // カーソル設定
    public static function printCursor($curx,$cury)
    {
        printf("\x1b[%d;%dH",$cury,$curx);
    }
    // m秒スリープ
    public static function sleep_mSec($mSec)
    {
        usleep($mSec*1000);
    }
}