using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private Life life;
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private bool lookAtCamera = true;
    
    // Start is called before the first frame update
    void Start()
    {
        life.onDamage += UpdateLifeBar;
        lifeText.text = life.GetActualLife().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Billboard effect
        if(lookAtCamera) 
            transform.rotation = Camera.main.transform.rotation;
    }

    private void UpdateLifeBar()
    {
        float ammount = Mathf.Lerp(0,1,life.GetActualLifePercentage());
        fill.transform.localScale = new Vector2(ammount, 1);
        lifeText.text = life.GetActualLife().ToString();
    }
}
