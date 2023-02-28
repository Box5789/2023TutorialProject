using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintButton : MonoBehaviour
{
    private int id;
    private bool is_activated;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }
    public void SetButton(Blueprint blueprint)
    {
        id = blueprint.GetId();
        image.sprite = blueprint.GetSprite();
        is_activated = blueprint.GetIsActivated();
    }
}
