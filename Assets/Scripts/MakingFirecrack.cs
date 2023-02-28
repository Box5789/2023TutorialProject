using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakingFirecrack : MonoBehaviour
{
    // Start is called before the first frame update
    //int color_Id, float transparency, bool isGradation, int particle_Id, blueprint bp

    [SerializeField]
    GameObject gapandae_popup;
    [SerializeField]
    GameObject blueprint_ui_prefab;
    [SerializeField]
    GameObject blueprint_button_position;

    private int color_id_1;
    private int color_id_2;
    private float transparency;
    private bool is_gradation;
    private int particle_id;
    private int bp; // Blueprint 클래스 구현이 안 돼있는 상황이라, 임시로 int형 사용. 

    private int[] inventory;
    private string[] inventory_string;
    private void Start()
    {
        gapandae_popup.SetActive(false);
    }

    public void OpenPopup()
    {
        gapandae_popup.SetActive(true);
        // Get item information from user inventory
        inventory = new int[5] { 1, 3, 4, 5, 7 };
        inventory_string = new string[5] { "a", "bc", "def", "ghij", "klmnop" };
        //MakeBlueprintUI();
    }
    //private void MakeBlueprintUI()
    //{
    //    Vector3 pos = new Vector3(0, 0, 0);
    //    int d = 20;
    //    for(int i=0; i<inventory_string.Length; i++)
    //    {
    //        GameObject blueprint_button = Instantiate(blueprint_ui_prefab, blueprint_scroll_object.transform);
    //        blueprint_button.GetComponent<BlueprintButtonText>().SetName(i,inventory_string[i]);
    //        if (i == 0) pos = blueprint_button.transform.position;
    //        else blueprint_button.transform.position = pos + new Vector3(0, -1 * d * i, 0);
    //    }
    //}

    public void SetColorId1(int _color_id_1) { color_id_1 = _color_id_1; }
    public void SetColorId2(int _color_id_2) { color_id_2 = _color_id_2; }
    public void SetTransparency(Slider s) { 
        transparency = s.value;
        Debug.Log(transparency);
    }
    public void SetIsGradation(bool _is_gradation) { is_gradation = _is_gradation; }
    public void SetIsParticleId(int _particle_id) { particle_id = _particle_id; }
    public void SetBP(int _bp) { bp = _bp; }
    public void Submit()
    {
        Debug.Log(color_id_1);
        Debug.Log(color_id_2);
        Debug.Log(transparency);
        Debug.Log(bp);
    }
}
