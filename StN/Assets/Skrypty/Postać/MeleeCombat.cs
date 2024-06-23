using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    public Animator animator;
    public Transform Attack_Point;
    public Vector2 boxSize = new Vector2(1.0f, 0.5f);
    public LayerMask enemyLayers;

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator nie jest przypisany!");
        }
        if (Attack_Point == null)
        {
            Debug.LogError("Attack_Point nie jest przypisany!");
        }
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
          Attack();
        }
    }
    void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            Debug.LogError("Animator jest null!");
        }
    }


    public void checkAttack()
    {
        if (Attack_Point != null)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(Attack_Point.position, boxSize,0f, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Uda³o Ci siê trafiæ " + enemy.name);
            }
        }
        else
        {
            Debug.LogError("Attack_Point jest null!");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (Attack_Point != null)
        {
            Gizmos.DrawWireCube(Attack_Point.position, boxSize);
        }
        else
        {
            Debug.LogError("Attack_Point jest null!");
        }
    }
}
