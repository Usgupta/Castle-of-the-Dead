using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public GameConstants gameConstants;
    public float speed;
    public float maxSpeed;

    public float upSpeed;
    private bool onGroundState = true;
    private SpriteRenderer monkSprite;
    private bool faceRightState = true;
    public BoolVariable marioFaceRight;

    private Rigidbody2D monkBody;

    public Animator monkAnimator;
    [System.NonSerialized]
    public bool alive = true;
    public Transform gameCamera;

    private bool moving = false;
    private bool jumpedState = false;
    public UnityEvent gameOver;
    public UnityEvent<String> killEnemy;

    public IntVariable gameMana;
    public UnityEvent<int> changeMana;
    int collisionLayerMask = ( (1 << 6));

    // Start is called before the first frame update

    void Start()
    {
        speed = gameConstants.speed;
        maxSpeed = gameConstants.maxSpeed;
        upSpeed = gameConstants.upSpeed;
        gameMana.Value = 100;
        
        Application.targetFrameRate = 30;
        monkBody = GetComponent<Rigidbody2D>();
        monkSprite = GetComponent<SpriteRenderer>();

        //update animator state
        monkAnimator.SetBool("onGround", onGroundState);
        monkBody.gameObject.layer = 0;
        monkBody.transform.position = gameConstants.monkStartingposition;
        
        StartCoroutine(updateMana());

    }
    
    IEnumerator updateMana()
    {
        while(gameMana.Value <=100)
        {
            if(gameMana.Value!=100)
                changeMana.Invoke(10);
            yield return new WaitForSecondsRealtime(3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        monkAnimator.SetFloat("xSpeed", Mathf.Abs(monkBody.velocity.x));
    }
    
    void FixedUpdate()
    {   
     
        if (alive && moving){
            Move(faceRightState ==true? 1:-1);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
            onGroundState = true;
        //update animator state
        monkAnimator.SetBool("onGround", onGroundState);

    }
    
    public void MoveCheck(int value)
    {
        if(value == 0)
            moving = false;
        else 
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }
    void FlipMarioSprite(int value)
    {
        if( value == -1 && faceRightState)
        {
            UpdateMonkShouldFaceRight(false);
            monkSprite.flipX = true;
        } 
        else if (value == 1 && !faceRightState)
        {
            UpdateMonkShouldFaceRight(true);
            monkSprite.flipX = false;
        }
    }
    
    private void UpdateMonkShouldFaceRight(bool value)
    {
        faceRightState = value;
        marioFaceRight.SetValue(value);
    }

    void Move(int value)
    {
        Vector2 movement = new Vector2(value, 0);
        if (monkBody.velocity.magnitude <= maxSpeed)
            monkBody.AddForce(movement * speed);
    }

    public void Jump(){
        if(alive && onGroundState){
            jumpedState = true;
            onGroundState = false;
            monkBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            monkAnimator.SetBool("onGround", onGroundState);
            PlayJumpSound();
        }
    }

    public void Jumphold(){
        if(alive && jumpedState){
            //jump higher
            monkBody.AddForce(Vector2.up * upSpeed * 50, ForceMode2D.Force);
            jumpedState = false;
            PlayJumpSound();
        }
    }
    
    void PlayJumpSound()
    {
        this.GetComponent<PlayerAudio>().PlayJumpSound();
    }

    public void Kick()
    {
        if (gameMana.Value >= 30 && alive)
        {
            monkAnimator.Play("monk-kick");
            changeMana.Invoke(-30);
        }
        
    }
    
    public void Punch()
    {
        if (gameMana.Value >= 30 && alive)
        {
            monkAnimator.Play("monk-punch");
            changeMana.Invoke(-30);
        }
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (CheckIfMonkInAction())
            {
                killEnemy.Invoke(other.gameObject.name);
            }
            else
            {
                MonkDie();
            }
        }
    }

    private bool CheckIfMonkInAction()
    {
        return monkAnimator.GetCurrentAnimatorClipInfo(this.gameObject.layer)[0].clip.name == "Kick" ||
               monkAnimator.GetCurrentAnimatorClipInfo(this.gameObject.layer)[0].clip.name == "Punch";
    }
    public void MonkDie()
    {
        monkAnimator.Play("monk-die");
        StartCoroutine(BlinkSpriteRenderer());
        alive = false;
    }
    
    private IEnumerator BlinkSpriteRenderer()
    {
        for (int i = 0; i < gameConstants.flickerspeed; i++)
        {
            monkSprite.enabled = !monkSprite.enabled;
            // Wait for the specified blink interval
            yield return new WaitForSeconds((int)(gameConstants.flickerInterval/gameConstants.flickerspeed));
        }
        gameOver.Invoke();
        monkSprite.enabled = true;
    }
   
}
