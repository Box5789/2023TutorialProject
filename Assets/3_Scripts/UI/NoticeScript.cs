using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoticeScript : MonoBehaviour
{
    [SerializeField] GameObject notice_text;
    public void ShowNotice(string s)
    {
        notice_text.GetComponent<TextMeshProUGUI>().text = s;
        StartCoroutine(CloseNotice());
    }
    IEnumerator CloseNotice()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
