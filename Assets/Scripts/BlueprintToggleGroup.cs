using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintToggleGroup : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject gapandae_button;
    public MakingFirecrack mf;
    void Start()
    {
        gapandae_button = GameObject.Find("GapandaeButton");
        mf = gapandae_button.GetComponent<MakingFirecrack>();
        //mf = null;
    }
    //public void SetMF()
    //{
    //    if (mf == null)
    //    {
    //    }
    //}
}
