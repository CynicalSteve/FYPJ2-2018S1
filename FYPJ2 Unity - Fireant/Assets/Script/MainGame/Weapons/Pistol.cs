using UnityEngine;

public class Pistol : WeaponBase
{
    public Pistol()
    {
        WeaponName = "Pistol";
        SecondsBetweenShots = 1.0f;
        WeaponSprite = Resources.Load<Sprite>("Pistol");
        WeaponDamage = 10;
    }
}
