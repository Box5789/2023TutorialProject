using System;
using System.Collections;
using System.Collections.Generic;
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
public class request
{
    private int _color_Id1;
    public int color_Id1 { get { return _color_Id1; } set { if (value < 0) _color_Id1 = 0; else _color_Id1 = value; } }

    private int _color_Id2;
    public int color_Id2 { get { return _color_Id2; } set { if (value < 0) _color_Id2 = 0; else _color_Id2 = value; } }

    private int _tag_Id;
    public int tag_Id { get { return _tag_Id; } set { if (value < 0) _tag_Id = 0; else _tag_Id = value; } }
}


[Serializable]
public class Blueprint
{
    private int _print_Id;
    public int print_Id { get { return _print_Id; } set { if (value < 0) _print_Id = 0; else _print_Id = value; } }

    private int[] _tag_Ids = new int[5];
    public int[] tag_Id { get { return _tag_Ids; } set { _tag_Ids = value; } }

    private bool _ishad;
    public bool ishad { get { return _ishad; } set { _ishad = value; } }

}

[Serializable]
public class FireCracker
{
    private int _color_Id1;
    public int color_Id1 { get { return _color_Id1; } set { if (value < 0) _color_Id1 = 0; else _color_Id1 = value; } }

    private int _color_Id2;
    public int color_Id2 { get { return _color_Id2; } set { if (value < 0) _color_Id2 = 0; else _color_Id2 = value; } }

    private float _transparency;
    public float transparency { get { return _transparency; } set { if (value < 0) _transparency = 0; else _transparency = value; } }

    private bool _isGradation;
    public bool isGradation { get { return _isGradation; } set { _isGradation = value; } }

    private int _particle_Id;
    public int particle_Id { get { return _particle_Id; } set { if (value < 0) _particle_Id = 0; else _particle_Id = value; } }

    private Blueprint _bp;
    public Blueprint bp { get { return _bp; } set { _bp = value; } }

}

[Serializable]


public class GameManager : MonoBehaviour
{
    /*하는 일
     * 1. 설계도 줍줍버튼 활성화
     * 2. 게임 전체 상태 변경
     * 3. 
     * 
     * 
     */


    [SerializeField] private List<Blueprint> blueprints_Database;

    [SerializeField] private GameState gameState;

    [SerializeField] private FireCracker present_fireCracker;






    #region request





    #endregion

}
