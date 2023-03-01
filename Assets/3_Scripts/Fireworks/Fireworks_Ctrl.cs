using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Fireworks_Ctrl : MonoBehaviour
{
    [SerializeField] private GameObject go_firwork_Basic_1;
    [SerializeField] private GameObject go_firwork_Basic_2;
    [SerializeField] private GameObject go_firwork_Basic_3;
    [SerializeField] private Firework firwork_Basic_1;
    [SerializeField] private Firework firwork_Basic_2;
    [SerializeField] private Firework firwork_Basic_3;
    [SerializeField] private VisualEffect visualEffect_Basic_1;
    [SerializeField] private VisualEffect visualEffect_Basic_2;
    [SerializeField] private VisualEffect visualEffect_Basic_3;

    [SerializeField] private GameObject go_firwork_Shape_1;
    [SerializeField] private GameObject go_firwork_Shape_2;
    [SerializeField] private GameObject go_firwork_Shape_3;
    [SerializeField] private Firework firwork_Shape_1;
    [SerializeField] private Firework firwork_Shape_2;
    [SerializeField] private Firework firwork_Shape_3;
    [SerializeField] private VisualEffect visualEffect_Shape_1;
    [SerializeField] private VisualEffect visualEffect_Shape_2;
    [SerializeField] private VisualEffect visualEffect_Shape_3;

    private void OnEnable()
    {
        firwork_Basic_1.Shoot();
        firwork_Basic_2.Shoot();
        firwork_Basic_3.Shoot();
        firwork_Shape_1.Shoot();
        firwork_Shape_2.Shoot();
        firwork_Shape_3.Shoot();
        Switching_Fireworks(true);
    }


    public void Switching_Fireworks(bool isBasic)
    {
        go_firwork_Basic_1.SetActive(isBasic);
        go_firwork_Basic_2.SetActive(isBasic);
        go_firwork_Basic_3.SetActive(isBasic);
        go_firwork_Shape_1.SetActive(!isBasic);
        go_firwork_Shape_2.SetActive(!isBasic);
        go_firwork_Shape_3.SetActive(!isBasic);
    }

    public void Applying(int amount, Gradient g, int amount1)
    {

        visualEffect_Basic_1.SetInt("Amount", amount);
        visualEffect_Basic_1.SetGradient("Gradiant", g);
        visualEffect_Basic_1.SetInt("Amount 1", amount1);

        visualEffect_Basic_2.SetInt("Amount", amount);
        visualEffect_Basic_2.SetGradient("Gradiant", g);
        visualEffect_Basic_2.SetInt("Amount 1", amount1);

        visualEffect_Basic_3.SetInt("Amount", amount);
        visualEffect_Basic_3.SetGradient("Gradiant", g);
        visualEffect_Basic_3.SetInt("Amount 1", amount1);
    }

    public void Applying(int amount, Gradient g1, int amount1, Gradient g2)
    {

        visualEffect_Basic_1.SetInt("Amount", amount);
        visualEffect_Basic_1.SetGradient("Gradiant", g1);
        visualEffect_Basic_1.SetInt("Amount 1", amount1);
        visualEffect_Basic_1.SetGradient("Gradiant 1", g2);

        visualEffect_Basic_2.SetInt("Amount", amount);
        visualEffect_Basic_2.SetGradient("Gradiant", g1);
        visualEffect_Basic_2.SetInt("Amount 1", amount1);
        visualEffect_Basic_2.SetGradient("Gradiant 1", g2);

        visualEffect_Basic_3.SetInt("Amount", amount);
        visualEffect_Basic_3.SetGradient("Gradiant", g1);
        visualEffect_Basic_3.SetInt("Amount 1", amount1);
        visualEffect_Basic_3.SetGradient("Gradiant 1", g2);
    }


    public void Applying(Texture t,int amount, float scale,Gradient g1, int amount1)
    {

        visualEffect_Shape_1.SetTexture("Shape", t);
        visualEffect_Shape_1.SetInt("Amount", amount);
        visualEffect_Shape_1.SetFloat("Scale", scale);
        visualEffect_Shape_1.SetGradient("Gradiant", g1);
        visualEffect_Shape_1.SetInt("Amount 1", amount1);

        visualEffect_Shape_2.SetTexture("Shape", t);
        visualEffect_Shape_2.SetInt("Amount", amount);
        visualEffect_Shape_2.SetFloat("Scale", scale);
        visualEffect_Shape_2.SetGradient("Gradiant", g1);
        visualEffect_Shape_2.SetInt("Amount 1", amount1);

        visualEffect_Shape_3.SetTexture("Shape", t);
        visualEffect_Shape_3.SetInt("Amount", amount);
        visualEffect_Shape_3.SetFloat("Scale", scale);
        visualEffect_Shape_3.SetGradient("Gradiant", g1);
        visualEffect_Shape_3.SetInt("Amount 1", amount1);


    }




}
