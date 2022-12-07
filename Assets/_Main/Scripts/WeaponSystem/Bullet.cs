using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletStartDamage = 1;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private int bulletHealth = 1;
    [SerializeField] private Rigidbody rb;

    private float bulletDamage;

    #region Properties
    public float BulletStartDamage { get => bulletStartDamage; private set => bulletStartDamage = value; }
    public float BulletSpeed { get => bulletSpeed; private set => bulletSpeed = value; }
    public int BulletHealth { get => bulletHealth; private set => bulletHealth = value; }
    public Rigidbody Rb { get => rb; private set => rb = value; }
    public float BulletDamage { get => bulletDamage; private set => bulletDamage = value; }
    #endregion

    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    public void SetDamage(float ammount)
    {
        bulletDamage = bulletStartDamage + ammount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        
        if (damageable != null)
        {
            damageable.GetDamage(bulletDamage);
        }

        bulletHealth--;
        if (bulletHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.GetDamage(bulletDamage);
        }

        bulletHealth--;
        if (bulletHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
