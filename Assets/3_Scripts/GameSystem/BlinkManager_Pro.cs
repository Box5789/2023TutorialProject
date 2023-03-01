using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlinkManager_Pro : MonoBehaviour
{

    SpriteRenderer sr;
    [SerializeField] private float transparency_Percent;
    
    [SerializeField] private bool isRand;
    [SerializeField] private float min;
    [SerializeField] private float max;
    [SerializeField] private float time;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }


    IEnumerator coroutine;
    private void OnEnable()
    {
        coroutine = Change_Transperency();
        StartCoroutine(coroutine);
    }

    private WaitForSeconds WFS_FDT = new WaitForSeconds(0.02f);
    private WaitForSeconds WFS_2sec = new WaitForSeconds(2.0f);

    private IEnumerator Change_Transperency()
    {
        yield return WFS_2sec;
        yield return WFS_FDT;
        yield return WFS_FDT;

        float time = 1.0f;
        float temp;
        while (true) 
        {
            temp = 1.0f;
            while (temp > 0.0f)
            {
                temp -= Time.fixedDeltaTime;
                sr.color = new Color(1f, 1f, 1f, temp / time * transparency_Percent + 1- transparency_Percent);
                yield return WFS_FDT;
            }
            temp = 0.0f;
            while (temp < time)
            {
                temp += Time.fixedDeltaTime;
                sr.color = new Color(1f, 1f, 1f, temp / time * transparency_Percent + 1- transparency_Percent);
                yield return WFS_FDT;
            }
        }

    }

    public void Stop_ThisCoroutine()
    {   
        if(coroutine != null)
        {
            sr.color = new Color(1f, 1f, 1f, 1f);
            StopCoroutine(coroutine);
            coroutine = null;
        }

    }
}
