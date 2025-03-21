using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class BossSceneDialogue : MonoBehaviour
{
    //���unity�檺�ʵe
    public Animator animator;
    //�x�s��r�������e
    public TextMeshProUGUI TextComponent;
    public string[] Lines;
    public string[] Lines2;
    public float TextSpeed;
    public float WaitForNextLine;
    //�O����r�i�שһ�Index
    private int Index;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("NPC").GetComponent<Animator>();
    }

    public void FirstTalkToPlayer()
    {
        StartCoroutine(TalkDialogue());
    }

    IEnumerator TalkDialogue()
    {
        yield return new WaitForSeconds(1f);
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
                AudioManager.Instance.PlaySound("Bye", transform.position);
            }
        }

        yield return new WaitForSeconds(4f);
    }

    IEnumerator TypeLine(string line)
    {
        foreach (char c in line.ToCharArray())
        {
            TextComponent.text += c;
            yield return new WaitForSeconds(TextSpeed);
        }
    }

}
