using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private SimplePlayer sp;
    public Text display;
    // Start is called before the first frame update
    void Start()
    {
        sp = GameObject.FindGameObjectWithTag("Player").GetComponent<SimplePlayer>();
        sp.SetDamageCallback(OnDamage);
        OnDamage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDamage()
    {
        float minDamage = sp.health;
        float maxDamage = sp.healthMax;

        string output = string.Format("{0,0:F0}/{1,0:F0}", minDamage, maxDamage);
        display.text = output;
    }
}
