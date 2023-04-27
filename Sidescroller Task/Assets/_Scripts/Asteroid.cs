using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float Speed { get; private set; }

    public void Init(float speed)
    {
        Speed = speed > 0 ? speed : 0.1f;
    }
}
