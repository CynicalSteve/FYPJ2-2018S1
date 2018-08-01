using UnityEngine;

public class Shotgun : WeaponBase
{
    public Shotgun()
    {
        WeaponName = "Shotgun";
        SecondsBetweenShots = 2.0f;
        WeaponSprite = Resources.Load<Sprite>("Shotgun");
        WeaponDamage = 15;
    }
}
