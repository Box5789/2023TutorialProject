using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

[Serializable]
enum GameState
{
    close_First = 0,
    close = 1,
    open_available = 2, //의뢰 가능한 상태   
    open_underRequest = 3 //의뢰 받은 상태
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
    [SerializeField] private bool _ishad;
    public bool ishad { get { return _ishad; } set { _ishad = value; } }


    [SerializeField] private int _print_Id;
    public int print_Id { get { return _print_Id; } set { if (value < 0) _print_Id = 0; else _print_Id = value; } }

    [SerializeField] private int[] _tag_Ids = new int[5];
    public int[] tag_Id { get { return _tag_Ids; } set { _tag_Ids = value; } }

    [SerializeField] private Texture _t;
    public Texture t { get { return _t; } set { _t = value; } }
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
        _transparency = 1.0f;
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

    [Header("Player Inventory")]
    [SerializeField] private int have_Gold = 0;

    [Header("Data")]
    [SerializeField] private StringController stringController;

    [SerializeField] private GameState gameState;
    [SerializeField] private FireCracker background_FireCracker;
    public FireCracker crafted_FireCracker;
    [SerializeField] private Request present_Request;
    [SerializeField] private List<Blueprint> blueprints_Database;
    

    [Header("Client")]
    [SerializeField] private GameObject client_GO;
    [SerializeField] private SpriteRenderer client_SR;
    [SerializeField] private SpriteRenderer talk_Ballon_SR;
    [SerializeField] private TMP_Text request_Text;

    [Header("Make_Get_BluePrint_Bt")]
    [SerializeField] private GameObject[] buttons_GO;

    [Header("TextUI")]
    [SerializeField] private TextMeshProUGUI tm;

    [SerializeField] private GameObject notice_maker;
    private NoticeMaker nm;

    private void Awake()
    {
        init();

        gameState = GameState.close_First;
        StartCoroutine(Make_Get_Blueprint_Event()); // 한번만 돌아가야함
        nm = notice_maker.GetComponent<NoticeMaker>();
    }

    private void init()
    {
        present_Request.Clear_Request();
        crafted_FireCracker.Clear_FireCracker();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Close_Gapandae();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Open_Gapandae();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Check_At_Submit();
        }
    }
    

    #region open and close // Change State

    public void Close_Gapandae()
    {
        gameState = GameState.close;
        DisappearClient();
    }
    public void Open_Gapandae()
    {
        gameState = GameState.open_available;
        StartCoroutine(Open_UnderRequest());
    }

    WaitForSeconds WFS_5sec = new WaitForSeconds(5.0f);
    WaitForSeconds WFS_FDT = new WaitForSeconds(0.02f);
    private IEnumerator Open_UnderRequest()
    {
        yield return WFS_5sec;
        yield return WFS_5sec;
        gameState = GameState.open_underRequest;
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
        present_Request.color_Id = UnityEngine.Random.Range(0,10);
        present_Request.tag_Id = Get_Rand_Tag_Among_Have();

        //여기서 text 적용
        request_Text.text = stringController.GetOrder(present_Request.color_Id , present_Request.tag_Id);
    }

    #endregion


    #region Submit and Check
    public void Check_At_Submit()
    {
        if (gameState != GameState.open_underRequest)
            return;

        bool isMet = true;

        //조건 : 조합된 색이 맞아야함
        if (present_Request.color_Id == Find_Color_Id(crafted_FireCracker.color_Id_1, crafted_FireCracker.color_Id_2))
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

        if (isMet)
        {
            Debug.Log("만족 :)");
            nm.Notice("Good :)");
            have_Gold++;
            tm.text = "" + have_Gold;
        }
        else
        {
            Debug.Log("불만족 :(");
            nm.Notice("Bad :(");
        }
    }


    public void Copy_FireCracker(FireCracker f_source, FireCracker f_target)
    {
        f_target.color_Id_1 = f_source.color_Id_1;
        f_target.color_Id_2 = f_source.color_Id_2;
        f_target.transparency = f_source.transparency;
        f_target.bp_Id = f_source.bp_Id;
    }
    #endregion


    #region blueprint 획득

    private bool isExisted = false; 
    private int eventUI_Id = -1;

    public void Get_Blueprint() //UI에서 획득 버튼들이 공통으로 참조하는(add)
    {
        isExisted = false;
        if (eventUI_Id != -1)
        {
            StartCoroutine(Button_OnOff(isExisted, eventUI_Id));
            eventUI_Id = -1; // 없음.
            Get_Rand_Print_Not_Have();
        }
    }
    public void BuyBlueprint(int id)
    {
        int price = 1;
        if (have_Gold >= price)
        {
            if (blueprints_Database[id].ishad)
            {
                nm.Notice("You already have this blueprint :)");
                Debug.Log("이미 갖고 있는 상품입니다");
            }
            else
            {
                blueprints_Database[id].ishad = true;
                have_Gold -= price; 
                nm.Notice("Now you can use blueprint " + id.ToString() + "!");
                Debug.Log(id.ToString() + "번 설계도 구매!");
                tm.text = "" + have_Gold;
            }
        }
        else
        {
            nm.Notice("You don\'t have enough gold :(");
            Debug.Log("돈 부족!!");
        }
    }

    private void Get_Rand_Print_Not_Have()
    {
        List<int> temp = new List<int>();
        int index;

        for (int i = 0; i < blueprints_Database.Count; i++)
        {
            temp.Add(i);
        }

        do
        {
            index = UnityEngine.Random.Range(0, temp.Count);
            if (!blueprints_Database[temp[index]].ishad)
            {
                break;
            }
            else
            {
                temp.RemoveAt(index);
            }
        } while (temp.Count != 0);

        //Debug.Log(temp[index]);
        //nm.Notice("You\'ve got Schematic "+temp[index].ToString()+"!");
        nm.BlueprintNotice(temp[index]);
        blueprints_Database[temp[index]].ishad = true;
    }

    WaitForSeconds WFS_20sec = new WaitForSeconds(20.0f);
    private IEnumerator Make_Get_Blueprint_Event()
    {
        yield return WFS_20sec;
        while (true)
        {
            if (Does_Have_All_BP())
            {
                Debug.Log("모든 도면을 가지고 있습니다.");
                nm.Notice("You had all blueprints.");
                yield break;
            }
            if (!isExisted)
            {
                isExisted = true;
                eventUI_Id = UnityEngine.Random.Range(0, 5);
                StartCoroutine(Button_OnOff(isExisted, eventUI_Id));
            }
            yield return WFS_20sec;
        }
    }

    private IEnumerator Button_OnOff(bool on, int index)
    {
        float time = 2.0f;
        float temp = 0.0f;

        Image image = buttons_GO[index].GetComponent<Image>();

        if (on)
        {
            buttons_GO[index].SetActive(on);
            while (temp < time)
            {
                temp += Time.fixedDeltaTime;
                image.color = new Color(1f, 1f, 1f, temp / time);
                yield return WFS_FDT;
            }
        }
        else
        {
            yield return WFS_FDT;
            temp = 2.0f;
            while (temp > 0.0f)
            {
                temp -= Time.fixedDeltaTime;
                image.color = new Color(1f, 1f, 1f, temp / time);
                yield return WFS_FDT;
            }
            buttons_GO[index].SetActive(on);
        }
    }

    #endregion


    #region Tools
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
                request_Text.color = new Color(0f, 0f, 0f, temp / time);

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
                request_Text.color = new Color(0f, 0f, 0f, temp / time);

                yield return WFS_FDT;
            }

            client_GO.SetActive(false);
        }


    }


    [SerializeField] private Fireworks_Ctrl fireworks_Ctrl;
    private IEnumerator Apply_FireCracker()
    {
        yield return WFS_5sec;

        //visualEffect.SetFloat("");
        //apply gktpyd~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        Gradient g1;
        Gradient g2;
        GradientColorKey[] gck;
        GradientAlphaKey[] gak;
        g1 = new Gradient();
        g2 = new Gradient();
        gck = new GradientColorKey[4];

        if (background_FireCracker.bp_Id == 0)
        {
            fireworks_Ctrl.Switching_Fireworks(true);

            gck[0].color = Find_Color(background_FireCracker.color_Id_1);
            gck[0].time = 0.0F;
            gck[1].color = Find_Color(Find_Color_Id(background_FireCracker.color_Id_1, background_FireCracker.color_Id_2));
            gck[1].time = 0.1F;
            gck[2].color = Find_Color(Find_Color_Id(background_FireCracker.color_Id_1, background_FireCracker.color_Id_2));
            gck[2].time = 0.9F;
            gck[3].color = Find_Color(background_FireCracker.color_Id_2);
            gck[3].time = 1.0F;
            gak = new GradientAlphaKey[2];
            gak[0].alpha = background_FireCracker.transparency;
            gak[0].time = 0.0F;
            gak[1].alpha = background_FireCracker.transparency;
            gak[1].time = 1.0F;


            g1.SetKeys(gck, gak);

            fireworks_Ctrl.Applying(200, g1, 0);
        }
        else if (background_FireCracker.bp_Id == 1)
        {
            fireworks_Ctrl.Switching_Fireworks(true);

            gck[0].color = Find_Color(Find_Color_Id(background_FireCracker.color_Id_1, background_FireCracker.color_Id_2));
            gck[0].time = 0.0F;
            gck[1].color = Find_Color(background_FireCracker.color_Id_1);
            gck[1].time = 0.1F;
            gck[2].color = Find_Color(background_FireCracker.color_Id_2);
            gck[2].time = 0.9F;
            gck[3].color = Find_Color(Find_Color_Id(background_FireCracker.color_Id_1, background_FireCracker.color_Id_2));
            gck[3].time = 1.0F;
            gak = new GradientAlphaKey[2];
            gak[0].alpha = background_FireCracker.transparency;
            gak[0].time = 0.0F;
            gak[1].alpha = background_FireCracker.transparency;
            gak[1].time = 1.0F;

            g1.SetKeys(gck, gak);


            gck[0].color = Find_Color(background_FireCracker.color_Id_1);
            gck[0].time = 0.0F;
            gck[1].color = Find_Color(Find_Color_Id(background_FireCracker.color_Id_1, background_FireCracker.color_Id_2));
            gck[1].time = 0.1F;
            gck[2].color = Find_Color(Find_Color_Id(background_FireCracker.color_Id_1, background_FireCracker.color_Id_2));
            gck[2].time = 0.9F;
            gck[3].color = Find_Color(background_FireCracker.color_Id_2);
            gck[3].time = 1.0F;
            gak = new GradientAlphaKey[2];
            gak[0].alpha = background_FireCracker.transparency;
            gak[0].time = 0.0F;
            gak[1].alpha = background_FireCracker.transparency;
            gak[1].time = 1.0F;

            g2.SetKeys(gck, gak);


            fireworks_Ctrl.Applying(150, g1, 3, g2);
        }
        else
        {
            fireworks_Ctrl.Switching_Fireworks(false);

            gck[0].color = Find_Color(background_FireCracker.color_Id_1);
            gck[0].time = 0.0F;
            gck[1].color = Find_Color(Find_Color_Id(background_FireCracker.color_Id_1, background_FireCracker.color_Id_2));
            gck[1].time = 0.1F;
            gck[2].color = Find_Color(Find_Color_Id(background_FireCracker.color_Id_1, background_FireCracker.color_Id_2));
            gck[2].time = 0.9F;
            gck[3].color = Find_Color(background_FireCracker.color_Id_2);
            gck[3].time = 1.0F;
            gak = new GradientAlphaKey[2];
            gak[0].alpha = background_FireCracker.transparency;
            gak[0].time = 0.0F;
            gak[1].alpha = background_FireCracker.transparency;
            gak[1].time = 1.0F;

            g1.SetKeys(gck, gak);

            fireworks_Ctrl.Applying(blueprints_Database[background_FireCracker.bp_Id].t, 400, 40.0f, g1, 0);
        }
    }


    private int Get_Rand_Tag_Among_Have()
    {
        List<Blueprint> temp = new List<Blueprint>();
        int r1, r2;

        for (int i = 0; i < blueprints_Database.Count; i++)
        {
            if (blueprints_Database[i].ishad)
                temp.Add(blueprints_Database[i]);
        }
        r1 = UnityEngine.Random.Range(0, temp.Count);
        r2 = UnityEngine.Random.Range(0, 5);

        return temp[r1].tag_Id[r2];
    }

    private bool Does_Have_All_BP()
    {
        for (int i = 0; i < blueprints_Database.Count; i++)
        {
            if (blueprints_Database[i].ishad == false)
            {
                return false;
            }
        }
        return true;
    }

    public bool[] Have_Bp()
    {
        bool[] b = new bool[blueprints_Database.Count];
        for (int i = 0; i < blueprints_Database.Count; i++)
        {
            b[i] = blueprints_Database[i].ishad;
        }
        return b;
    }

    private bool Find_Tag_Id(int tag_Id, int bp_Id)
    {
        for (int i = 0; i < 5; i++)
        {
            if (blueprints_Database[bp_Id].tag_Id[i] == tag_Id)
            {
                return true;
            }
        }
        return false;
    }
    private int Find_Color_Id(int i, int j)
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
        else if ((i == 0 && j == 1) || (i == 1 && j == 0))
            temp = 4;
        else if ((i == 0 && j == 2) || (i == 2 && j == 0))
            temp = 5;
        else if ((i == 0 && j == 3) || (i == 3 && j == 0))
            temp = 6;
        else if ((i == 1 && j == 2) || (i == 2 && j == 1))
            temp = 7;
        else if ((i == 1 && j == 3) || (i == 3 && j == 1))
            temp = 8;
        else if ((i == 2 && j == 3) || (i == 3 && j == 2))
            temp = 9;

        return temp;

    }
    private Color Find_Color(int i)
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
                c = new Color(0.0f, 12 / 255f, 1.0f);
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
