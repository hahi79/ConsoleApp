<?php
//======================================
//	ターン制バトル  Main
//======================================

enum Command : int
{
    case Fight=0;  // たたかう
    case Spell=1;  // じゅもん
    case Escape=2; // にげる
    case Max=3;

    public function commandName(): string{
        return match($this){
            Command::Fight => "たたかう",
            Command::Spell => "じゅもん",
            Command::Escape => "にげる",
        };
    }
}
