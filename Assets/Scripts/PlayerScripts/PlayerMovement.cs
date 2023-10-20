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
    public AudioSource monkAudio;
    public AudioClip monkJumpAudio;
    public AudioClip monkPunchAudio;
    public AudioClip monkKickAudio;

    public AudioClip monkDieAudio;
    [System.NonSerialized]
    public bool alive = true;
    public Transform gameCamera;

    private bool moving = false;
    private bool jumpedState = false;
    public UnityEvent gameOver;
    public UnityEvent<String> killEnemy;

    public IntVariable gameMana;
    public UnityEvent<int> changeMana;

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
        
        
        // if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && faceRightState)
        // {
        //     faceRightState = false;
        //     monkSprite.flipX = true;
        //     // if (marioBody.velocity.x > 0.1f)
        //         // marioAnimator.SetTrigger("onSkid");
        // }
        //
        // if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !faceRightState)
        // {
        //     faceRightState = true;
        //     monkSprite.flipX = false;
        //     // if (marioBody.velocity.x < -0.1f)
        //     //     marioAnimator.SetTrigger("onSkid");
        // }
        monkAnimator.SetFloat("xSpeed", Mathf.Abs(monkBody.velocity.x));
    }

    int collisionLayerMask = ( (1 << 6));

    void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
            onGroundState = true;
        //update animator state

        monkAnimator.SetBool("onGround", onGroundState);

    }

    void FixedUpdate()
    {   
        // if (GetComponent<MarioStateController>().currentState.name == "DeadMario")
        //     alive = false;
        // else
        // {
        //     alive = true;
        // }
        if (alive && moving){
            Move(faceRightState ==true? 1:-1);
        }
        //
        //
        // if (!alive)
        // {
        //     marioBody.gameObject.layer = 3;
        // }
        // else{
        //     marioBody.gameObject.layer = 0;
        // }
        // float moveHorizontal = Input.GetAxisRaw("Horizontal");
        //
        //
        // if (Mathf.Abs(moveHorizontal) > 0)
        // {
        //
        //     Vector2 movement = new Vector2(moveHorizontal, 0);
        //
        //     if (monkBody.velocity.magnitude < maxSpeed)
        //         monkBody.AddForce(movement * speed);
        //
        // }
        // //stop
        //
        // if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        // {
        //     //stop
        //     monkBody.velocity = Vector2.zero;
        // }
        // // Debug.Log("fix update");
        // // Debug.Log(onGroundState);
        // // Debug.Log("call func");
        // if (Input.GetKeyDown(KeyCode.Space) && onGroundState)
        // {   
        //     monkBody.velocity.Set(monkBody.velocity.x,0);
        //     monkBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
        //     onGroundState = false;
        //     // Debug.Log(onGroundState);
        //     //update animator state
        //     monkAnimator.SetBool("onGround", onGroundState);
        // }
    }

    // }

    

    void FlipMarioSprite(int value)
    {
    if( value == -1 && faceRightState)
    {
        // faceRightState = false;
        updateMarioShouldFaceRight(false);
        monkSprite.flipX = true;
        // if (marioBody.velocity.x > 0.1f)
            // marioAnimator.SetTrigger("onSkid");
    
    } 
    else if (value == 1 && !faceRightState)
    {
        updateMarioShouldFaceRight(true);
        monkSprite.flipX = false;
        // if (monkBody.velocity.x < -0.1f)
        //     marioAnimator.SetTrigger("onSkid");
    }
    }

    public void MoveCheck(int value)
    {
        
        // Debug.Log(moving.ToString());
        if(value == 0)
            moving = false;
        else 
        {
            Debug.Log("move received "+ value.ToString());
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    void Move(int value)
    {
        Vector2 movement = new Vector2(value, 0);
        Debug.Log("move ment is in" + movement.ToString());

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

    public void Kick()
    {
        if (gameMana.Value >= 30)
        {
            monkAnimator.Play("monk-kick");
            changeMana.Invoke(-30);
            // gameMana.Value = gameMana.Value - 30;
            Debug.Log(gameMana.Value);
            PlayKickSound();
        }
        
    }
    
    public void Punch()
    {
        if (gameMana.Value >= 30)
        {
            monkAnimator.Play("monk-punch");
            changeMana.Invoke(-30);
            Debug.Log(gameMana.Value);
            PlayPunchSound();
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            
            Debug.Log("Collided with " + other.gameObject.name);

            if (monkAnimator.GetCurrentAnimatorClipInfo(this.gameObject.layer)[0].clip.name == "Kick" ||
                monkAnimator.GetCurrentAnimatorClipInfo(this.gameObject.layer)[0].clip.name == "Punch")
            {
                killEnemy.Invoke(other.gameObject.name);
                // Debug.Log("kill enemy");
            }
            
            else
            {
                MonkDie();
                Debug.Log("kill player");
            }
            
        }
        }

       

    public void MonkDie()
    {
        monkAnimator.Play("monk-die");
        StartCoroutine(BlinkSpriteRenderer());
        alive = false;
        // DamageMario();
    }
    
    private IEnumerator BlinkSpriteRenderer()
    {
        // monkSprite = GetComponent<SpriteRenderer>();
        for (int i = 0; i < gameConstants.flickerspeed; i++)
        {
            monkSprite.enabled = !monkSprite.enabled;

            // Wait for the specified blink interval
            yield return new WaitForSeconds((int)(gameConstants.flickerInterval/gameConstants.flickerspeed));
        }
        
        this.gameObject.SetActive(false);
        monkSprite.enabled = true;
    }

    public void RestartGame()
    {
        //reset position
        monkBody.transform.position = new Vector3(-7.523f, -6.098f, 0.0f);
        //reset face direction
        monkSprite.flipX = false;
        faceRightState = true;
   
        //stop mario
        monkBody.velocity = Vector2.zero;
        alive = true;
        gameCamera.transform.localPosition = new Vector3(1.69f, 0, -10);
        monkBody.gameObject.layer = 1;
    }

    void GameOverScene()
    {
        Debug.Log("you are dying");
        gameOver.Invoke();
        // GameManager.instance.GameOver();
        // Time.timeScale = 0.0f;
        // GameOverScreen.SetActive(true);
        // RestartButton.transform.localPosition = new Vector3(-68, -143, 0.0f);
        // scoreText.transform.localPosition = new Vector3(37, -20, 0);

    }

    void PlayJumpSound()
    {
        monkAudio.PlayOneShot(monkJumpAudio);
    }

    void PlayWalkSound()
    {
        monkAudio.PlayOneShot(monkAudio.clip);
    }
    
    void PlayPunchSound()
    {
        monkAudio.PlayOneShot(monkPunchAudio);
    }
    
    void PlayKickSound()
    {
        monkAudio.PlayOneShot(monkKickAudio);
    }

    public void PlayDeathImpluse()
    {
        // monkBody.velocity = Vector2.zero;
        // monkBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
        // monkAudio.PlayOneShot(monkDieAudio);
        // new WaitForSeconds(2);
        // this.gameObject.SetActive(false);
    }

    public void SetStartingPosition(Scene current, Scene next)
    {
        if(next.name == "World-1-2")
            monkBody.transform.position = new Vector3(-11.72f, -6.18f, 0);
    }


    private void updateMarioShouldFaceRight(bool value)
    {
        faceRightState = value;
        marioFaceRight.SetValue(value);
    }
   
}
