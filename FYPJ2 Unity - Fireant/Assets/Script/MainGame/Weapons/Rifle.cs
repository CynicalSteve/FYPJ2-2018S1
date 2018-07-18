using UnityEngine;

public class Rifle : WeaponBase
{
    public Rifle()
    {
        WeaponName = "Rifle";
        SecondsBetweenShots = 0.3f;
        WeaponSprite = Resources.Load<Sprite>("RifleTexture");
    }
}

