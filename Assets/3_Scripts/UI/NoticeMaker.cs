using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoticeMaker : MonoBehaviour
{
    [SerializeField] GameObject notice_prefab;
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] GameObject blueprint_notices;
    private int num;
    private void Awake()
    {
        num = 0;
    }
    public void Notice(string s)
    {
        // s로 알림 만들기
        GameObject n = Instantiate(notice_prefab,gameObject.transform);
        n.GetComponent<NoticeScript>().ShowNotice(s);
    }
    
    public void BlueprintNotice(int _num)
    {
        num = _num;
        blueprint_notices.SetActive(true);
        blueprint_notices.transform.GetChild(num).gameObject.SetActive(true);
        StartCoroutine(CloseBlueprintNotice());
    }
    
    IEnumerator CloseBlueprintNotice()
    {
        yield return new WaitForSeconds(3);
        blueprint_notices.transform.GetChild(num).gameObject.SetActive(false);
    }

    

}
