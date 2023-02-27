using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
enum GameState
{
    close = 0,
    open_available = 1, //�Ƿ� ������ ����   
    open_underRequest = 2 //�Ƿ� ���� ����
}

[Serializable]
public class Request
{
    private int _color_Id;
    public int color_Id { get { return _color_Id; } set { if (value < 0) _color_Id = 0; else _color_Id = value; } }

    private int _tag_Id;
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

    [SerializeField] private bool _isGradation;
    public bool isGradation { get { return _isGradation; } set { _isGradation = value; } }

    [SerializeField] private int _particle_Id;
    public int particle_Id { get { return _particle_Id; } set { if (value < 0) _particle_Id = 0; else _particle_Id = value; } }

    [SerializeField] private Blueprint _bp;
    public Blueprint bp { get { return _bp; } set { _bp = value; } }


    public void Clear_FireCracker()
    {
        _color_Id1 = 0;
        _color_Id2 = 0;
        _transparency = 0.0f;
        _isGradation = false;
        _particle_Id = 0;
        _bp.Clear_Blueprint();
    }

}

/*
 * �ȳ��ϼ���. �ۡ� ������� ������ּ���. ����� �� ����� �÷��� ����̿���. 
 * �ۡ� ������� ��Ź�����. ���� ����� ���ڳ׿�. 
 * ���� �ҽ��ѵ� ��� �����ó׿�! �ۡ� ����� ��� ���� ��Ź�����!
 * ���� ����Ͻô� ����? ��� �ۡ� ��� ���� �ּ���. 
 * ��� �ۡ� ��� ������ ������ּ���. �� ��Ź�帳�ϴ�. 
 * ���� ����� �����ؿ�. �ۡ� ��絵��. �ƽð���?
 * ����� �ۡ� ��� ���� �ϳ� �ֽðھ��? �׳����� �̰��� ���� �Ƹ���׿�... 
 * ��� �ۡ� ����̿�. 
 * ��... ������ �ǰ� ���׿�. �ۡ� ������� �ּ���. �����̿�? ��ø���. ��.. ���� ������.. ������� ���ּ���. 
 * �ۡ� ��� ��� ���� �ϳ���. ������ü ����?
 * ����, ��� �ۡ۸�� �ϳ�.
 * 
 * 
 */

public class GameManager : MonoBehaviour
{
    /*�ϴ� ��
     * 1. ���赵 ���ݹ�ư Ȱ��ȭ
     * 2. ���� ��ü ���� ����
     * 3. 
     * 
     * 
     */
    [SerializeField] private GameState gameState;

    [SerializeField] private FireCracker background_fireCracker;
    [SerializeField] private FireCracker crafted_fireCracker;
    [SerializeField] private Request present_request;

    [SerializeField] private List<Blueprint> blueprints_Database;



    private void Awake()
    {
        init();

        gameState = GameState.close;

    }

    private void init()
    {
        present_request.Clear_Request();
        crafted_fireCracker.Clear_FireCracker();
    }


    private void FixedUpdate()
    {


    }




    #region open and close // Change State

    public void Close_Gapandae()
    {
        gameState = GameState.close;
    }
    public void Open_Gapandae()
    {
        gameState = GameState.open_available;

    }

    private void Open_UnderRequest()
    {
        gameState = GameState.open_underRequest;
    }

    #endregion



    #region request

    public bool Check_At_Submit(Request request) 
    {
        bool isMet = true;

        if (crafted_fireCracker.isGradation)
        {
            if(crafted_fireCracker.color_Id1 == request.color_Id ||
                crafted_fireCracker.color_Id2 == request.color_Id)
            {
                //�� ���߱� ����~
            }
            else
            {
                isMet = false;
            }
        }
        else
        {
            if(request.color_Id == FindColor(crafted_fireCracker.color_Id1, crafted_fireCracker.color_Id2))
            {
                //�� ���߱� ����~
            }
            else
            {
                isMet = false;
            }
        }

        bool isBreaked = false;
        for (int i = 0; i < 5; i++)
        {
            if (request.tag_Id == crafted_fireCracker.bp.tag_Id[i]) //tag
            {
                isBreaked = true;
                break;
            }
        }

        if (isBreaked)
        {
            //�±� ���߱� ����~
        }
        else
        {
            isMet = false;
        }


        background_fireCracker = crafted_fireCracker;
        init();

        return isMet;
    }

    private int FindColor(int i, int j)
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


    #endregion

}
