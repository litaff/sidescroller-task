using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private int bonus;
    [SerializeField] private AsteroidSize size;
    [SerializeField] private float speed;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="deviation"> Speed is multiplied by this </param>
    public void DeviateSpeed(float deviation)
    {
        if (deviation > 0)
        {
            speed *= deviation;
        }
    }

    public AsteroidSize GetSize()
    {
        return size;
    }

    public float GetSpeed()
    {
        return speed;
    }
    
    public int GetBonus()
    {
        return bonus;
    }
}
