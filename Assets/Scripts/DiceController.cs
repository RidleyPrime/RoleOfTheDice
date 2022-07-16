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
    public int diceMeter = 0;
    [SerializeField] Slider diceMeterUI;
    [SerializeField] Slider diceOverchargeUI;
    [SerializeField] Image nextRoleImage;

    [SerializeField] Image dice1;
    [SerializeField] Image dice2;
    [SerializeField] Image dice3;

    [SerializeField] List<Sprite> diceFaces = new List<Sprite>();

    void Start()
    {
        health = GetComponent<PlayerHealth>();
        currentClass = GetComponent<ClassController>();
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
            diceMeter = 150;
        }
        diceMeterUI.value = diceMeter;
        diceOverchargeUI.value = 50 - (diceMeter - 100);

        
    }

    private void RollNextRole()
    {
        HashSet<ClassController.Role> validRoles = new HashSet<ClassController.Role>(System.Enum.GetValues(typeof(ClassController.Role)) as IEnumerable<ClassController.Role>);
        validRoles.Remove(currentClass.role);
        validRoles.Remove((ClassController.Role)nextRole);
        var newRole = validRoles.OrderBy(e => Random.Range(System.Int32.MinValue, System.Int32.MaxValue)).FirstOrDefault();
        nextRole = (int)newRole;
        Debug.Log("Next Dice is " + nextRole);

        nextRoleImage.sprite = diceFaces[nextRole - 1];
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
        RollNextRole();
        if (diceMeter < 150)
        {
            diceMeter = 150; // otherwise set meter to full, and reveal next role
            SetDiceCharge(diceMeter);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
