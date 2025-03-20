using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class DialogueTake : MonoBehaviour
{
    //�����ʵe
    public UnityEvent openAction;
    //�x�s��r�������e
    public TextMeshProUGUI TextComponent;
    public string[] Lines;
    public string[] Lines2;
    public float TextSpeed;
    public float WaitForNextLine;
    //�O����r�i�שһ�Index
    private int Index;
    //�ݭn�������Ĥ@�D�̻����
    public GameObject LockWall;
    //�Ĥ@�ϳ̫�@�q��r�һݶǰe��delegate�]�D�n�Ω󱱨�unity��)
    public event Action LastTakeAction;
    //�@��������ݭn�ǰe��delegate
    public event Action TakeFinish;

    void Start()
    {
        TextComponent.text = string.Empty;

        StartCoroutine(UIAnimation());
        StartCoroutine(DisplayDialogue());
    }

    public void AreaTwoTakes()
    {
        TextComponent.text = string.Empty;
        gameObject.SetActive(true);
        StartCoroutine(OpenUIAnimation());
        StartCoroutine(DisplayDialogue2());
    }

    IEnumerator DisplayDialogue()
    {
        yield return new WaitForSeconds(3.5f);
        for (Index = 0; Index < Lines.Length; Index++)
        {
            yield return StartCoroutine(TypeLine(Lines[Index]));

            if (Index < Lines.Length - 1) 
            {
                yield return new WaitForSeconds(WaitForNextLine);
                TextComponent.text = string.Empty;
            }
            if (Index == Lines.Length - 1)
            {
                LastTakeAction?.Invoke();
            }
        }

        yield return new WaitForSeconds(4f);
        TakeFinish?.Invoke();
        LockWall?.SetActive(false);
        StartCoroutine(CloseUIAnimation());
    }

    IEnumerator DisplayDialogue2()
    {
        yield return new WaitForSeconds(1f);
        for (Index = 0; Index < Lines2.Length; Index++)
        {
            yield return StartCoroutine(TypeLine(Lines2[Index]));

            if (Index < Lines2.Length - 1)
            {
                yield return new WaitForSeconds(WaitForNextLine);
                TextComponent.text = string.Empty;
            }
            if (Index == Lines2.Length - 1)
            {
                LastTakeAction?.Invoke();
            }
        }

        yield return new WaitForSeconds(4f);
        TakeFinish?.Invoke();
        StartCoroutine(CloseUIAnimation());
    }

    IEnumerator TypeLine(string line)
    {
        foreach(char c in line.ToCharArray())
        {
            TextComponent.text += c;
            yield return new WaitForSeconds(TextSpeed);
        }
    }

    private IEnumerator UIAnimation()
    {
        yield return new WaitForSeconds(2.1f);
        gameObject.GetComponent<DialogueTake>().openAction.Invoke();
    }
    private IEnumerator CloseUIAnimation()
    {
        gameObject.GetComponent<DialogueTake>().openAction.Invoke();
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
    private IEnumerator OpenUIAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<DialogueTake>().openAction.Invoke();
    }
}
