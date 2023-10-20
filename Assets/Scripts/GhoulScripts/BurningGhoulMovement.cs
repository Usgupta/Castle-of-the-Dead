using UnityEngine;

public class BurningGhoulMovement : MonoBehaviour
{

    public GameConstants gameConstants;
    private float originalX;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    public Vector3 startPosition = new Vector3(0.0f,0.0f,0.0f);
    
    void Start()
    {
        enemyBody = gameObject.GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = gameObject.transform.localPosition.x;
        ComputeVelocity();

    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * gameConstants.ghoulMaxOffset / gameConstants.ghoulPatrolTime, 0);
    }
    void Moveghoul()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {   
        if (Mathf.Abs(enemyBody.gameObject.transform.localPosition.x - originalX) < gameConstants.ghoulMaxOffset)
        {
            Moveghoul();
        }
        else
        {
            changeDirection();
        }
        
    }
    
    void changeDirection()
    {
        Debug.Log("collision");
        moveRight *= -1;
        enemyBody.gameObject.GetComponent<SpriteRenderer>().flipX =
            !enemyBody.gameObject.GetComponent<SpriteRenderer>().flipX;
        ComputeVelocity();
        Moveghoul();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        changeDirection();
    }

    public void GameRestart()
    {      //reset ghoul
        if(!gameObject.active)
        {
            gameObject.SetActive(true);
        }
        gameObject.transform.localPosition = new Vector3(originalX,0.0f,0.0f);
        originalX = gameObject.transform.localPosition.x;
        ComputeVelocity();
        
    }
    
}