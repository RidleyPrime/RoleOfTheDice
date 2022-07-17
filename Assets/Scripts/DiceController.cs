using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    PlayerHealth health;
    ClassController currentClass;
    
    public int numDice = 0;
    public int maxDice = 3;
    public int diceHeal = 25;
 
    public int nextRole = -1;
    public bool RoleReady = false;
    public int diceMeter = 0;
    public int diceMeterRollCost = 100;
    public int overChargeThreshold = 50;
    private int diceMeterMax;

    //UI DiceMeter
    [SerializeField] Slider diceMeterUI;
    [SerializeField] Slider diceOverchargeUI;
    [SerializeField] Image nextRoleImage;
    //PowerUps
    [SerializeField] Image dice1;
    [SerializeField] Image dice2;
    [SerializeField] Image dice3;
    //DisplayNextDiceFace
    [SerializeField] List<Sprite> diceFaces = new List<Sprite>();
    [SerializeField] Sprite defualtDiceFace;
    //Recharge
    float Timer;
    public int DelayAmount = 1;

    void Start()
    {
        health = GetComponent<PlayerHealth>();
        currentClass = GetComponent<ClassController>();
        diceMeterMax = diceMeterRollCost + overChargeThreshold;
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
    private void updateUI()
    {
        diceMeterUI.value = diceMeter;
        diceOverchargeUI.value = overChargeThreshold - (diceMeter - diceMeterRollCost);
        if(diceMeter >= diceMeterMax)
        {
            nextRoleImage.sprite = diceFaces[nextRole - 1];
        }
    }

    public void AddDiceCharge(int diceCharge)
    {
        if (diceCharge + diceMeter >= diceMeterMax)
        {
            diceMeter = diceMeterMax;
        }
        else
        {
            diceMeter += diceCharge;
            if(diceMeter >= diceMeterRollCost && !RoleReady)
            {
                RollNextRole();
            }
        }
        updateUI();
    }
    public void ResetDiceCharge()
    {
        diceMeter = 0;
        nextRoleImage.sprite = defualtDiceFace;
        updateUI();
    }
    public void FillDiceCharge()
    {
        diceMeter = diceMeterMax;
        updateUI();
    }

    public ClassController.Role getNextRole()
    {
        if(!RoleReady) 
        {
            RollNextRole();
        }
        return (ClassController.Role)nextRole;
    }
    private void RollNextRole()
    {
        HashSet<ClassController.Role> validRoles = new HashSet<ClassController.Role>(System.Enum.GetValues(typeof(ClassController.Role)) as IEnumerable<ClassController.Role>);
        validRoles.Remove(currentClass.role);
        validRoles.Remove((ClassController.Role)nextRole);
        var newRole = validRoles.OrderBy(e => Random.Range(System.Int32.MinValue, System.Int32.MaxValue)).FirstOrDefault();
        nextRole = (int)newRole;
        Debug.Log("Next Dice is " + nextRole);
        RoleReady = true;
        updateUI();
    }
    void OnQuit()
    {
        Application.Quit(); 
    }

    public void CollectDice()
    {
        if(numDice < maxDice)
        {
            numDice++;
            ShowDice();
        }
        else if(numDice == maxDice)
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
        //Debug.Log("You have " + numDice);
        //Debug.Log("Current class is " + (int)currentClass.role);
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
        RollNextRole();
        FillDiceCharge();
    }

    public bool canRoll() 
    {
        if (diceMeter >= diceMeterRollCost)
        {
            return true;
        }
        else if(numDice > 0) 
        {
            numDice--;
            ShowDice();
            Consume();
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update() //RechargeDiceMeter Over Time
    {
        Timer += Time.deltaTime;

        if (Timer >= DelayAmount)
        {
            Timer = 0f;
            AddDiceCharge(5); 
        }
    }

    
}
