using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    ClassController player;

    public GameObject WarriorWeapon;
    public GameObject RangerWeapon;
    public GameObject WizardWeapon;

    public GameObject WarriorHat;
    public GameObject RangerHat;
    public GameObject WizardHat;
    public GameObject PaladinHat;
    public GameObject ThiefHat;
    public GameObject LancerHat;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerArmature").GetComponent<ClassController>();

        //WarriorHat = GameObject.Find("WarriorHat");
        //RangerHat = GameObject.Find("RangerHat");
        //WizardHat = GameObject.Find("WizardHat");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showRightWeapons()
    {
        hideItems();
        if (player.role == ClassController.Role.Warrior)
        {
            WarriorWeapon.SetActive(true);

            WarriorHat.SetActive(true);

        }
        if (player.role == ClassController.Role.Ranger)
        {
            RangerWeapon.SetActive(true);

            RangerHat.SetActive(true);
        }
        if (player.role == ClassController.Role.Wizard)
        {
            WizardWeapon.SetActive(true);

            WizardHat.SetActive(true);
        }

        if (player.role == ClassController.Role.Paladin)
        {
            //WarriorWeapon.SetActive(true);

            PaladinHat.SetActive(true);

        }
        if (player.role == ClassController.Role.Thief)
        {
            //RangerWeapon.SetActive(true);

            ThiefHat.SetActive(true);
        }
        if (player.role == ClassController.Role.Lancer)
        {
            //WizardWeapon.SetActive(true);

            LancerHat.SetActive(true);
        }
    }

    public void hideItems ()
    {
        WarriorWeapon.SetActive(false);
        RangerWeapon.SetActive(false);
        WizardWeapon.SetActive(false);

        WarriorHat.SetActive(false);
        RangerHat.SetActive(false);
        WizardHat.SetActive(false);
        PaladinHat.SetActive(false);
        ThiefHat.SetActive(false);
        LancerHat.SetActive(false);
    }
}

