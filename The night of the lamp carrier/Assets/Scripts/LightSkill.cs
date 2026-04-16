using UnityEngine;
using System.Collections;
public class LightningSkill : MonoBehaviour
{
    [Header("技能按键")]
    public KeyCode skillKey = KeyCode.E;

    [Header("技能属性")]
    public int energyCost = 30;
    public float skillRange = 10f;
    public int damagePerHit = 15;
    public float timeBetweenHits = 0.2f;

    [Header("目标设置")]
    public LayerMask enemyLayer;

    private CharacterStats playerStats;

    private void Start()
    {
        playerStats = GetComponent<CharacterStats>();
        if (playerStats == null)
        {
            Debug.LogError("无法在玩家身上找到 CharacterStats 组件!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(skillKey))
        {
            AttemptToCastSkill();
        }
    }

    private void AttemptToCastSkill()
    {
        if (playerStats.currentEnergy >= energyCost)
        {
            CastSkill();
        }
        else
        {
            Debug.Log("能量不足，无法释放技能!");
        }
    }

    private void CastSkill()
    {
        playerStats.TakeConsumption(energyCost);
        Debug.Log("释放闪电技能，消耗 " + energyCost + " 能量。");

        Transform nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            Debug.Log("找到最近的敌人: " + nearestEnemy.name);
            CharacterStats enemyStats = nearestEnemy.GetComponent<CharacterStats>();
            if (enemyStats != null)
            {
                StartCoroutine(HitTargetTwice(enemyStats));
            }
        }
        else
        {
            Debug.Log("技能范围内没有敌人!");
        }
    }

    private Transform FindNearestEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skillRange, enemyLayer);

        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Collider2D col in colliders)
        {
            Vector3 directionToTarget = col.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = col.transform;
            }
        }

        return bestTarget;
    }

    private IEnumerator HitTargetTwice(CharacterStats targetStats)
    {
        Debug.Log("对 " + targetStats.name + " 造成第一次伤害: " + damagePerHit);
        targetStats.TakeDamage(damagePerHit);

        yield return new WaitForSeconds(timeBetweenHits);

        if (targetStats != null && !targetStats.isDead)
        {
            Debug.Log("对 " + targetStats.name + " 造成第二次伤害: " + damagePerHit);
            targetStats.TakeDamage(damagePerHit);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, skillRange);
    }
}