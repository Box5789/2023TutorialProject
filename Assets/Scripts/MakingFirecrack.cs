using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakingFirecrack : MonoBehaviour
{
    // Start is called before the first frame update
    //int color_Id, float transparency, bool isGradation, int particle_Id, blueprint bp

    [SerializeField]
    GameObject gapandae_popup;

    [SerializeField]
    private GameManager gameManager;

    private int color_id_1;
    private int color_id_2;
    private float transparency; 
    private int bp;
    //private bool is_gradation;
    //private int particle_id;


    private void Start()
    {
        gapandae_popup.SetActive(false);
    }

    public void OpenPopup()
    {
        gapandae_popup.SetActive(true);
    }

    public void ClosePopup()
    {
        gapandae_popup.SetActive(false);
    }

    public void SetColorId1(int _color_id_1) { color_id_1 = _color_id_1; }
    public void SetColorId2(int _color_id_2) { color_id_2 = _color_id_2; }
    public void SetTransparency(Slider s) { 
        transparency = s.value;
        Debug.Log(transparency);
    }
    //public void SetIsGradation(bool _is_gradation) { is_gradation = _is_gradation; }
    //public void SetIsParticleId(int _particle_id) { particle_id = _particle_id; }

    public void SetBP(int _bp) { bp = _bp; }
    public void Submit()
    {
        gameManager.crafted_FireCracker.color_Id_1 = color_id_1;
        gameManager.crafted_FireCracker.color_Id_2 = color_id_2;
        gameManager.crafted_FireCracker.transparency = transparency;
        gameManager.crafted_FireCracker.bp_Id = bp;
        gameManager.Check_At_Submit();
    }
}
