using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{
    public ParticleSystem particle;

    public Image blackScreen;

    public Image titleEleOpen;
    private RectTransform titleEleOpenRect;

    public Image titleEle;

    public Image titleText;
    private RectTransform titleTextRect;

    public Image titleCircle;
    private RectTransform titleCircleRect;

    public RectTransform flash;
    public Image shadowImage;

    public GameObject ButtonList;
    public RectTransform ButtonListStart;
    public RectTransform ButtonListSettings;
    public RectTransform ButtonListExit;

    public InputNameUI inputNameUI;
    public ExitUI exitUI;

    private float rotationSpeed = 20f;
    private float targetSpeed = 20f;

    private EasyInOut easyInOut;

    private void Awake()
    {
        titleEleOpenRect = titleEleOpen.GetComponent<RectTransform>();
        titleCircleRect = titleCircle.GetComponent<RectTransform>();
        titleTextRect = titleText.GetComponent<RectTransform>();
    }

    private void Start()
    {
        StartCoroutine(titleOpening());
    }

    // Update is called once per frame
    void Update()
    {
        //���߶�ئ���ĪG
        rotationSpeed = Mathf.Lerp(rotationSpeed, targetSpeed, Time.deltaTime * 5f);

        // �����ϥ� Time.deltaTime �p�����q�A�T�O���|�]�t���ܤƦӦ^�u
        titleCircleRect.rotation *= Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator titleOpening()
    {
        easyInOut = FindObjectOfType<EasyInOut>();
        StartCoroutine(easyInOut.ChangeValue(
           new Vector4(0f, 0f, 0f, 1f), Vector4.zero, 3f,
           value => blackScreen.color = value,
           EasyInOut.EaseIn));

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(easyInOut.ChangeValue(
            0f, 839f, 1f,
            value => titleEleOpenRect.sizeDelta = new Vector2(value, titleEleOpenRect.sizeDelta.y),
            EasyInOut.EaseIn));

        yield return new WaitForSeconds(1f);
        titleEleOpen.gameObject.SetActive(false);
        //-----
        titleEle.gameObject.SetActive(true);
        titleCircle.gameObject.SetActive(true);
        titleText.gameObject.SetActive(true);
        flash.gameObject.SetActive(true);


        //��r���D
        StartCoroutine(easyInOut.ChangeValue(
           new Vector3(1.1f, 1.1f, 1.1f), Vector3.one, 1f,
           value => titleTextRect.localScale = value,
           EasyInOut.EaseOut));

        //���߶��
        StartCoroutine(easyInOut.ChangeValue(
            new Vector3(1.2f, 1.2f, 1.2f), Vector3.one, 2f,
            value => titleCircleRect.localScale = value,
            EasyInOut.EaseOut));

        //�{��
        StartCoroutine(easyInOut.ChangeValue(
            new Vector4(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f),
            new Vector4(255 / 255f, 255 / 255f, 255 / 255f, 0f),
            1f,
           value => flash.GetComponent<Image>().color = value,
           EasyInOut.EaseOut));

        //�������v
        shadowImage.gameObject.SetActive(true);
        StartCoroutine(easyInOut.ChangeValue(
           Vector4.zero,
           new Vector4(0f, 0f, 0f, 0.4f),
           1f,
          value => shadowImage.color = value,
          EasyInOut.EaseOut));

        //�ɤl�ĪG
        particle.gameObject.SetActive(true);
        particle.Play();

        //��歸�J
        yield return new WaitForSeconds(1.25f);
        ButtonListStart.anchoredPosition = new Vector2(120f, -30f);
        ButtonListSettings.anchoredPosition = new Vector2(120f, -115f);
        ButtonListExit.anchoredPosition = new Vector2(120f, -200f);

        ButtonList.gameObject.SetActive(true);
        StartCoroutine(easyInOut.ChangeValue(
           0f, 1f, 1f,
          value => ButtonList.GetComponent<CanvasGroup>().alpha = value,
          EasyInOut.EaseOut));

        //Start
        ButtonListStart.gameObject.SetActive(true);
        StartCoroutine(easyInOut.ChangeValue(
          ButtonListStart.anchoredPosition, new Vector2(100f, -30f), 1.3f,
          value => ButtonListStart.anchoredPosition = value,
          EasyInOut.EaseOut));
        yield return new WaitForSeconds(0.5f);

        //Settings
        ButtonListSettings.gameObject.SetActive(true);
        StartCoroutine(easyInOut.ChangeValue(
          ButtonListSettings.anchoredPosition, new Vector2(100f, -115f), 1.5f,
          value => ButtonListSettings.anchoredPosition = value,
          EasyInOut.EaseOut));
        yield return new WaitForSeconds(0.35f);

        //Exit
        ButtonListExit.gameObject.SetActive(true);
        StartCoroutine(easyInOut.ChangeValue(
          ButtonListExit.anchoredPosition, new Vector2(100f, -200f), 1.7f,
          value => ButtonListExit.anchoredPosition = value,
          EasyInOut.EaseOut));
    }

    //Start�ƥ�--
    public void OnClickStartButton()
    {
        StartCoroutine(StartOpening());
    }

    private IEnumerator StartOpening()
    {
        //�����Ҧ����䤬��
        ButtonListStart.GetComponent<Button>().enabled = false;
        ButtonListSettings.GetComponent<Button>().enabled = false;
        ButtonListExit.GetComponent<Button>().enabled = false;

        //�{��
        StartCoroutine(easyInOut.ChangeValue(
            new Vector4(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f),
            new Vector4(255 / 255f, 255 / 255f, 255 / 255f, 0f),
            1f,
           value => flash.GetComponent<Image>().color = value,
           EasyInOut.EaseOut));

        //���߶�ج�M�[�t����
        float originalSpeed = rotationSpeed; //�O����l�t��
        targetSpeed = 200f; //�]�w�u�ȥ[�t�t��

        StartCoroutine(easyInOut.ChangeValue(
           Vector3.one, new Vector3(1.05f, 1.05f, 1.05f),  0.5f,
           value => titleCircleRect.localScale = value,
           EasyInOut.EaseOut));

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(easyInOut.ChangeValue(
           new Vector3(1.05f, 1.05f, 1.05f), Vector3.one, 0.5f,
           value => titleCircleRect.localScale = value,
           EasyInOut.EaseInOut));

        yield return new WaitForSeconds(0.3f);
        targetSpeed = originalSpeed;

        //�¹�
        StartCoroutine(easyInOut.ChangeValue(
           Vector4.zero, new Vector4(0f, 0f, 0f, 1f),  1.5f,
           value => blackScreen.color = value,
           EasyInOut.EaseIn));

        yield return new WaitForSeconds(2f);
        inputNameUI.ShowInputNameUI();

    }

    //Exit�ƥ�--
    public void OnClickExitButton()
    {
        StartCoroutine(ExitOpening());
    }

    private IEnumerator ExitOpening()
    {
        exitUI.ShowExitUI();
        yield return null;
    }
}
