using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blueprint
{
    private int id;
    private string name;
    private bool is_activated;
    private Sprite sprite;

    public int GetId() { return id; }
    public string GetName() { return name; }
    public bool GetIsActivated() { return is_activated; }
    public Sprite GetSprite() { return sprite; }

}
public class BluePrintUIManager : MonoBehaviour
{
    private bool[] player_had_list;
    //private Blueprint[] all_blueprints;
    private int[] all_blueprints; // Set int type for test
    private int blueprints_view_length;
    private int pointer;
    private Vector3[] pos_list;
    public GameObject locked_prefab;
    void Start()
    {
        blueprints_view_length = 4;
        all_blueprints = new int[10] {1,2,3,4,5,6,7,8,9,10 };
        player_had_list = new bool[10] { true, false, false, true, true, false, true, true, false, true };
        pointer = 0;
        pos_list = new Vector3[all_blueprints.Length];
        Vector3 first_pos = transform.GetChild(0).position;
        for(int i=0; i<blueprints_view_length; i++)
        {
            pos_list[i] = first_pos + new Vector3(73*i, 0, 0);
            transform.GetChild(i).gameObject.SetActive(true);
        }
        for(int i=blueprints_view_length; i<all_blueprints.Length; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        LoadUI();
    }
    public void IncreasePointer()
    {
        if (pointer < all_blueprints.Length - blueprints_view_length)
        {
            pointer++;
            LoadUI();
        }
    }
    public void DecreasePointer()
    {
        if (pointer > 0)
        {
            pointer--;
            LoadUI();
        }
    }
    public void LoadUI()
    {
        GameObject go;
        int pos_num = 0;
        for(int i=0; i<all_blueprints.Length; i++)
        {
            go = transform.GetChild(i).gameObject;
            if (pointer <= i && i < pointer + blueprints_view_length)
            {
                go.transform.position = pos_list[pos_num++];
                go.SetActive(true);
                if (player_had_list[i]==false)
                {
                    Locked(go);
                }
            }
            else
            {
                go.SetActive(false);
            }
        }
    }

    public void Locked(GameObject go)
    {
        GameObject locked = Instantiate(locked_prefab,go.transform);
        locked.transform.position = go.transform.position;
    }

    public void Clicked(int num)
    {
        for(int i=0; i<all_blueprints.Length; i++)
        {
            if (i == num)
            {
                Color color = new Color();
                color.r = 255;
                color.g = 255;
                color.b = 255;
                color.a = 255;
                transform.GetChild(i).gameObject.GetComponent<Image>().color = color;
                Debug.Log(transform.GetChild(i).gameObject.GetComponent<Image>().color);
            }
            else
            {
                Color color = new Color();
                color.r = 155;
                color.g = 155;
                color.b = 155;
                color.a = 255;
                transform.GetChild(i).gameObject.GetComponent<Image>().color = color;
                Debug.Log(transform.GetChild(i).gameObject.GetComponent<Image>().color);
            }
        }
    }
}
