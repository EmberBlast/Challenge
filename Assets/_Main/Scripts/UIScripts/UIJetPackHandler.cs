using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIJetPackHandler : MonoBehaviour
{
    [SerializeField] private JetPack jetPack;
    [SerializeField] private Image fuelBar;
  
    // Update is called once per frame
    void Update()
    {
        UpdateJetPackStatus();   
    }

    private void UpdateJetPackStatus()
    {
        float fuel = Mathf.Lerp(0, 1, jetPack.Fuel / jetPack.MaxFuel);
        fuelBar.transform.localScale = new Vector2(1, fuel);
    }
}
