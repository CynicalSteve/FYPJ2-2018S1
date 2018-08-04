using UnityEngine;

public class LMG : WeaponBase
{
    public LMG() //Carbine
    {
        WeaponName = "LMG";
        SecondsBetweenShots = 0.1f;
        WeaponSprite = Resources.Load<Sprite>("LMG");
        WeaponDamage = 30;
    }
}

