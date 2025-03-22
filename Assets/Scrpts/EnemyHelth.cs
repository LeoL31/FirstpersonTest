using JetBrains.Annotations;
using UnityEngine;

public class EnemyHelth : MonoBehaviour, Damageable
{

    [SerializeField] int maxHelth = 100;
    [SerializeField] int damage = 25;
    private int helth;
    void Start()
    {
        helth = maxHelth;
    }

    public void TakeDamage()
    {
        helth -= damage;
        if (helth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

}
