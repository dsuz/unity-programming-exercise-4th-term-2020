using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DelegateSample : MonoBehaviour
{
    delegate void DelegateVariable01(int i, string s);  // デリゲート（の型名）を宣言する
    [SerializeField] Text m_counter = null; 

    /// <summary>
    /// 基本的なデリゲート
    /// </summary>
    public void Example01()
    {
        DelegateVariable01 v = Method01;    // デリゲートにメソッドを「代入」する
        v.Invoke(1, "a");   // デリゲートに代入したメソッドを呼び出す
        v += Method02;      // デリゲートにメソッドを「追加」する
        v.Invoke(2, "b");   // デリゲートに追加されているメソッド（２つ）を呼び出す
        // v = Method03;    // パラメータが合わない時はデリゲートにメソッドを追加できない
        v -= Method02;      // デリゲートからメソッドを削除する
        v.Invoke(3, "c");   // デリゲートに入っているメソッド（１つ）を呼び出す
    }

    /// <summary>
    /// パラメータに関数を渡す方法、delegate を使った匿名メソッド、ラムダ式を使った匿名メソッド
    /// </summary>
    public void Example02()
    {
        DelegateExecutor(Method01); // こういう呼び出し方もできる。
        // ↑この呼び出し方は「DelegateExecutor 内の処理をした後に、Method01 を実行する」の様に
        //「●●の後に××を実行する」ために使われることが多く、このようなやり方を「コールバック (callback) 」と言う。

        // 匿名メソッドを渡すこともできる
        DelegateExecutor(delegate (int i, string s)
        {
            Debug.Log($"匿名メソッドが呼ばれました。{i.ToString()}, {s}");
        });

        // 匿名メソッドをラムダ式で書くこともできる
        DelegateExecutor((i, s) => Debug.Log($"ラムダ式で定義した匿名メソッドが呼ばれました。 {i.ToString()}, {s}"));

        // ラムダ式と言えば、このような書き方もできる
        DelegateVariable01 v = new DelegateVariable01((i, s) => Debug.Log($"ラムダ式で定義した匿名メソッドが呼ばれました。 {i.ToString()}, {s}"));
        v.Invoke(4, "d");
    }

    /// <summary>
    /// カウンターをカウントアップする
    /// </summary>
    public void Example03()
    {
        Sequence seq = DOTween.Sequence();

        // 10秒かけて 0 から 99999 までカウントアップする。
        seq.Append(DOTween.To(Getter, Setter, 99999, 10f));

        int Getter()
        {
            return 0;
        }

        void Setter(int x)
        {
            m_counter.text = x.ToString("d5");
        }

        // 10秒かけてカウントダウンする。delegate を使って匿名メソッドを書いた。
        seq.Append(DOTween.To(delegate () { return int.Parse(m_counter.text); },
            delegate (int x) { m_counter.text = x.ToString("d5"); },
            0,
            10f));
    }

    void Method01(int i, string s)
    {
        Debug.Log($"Method01 が呼び出されました。{i.ToString()}, {s}");
    }

    void Method02(int i, string s)
    {
        Debug.Log($"Method02 が呼び出されました。{s}, {i.ToString()}");
    }

    void Method03(int i)
    {
        Debug.Log($"Method03 が呼び出されました。{i.ToString()}");
    }

    void DelegateExecutor(DelegateVariable01 method)
    {
        method(0, ".");
    }
}
