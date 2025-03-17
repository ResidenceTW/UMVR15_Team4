using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverFullHpPp : MonoBehaviour
{
    public bool isUseable = false;

    public bool isBeUse = false; //����u��ϥΤ@��
    public GameObject standbyParticle;
    public ParticleSystem useParticle1;
    public ParticleSystem useParticle2;
    public ParticleSystem useParticle3;
    private PlayerHealth health;

    private void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        GameInput.Instance.OnInteraction += HealthPlayer;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isUseable == false)
        {
            isUseable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isUseable == true)
        {
            isUseable = false;
        }
    }

    private void HealthPlayer()
    {
        if (isUseable == true && isBeUse == false)
        {
            //StartCoroutine(TeleportSequence());
            isUseable = false;
            isBeUse = true;
            standbyParticle.SetActive(false);

            useParticle1.gameObject.SetActive(true);
            useParticle1.Play();
            useParticle2.gameObject.SetActive(true);
            useParticle2.Play();
            useParticle3.gameObject.SetActive(true);
            useParticle3.Play();
            health.Heal(1000);
            health.HealPP(1000);

            AudioManager.Instance.PlaySound("Health", transform.position);
        }
    }
}
