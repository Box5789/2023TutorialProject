using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkManager : MonoBehaviour
{
    float time;
    Image image;
    Color color;
    bool flag;
    private void Start()
    {
        time = 0;
        image = gameObject.GetComponent<Image>();
        color = image.color;
        flag = true;
    }
    IEnumerator TransparenceAnimation()
    {
        for(int i=1; i<=4; i++)
        {
            color.a = 1 - 0.1f * i;
            gameObject.GetComponent<Image>().color = color;
            yield return new WaitForSeconds(0.25f);
        }
        for(int i=3; i>=0; i--)
        {
            color.a = 1 - 0.1f * i;
            gameObject.GetComponent<Image>().color = color;
            yield return new WaitForSeconds(0.25f);
        }
        flag = true;
    }
    void Update()
    {
        if (flag)
        {
            flag = false;
            StartCoroutine(TransparenceAnimation());
        }
    }
}
