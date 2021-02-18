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
    /// <summary>スコアを表示するための Text (UI)</summary>
    [SerializeField] Text m_scoreText = null;
    /// <summary>ライフの初期値</summary>
    [SerializeField] int m_initialLife = 500;
    /// <summary>ライフを表示するための Text (UI)</summary>
    [SerializeField] Text m_lifeText = null;
    /// <summary>この得点ごとにライフが増える</summary>
    [SerializeField] int m_scoreIntervalForLifeUp = 5000;
    int m_nextLifeUpScore = 0;
    /// <summary>弾を撃った時に呼び出す処理</summary>
    [SerializeField] UnityEngine.Events.UnityEvent m_onShoot = null;
    /// <summary>ゲームスタート時に呼び出す処理</summary>
    [SerializeField] UnityEngine.Events.UnityEvent m_onGameStart = null;
    /// <summary>ゲームオーバー時に呼び出す処理</summary>
    [SerializeField] UnityEngine.Events.UnityEvent m_onGameOver = null;
    /// <summary>ボーナスでライフが増える時に呼び出す処理</summary>
    [SerializeField] UnityEngine.Events.UnityEvent m_onLifeUp = null;
    /// <summary>ライフの現在値</summary>
    int m_life;
    /// <summary>ゲームのスコア</summary>
    int m_score = 0;
    /// <summary>全ての敵オブジェクトを入れておくための List</summary>
    List<GunEnemyController> m_enemies = null;
    /// <summary>現在照準で狙われている敵</summary>
    GunEnemyController m_currentTargetEnemy = null;

    void Start()
    {
        m_onGameStart.Invoke();

        m_life = m_initialLife;
        m_score = 0;
        m_enemies = GameObject.FindObjectsOfType<GunEnemyController>().ToList();    // LINQ を使うために配列ではなく List に保存する
        m_lifeText.text = string.Format("{0:000}", m_life);
        m_scoreText.text = string.Format("{0:0000000000}", m_score);
        m_nextLifeUpScore = m_scoreIntervalForLifeUp;

        if (m_hideSystemMouseCursor)
        {
            Cursor.visible = false;
        }
    }

    /// <summary>
    /// ゲームをやり直す
    /// </summary>
    public void Restart()
    {
        Debug.Log("Restart");
        m_enemies.ForEach(enemy => enemy.gameObject.SetActive(true));   // LINQ メソッド
        Start();
    }

    /// <summary>
    /// ゲームオーバー
    /// </summary>
    void Gameover()
    {
        Debug.Log("Gameover");
        m_enemies.ForEach(enemy => enemy.gameObject.SetActive(false));  // LINQ メソッド
        m_onGameOver.Invoke();
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
        m_crosshairImage.color = isEnemyTargeted ? m_colorFocus : m_colorNormal;    // 三項演算子 ? を使っている
        m_currentTargetEnemy = isEnemyTargeted ? hit.collider.gameObject.GetComponent<GunEnemyController>() : null;    // 三項演算子 ? を使っている

        if (Input.GetButtonDown("Fire1"))
        {
            m_onShoot.Invoke();

            // 敵に当たったら得点を足して表示を更新する
            if (m_currentTargetEnemy)
            {
                m_score += m_currentTargetEnemy.Hit();
                
                if (m_score > m_nextLifeUpScore)
                {
                    m_nextLifeUpScore += m_scoreIntervalForLifeUp;
                    m_onLifeUp.Invoke();
                }

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
        Debug.Log("Hit by enemy");
        // ライフを減らして表示を更新する。
        m_life -= 1;
        m_lifeText.text = string.Format("{0:000}", m_life);
        
        if (m_life < 1)
        {
            Gameover();
        }
    }

    /// <summary>
    /// ライフを増やす
    /// </summary>
    /// <param name="life">増やすライフ</param>
    public void LifeUp(int life)
    {
        m_life += life;
    }
}
