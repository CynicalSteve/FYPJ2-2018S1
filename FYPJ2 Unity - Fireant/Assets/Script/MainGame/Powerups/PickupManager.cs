using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupManager : MonoBehaviour {
    
    public enum POWERUP_TYPE
    {
        POWERUP_RANDOM = -1,
        POWERUP_INVINCIBILITY,
        POWERUP_HEALTH,

        TOTAL_POWERUPS
    }

    public enum SPECIALGUN_TYPE
    {
        SPECIALGUN_RANDOM = -1,


        TOTAL_SPECIALGUN
    }
    
    public void SpawnPowerUp(Vector3 position, POWERUP_TYPE powerupType = POWERUP_TYPE.POWERUP_RANDOM)
    {
        GameObject PowerupObj = null;

        if(powerupType == POWERUP_TYPE.POWERUP_RANDOM)
        {
           powerupType = (POWERUP_TYPE)Random.Range(0, (float)POWERUP_TYPE.TOTAL_POWERUPS - 1);
        }
        
        if (powerupType == POWERUP_TYPE.POWERUP_HEALTH)
        {
            PowerupObj = Instantiate(Resources.Load("HealthPowerup") as GameObject, position, gameObject.transform.rotation, gameObject.transform);
        }
        else if (powerupType == POWERUP_TYPE.POWERUP_INVINCIBILITY)
        {
            PowerupObj = Instantiate(Resources.Load("InvincibilityPowerup") as GameObject, position, gameObject.transform.rotation, gameObject.transform);
        }
        
        PowerupObj.GetComponent<RectTransform>().position = position;
        
    }

    public void SpawnSpecialGun(SPECIALGUN_TYPE specialGun = SPECIALGUN_TYPE.SPECIALGUN_RANDOM)
    {

    }


}
