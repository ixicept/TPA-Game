using System.Collections;
using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    private int damageAmount;
    private float attackDelay = 0.6f;
    private int maxHitCount = 4;
    public void Setup(int damageAmount)
    {
        this.damageAmount = damageAmount;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(TakeDamage(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("Minion"))
        {
            StartCoroutine(TakeDamage(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            StartCoroutine(TakeDamage(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("Minotaur"))
        {
            StartCoroutine(TakeDamage(collision.gameObject));
        }
    }

    private IEnumerator TakeDamage(GameObject enemy)
    {


        if (enemy.CompareTag("Enemy"))
        {
            for (int i = 0; i < maxHitCount; i++)
            {
                enemy.GetComponent<RhinoScript>().TakeDamage(damageAmount);
                yield return new WaitForSeconds(attackDelay);
            }
        }
        else if (enemy.CompareTag("Minion"))
        {
            for (int i = 0; i < maxHitCount; i++)
            {
                enemy.GetComponent<MinionScript>().TakeDamage(damageAmount);
                yield return new WaitForSeconds(attackDelay);
            }
        }
        else if (enemy.CompareTag("Boss"))
        {
            for (int i = 0; i < maxHitCount; i++)
            {
                enemy.GetComponent<BossScript>().TakeDamage(damageAmount);
                yield return new WaitForSeconds(attackDelay);
            }
        }
        else if (enemy.CompareTag("Minotaur"))
        {
            for (int i = 0; i < maxHitCount; i++)
            {
                enemy.GetComponent<MinoScript>().TakeDamage(damageAmount);
                yield return new WaitForSeconds(attackDelay);
            }
        }
    }
}
