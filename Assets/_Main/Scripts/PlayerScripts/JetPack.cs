using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    [SerializeField] private float jetpackForce = 10f;
    [SerializeField] private float fuelConsumptionRate = 0.1f;
    [SerializeField] private float fuelRegenerationRate = 0.01f;
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] private ParticleSystem jetPackVFX;

    private float fuel;
    private Rigidbody rb;
    private bool isJumping = false;

    public float MaxFuel { get => maxFuel; private set => maxFuel = value; }
    public float Fuel { get => fuel; private set => fuel = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Fuel = MaxFuel;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            UseJetPack();
            ActivateVFX(true);
        }
        else
        {
            isJumping = false;
            ActivateVFX(false);
        }

        RegenerateFuelSystem();
    }

    private void RegenerateFuelSystem()
    {
        if (!isJumping && Fuel < MaxFuel)
        {
            Fuel += fuelRegenerationRate;
        }
    }

    private void UseJetPack()
    {
        if (Fuel > 0)
        {
            rb.AddForce(new Vector3(0, jetpackForce, 0), ForceMode.Force);
            Fuel -= fuelConsumptionRate;
           
        }
        else
        {
            isJumping = false;
        }
    }

    private void ActivateVFX(bool activate)
    {
        if (activate)
        {
            jetPackVFX.gameObject.SetActive(true);
            jetPackVFX.Play();
            return;
        }

        jetPackVFX.Stop();

    }

    [ContextMenu("TEST - Refill Jetpack")]
    private void TEST_RefillJetPack()
    {
        Fuel = maxFuel;
    }

    [ContextMenu("TEST - Infinit Fuel")]
    private void TEST_InfinitFuel()
    {
        Fuel = 10000000000;
        fuelConsumptionRate = 0;
    }
}
