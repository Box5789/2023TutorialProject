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
    [SerializeField] private int _color_Id_1;
    public int color_Id_1 { get { return _color_Id_1; } set { if (value < 0) _color_Id_1 = 0; else _color_Id_1 = value; } }

    [SerializeField] private int _color_Id_2;
    public int color_Id_2 { get { return _color_Id_2; } set { if (value < 0) _color_Id_2 = 0; else _color_Id_2 = value; } }

    [SerializeField] private float _transparency = 1;
    public float transparency { get { return _transparency; } set { if (value < 0) _transparency = 0; else _transparency = value; } }

    [SerializeField] private int _bp_Id;
    public int bp_Id { get { return _bp_Id; } set { if (value < 0) _bp_Id = 0; else _bp_Id = value; } }


    public void Clear_FireCracker()
    {
        _color_Id_1 = 0;
        _color_Id_2 = 0;
        _transparency = 0.0f;
        _bp_Id = 0;
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

    [SerializeField] private FireCracker background_FireCracker;
    public FireCracker crafted_FireCracker;

    [SerializeField] private Request present_Request;

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
        present_Request.Clear_Request();
        crafted_FireCracker.Clear_FireCracker();
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
        present_Request.color_Id = 1;
        present_Request.tag_Id = 3;
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


    #region Submit and Check
    public bool Check_At_Submit()
    {
        bool isMet = true;

        //조건 : 조합된 색이든 포함이 되어있든 둘 중 하나만 만족하면 됨
        if (present_Request.color_Id == FindColor_Id(crafted_FireCracker.color_Id_1, crafted_FireCracker.color_Id_2) ||
            present_Request.color_Id == crafted_FireCracker.color_Id_1 ||
            present_Request.color_Id == crafted_FireCracker.color_Id_2)
        {
            //색 맞추기 성공~
        }
        else
        {
            isMet = false;
        }

        bool isThere = false;

        isThere = Find_Tag_Id(present_Request.tag_Id, crafted_FireCracker.bp_Id);

        if (isThere)
        {
            //태그 맞추기 성공~
        }
        else
        {
            isMet = false;
        }

        Copy_FireCracker(crafted_FireCracker, background_FireCracker);


        init();

        Open_Gapandae();

        DisappearClient();
        StartCoroutine(Apply_FireCracker());


        return isMet;
    }


    public void Copy_FireCracker(FireCracker f_source, FireCracker f_target)
    {
        f_target.color_Id_1 = f_source.color_Id_1;
        f_target.color_Id_2 = f_source.color_Id_2;
        f_target.transparency = f_source.transparency;
        f_target.bp_Id = f_source.bp_Id;
    }
    #endregion


    private IEnumerator Apply_FireCracker()
    {
        yield return WFS_5sec;

        //visualEffect.SetFloat("");
        //apply gktpyd~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        

        Gradient g;
        GradientColorKey[] gck;
        GradientAlphaKey[] gak;
        g = new Gradient();
        gck = new GradientColorKey[4];
        gck[0].color = FindColor(background_FireCracker.color_Id_1);
        gck[0].time = 0.0F;
        gck[1].color = FindColor(FindColor_Id(background_FireCracker.color_Id_1, background_FireCracker.color_Id_2));
        gck[1].time = 0.1F;
        gck[2].color = FindColor(FindColor_Id(background_FireCracker.color_Id_1, background_FireCracker.color_Id_2));
        gck[2].time = 0.9F;
        gck[3].color = FindColor(background_FireCracker.color_Id_2);
        gck[3].time = 1.0F;
        gak = new GradientAlphaKey[2];
        gak[0].alpha = background_FireCracker.transparency;
        gak[0].time = 0.0F;
        gak[1].alpha = background_FireCracker.transparency;
        gak[1].time = 1.0F;
        g.SetKeys(gck, gak);

        visualEffect.SetGradient("Gradiant", g);


    }


    #region Tools

    private bool Find_Tag_Id(int tag_Id, int bp_Id)
    {
        for (int i=0;i< 5;i++)
        {            
            if (blueprints_Database[bp_Id].tag_Id[i] == tag_Id)
            {
                return true;
            }
        }
        return false;
    }

    private int FindColor_Id(int i, int j)
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
    private Color FindColor(int i)
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

        Color c = new Color();

        switch (i)
        {
            case 0:
                c = Color.red;
                break;
            case 1:
                //c = Color.blue;
                c = new Color( 0.0f , 12/255f , 1.0f);
                break;
            case 2:
                c = Color.yellow;
                break;
            case 3:
                c = Color.white;
                break;
            case 4:
                ColorUtility.TryParseHtmlString("#7F38EC", out c); //보라
                break;
            case 5:
                ColorUtility.TryParseHtmlString("#FF8040", out c); //주황
                break;
            case 6:
                //ColorUtility.TryParseHtmlString("#FFC1CC", out c); //분홍
                c = new Color(253 / 255f, 85 / 255f, 114 / 255f);
                break;
            case 7:
                //ColorUtility.TryParseHtmlString("#387C44",out c); //초록
                ColorUtility.TryParseHtmlString("#00994c", out c); //초록

                break;
            case 8:
                //ColorUtility.TryParseHtmlString("#CBEAFB", out c); //하늘

                c = new Color(70 / 255f, 190 / 255f, 255 / 255f);
                break;
            case 9:
                //ColorUtility.TryParseHtmlString("#FFF8C6", out c); //레몬

                c = new Color(255 / 255f, 240 / 255f, 114 / 255f);

                break;
            default:
                break;
        }
        return c;

    }

    #endregion
}
