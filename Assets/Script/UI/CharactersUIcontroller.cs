using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharactersUIcontroller : MonoBehaviour
{
    //�ב����x���`
    public UnityEvent openAction;
    //�S�挀��UI���
    public DialogueTake dialogue;


    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(UIAnimation());
        dialogue.Take1Finish += enabledUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void enabledUI()
    {
        StartCoroutine(UIAnimation());
    }

    private IEnumerator UIAnimation()
    {
        yield return new WaitForSeconds(2.1f);
        gameObject.GetComponent<CharactersUIcontroller>().openAction.Invoke();
    }
}
