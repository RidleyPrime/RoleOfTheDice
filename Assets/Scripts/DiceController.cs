using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    PlayerHealth health;
    PlayerMeter meter;
    public int numDice = 0;
    public int maxDice = 3;
    public int diceHeal = 25;

    void Start()
    {
        health = GetComponent<PlayerHealth>();
        meter = GetComponent<PlayerMeter>();
    }

    public void CollectDice()
    {
        if(numDice < maxDice)
        {
            numDice++;
        }
        if(numDice == maxDice)
        {
            Consume();
        }
    }

    void OnUse() //consume your saved dice at any time
    {
        if (numDice > 0)
        {
            numDice--;
            Consume();
        }
    }

    void Consume()
    {
        health.heal(diceHeal);
        //restore meter and reroll next class
    }

    // Update is called once per frame
    void Update()
    {
        //UI Mumbo jumbo, track numdice
    }
}
