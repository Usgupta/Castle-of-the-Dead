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
            increaseScore.Invoke(1);
            StartCoroutine(PlayEnemyDieAnimation());
        }
        
    }

    IEnumerator PlayEnemyDieAnimation()
    {

        EnemyAnimator.SetTrigger("enemyDie");
        EnemyAudio.PlayOneShot(EnemyAudio.clip);
        yield return new WaitForSecondsRealtime(1.1f);
        this.gameObject.SetActive(false);
        
    }
    
    
}