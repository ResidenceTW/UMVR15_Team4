using UnityEngine;

public class BossController : MonoBehaviour
{
    private Material bossMaterial;
    private float dissolveProgress = 0f;
    private bool isBossDead = false;

    void Start()
    {
        bossMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (isBossDead)
        {
            dissolveProgress += Time.deltaTime * 0.5f; //����B�{�t��
            bossMaterial.SetFloat("_DissolveAmount", Mathf.Clamp01(dissolveProgress));
        }
    }

    //�o�Ӥ�k���Ӧb BOSS ���`�ɳQ�I�s�I
    public void OnBossDeath()
    {
        isBossDead = true;
    }
}
