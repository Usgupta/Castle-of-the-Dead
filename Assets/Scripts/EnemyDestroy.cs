using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDestroy : MonoBehaviour
{
    public Animator EnemyAnimator;
    public AudioSource EnemyAudio;
    public UnityEvent<int> increaseScore;
    public void DestroyEnemy(String name)
    {
        if (name == this.gameObject.name)
        {
            Debug.Log("wanteed to kill"+ name);
            // this.GetComponent<Collider>().
            Debug.Log(this.gameObject.name);
            Debug.Log("trying to kill");
            increaseScore.Invoke(1);
            Debug.Log("start to kill");
            StartCoroutine(PlayEnemyDieAnimation());
        }
        
    }

    IEnumerator PlayEnemyDieAnimation()
    {
        Debug.Log("trigger pls");
        EnemyAnimator.SetTrigger("enemyDie");
        // EnemyAnimator.Play("ghoul-die");
        Debug.Log("play die animaiton");
        EnemyAudio.PlayOneShot(EnemyAudio.clip);
        yield return new WaitForSecondsRealtime(1.1f);
        this.gameObject.SetActive(false);
        
    }
    
}