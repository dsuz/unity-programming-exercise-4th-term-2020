using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference : MonoBehaviour
{
    /// <summary>クリックして移動させるオブジェクト</summary>
    [SerializeField] GameObject m_go = null;

    void Update()
    {
        // クリックした時に RayCast する
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) // 左クリック OR　右クリック
        {
            // マウスをクリックした場所への RayCast （参照: https://docs.unity3d.com/ja/2019.4/Manual/CameraRays.html
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // ray がコライダーに当たった場合はこの中が実行される
                Debug.LogFormat("ray が {0} に当たった", hit.collider.gameObject.name);

                if (Input.GetMouseButtonDown(0))    // 左クリックの時
                {
                    // オブジェクトを移動する
                    m_go.transform.position = hit.collider.transform.position;
                }
                else // 右クリックの時
                {
                    // 課題: ここにコードを追加し、「右クリックしたらクリックした位置にオブジェクトが移動する」機能を追加せよ
                }
            }
        }
    }

    /// <summary>
    /// 「値のコピー」の例1
    /// </summary>
    public void Function01()
    {
        int a;
        int b;

        a = 1;
        b = a;  // a -> b にコピーする

        b = 999;    // b の中身を変える

        Debug.LogFormat("a: {0}, b: {1}", a, b);    // b の中身を変えた事は a に影響しない。値をコピーしているため。
    }

    /// <summary>
    /// 「参照のコピー」の例
    /// </summary>
    public void Function02()
    {
        GameObject a;
        GameObject b;

        a = Camera.main.gameObject;
        b = a;  // a -> b にコピーする

        b.name = "New Name";    // b の中身を変える

        Debug.LogFormat("a: {0}, b: {1}", a.name, b.name);  // b の中身を変えたことが a にも影響する。「参照」をコピーしているため。
    }

    /// <summary>
    /// 「値のコピー」の例2
    /// </summary>
    public void Function03()
    {
        Vector3 a;
        Vector3 b;

        a = Camera.main.transform.position;
        b = a;  // a -> b にコピーする

        b.x = 999;    // b の中身を変える

        Debug.LogFormat("a: {0}, b: {1}", a, b);    // b の中身を変えた事は a に影響しない。値をコピーしているため。
    }

    public void Function04()
    {
        string a;
        string b;

        a = "a";
        b = a;

        b = "b";

        Debug.LogFormat("a: {0}, b: {1}", a, b);
    }

    public void Function05(string p)
    {
        int i;
        if (int.TryParse(p, out i))
        {
            Debug.Log($"{p} は整数 {i} に変換されました");
        }
        else
        {
            Debug.Log($"{p} は整数に変換できませんでした");
        }
    }
}
