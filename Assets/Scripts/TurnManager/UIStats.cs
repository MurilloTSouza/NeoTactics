using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStats : MonoBehaviour
{
    public Text nick;
    public Text level;

    public Slider hp;
    public Slider ap;

    public Text atk;
    public Text def;

    public Text move;
    public Text speed;
    public Text jump;

    public void SetStats(UnitStats stats)
    {
        nick.text = stats.nick;
        level.text = stats.level.ToString();

        hp.maxValue = stats.maxhp;
        hp.value = stats.hp;

        ap.maxValue = stats.maxap;
        ap.value = stats.ap;

        atk.text = stats.atk.ToString();
        def.text = stats.def.ToString();

        move.text = stats.move.ToString();
        speed.text = stats.speed.ToString();
        jump.text = stats.jump.ToString();
    }
}
