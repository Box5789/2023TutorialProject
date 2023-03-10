using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePrintUIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private bool[] player_had_list;
    //private Blueprint[] all_blueprints;
    private int blueprints_Num; // Set int type for test
    private int blueprints_view_length;
    //private int pointer;
    private Vector3[] pos_list;

    public GameObject[] blueprint_bt_go_s = new GameObject[8];
    void Start()
    {
        blueprints_view_length = 4;
        blueprints_Num = 14;

        player_had_list = new bool[14];
        //pointer = 0;
        pos_list = new Vector3[blueprints_Num];
        //Vector3 first_pos = transform.GetChild(0).position;
        //for (int i = 0; i < blueprints_view_length; i++)
        //{
        //    pos_list[i] = first_pos + new Vector3(73 * i, 0, 0);
        //    //transform.GetChild(i).gameObject.SetActive(true);
        //}

        LoadUI();
    }

    public void LoadUI()
    {
        for (int i = 0; i < blueprints_Num; i++)
        {
            player_had_list = gameManager.Have_Bp();
        }
        for (int i = 0; i < blueprints_Num; i++)
        {
            //Locked(blueprint_bt_go_s[i]);
            blueprint_bt_go_s[i].transform.GetChild(1).gameObject.SetActive(!player_had_list[i]);
                //transform.GetChild(i).GetChild(1).gameObject.SetActive(player_had_list[i]);
        }
    }
    //public void Locked(GameObject go)
    //{
    //    GameObject locked = Instantiate(locked_prefab, go.transform);
    //    locked.transform.position = go.transform.position;
    //}
    //public void UnLocked(GameObject go)
    //{
    //    GameObject locked = Instantiate(locked_prefab, go.transform);
    //    locked.transform.position = go.transform.position;
    //}
}

