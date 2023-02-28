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
    close_First = 0,
    close = 1,
    open_available = 2, //�Ƿ� ������ ����   
    open_underRequest = 3 //�Ƿ� ���� ����
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
    /*�ϴ� ��
     * 1. ���赵 ���ݹ�ư Ȱ��ȭ
     * 2. ���� ��ü ���� ����
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
    [SerializeField] private TextMeshPro request_Text;


    [Header("Fireworks")]
    [SerializeField] private VisualEffect visualEffect;


    [Header("Make_Get_BluePrint_Bt")]
    [SerializeField] private GameObject[] buttons_GO;

    private void Awake()
    {
        init();

        gameState = GameState.close_First;
        StartCoroutine(Make_Get_Blueprint_Event()); // �ѹ��� ���ư�����
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

        //���⼭ text ����
        request_Text.text = stringController.GetOrder(present_Request.color_Id , present_Request.tag_Id);
    }

    #endregion


    #region Submit and Check
    public void Check_At_Submit()
    {
        bool isMet = true;

        //���� : ���յ� ���̵� ������ �Ǿ��ֵ� �� �� �ϳ��� �����ϸ� ��
        if (present_Request.color_Id == Find_Color_Id(crafted_FireCracker.color_Id_1, crafted_FireCracker.color_Id_2) ||
            present_Request.color_Id == crafted_FireCracker.color_Id_1 ||
            present_Request.color_Id == crafted_FireCracker.color_Id_2)
        {
            //�� ���߱� ����~
        }
        else
        {
            isMet = false;
        }

        bool isThere = false;

        isThere = Find_Tag_Id(present_Request.tag_Id, crafted_FireCracker.bp_Id);

        if (isThere)
        {
            //�±� ���߱� ����~
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
            Debug.Log("���� :(");
            have_Gold++;
        }
        else
        {
            Debug.Log("�Ҹ��� :(");
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


    #region blueprint ȹ��

    private bool isExisted = false; 
    private int eventUI_Id = -1;

    public void Get_Blueprint() //UI���� ȹ�� ��ư���� �������� �����ϴ�(add)
    {
        isExisted = false;
        eventUI_Id = -1; // ����.
        Button_OnOff(isExisted);
        Get_Rand_Print_Not_Have();
    }

    private void Get_Rand_Print_Not_Have()
    {
        List<int> temp = new List<int>();
        int r;

        for (int i = 0; i < 10; i++)
        {
            temp.Add(i);
        }

        do
        {
            r = UnityEngine.Random.Range(0, temp.Count);
            temp.Remove(r);
        } while (blueprints_Database[temp[r]].ishad);

        blueprints_Database[temp[r]].ishad = true;
    }

    WaitForSeconds WFS_20sec = new WaitForSeconds(20.0f);
    private IEnumerator Make_Get_Blueprint_Event()
    {
        if (Does_Have_All_BP())
        {
            Debug.Log("��� ������ ������ �ֽ��ϴ�.");
            yield break;
        }

        yield return WFS_20sec;
        while (true)
        {
            if (!isExisted)
            {
                isExisted = true;
                eventUI_Id = UnityEngine.Random.Range(0, 5);
                Button_OnOff(isExisted);
            }
            yield return WFS_20sec;
        }
    }

    private void Button_OnOff(bool on)
    {
        buttons_GO[eventUI_Id].SetActive(on);
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
        g.SetKeys(gck, gak);

        visualEffect.SetGradient("Gradiant", g);


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
        
        return blueprints_Database[r1].tag_Id[r2];
    }

    private bool Does_Have_All_BP()
    {
        for (int i = 0; i < 10; i++)
        {
            if (blueprints_Database[i].ishad == false)
            {
                return false;
            }
        }
        return true;
    }


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
    private int Find_Color_Id(int i, int j)
    {
        /*
            0 : ��
            1 : �� 
            2 : ��
            3 : ��
            4 : ����
            5 : ��Ȳ
            6 : ��ȫ
            7 : �ʷ�
            8 : �ϴ�
            9 : �����
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
    private Color Find_Color(int i)
    {
        /*
            0 : ��
            1 : �� 
            2 : ��
            3 : ��
            4 : ����
            5 : ��Ȳ
            6 : ��ȫ
            7 : �ʷ�
            8 : �ϴ�
            9 : �����
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
                ColorUtility.TryParseHtmlString("#7F38EC", out c); //����
                break;
            case 5:
                ColorUtility.TryParseHtmlString("#FF8040", out c); //��Ȳ
                break;
            case 6:
                //ColorUtility.TryParseHtmlString("#FFC1CC", out c); //��ȫ
                c = new Color(253 / 255f, 85 / 255f, 114 / 255f);
                break;
            case 7:
                //ColorUtility.TryParseHtmlString("#387C44",out c); //�ʷ�
                ColorUtility.TryParseHtmlString("#00994c", out c); //�ʷ�

                break;
            case 8:
                //ColorUtility.TryParseHtmlString("#CBEAFB", out c); //�ϴ�

                c = new Color(70 / 255f, 190 / 255f, 255 / 255f);
                break;
            case 9:
                //ColorUtility.TryParseHtmlString("#FFF8C6", out c); //����

                c = new Color(255 / 255f, 240 / 255f, 114 / 255f);

                break;
            default:
                break;
        }
        return c;

    }

    #endregion
}
