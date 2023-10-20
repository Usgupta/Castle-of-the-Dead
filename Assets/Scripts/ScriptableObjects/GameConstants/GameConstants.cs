using UnityEngine;

[CreateAssetMenu(fileName ="GameConstants", menuName ="ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    //Monk's movement
    public int speed;
    public int maxSpeed;
    public int upSpeed;
    public int flickerInterval;

    //Ghoul's movement
    public float ghoulPatrolTime;
    public float ghoulMaxOffset;
}