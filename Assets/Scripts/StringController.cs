using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringController : MonoBehaviour
{
    private string path;
    private string textValue;
    private string[] string_list;
    private string[] color_name_list;
    private string[] shape_name_list;
    const int color_num = 4; // 임시로 둠
    const int shape_num = 4; // 임시로 둠 

    private void Start()
    {
        color_name_list = new string[] { "a0", "a1", "a2", "a3" };
        shape_name_list = new string[] { "b0", "b1", "b2", "b3" };
        string_list = new string[10];
        path = @"./Assets/Scripts/request.txt";
        textValue = System.IO.File.ReadAllText(path);
        //textValue = "나는 A색의 B모양 불꽃이 좋아.\n나는 겨울과 같은 B모양의 A색이 더 좋아.";
        string_list = textValue.Split('\n');
        GetOrder();
    }

    public string GetOrder()
    {
        int sentence_type_id = Random.RandomRange(0, string_list.Length);
        int color_id = Random.RandomRange(0, color_num);
        int shape_id = Random.RandomRange(0, shape_num);
        string ans = "";
        for(int i=0; i<string_list[sentence_type_id].Length; i++)
        {
            char c = string_list[sentence_type_id][i];
            if (c == 'A')
            {
                ans += color_name_list[color_id];
            }
            else if (c == 'B')
            {
                ans += shape_name_list[shape_id];
            }
            else
            {
                ans += c;
            }
        }
        Debug.Log(ans);
        return ans;
    }

}
