using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human_Generator : MonoBehaviour
{
    [SerializeField] private GameObject[] human_Prefabs;


    private void OnEnable()
    {
        StartCoroutine(Generate_Human1());
        StartCoroutine(Generate_Human2());
        StartCoroutine(Generate_Human3());
        StartCoroutine(Generate_Human4());
        StartCoroutine(Generate_Human5());
        StartCoroutine(Generate_Human6());
    }

    private IEnumerator Generate_Human1()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0,10));
        while (true)
        {
            GameObject h_go = Instantiate(human_Prefabs[0], new Vector3(UnityEngine.Random.Range(-1850.0f, 1850.0f), UnityEngine.Random.Range(-700.0f, -800.0f), 0.0f), Quaternion.Euler(Vector3.zero), transform);
            Human h = h_go.GetComponent<Human>();

            float t = UnityEngine.Random.Range(150f, 300f);
            h.First_Set(t);

            yield return new WaitForSeconds(t + 10.0f);
        }
    }
    private IEnumerator Generate_Human2()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, 10));
        while (true)
        {
            GameObject h_go = Instantiate(human_Prefabs[1], new Vector3(UnityEngine.Random.Range(-1850.0f, 1850.0f), UnityEngine.Random.Range(-700.0f, -800.0f), 0.0f), Quaternion.Euler(Vector3.zero), transform);
            Human h = h_go.GetComponent<Human>();

            float t = UnityEngine.Random.Range(150f, 300f);
            h.First_Set(t);

            yield return new WaitForSeconds(t + 10.0f);
        }
    }
    private IEnumerator Generate_Human3()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, 10));
        while (true)
        {
            GameObject h_go = Instantiate(human_Prefabs[2], new Vector3(UnityEngine.Random.Range(-1850.0f, 1850.0f), UnityEngine.Random.Range(-700.0f, -800.0f), 0.0f), Quaternion.Euler(Vector3.zero), transform);
            Human h = h_go.GetComponent<Human>();

            float t = UnityEngine.Random.Range(150f, 300f);
            h.First_Set(t);

            yield return new WaitForSeconds(t + 10.0f);
        }
    }
    private IEnumerator Generate_Human4()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, 10));
        while (true)
        {
            GameObject h_go = Instantiate(human_Prefabs[3], new Vector3(UnityEngine.Random.Range(-1850.0f, 1850.0f), UnityEngine.Random.Range(-700.0f, -800.0f), 0.0f), Quaternion.Euler(Vector3.zero), transform);
            Human h = h_go.GetComponent<Human>();

            float t = UnityEngine.Random.Range(150f, 300f);
            h.First_Set(t);

            yield return new WaitForSeconds(t + 10.0f);
        }
    }
    private IEnumerator Generate_Human5()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, 10));
        while (true)
        {
            GameObject h_go = Instantiate(human_Prefabs[4], new Vector3(UnityEngine.Random.Range(-1850.0f, 1850.0f), UnityEngine.Random.Range(-700.0f, -800.0f), 0.0f), Quaternion.Euler(Vector3.zero), transform);
            Human h = h_go.GetComponent<Human>();

            float t = UnityEngine.Random.Range(150f, 300f);
            h.First_Set(t);

            yield return new WaitForSeconds(t + 10.0f);
        }
    }
    private IEnumerator Generate_Human6()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, 10));
        while (true)
        {
            GameObject h_go = Instantiate(human_Prefabs[5], new Vector3(UnityEngine.Random.Range(-1850.0f, 1850.0f), UnityEngine.Random.Range(-700.0f, -800.0f), 0.0f), Quaternion.Euler(Vector3.zero), transform);
            Human h = h_go.GetComponent<Human>();

            float t = UnityEngine.Random.Range(150f, 300f);
            h.First_Set(t);

            yield return new WaitForSeconds(t + 10.0f);
        }
    }




}
