using UnityEngine;
using DG.Tweening;

public class ElevatorController : MonoBehaviour
{
    /// <summary>エレベーターの移動先</summary>
    [SerializeField] Transform m_target = null;
    /// <summary>エレベーターのスイッチとなるトリガー</summary>
    [SerializeField] Collider m_trigger = null;
    /// <summary>エレベーターの初期位置</summary>
    Vector3 m_initialPosition;
    [SerializeField] float m_moveToSeconds = 1f;
    [SerializeField] float m_interval = 3f;
    [SerializeField] float m_moveBackSeconds = 3f;

    void Start()
    {
        m_initialPosition = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Elevate();
        }
    }

    private void Elevate()
    {
        // 課題: 以下の仕様のエレベーターを DOTween を使って作れ。
        // エレベーターを動かすスイッチとなっているトリガーを無効にする
        // m_target の位置まで 1 秒で移動する
        // 移動が完了したら 3 秒間待つ
        // 3 秒間待ったら、3 秒かけて元の位置に戻る
        // 元の位置に戻ったら 3 秒後にトリガーが有効に戻す

        m_trigger.enabled = false;
        this.transform.position = m_target.position;
        m_trigger.enabled = true;
    }
}
