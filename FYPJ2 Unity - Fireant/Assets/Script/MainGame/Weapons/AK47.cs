using UnityEngine;

public class AK47 : WeaponBase
{
    public AK47() //AK-47
    {
        WeaponName = "AK47";
        SecondsBetweenShots = 0.5f;
        WeaponSprite = Resources.Load<Sprite>("AK47");
        WeaponDamage = 35;
    }
}

