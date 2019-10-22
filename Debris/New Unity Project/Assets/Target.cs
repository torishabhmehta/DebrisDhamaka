
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100f;
    public void TakeDamage (float amt)
    {
      
            Die();
        
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
