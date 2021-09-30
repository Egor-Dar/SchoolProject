using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

public class TextController : TextBase
{
    PlayerControl playerControl;

    private void Awake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        playerControl.Attack += WriteText;
    }


    protected override void WriteText([CanBeNull] object sender, EventArgs e)
    {
        Panel.SetActive(true);
        PlayerControl p = (PlayerControl) sender;

        textField.text =
            $"Враг имеет {p.enemyHealth} жизней, ты нанёс ему {p.playerDamage} урона. Сколько жизней осталось у врага?";
    }
}

public struct DisplacementValue
{
    public int HealthEnemy;
    public int Damage;

    public DisplacementValue(int initialHealthEnemy, int initialDamage)
    {
        HealthEnemy = initialHealthEnemy;
        Damage = initialDamage;
    }

    public static DisplacementValue operator
        +(DisplacementValue value)
    {
        return new DisplacementValue(value.HealthEnemy, value.Damage);
    }
}