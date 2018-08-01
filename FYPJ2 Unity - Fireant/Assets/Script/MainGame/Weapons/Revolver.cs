using UnityEngine;

public class Revolver : WeaponBase
{
    public Revolver()
    {
        WeaponName = "Revolver";
        SecondsBetweenShots = 1.5f;
        WeaponSprite = Resources.Load<Sprite>("Revolver");
        WeaponDamage = 25;
    }
}
