using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class DialogueTake : MonoBehaviour
{
    //�ב����x���`
    public UnityEvent openAction;
    //�ב�������萎��u
    public TextMeshProUGUI TextComponent;
    public string[] Lines;
    public float TextSpeed;
    public float WaitForNextLine;
    //�p�ҝS��ב�����Iint
    private int Index;
    //�p�Ғʒm�l�����x�����o��������I�ϔh
    public event Action Take1Finish;
    //�p�Ғʒm�l���Y��Y������
    public event Action LastTakeAction;

    void Start()
    {
        TextComponent.text = string.Empty;

        StartCoroutine(UIAnimation());
        StartCoroutine(DisplayDialogue());
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
        Take1Finish?.Invoke();
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
}
