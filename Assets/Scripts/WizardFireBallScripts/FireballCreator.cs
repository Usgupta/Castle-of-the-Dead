using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TestTools;

public class FireballCreator : MonoBehaviour
{
    public int maxPrefabInScene = 3;
    public Vector2 velocity = new Vector2(10f,0f);
    public float impulseForce = 1;
    public float degree = 45;
    public GameObject attackPrefab;
    
    public void Start()
    {
        StartCoroutine(CreateFireballs());

    }

    public void Update()
    {
        
    }

    IEnumerator CreateFireballs()
    {
        GameObject[] instantiatedPrefabsInScene = GameObject.FindGameObjectsWithTag(attackPrefab.tag);

        while (true)
        {
            if (instantiatedPrefabsInScene.Length < maxPrefabInScene)
            {
                Debug.Log(this.gameObject.name + "sucks");
                Debug.Log("putting here"+this.gameObject.transform.position);
                // instantiate it where controller (mario) is
                Vector3 parentPosition = this.gameObject.transform.position;
                Vector3 fireballPosition = new Vector3(parentPosition.x - 0.5f, parentPosition.y + 0.1f, parentPosition.z );
                GameObject x = Instantiate(attackPrefab, fireballPosition, Quaternion.identity);
                x.transform.parent = this.gameObject.transform;

                // Get the Rigidbody component of the instantiated object
                Rigidbody2D rb = x.GetComponent<Rigidbody2D>();
                // Check if the Rigidbody component exists
                if (rb != null)
                {
                    
                    // rb.MovePosition(new Vector2(fireballPosition.x - 0.5f,fireballPosition.y));
                    // compute direction vector
                    // Apply a rightward impulse force to the object
                    // rb.AddForce(direction * impulseForce, ForceMode2D.Force);
                }

            }

            yield return new WaitForSecondsRealtime(6);

        }
       
    }
    

    
}