using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    PlayerHealth health;
    public int numDice = 0;
    public int maxDice = 3;
    public int diceHeal = 25;

    public int nextRole = 0;
    public int diceMeter = 0;
    [SerializeField] Slider diceMeterUI;
    [SerializeField] Slider diceOverchargeUI;

    [SerializeField] Image dice1;
    [SerializeField] Image dice2;
    [SerializeField] Image dice3;

    [SerializeField] List<Sprite> diceFaces = new List<Sprite>();

    void Start()
    {
        health = GetComponent<PlayerHealth>();
        ShowDice();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Powerup")
        {
            CollectDice();
            

            Destroy(other.gameObject);
        }
    }

    private void SetDiceCharge(int dicemeter)
    {
        if (diceMeter >= 150)
        {
            if (nextRole == 0) // if nextRole is Zero, its not rolled yet, so roll it.
            {
                RollNextRole();
            }
            diceMeter = 150;
        }
        diceMeterUI.value = diceMeter;
        diceOverchargeUI.value = diceMeter - 100;

    }

    private void RollNextRole()
    {
        nextRole = Random.Range(0, 6);
    }

    public void CollectDice()
    {
        if(numDice < maxDice)
        {
            numDice++;
            ShowDice();
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
            ShowDice();
            Consume();
        }
    }

    void ShowDice() //Display how many dice you have
    {
        Debug.Log("You have" + numDice);
        if (numDice == 0)
        {
            dice1.enabled = false;
            dice2.enabled = false;
            dice3.enabled = false;
        }
        else if (numDice == 1)
        {
            dice1.enabled = true;
            dice2.enabled = false;
            dice3.enabled = false;
        }
        else if (numDice == 2)
        {
            dice1.enabled = true;
            dice2.enabled = true;
            dice3.enabled = false;
        }
        else if (numDice == 3)
        {
            dice1.enabled = true;
            dice2.enabled = true;
            dice3.enabled = true;
        }
    }

    void Consume()
    {
        health.heal(diceHeal);
        if (diceMeter >= 150)
        {
            RollNextRole(); // if dice meter is already max, reroll next role
        }
        else
        {
            diceMeter = 150; // if dice meter is already max, reroll next role
            SetDiceCharge(diceMeter);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //UI Mumbo jumbo, track numdice
    }
}
