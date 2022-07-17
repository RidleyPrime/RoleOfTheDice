using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health; //the number that is our actual health
    public int maxHealth = 100; //our maximum health
    public Slider healthSlider; //the slider that represents our health
    public TextMeshProUGUI Win;

    public void Start()
     {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        Win.enabled = false;
    }

    public void heal(int healAmount)
    {
        if(health + healAmount > maxHealth)
        {
            health = maxHealth;
        }
        else if (health + healAmount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Die
        }
        else
        {
            health += healAmount;
        }
        healthSlider.value = health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Void")
        {
            Debug.Log("Damage");
            heal (-100);
            //  healthSlider.value = health;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Win")
        {
            Debug.Log("Damage");
            Win.enabled = true;
        }
    }
}
