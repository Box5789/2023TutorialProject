using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakingFirecrack : MonoBehaviour
{
    // Start is called before the first frame update
    //int color_Id, float transparency, bool isGradation, int particle_Id, blueprint bp

    private int color_id_1;
    private int color_id_2;
    private float transparency;
    private bool is_gradation;
    private int particle_id;
    private int bp; // Blueprint Ŭ���� ������ �� ���ִ� ��Ȳ�̶�, �ӽ÷� int�� ���. 

    public void SetColorId1(int _color_id_1) { color_id_1 = _color_id_1; }
    public void SetColorId2(int _color_id_2) { color_id_2 = _color_id_2; }
    public void SetTransparency(float _transparency) { transparency = _transparency; }
    public void SetIsGradation(bool _is_gradation) { is_gradation = _is_gradation; }
    public void SetIsParticleId(int _particle_id) { particle_id = _particle_id; }
    public void SetIsGradation(int _bp) { bp = _bp; }
}
