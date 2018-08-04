using UnityEngine;

public class Carbine : WeaponBase
{
    public Carbine() //Carbine
    {
        WeaponName = "Carbine";
        SecondsBetweenShots = 0.2f;
        WeaponSprite = Resources.Load<Sprite>("Carbine");
        WeaponDamage = 30;
    }
}

