using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ClassController : MonoBehaviour
{
    //controls what class we are in, modifying the speed and defense values, along with changing the attack function, animations, and model.
    
    
    /* 1: thief = moves quick, low range, squishy. High DPS knives. excels at hitting one enemy a lot in close range
     * 2: Ranger = single target ranged arrow attack, moves quick, squishy. Excels at taking out enemies from afar or fighting ranged battles, but suffers in close range
     * 3: Wizard = slow ranged aoe lighting bolt that roots them in place to use, but is devastating burst damage. poor against fast moving enemies
     * 4: Warrior = great dps, decent tankiness. Standard longsword. Good in most situations, but doesn't excel in any.
     * 5: Lancer = poke with spear, need to hit with tip to get good dps, requiring repositioning. great for 1 on 1s
     * 6: Paladin = Very tanky, slow aoe hammer attacks in a decent range, good vs swarms / hordes
     * 
    */
    public enum Roles 
    {
        Thief = 1,
        Ranger = 2,
        Wizard = 3,
        Warrior = 4,
        Lancer = 5,
        Paladin = 6
    }
    Roles role;
    
    public float attackMoveSpeedModifier = 0.1f;

    public Animator animator;
    public ThirdPersonController controller;

    //These floats are used for controlling character move speed during attacks
    private float tempSpeed;
    private float originalSpeed;
    private float timeElapsed;
    private float attackLerpDuration = .1f;




    private void Start()
    {
        role = Roles.Warrior;
        controller = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();

        originalSpeed = controller.SprintSpeed;
    }





    void OnAttack()
    {
        animator.SetInteger("Class", (int)role);
        animator.SetTrigger("Attack");

        if(animator.GetBool("MidAttack"))
        {
            animator.SetBool("CanCombo", true);
        }
    }

    void OnRoll()
    {
        animator.SetTrigger("Roll");
    }

    public void LockMovement()
        /*
         * Gets called at the beginning of attack animations in an animation event
         * 
         */
    {
        //animator.SetBool("CanMove", false);
        tempSpeed = attackMoveSpeedModifier * controller.SprintSpeed;
        controller.SprintSpeed = tempSpeed;
        animator.SetBool("MidAttack",true);
        //animator.SetBool("CanCombo", false);

    }

    public void UnlockMovement() // Gets called at end of attack animation in animation event
    {
        //animator.SetBool("CanMove", true);
        attackLerpDuration = attackMoveSpeedModifier;
        StartCoroutine(AttackLerp());
        animator.SetBool("MidAttack", false);

    }

    IEnumerator AttackLerp()//This lerps the attack speed back to the original speed
    {
        float timeElapsed = 0;
        while (timeElapsed < attackLerpDuration)
        {
            controller.SprintSpeed = Mathf.Lerp(tempSpeed, originalSpeed, timeElapsed / attackLerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        controller.SprintSpeed = originalSpeed;
    }
}
