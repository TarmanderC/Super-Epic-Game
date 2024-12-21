using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BattleEnemy", menuName = "Combat/BattleEnemy")]
public class BattleEnemy : ScriptableObject
{
    public String enemyName;
    public int maxHealth;
    public List<Attack> attacks;
    public AnimationClip idleAnimation;
    public AnimationClip attackAnimation;
    public AnimationClip specialAnimation;
    public AnimationClip hurtAnimation;
    public AnimationClip dieAnimation;
}
