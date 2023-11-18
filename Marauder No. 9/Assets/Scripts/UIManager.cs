using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;
    public Slider healthSlider;
    public TMP_Text bulletText;
    public TMP_Text armorText;
    public Slider armorSlider;
    public TMP_Text goldText;


    // Start is called before the first frame update
    void Start()
    {
        if (healthSlider)
        {
            healthSlider.maxValue = 100;
            healthSlider.wholeNumbers = true;
        }
        if (armorSlider)
        {
            armorSlider.maxValue = 100;
            armorSlider.wholeNumbers = true;
        }
        SetHealth(5);
        //SetBullets(30);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBullets(int bullets)
    {
        if (bulletText)
        {
            string text = "BULLETS  " + bullets.ToString();
            this.bulletText.text = text;
        }
    }

    public void SetHealth(int health)
    {
        if (healthText)
        {
            string text = "HEALTH  " + health.ToString();
            healthText.text = text;
        }

        if (healthSlider)
        {
            healthSlider.value = health;
        }
    }

    public void SetArmor(float armor)
    {
        if (armorText)
        {
            string text = "HEALTH  " + (armor * 100).ToString();
            armorText.text = text;
        }

        if (armorSlider)
        {
            armorSlider.value = armor;
        }
    }
    public void SetGold(int gold)
    {
        if (goldText)
        {
            string text = "GOLD  " + gold.ToString();
            goldText.text = text;
        }
    }
}
