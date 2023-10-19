using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameConstants", menuName ="ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    public int maxlives;

    //Monk's movement
    public int speed;
    public int maxSpeed;
    public int upSpeed;
    public int deathImpulse;
    public Vector3 marioStartingPostion;
    public int flickerInterval;

    //Goomba's movement

    public float goombaPatrolTime;
    public float goombaMaxOffset;
}