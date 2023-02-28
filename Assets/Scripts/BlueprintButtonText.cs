using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlueprintButtonText : MonoBehaviour
{
    public TextMeshProUGUI tmpu;
    private GameObject parent;
    private Toggle toggle;
    private GameObject gapandae_button;
    public MakingFirecrack mf;
    private int value;
    // Start is called before the first frame update
    private void Start()
    {
        gapandae_button = GameObject.Find("GapandaeButton");
        mf = gapandae_button.GetComponent<MakingFirecrack>();
        toggle = GetComponent<Toggle>();
    }
    public void OnChange(Toggle toggle)
    {
        if (parent == null)
        {
            parent = transform.parent.gameObject;
        }
        if (true)
        {
            parent.GetComponent<BlueprintToggleGroup>().mf.SetBP(value);
        }
    }
}
