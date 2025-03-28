using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FlySword : BaseSkill
{
    private float lifeTime = 0.8f;
    private float speed = 15f;
    private float radius = 4f;
    private float attackIntervalTime = 0.5f;
    private Vector3 attackPosition;
    private Collider[] hitColliderArray;

    public override void SkillAbility()
    {
        Transform playerTransform = FindObjectOfType<PlayerController>()?.transform;

        if (playerTransform != null)
        {
            transform.position = playerTransform.position;
            transform.forward = playerTransform.forward;
            skillParticleSystem.Play();

            StartCoroutine(AttackCheckCoroutine());
            AudioManager.Instance.PlaySound("FlySword", transform.position);
        }
    }

    private IEnumerator AttackCheckCoroutine()
    {
        attackPosition = transform.position;
        float timer = 0f;

        while(timer < lifeTime)
        {
            timer += Time.deltaTime;
            attackPosition += transform.forward * speed * Time.deltaTime;

            hitColliderArray = Physics.OverlapSphere(attackPosition, radius);
            foreach (Collider hit in hitColliderArray)
            {
                if (hit.TryGetComponent(out Health enemyHealth))
                {
                    enemyHealth.TakeDamage(GetSkillDataSO().damage);
                }
            }

            yield return new WaitForSeconds(attackIntervalTime);
            timer += attackIntervalTime;
        }
    }
}
