using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// ゲームを管理する。適当なオブジェクトにアタッチし、各種設定をすれば動作する。
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>Windows のマウスカーソルをゲーム中に消すかどうかの設定</summary>
    [SerializeField] bool m_hideSystemMouseCursor = false;
    /// <summary>敵オブジェクトがいるレイヤー</summary>
    [SerializeField] LayerMask m_enemyLayer;
    /// <summary>照準の Image (UI)</summary>
    [SerializeField] Image m_crosshairImage = null;
    /// <summary>照準に敵が入っていない時の色</summary>
    [SerializeField] Color m_colorNormal = Color.white;
    /// <summary>照準に敵が入っている時の色</summary>
    [SerializeField] Color m_colorFocus = Color.red;
    /// <summary>銃のオブジェクト</summary>
    [SerializeField] GameObject m_gunObject = null;
    /// <summary>銃の操作のために Ray を飛ばす距離</summary>
    [SerializeField] float m_rangeDistance = 100f;
    /// <summary>現在照準で狙われている敵</summary>
    GunEnemyController m_currentTargetEnemy = null;
    /// <summary>発砲した時の効果音</summary>
    [SerializeField] AudioClip m_gunFireSfx = null;
    AudioSource m_audio = null;
    int m_score = 0;
    /// <summary>スコアを表示するための Text (UI)</summary>
    [SerializeField] Text m_scoreText = null;
    /// <summary>ライフの初期値</summary>
    [SerializeField] int m_initialLife = 500;
    /// <summary>ライフの現在値</summary>
    int m_life;
    /// <summary>ライフを表示するための Text (UI)</summary>
    [SerializeField] Text m_lifeText = null;
    /// <summary>カメラを揺らす設定</summary>
    [SerializeField] Cinemachine.CinemachineImpulseSource m_cameraShakeSource = null;
    /// <summary>ダメージを受けた時に赤くして表現するためのパネル</summary>
    [SerializeField] Animator m_hitScreen = null;
    /// <summary>全ての敵オブジェクトを入れておくための List</summary>
    List<GunEnemyController> m_enemies = null;
    /// <summary>ゲームオーバー時に表示するパネル</summary>
    [SerializeField] Animator m_gameoverPanel = null;

    void Start()
    {
        if (m_hideSystemMouseCursor)
        {
            Cursor.visible = false;
        }

        m_life = m_initialLife;
        m_score = 0;
        m_audio = GetComponent<AudioSource>();
        m_enemies = GameObject.FindObjectsOfType<GunEnemyController>().ToList();
        m_lifeText.text = string.Format("{0:000}", m_life);
        m_scoreText.text = string.Format("{0:0000000000}", m_score);
        m_gameoverPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// ゲームをやり直す
    /// </summary>
    public void Restart()
    {
        Debug.Log("Restart");
        m_enemies.ForEach(enemy => enemy.gameObject.SetActive(true));   // LINQ
        Start();
    }

    /// <summary>
    /// ゲームオーバー
    /// </summary>
    void Gameover()
    {
        m_gameoverPanel.gameObject.SetActive(true);
        m_gameoverPanel.Play("Show");
        m_enemies.ForEach(enemy => enemy.gameObject.SetActive(false));  // LINQ
    }

    void Update()
    {
        m_crosshairImage.rectTransform.position = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, m_rangeDistance))
        {
            m_gunObject.transform.LookAt(hit.point);    // 銃の方向を変えている
        }

        // 敵が照準に入っているか調べる
        bool isEnemyTargeted = Physics.Raycast(ray, out hit, m_rangeDistance, m_enemyLayer);
        m_crosshairImage.color = isEnemyTargeted ? m_colorFocus : m_colorNormal;
        m_currentTargetEnemy = isEnemyTargeted ? hit.collider.gameObject.GetComponent<GunEnemyController>() : null;

        if (Input.GetButtonDown("Fire1"))
        {
            // カメラを揺らして効果音を鳴らす
            m_cameraShakeSource.GenerateImpulse();
            m_audio.clip = m_gunFireSfx;
            m_audio.Play();

            // 敵に当たったら得点を足して表示を更新する
            if (m_currentTargetEnemy)
            {
                m_score += m_currentTargetEnemy.Hit();
                m_scoreText.text = string.Format("{0:0000000000}", m_score);
            }
        }
    }

    private void OnApplicationQuit()
    {
        Cursor.visible = true;
    }

    /// <summary>
    /// 攻撃を食らった時に呼ぶ
    /// </summary>
    public void Hit()
    {
        // ライフを減らして表示を更新し、エフェクトを再生する
        m_life -= 1;
        m_lifeText.text = string.Format("{0:000}", m_life);
        m_hitScreen.SetTrigger("Hit");
        
        if (m_life < 1)
        {
            Gameover();
        }
    }
}
