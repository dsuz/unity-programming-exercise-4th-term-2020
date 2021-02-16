using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ConeController : MonoBehaviour
{
    [SerializeField] Slider m_lifeGauge = null;
    [SerializeField] int m_maxLife = 100;
    [SerializeField] GameObject m_effectPrefab = null;
    int m_life;

    void Start()
    {
        m_life = m_maxLife;
        m_lifeGauge.gameObject.SetActive(false);
        Hit(0);
    }

    /// <summary>
    /// 攻撃を受けた時に外部から呼び出される。
    /// </summary>
    /// <param name="damage"></param>
    public void Hit(int damage)
    {
        m_life -= damage;

        // 初めて攻撃を受けた時にゲージを表示する
        if (m_life < m_maxLife && m_life > 0)
        {
            m_lifeGauge.gameObject.SetActive(true);
        }

        // ライフが 0 以下ならエフェクトを出してオブジェクトを破棄する。ライフが残っているならライフゲージを減らす。
        if (m_life <= 0)
        {
            Instantiate(m_effectPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else
        {
            // 課題: ライフゲージを瞬間的に減らすのではなく、DOTween.To を使って滑らかに減らすように修正せよ。

            //m_lifeGauge.value = (float) m_life / m_maxLife;

            DOTween.To(
                () => m_lifeGauge.value, // getter
                x => m_lifeGauge.value = x, // setter
                (float)m_life / m_maxLife, // ターゲットとなる値
                1f  // 時間（秒）
                ).SetEase(Ease.OutCubic);
        }
    }
}
