using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    [SerializeField] HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Damage");
            health--;
            healthBar.SetHealth(health);
        }
    }
}
