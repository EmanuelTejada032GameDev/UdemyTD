using UnityEngine;

public class SimpleTestGlass : MonoBehaviour
{
    public float gizmosRadius;

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, gizmosRadius);
    }
}
