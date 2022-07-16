using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health; //the number that is our actual health
    public int maxHealth = 100; //our maximum health
    public Slider healthSlider; //the slider that represents our health

    void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.minValue = 0;
        healthSlider.SetDirection(Slider.Direction.LeftToRight, true); //I have no idea if this should be true or false
    }

    // Update is called once per frame
    void Update()
    {
        //UI Mumbo Jumbo, checks health in real time. need to make sure this works
    }
    public void heal(int healAmount)
    {
        int tempHealth = health;
        if(tempHealth + healAmount > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += healAmount;
        }
    }
    //make a damage function? or is this fine if we just pass the right colliders

    private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("Damage");
                health--;
                healthSlider.value = health;
        }
        }
}
