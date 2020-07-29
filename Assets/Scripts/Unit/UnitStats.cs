using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats
{
    public string nick;
    public int level;
    public int exp;

    public int maxhp;
    public int hp;
    public int maxap;
    public int ap;

    public int atk;
    public int def;

    public int speed;
    public int move;
    public int jump;
}

public class UnitStatsBuilder {

    private UnitStats stats;

    public UnitStatsBuilder() { Reset(); }
    public UnitStatsBuilder Reset() { stats = new UnitStats(); return this; }
    public UnitStatsBuilder WithNick(string nick) { stats.nick = nick; return this; }
    public UnitStatsBuilder WithLevel(int level) { stats.level = level; return this; }
    public UnitStatsBuilder WithExp(int exp) { stats.exp = exp; return this; }
    public UnitStatsBuilder WithMaxHp(int maxhp) { stats.maxhp = maxhp; return this; }
    public UnitStatsBuilder WithHp(int hp) { stats.hp = hp; return this; }
    public UnitStatsBuilder WithMaxAp(int maxap) { stats.maxap = maxap; return this; }
    public UnitStatsBuilder WithAp(int ap) { stats.ap = ap; return this; }
    public UnitStatsBuilder WithAtk(int atk) { stats.atk = atk; return this; }
    public UnitStatsBuilder WithDef(int def) { stats.def = def; return this; }
    public UnitStatsBuilder WithSpeed(int speed) { stats.speed = speed; return this; }
    public UnitStatsBuilder WithMove(int move) { stats.move = move; return this; }
    public UnitStatsBuilder WithJump(int jump) { stats.jump = jump; return this; }
    public UnitStats Build() { return stats; }











}

