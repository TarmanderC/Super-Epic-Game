using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Attack", menuName = "Combat/Attack")]
public class Attack : ScriptableObject
{
    public String attackName;
    public int TUCost;
    public int APCost;
    public int minDamage;
    public int maxDamage;
    public AnimationClip attackAnimation;
    public GameObject attackButton;
}
