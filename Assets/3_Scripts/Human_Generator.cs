using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human_Generator : MonoBehaviour
{
    [SerializeField] private GameObject human_Prefab;


    private void OnEnable()
    {
        Invoke("Generate_Human1", 1.0f);
        Invoke("Generate_Human2", 3.0f);
        Invoke("Generate_Human3", 5.0f);
        Invoke("Generate_Human4", 7.0f);
    }

    private void OnDisable()
    {
        CancelInvoke("Generate_Human1");
        CancelInvoke("Generate_Human2");
        CancelInvoke("Generate_Human3");
        CancelInvoke("Generate_Human4");
    }

    private void Generate_Human1()
    {
        Instantiate(human_Prefab, new Vector3(30.0f,-13.0f,0.0f), Quaternion.Euler(Vector3.zero), transform);

        Invoke("Generate_Human1", 300.0f);
    }
    private void Generate_Human2()
    {
        Instantiate(human_Prefab, new Vector3(-30.0f, -13.0f, 0.0f), Quaternion.Euler(Vector3.zero), transform);
        Invoke("Generate_Human2", 300.0f);
    }
    private void Generate_Human3()
    {
        Instantiate(human_Prefab, new Vector3(29.0f, -13.0f, 0.0f), Quaternion.Euler(Vector3.zero), transform);

        Invoke("Generate_Human3", 300.0f);
    }
    private void Generate_Human4()
    {
        Instantiate(human_Prefab, new Vector3(-29.0f, -13.0f, 0.0f), Quaternion.Euler(Vector3.zero), transform);

        Invoke("Generate_Human4", 300.0f);
    }

}
