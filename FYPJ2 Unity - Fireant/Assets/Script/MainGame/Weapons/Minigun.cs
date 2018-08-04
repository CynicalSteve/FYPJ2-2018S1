using UnityEngine;

public class Minigun : WeaponBase
{
    public Minigun() //Carbine
    {
        WeaponName = "Minigun";
        SecondsBetweenShots = 0.05f;
        WeaponSprite = Resources.Load<Sprite>("Minigun");
        WeaponDamage = 30;
    }
}

