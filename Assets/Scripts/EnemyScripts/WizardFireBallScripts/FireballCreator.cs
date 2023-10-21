using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TestTools;

public class FireballCreator : MonoBehaviour
{
    public int maxPrefabInScene = 3;
    public GameObject attackPrefab;
    
    public void Start()
    {
        StartCoroutine(CreateFireballs());

    }

    IEnumerator CreateFireballs()
    {
        GameObject[] instantiatedPrefabsInScene = GameObject.FindGameObjectsWithTag(attackPrefab.tag);

        while (true)
        {
            if (instantiatedPrefabsInScene.Length < maxPrefabInScene)
            {
                // instantiate it where controller (mario) is
                Vector3 parentPosition = this.gameObject.transform.position;
                Vector3 fireballPosition = new Vector3(parentPosition.x - 1.3f, parentPosition.y + 0.65f, parentPosition.z );
                GameObject x = Instantiate(attackPrefab, fireballPosition, Quaternion.identity);
                x.transform.parent = this.gameObject.transform;
            }

            yield return new WaitForSecondsRealtime(5);

        }
       
    }
    

    
}