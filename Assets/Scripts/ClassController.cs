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
        /*
         * This is the combo system that uses bool parameters in the animator to transition between the attacks back to idle
         */
        animator.SetInteger("Class", (int)role);
        animator.SetTrigger("Attack");
        bool attacking = animator.GetBool("MidAttack");
        bool attack1 = animator.GetBool("Attack1");
        bool attack2 = animator.GetBool("Attack2");
        bool attack3 = animator.GetBool("Attack3");


        if (!attack2&!attack3)
        {
            animator.SetBool("Attack1", true);
        }

        if (attacking)
        {
            animator.SetBool("Attack2", true);
            animator.SetBool("Attack1", false);


        }
        if (attack2)
        {
            animator.SetBool("Attack3", true);

        }
    }

    void OnRoll()
    {
        animator.SetTrigger("Roll");
    }

    public void LockMovement()
        /*
         * Gets called at the beginning of attack animations in an animation event
         */
    {
        tempSpeed = attackMoveSpeedModifier * controller.SprintSpeed;
        controller.SprintSpeed = tempSpeed;

        animator.ResetTrigger("Attack");


    }

    public void UnlockMovement() // Gets called at end of attack animation in animation event
    {
        attackLerpDuration = attackMoveSpeedModifier;
        StartCoroutine(AttackLerp());
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

    public void OpenComboWindow()//Called at beginning of attack animations
    {
        animator.SetBool("MidAttack", true);
    }
    public void CloseComboWindow()//Called at end of attack animations
    {
        animator.SetBool("MidAttack", false);
    }
}

