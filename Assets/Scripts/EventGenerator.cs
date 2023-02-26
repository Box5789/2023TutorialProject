using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGenerator : MonoBehaviour
{
    private const int term_min=5;
    private const int term_max=10;
    private int term;
    private float delta_time=-1;
    
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (delta_time < 0)
        {
            // 이벤트 실행
            term = Random.RandomRange(term_min, term_max);
            delta_time = term;
        }
        else
        {
            delta_time -= Time.deltaTime;
        }
    }
}
