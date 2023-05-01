using UnityEngine;

public class SpawnPositionManager : MonoBehaviour
{
    [SerializeField] private float spawnHeight;
    [SerializeField] private float spawnDeltaX;

    public Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(-spawnDeltaX, spawnDeltaX), spawnHeight);
    }
    
    private void OnDrawGizmosSelected()
    {
        // visualize spawning area
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            new Vector3(0f, spawnHeight, 0f), 
            new Vector3(spawnDeltaX*2, .5f, 1f));
    }
}