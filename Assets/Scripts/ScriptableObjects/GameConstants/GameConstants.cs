using UnityEngine;

[CreateAssetMenu(fileName ="GameConstants", menuName ="ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    //Monk's movement
    public int speed;
    public int maxSpeed;
    public int upSpeed;
    public int flickerInterval;
    public int flickerspeed;
    public Vector3 monkStartingposition = new Vector3(-4f, -2.98f, 0f);
    public int maxTimer = 45;

    //Ghoul's movement
    public float ghoulPatrolTime;
    public float ghoulMaxOffset;
}