using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    [SerializeField] GameObject game_manager;
    int id;
    public void SetId(int _id)
    {
        id = _id;
        Debug.Log(id.ToString() + "번으로 구매 목록 설정");
    }
    public void BuyBlueprint()
    {
        game_manager.GetComponent<GameManager>().BuyBlueprint(id);
    }
}
