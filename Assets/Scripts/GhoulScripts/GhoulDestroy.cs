using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GhoulDestroy : MonoBehaviour
{
    public Animator EnemyAnimator;
    public AudioSource EnemyAudio;
    public UnityEvent<int> increaseScore;
    public void DestroyEnemy()
    {
        increaseScore.Invoke(1);
        StartCoroutine(PlayEnemyDieAnimation());
    }

    IEnumerator PlayEnemyDieAnimation()
    {
        EnemyAnimator.Play("ghoul-die");
        Debug.Log("play die animaiton");
        EnemyAudio.PlayOneShot(EnemyAudio.clip);
        yield return new WaitForSecondsRealtime(EnemyAudio.clip.length);
        gameObject.SetActive(false);
        
    }
    
}
