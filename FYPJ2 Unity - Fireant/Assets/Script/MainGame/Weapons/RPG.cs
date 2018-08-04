using UnityEngine;

public class RPG : WeaponBase
{
    public RPG()
    {
        WeaponName = "RPG";
        SecondsBetweenShots = 2.0f;
        WeaponSprite = Resources.Load<Sprite>("RPG");
        WeaponDamage = 100;
    }
}
