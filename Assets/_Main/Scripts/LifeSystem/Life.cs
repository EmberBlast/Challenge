using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxLife;

    private float actualLife;

    public Action onDamage { get => damage; set => damage = value; }
    public Action onHeal { get => heal; set => heal = value; }
    public Action onDeath { get => death; set => death = value; }

    private Action damage;
    private Action heal;
    private Action death;


    private void Start()
    {
        actualLife = maxLife;
    }

    public void RestoreLife()
    {
        actualLife = maxLife;
    }

    public void GetDamage(float damage)
    {
        onDamage?.Invoke();
        actualLife--;
        
        if (actualLife <= 0)
        {
            actualLife = 0;
            Die();
        }
    }

    public void GetHealed(float healAmmount)
    {
        onHeal?.Invoke();
        actualLife++;
        if (actualLife >= maxLife)
        {
            actualLife = maxLife;
        }
    }

    public void Die()
    {
        onDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public float GetActualLifePercentage()
    {
        return actualLife / maxLife;
    }

    public float GetActualLife()
    {
        return actualLife;
    }
}
