using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    ClassController player;

    public GameObject WarriorWeapon;
    public GameObject RangerWeapon;
    public GameObject WizardWeapon;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerArmature").GetComponent<ClassController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.role==ClassController.Role.Warrior)
        {
            WarriorWeapon.SetActive(true);
            RangerWeapon.SetActive(false);
            WizardWeapon.SetActive(false);


        }
        if (player.role == ClassController.Role.Ranger)
        {
            RangerWeapon.SetActive(true);
            WarriorWeapon.SetActive(false);
            WizardWeapon.SetActive(false);
        }
        if(player.role==ClassController.Role.Wizard)
        {
            WizardWeapon.SetActive(true);
            WarriorWeapon.SetActive(false);
            RangerWeapon.SetActive(false);

        }
    }
}

