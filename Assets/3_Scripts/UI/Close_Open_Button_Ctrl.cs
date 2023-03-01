using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Close_Open_Button_Ctrl : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool isClosed = true;
    [SerializeField] bool isFirst = true;

    [Header("Assign")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] GameObject black;
    [SerializeField] Sprite close;
    [SerializeField] Sprite open;
    Image image;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }


    public void ChangeState()
    {
        if (isFirst)
        {
            black.SetActive(false);
            isFirst = false;
        }
        if (isClosed)
        {
            gameManager.Open_Gapandae();
            image.sprite = open;
        }
        else
        {
            gameManager.Close_Gapandae();
            image.sprite = close;
        }
        isClosed = !isClosed;
    }
}
