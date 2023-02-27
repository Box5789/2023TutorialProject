using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlueprintButtonText : MonoBehaviour
{
    public TextMeshProUGUI tmpu;
    [SerializeField]
    private GameObject _parent;
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    public void SetName(string _name)
    {
        tmpu.text = _name;
    }
}
