using UnityEngine;

/// <summary>
/// 樽オブジェクトを制御する。
/// </summary>
public class BarrelController : MonoBehaviour
{
    /// <summary>ラベルとなる UI オブジェクト</summary>
    [SerializeField] UnityEngine.UI.Text m_label = null;

    /// <summary>
    /// 引数に指定された番号をラベルに表示する。
    /// </summary>
    /// <param name="number"></param>
    public void SetNumber(int number)
    {
        m_label.text = number.ToString();
    }
}
