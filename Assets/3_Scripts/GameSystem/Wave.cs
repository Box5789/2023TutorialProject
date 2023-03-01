using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float speed;
    public float left_side;
    public float right_side;

    public bool dir;
    private Vector3 right_position;
    private Vector3 left_position;

    private void Start()
    {
        var position = transform.position;
        right_position = new Vector3(position.x + right_side, position.y, position.z);
        left_position = new Vector3(position.x + left_side, position.y, position.z);
    }

    private void Update()
    {
        switch (dir)
        {
            case true:
                {
                    transform.position = Vector3.MoveTowards(gameObject.transform.position, right_position, speed);
                    dir = false;
                    break;
                }
            case false:
                {
                    transform.position = Vector3.MoveTowards(gameObject.transform.position, left_position, speed);
                    dir = true;
                    break;
                }
        }
    }
}
