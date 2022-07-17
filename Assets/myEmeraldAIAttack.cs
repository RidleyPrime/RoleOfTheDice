using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

public class myEmeraldAIAttack : MonoBehaviour
{
    public int AttackDamage = 5;
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            EmeraldAISystem emerald = collision.gameObject.GetComponent<EmeraldAISystem>();
            if(emerald != null)
            {
                emerald.Damage(AttackDamage, EmeraldAISystem.TargetType.Player, gameObject.transform);
                emerald.CurrentHealth -= AttackDamage;
                Debug.Log("Attack");
            }
        }
    }
}
