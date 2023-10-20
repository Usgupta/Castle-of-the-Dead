using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class FireballController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float scaleSpeed = 1.0f;
    public UnityEvent playerDamaged;

    void Start()
    {
        StartCoroutine(ScaleAndDestroyCoroutine());
        GetComponent<AudioSource>().Play();
    }

    private IEnumerator ScaleAndDestroyCoroutine()
    {
        // Wait for 2 seconds
        transform.GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x - 0.1f,transform.position.y));
        yield return new WaitForSecondsRealtime(0.5f);
        // Gradually scale down the GameObject
        while (transform.localScale.x > 0.01f)
        {
            transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
            transform.GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x - 0.1f,transform.position.y));
            yield return null;
        }

        // Ensure the GameObject is completely scaled down
        transform.localScale = Vector3.zero;

        // Destroy the GameObject
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision happened with"+ collision.gameObject.name);
        if (collision.gameObject.tag == "Enemy")
        {
            // destroy self
            Destroy(gameObject);
        }
        
        if (collision.gameObject.tag == "Player")
        {
            // destroy self
            playerDamaged.Invoke();
            
        }
    }
}