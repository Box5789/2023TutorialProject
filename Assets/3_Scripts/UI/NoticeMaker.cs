using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoticeMaker : MonoBehaviour
{
    [SerializeField] GameObject notice;
    [SerializeField] TextMeshProUGUI txt;
    private void Awake()
    {
        notice.SetActive(false);
    }
    public void Notice(string s)
    {
        // s로 알림 만들기
        notice.SetActive(true);
        txt.text = s;

        StartCoroutine(MakeNotice());
    }
    IEnumerator MakeNotice()
    {
        yield return new WaitForSeconds(3);
        notice.SetActive(false);
        //Color txt_color = notice.GetComponent<Image>().color;

        //for(int i=0; i<10; i++)
        //{
            
        //    yield return new WaitForSeconds(0.1f);
        //}
    }
}
