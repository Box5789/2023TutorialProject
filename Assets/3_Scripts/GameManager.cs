using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

[Serializable]
enum GameState
{
    close = 0,
    open_available = 1, //의뢰 가능한 상태   
    open_underRequest = 2 //의뢰 받은 상태
}

[Serializable]
public class Request
{
    [SerializeField] private int _color_Id;
    public int color_Id { get { return _color_Id; } set { if (value < 0) _color_Id = 0; else _color_Id = value; } }

    [SerializeField] private int _tag_Id;
    public int tag_Id { get { return _tag_Id; } set { if (value < 0) _tag_Id = 0; else _tag_Id = value; } }

    public void Clear_Request()
    {
        _color_Id = 0;
        _tag_Id = 0;
    }
}

[Serializable]
public class Blueprint
{
    [SerializeField] private int _print_Id;
    public int print_Id { get { return _print_Id; } set { if (value < 0) _print_Id = 0; else _print_Id = value; } }

    [SerializeField] private bool _ishad;
    public bool ishad { get { return _ishad; } set { _ishad = value; } }

    [SerializeField] private int[] _tag_Ids = new int[5];
    public int[] tag_Id { get { return _tag_Ids; } set { _tag_Ids = value; } }

    public void Clear_Blueprint()
    {
        _print_Id = 0;
        _ishad = false;
        for (int i=0;i<5;i++)
        {
            _tag_Ids[i] = 0;
        }
    }
}

[Serializable]
public class FireCracker
{
    [SerializeField] private int _color_Id1;
    public int color_Id1 { get { return _color_Id1; } set { if (value < 0) _color_Id1 = 0; else _color_Id1 = value; } }

    [SerializeField] private int _color_Id2;
    public int color_Id2 { get { return _color_Id2; } set { if (value < 0) _color_Id2 = 0; else _color_Id2 = value; } }

    [SerializeField] private float _transparency;
    public float transparency { get { return _transparency; } set { if (value < 0) _transparency = 0; else _transparency = value; } }

    [SerializeField] private Blueprint _bp;
    public Blueprint bp { get { return _bp; } set { _bp = value; } }


    public void Clear_FireCracker()
    {
        _color_Id1 = 0;
        _color_Id2 = 0;
        _transparency = 0.0f;
        _bp.Clear_Blueprint();
    }

}

public class GameManager : MonoBehaviour
{
    /*하는 일
     * 1. 설계도 줍줍버튼 활성화
     * 2. 게임 전체 상태 변경
     * 3. 
     */

    [Header("Data")]
    [SerializeField] private GameState _gameState;

    [SerializeField] private FireCracker background_fireCracker;
    [SerializeField] private FireCracker crafted_fireCracker;
    [SerializeField] private Request present_request;

    [SerializeField] private List<Blueprint> blueprints_Database;

    [Header("Client")]
    [SerializeField] private GameObject client_GO;
    [SerializeField] private SpriteRenderer client_SR;
    [SerializeField] private SpriteRenderer talk_Ballon_SR;
    [SerializeField] private TextMeshPro request_Text;


    [Header("Fireworks")]
    [SerializeField] private VisualEffect visualEffect;

    private void Awake()
    {
        init();

        _gameState = GameState.close;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Open_Gapandae();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            DisappearClient();
            if (Check_At_Submit())
            {
                Debug.Log("Clear!! ");
            }
            else
            {
                Debug.Log("fail :( ");
            }
        }
    }


    private void init()
    {
        present_request.Clear_Request();
        crafted_fireCracker.Clear_FireCracker();
    }


    #region open and close // Change State

    public void Close_Gapandae()
    {
        _gameState = GameState.close;
    }
    public void Open_Gapandae()
    {
        _gameState = GameState.open_available;

        StartCoroutine(Open_UnderRequest());
    }

    WaitForSeconds WFS_5sec = new WaitForSeconds(5.0f);
    WaitForSeconds WFS_FDT = new WaitForSeconds(0.02f);
    private IEnumerator Open_UnderRequest()
    {
        yield return WFS_5sec;
        _gameState = GameState.open_underRequest;
        AppearClient();
    }

    private void AppearClient()
    {
        //present_request set
        Set_Present_Request();
        StartCoroutine(Change_Transperency(true));
    }

    private void DisappearClient()
    {
        StartCoroutine(Change_Transperency(false));
    }

    private void Set_Present_Request()  //random
    {
        present_request.color_Id = 1;
        present_request.tag_Id = 3;
    }

    private IEnumerator Change_Transperency(bool isAppearing)
    {
        float time = 4.0f;
        float temp = 0.0f;

        if (isAppearing)
        {
            client_GO.SetActive(true);

            temp = 0.0f;
            
            while (temp < time)
            {
                temp += Time.fixedDeltaTime;
                client_SR.color = new Color(1f, 1f, 1f, temp / time);
                talk_Ballon_SR.color = new Color(1f, 1f, 1f, temp / time);
                request_Text.color = new Color(1f, 1f, 1f, temp / time);

                yield return WFS_FDT;
            }
        }
        else
        {
            temp = 4.0f;

            while (temp > 0.0f)
            {
                temp -= Time.fixedDeltaTime;
                client_SR.color = new Color(1f, 1f, 1f, temp / time);
                talk_Ballon_SR.color = new Color(1f, 1f, 1f, temp / time);
                request_Text.color = new Color(1f, 1f, 1f, temp / time);

                yield return WFS_FDT;
            }

            client_GO.SetActive(false);
        }

        
    }

    #endregion


    #region request

    public bool Check_At_Submit() 
    {
        bool isMet = true;


        if (present_request.color_Id == FindColor(crafted_fireCracker.color_Id1, crafted_fireCracker.color_Id2))
        {
            //색 맞추기 성공~
        }
        else
        {
            isMet = false;
        }

        bool isBreaked = false;
        for (int i = 0; i < 5; i++)
        {
            if (present_request.tag_Id == crafted_fireCracker.bp.tag_Id[i]) //tag
            {
                isBreaked = true;
                break;
            }
        }

        if (isBreaked)
        {
            //태그 맞추기 성공~
        }
        else
        {
            isMet = false;
        }
        background_fireCracker = crafted_fireCracker;
        init();

        Open_Gapandae();
        return isMet;
    }

    private int FindColor(int i, int j)
    {
        /*
            0 : 빨
            1 : 파 
            2 : 노
            3 : 흰
            4 : 보라
            5 : 주황
            6 : 분홍
            7 : 초록
            8 : 하늘
            9 : 연노랑
         */

        int temp = 0;

        if (i == j) //
            temp = i;
        else if (i == 0 && j == 1)
            temp = 4;
        else if (i == 0 && j == 2)
            temp = 5;
        else if (i == 0 && j == 3)
            temp = 6;
        else if (i == 1 && j == 2)
            temp = 7;
        else if (i == 1 && j == 3)
            temp = 8;
        else if (i == 2 && j == 3)
            temp = 9;

        return temp;

    }


    #endregion

}
