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
        //toggle.onValueChanged.AddListener((ison) => {
        //    if (ison)
        //    {
                 
        //    }
        //});
    }
    public void SetName(int _value, string _name)
    {
        parent = transform.parent.gameObject;
        gameObject.GetComponent<Toggle>().group = parent.GetComponent<ToggleGroup>();
        tmpu.text = _name;
        value = _value;
        parent = null;
    }
    public void OnChange(bool _is_on)
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
