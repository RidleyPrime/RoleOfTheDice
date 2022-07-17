using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using Cinemachine;

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
    public enum Role //changing it to just Role instead of Roles, since singular enum is common practice
    {
        Thief = 1,
        Ranger = 2,
        Wizard = 3,
        Warrior = 4,
        Lancer = 5,
        Paladin = 6
    }
    public Role role;
    
    public float attackMoveSpeedModifier = 0.1f;

    public Animator animator;
    public ThirdPersonController controller;
    public InputAction leftClick;

    public GameObject RangerProjectile;
    public float RangerProjectileSpeed = 20f;
    public DiceController dice;
    public GameObject WizardProjectile;
    public float WizardProjectileSpeed = 15f;

    //These floats are used for controlling character move speed during attacks
    private float tempSpeed;
    private float originalSpeed;
    private float timeElapsed;
    private float attackLerpDuration = .1f;

    [SerializeField] WeaponManager weaponManager;
    [SerializeField] DiceController diceController;


    private void Start()
    {
        //role = Role.Ranger;
        controller = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        dice = GetComponent<DiceController>();
        originalSpeed = controller.SprintSpeed;
    }

    void OnAttack()
    {
        /*
         * This is the combo system that uses bool parameters in the animator to transition between the attacks back to idle
         */
        if(role==Role.Warrior)
        {
            animator.SetTrigger("Attack");

            if (animator.GetBool("MidAttack"))
            {
                gameObject.transform.LookAt(GameObject.Find("PlayerAimPivot").transform);
            }

            //Warrior Start
            bool attacking = animator.GetBool("MidAttack");
            bool attack1 = animator.GetBool("Attack1");
            bool attack2 = animator.GetBool("Attack2");
            bool attack3 = animator.GetBool("Attack3");


            if (!attack2 & !attack3)
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
        //Warrior End


        //Ranger Start
        if(role==Role.Ranger)
        {
            animator.SetTrigger("Attack");
            
            //gameObject.transform.LookAt(GameObject.Find("PlayerAimPivot").transform);
        }
        //Ranger End

        //Wizard Start

        if(role==Role.Wizard)
        {
            animator.SetTrigger("Attack");
        }
        //Wizard End
    }

    void OnRoll()
    {
        /*if (diceController.diceMeter >= 100)
        {
            animator.SetTrigger("Roll");

            if (diceController.nextRole != -1)
            {
                role = (Role)diceController.nextRole;
            }
            else
            {
                role = (Role)Random.Range(1, 7);
            }
            diceController.ResetDiceCharge();
            weaponManager.showRightWeapons();
        }*/
        if (dice.canRoll())
        {
            animator.SetTrigger("Roll");
            Debug.Log("Roll!!!!");
            dice.ResetDiceCharge();
            role = dice.getNextRole();
            dice.RoleReady = false;
            weaponManager.showRightWeapons();
        }

    }

    private void Update()
    {
        animator.SetInteger("Class", (int)role);

        if(animator.GetBool("MidAttack"))
        {
            gameObject.transform.LookAt(GameObject.Find("PlayerAimPivot").transform);
        }
        //RANGER RELEASE CHECK
        if (role == Role.Ranger)
        {
            if (!animator.GetBool("MidAttack"))
            {
                animator.SetBool("Attack2", true);
            }
            else
            {
                gameObject.transform.LookAt(GameObject.Find("PlayerAimPivot").transform);
            }
        }

        //Wizard Rotation Control
        if (role == Role.Wizard)
        {
            if (animator.GetBool("MidAttack"))
            {
                gameObject.transform.LookAt(GameObject.Find("PlayerAimPivot").transform);
            }
        }
    }

    void RangerAttack()
    {
        Transform pivot = GameObject.Find("Left_Hand").transform;

        GameObject tempArrow = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        tempArrow.transform.localScale = new Vector3(0.1f,.1f,.1f);
        tempArrow.AddComponent<Rigidbody>();
        tempArrow.GetComponent<Rigidbody>().useGravity = false;
        tempArrow.layer = 11;
        tempArrow.AddComponent<myEmeraldAIAttack>();

        GameObject arrow = Instantiate(tempArrow, pivot.position, pivot.rotation);
        arrow.GetComponent<Rigidbody>().velocity = -RangerProjectileSpeed*Vector3.Normalize(pivot.position - GameObject.Find("PlayerAimPivot").transform.position);
    }

    void WizardAttack()
    {
        Transform pivot = GameObject.Find("WizardAttackPivot").transform;

        GameObject tempProjectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        tempProjectile.transform.localScale = new Vector3(0.5f, .5f, .5f);
        tempProjectile.AddComponent<Rigidbody>();
        tempProjectile.GetComponent<Rigidbody>().useGravity = false;
        tempProjectile.layer = 11;

        GameObject projectile = Instantiate(tempProjectile, pivot.position, pivot.rotation);
        projectile.GetComponent<Rigidbody>().velocity = -RangerProjectileSpeed * Vector3.Normalize(pivot.position - GameObject.Find("PlayerAimPivot").transform.position);


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

