using UnityEngine;

public class Pistol : WeaponBase
{
    public Pistol()
    {
        WeaponName = "Pistol";
        SecondsBetweenShots = 1.0f;
        WeaponSprite = (Sprite)Resources.Load("PistolTexture");
    }
}
