using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;
    public Slider healthSlider;
    public TMP_Text bullets;

    
    // Start is called before the first frame update
    void Start()
    {
        setHealth(0.5f);
        setBullets(30);
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    void setBullets(int bullets)
    {
        string text = "BULLETS  " + bullets.ToString();
        this.bullets.text = text;
    }

    void setHealth(float health)
    {
        if (healthText)
        {
            string text = "HEALTH  " + (health * 100).ToString();
            healthText.text = text;
        }

        if (healthSlider)
        {
            healthSlider.value = health;
        }
    }
}
