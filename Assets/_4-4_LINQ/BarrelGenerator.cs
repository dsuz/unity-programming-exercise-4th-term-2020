using System.Collections;
using UnityEngine;

/// <summary>
/// m_barrel に指定されたオブジェクトをシーン内のランダムな場所に生成する
/// </summary>
public class BarrelGenerator : MonoBehaviour
{
    [SerializeField] GameObject m_barrel = null;
    [SerializeField] float m_generateInterval = 0.1f;
    [SerializeField] int m_generateCount = 8;

    void Start()
    {
        StartCoroutine(GenerateBarrel());
    }

    IEnumerator GenerateBarrel()
    {
        int count = 0;

        while (count < m_generateCount)
        {
            float x = Random.Range(-4f, 4f);
            float z = Random.Range(-4f, 4f);
            Vector3 position = new Vector3(x, 0f, z);
            Instantiate(m_barrel, position, Quaternion.identity);
            count++;
            yield return new WaitForSeconds(m_generateInterval);
        }
    }
}
