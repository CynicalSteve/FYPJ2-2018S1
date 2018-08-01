using UnityEngine;

public class Rifle02 : WeaponBase
{
    public Rifle02() //AK-47
    {
        WeaponName = "Rifle02";
        SecondsBetweenShots = 0.5f;
        WeaponSprite = Resources.Load<Sprite>("AK47");
        WeaponDamage = 35;
    }
}

