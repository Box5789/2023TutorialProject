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
    open_available = 1, //의뢰 가능한 상태   
    open_underRequest = 2 //의뢰 받은 상태
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
 * 안녕하세요. ○○ 모양으로 만들어주세요. 참고로 제 행운의 컬러는 □□이에요. 
 * ○○ 모양으로 부탁드려요. 색은 □□이 좋겠네요. 
 * 날이 쌀쌀한데 고생 많으시네요! ○○ 모양의 □□ 폭죽 부탁드려요!
 * 아직 장사하시는 거죠? □□ ○○ 모양 폭죽 주세요. 
 * □□ ○○ 모양 폭죽을 만들어주세요. 잘 부탁드립니다. 
 * 저는 □□을 좋아해요. ○○ 모양도요. 아시겠죠?
 * □□의 ○○ 모양 폭죽 하나 주시겠어요? 그나저나 이곳은 정말 아름답네요... 
 * □□ ○○ 모양이요. 
 * 음... 종류가 되게 많네요. ○○ 모양으로 주세요. 색깔이요? 잠시만요. 어.. 뭐가 좋을까.. □□으로 해주세요. 
 * ○○ 모양 □□ 폭죽 하나요. 계좌이체 되죠?
 * 어이, □□ ○○모양 하나.
 * 
 * 
 */

public class GameManager : MonoBehaviour
{
    /*하는 일
     * 1. 설계도 줍줍버튼 활성화
     * 2. 게임 전체 상태 변경
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
                //색 맞추기 성공~
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
                //색 맞추기 성공~
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
            //태그 맞추기 성공~
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
