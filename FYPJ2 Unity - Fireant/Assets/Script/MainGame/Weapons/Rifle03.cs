using UnityEngine;

public class Rifle03 : WeaponBase
{
    public Rifle03() //Carbine
    {
        WeaponName = "Rifle03";
        SecondsBetweenShots = 0.2f;
        WeaponSprite = Resources.Load<Sprite>("Carbine");
        WeaponDamage = 30;
    }
}

