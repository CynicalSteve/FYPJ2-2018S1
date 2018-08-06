using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField]
    public float DetectionRadius = 500;
    [SerializeField]
    public float AttackRadius = 50;
    [SerializeField]
    public float Health = 100;
    [SerializeField]
    public float MaximumHealth = 100;
    [SerializeField]
    public float MovementSpeed = 10;
    [SerializeField]
    public float secondsBetweenShots = 1;
    [SerializeField]
    public float fireRateTimer;
    [SerializeField]
    public Image HealthBar;
    [SerializeField]
    public float WeaponDamage = 10;

    protected bool canShoot = true;

    protected STATE_ENEMY enemyState;

    protected CharacterObject theCharacter;
    protected GeneralMovement generalMovementScript;
    protected Image enemyTexture;

    protected enum STATE_ENEMY
    {
        STATE_IDLE,
        STATE_CHASE,
        STATE_ATTACK,

        TOTAL_ENEMY_STATES
    }

    public abstract void EnemyInit();

    public abstract void EnemyUpdate();

    public void Shoot()
    {
        //Create a bullet and add it as child to Scene control and object List
        GameObject BulletObj = Instantiate(Resources.Load("GenericBullet") as GameObject, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent.transform);

        //Init Bullet variables
        BulletObj.GetComponent<BulletObject>().BulletObjectInit();

        //Set the bullet to be able to hit player
        BulletObj.GetComponent<BulletObject>().CanHitPlayer = true;

        //Set the pos of bullet to the enemy pos
        BulletObj.transform.position.Set(gameObject.transform.position.x, gameObject.transform.position.y, 0);

        //Rotate the bullet in the direction of destination
        Vector3 normalizedDir = (theCharacter.transform.position - BulletObj.transform.position).normalized;
        normalizedDir.z = 0;
        BulletObj.transform.up = normalizedDir;

        BulletObj.GetComponent<BulletObject>().BulletDamage = WeaponDamage;

        //Set the bullet's destination to player
        BulletObj.GetComponent<BulletObject>().SetDirection(theCharacter.transform.position - gameObject.transform.position);

        //Add bullet obj to list
        gameObject.transform.parent.GetComponent<EnemyManager>().BulletList.Add(BulletObj);
    }

    //Getters & setters
    public float GetHealth()
    {
        return Health;
    }
    public void SetHealth(float newHealth)
    {
        Health = newHealth;
    }
    public void DecreaseHealth(float reduceAmount)
    {
        Health -= reduceAmount;

        //Update Health Bar Appearance
        HealthBar.fillAmount = Health / MaximumHealth;

        if (Health <= 0)
        {
            Destroy(gameObject);
            theCharacter.money += 20;
        }
    }
    public void IncreaseHealth(float increaseAmount)
    {
        Health += increaseAmount;

        if (Health > MaximumHealth)
        {
            Health = MaximumHealth;
        }

        //Update Health Bar Appearance
        HealthBar.fillAmount = Health / MaximumHealth;
    }
}