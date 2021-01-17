using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  // LINQ を使うために必要

/// <summary>
/// LINQ の演習のためのサンプル。
/// LinqExercise01, 02, 05 が例題になっている。
/// LinqExercise03, 04, 06 を解け。
/// </summary>
public class LinqSample : MonoBehaviour
{
    int[] intArray = { 9, 3, 5, 8, 1, 7, 2, 4, 6, 0 };
    string[] stringArray = { "banana", "orange", "kiwi", "apple", "pineapple" };
    GameObject m_player = null;

    void Start()
    {
        LinqExercise01();
        // LinqExercise02();
        // LinqExercise03();
        // LinqExercise04();

        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        LinqExercise05();
        // LinqExercise06();
    }

    /// <summary>
    /// 配列から条件に一致したものを抽出する
    /// </summary>
    void LinqExercise01()
    {
        Debug.Log("LinqExercise01");
        var result = intArray.Where(i => i > 5).ToArray();    // LINQ を使って i が 5 より大きいものを抽出する
        var filteredArray = result.ToArray();

        for (int i = 0; i < filteredArray.Length; i++)
        {
            Debug.Log($"{i} : {filteredArray[i]}");
        }
    }

    /// <summary>
    /// 配列から条件に一致したものを抽出する
    /// </summary>
    void LinqExercise02()
    {
        Debug.Log("LinqExercise02");
        var filteredArray = stringArray.Where(s => s.Length > 5).ToArray();    // LINQ を使って文字列の長さが 5 文字より長いものを抽出する

        string result = string.Join(", ", filteredArray);
        Debug.Log(result);
    }

    /// <summary>
    /// intArray から「LINQ を使って」偶数を抽出せよ
    /// </summary>
    void LinqExercise03()
    {
        Debug.Log("LinqExercise03");
    }

    /// <summary>
    /// stringArray から「LINQ を使って」'n' を含む文字列を抽出せよ
    /// </summary>
    void LinqExercise04()
    {
        Debug.Log("LinqExercise04");
    }

    /// <summary>
    /// BarrelController がアタッチされたオブジェクトに対して、原点から遠い順に番号を振る
    /// </summary>
    void LinqExercise05()
    {
        var barrelArray = GameObject.FindObjectsOfType<BarrelController>();
        var sortedArray = barrelArray.OrderByDescending(obj => obj.transform.position.sqrMagnitude).ToArray();    // LINQ を使って「原点からの距離が大きい順」に並べ替える

        for (int i = 0; i < sortedArray.Length; i++)
        {
            sortedArray[i].SetNumber(i + 1);
        }
    }

    /// <summary>
    /// BarrelController がアタッチされたオブジェクトに対して、プレイヤーから近い順に番号を振れ
    /// </summary>
    void LinqExercise06()
    {
        var barrelArray = GameObject.FindObjectsOfType<BarrelController>();

        // LINQ を使って「プレイヤーとの距離が近い順」に並べ替える。関数は LinqExercise05 の OrderByDescending() の代わりに OrderBy() を使う

        // LinqExercise05 と同じように BarrelController.SetNumber() を使って番号を振る
    }
}
