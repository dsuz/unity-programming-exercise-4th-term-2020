using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TorchManager : MonoBehaviour
{
    [SerializeField] Transform m_object = null;
    [SerializeField] RectTransform m_panel = null;
    TorchController[] m_torches = null;

    void Start()
    {
        m_torches = GameObject.FindObjectsOfType<TorchController>();
    }

    /// <summary>
    /// 全ての炎台に火を灯す
    /// </summary>
    public void Exercise01()
    {
        foreach (var torch in m_torches)
        {
            torch.PlayParticle();
        }

        // 課題: 上記の処理を ForEach メソッドを使って書け
    }

    /// <summary>
    /// 全ての炎台を回して火を灯す
    /// </summary>
    public void Exercise02()
    {
        foreach (var torch in m_torches)
        {
            torch.PlayParticle();
            torch.PlayAnimation("Spin");
        }

        // 課題: 上記の処理を ForEach メソッドを使って書け
    }

    /// <summary>
    /// m_object の一番近くにある炎台に火を灯す
    /// </summary>
    public void Exercise03()
    {
        m_torches.OrderBy(torch => Vector3.Distance(m_object.position, torch.transform.position)).FirstOrDefault().PlayParticle();

        // 課題: 上記の処理を for または foreach 文を使って書け
    }

    /// <summary>
    /// m_object の近くから count 個の炎台に火を灯す
    /// 例: count = 3 の時は、近い順に３つ、つまり一番近い・２番目に近い・３番目に近い炎台に火を灯す
    /// </summary>
    /// <param name="count"></param>
    public void Exercise04(int count)
    {
        // 課題: 関数のコメントに書かれている処理を書け
    }

    /// <summary>
    /// m_object の近くから count 番目に近いの炎台に火を灯す
    /// 例: count = 3 の時は、３番目に近い炎台に火を灯す
    /// </summary>
    /// <param name="count"></param>
    public void Exercise05(int count)
    {
        // 課題: 関数のコメントに書かれている処理を書け
    }

    /// <summary>
    /// m_panel の子オブジェクトである Image のうち、色が赤のものを白にする
    /// </summary>
    public void Exercise06()
    {
        Image[] images = m_panel.GetComponentsInChildren<Image>();

        foreach (var image in images)
        {
            if (image.color == Color.red)
            {
                image.color = Color.white;
            }
        }

        // 課題: 上記の処理を LINQ を使って簡潔に書け。
    }

    /// <summary>
    /// m_panel の子オブジェクトである Image の色リストを Console に出力する
    /// ただし重複した色を出力してはいけない
    /// </summary>
    public void Exercise07()
    {
        Image[] images = m_panel.GetComponentsInChildren<Image>();

        List<Color> colorList = new List<Color>();
        foreach(var image in images)
        {
            if (!colorList.Contains(image.color))
            {
                colorList.Add(image.color);
                Debug.Log(image.color.ToString());
            }
        }

        // 課題: 上記の処理を LINQ を使って簡潔に書け。
    }
}
