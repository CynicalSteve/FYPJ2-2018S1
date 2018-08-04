using UnityEngine;

public class RocketObject : MonoBehaviour
{
    [SerializeField]
    public float RocketMovementSpeed = 0;
    public float RocketDamage = 70;

    GeneralMovement generalMovementScript;
    EnemyManager enemyManager;
    CharacterObject theCharacter;
    Vector3 Destination;
    Vector3 Direction;

    public bool CanHitPlayer = false;

    public void RocketObjectInit()
    {
        generalMovementScript = GameObject.FindGameObjectWithTag("GeneralScripts").GetComponent<GeneralMovement>();
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        Destination = new Vector3(0, 0, 0);

        theCharacter = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<CharacterObject>();
    }

    // Update is called once per frame
    public void RocketObjectUpdate()
    {
        generalMovementScript.moveBy(gameObject, RocketMovementSpeed, Direction);

        if (gameObject.transform.localPosition.x > theCharacter.theCanvas.transform.localPosition.x + theCharacter.theCanvas.GetComponent<RectTransform>().rect.width)
        {
            Destroy(gameObject);
        }
    }

    public void SetDestination(Vector3 newDestination)
    {
        Destination = newDestination;
    }

    public Vector3 GetDestination()
    {
        return Destination;
    }

    public void SetDirection(Vector3 newDirection)
    {
        Direction = newDirection;
    }
}
