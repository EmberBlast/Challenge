using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Guns/New Gun")]
public class Gun : ScriptableObject
{
    [Header("Gun stats")]
    [SerializeField] private float gunDamage;
    [SerializeField] private float gunFiringRate;
    [SerializeField] private float gunFiringTime;
    [SerializeField] private ProjectileTag projectileTag;

    [Header("VFX")]
    [SerializeField] private GameObject muzzlePrefab;

    GameObject bullet;

    #region Properties
    public float GunDamage { get => gunDamage; private set => gunDamage = value; }
    public float GunFiringRate { get => gunFiringRate; private set => gunFiringRate = value; }
    public ProjectileTag ProjectileTag { get => projectileTag; private set => projectileTag = value; }
    public float GunFiringTime { get => gunFiringTime; private set => gunFiringTime = value; }
    #endregion

    public void Shoot(Transform exitPoint)
    {
        GameObject muzzle = Instantiate(muzzlePrefab, exitPoint.position ,Quaternion.identity);
        bullet = ProjectilePool.Instance.GetItem(ProjectileTag);
        
        if (bullet)
        {
            Bullet b = bullet.GetComponent<Bullet>();
            b.SetDamage(GunDamage);
            b.Rb.velocity = Vector3.zero;
            bullet.gameObject.transform.position = exitPoint.position;
            b.Rb.AddForce(exitPoint.TransformDirection(Vector3.forward) * b.BulletSpeed, ForceMode.Impulse);
        }
    }
}
