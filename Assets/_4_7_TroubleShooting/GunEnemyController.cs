using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敵を制御するコンポーネント。敵は独立して動いているため、プレハブを増やせば敵を増やすことができる。
/// ただし、m_onShoot だけはプレハブを置いた後に設定しなければならないため、動的に敵を生成する場合は m_onShoot を設定する処理をすること。
/// </summary>
public class GunEnemyController : MonoBehaviour
{
    /// <summary>Idle 状態から Ready 状態に移る時間の最小値（単位: 秒）</summary>
    [SerializeField] float m_minInterval = 1f;
    /// <summary>Idle 状態から Ready 状態に移る時間の最大値（単位: 秒）</summary>
    [SerializeField] float m_maxInterval = 5f;
    /// <summary>Ready 状態から攻撃までの時間の最小値（単位: 秒）</summary>
    [SerializeField] float m_minFireTime = 0.1f;
    /// <summary>Ready 状態から攻撃までの時間の最大値（単位: 秒）</summary>
    [SerializeField] float m_maxFireTime = 0.3f;
    /// <summary>キャラクターアニメーションのための Animator</summary>
    [SerializeField] Animator m_animator = null;
    /// <summary>攻撃が当たった時に加算される点</summary>
    [SerializeField] int m_score = 100;
    /// <summary>キャラクターの状態</summary>
    GunEnemyStatus m_status = GunEnemyStatus.Idle;
    /// <summary>タイマー</summary>
    float m_timer;
    /// <summary>Idle 状態から Ready 状態に移るまでの時間（単位: 秒）</summary>
    float m_interval;
    /// <summary>Ready 状態から攻撃までの時間（単位: 秒）</summary>
    float m_fireTime;
    /// <summary>攻撃判定のためのコライダー</summary>
    Collider m_collider = null;             // Collider は BoxCollider, SphereCollider などの基底クラスである
    /// <summary>敵が銃を撃った時に実行される処理</summary>
    [SerializeField] UnityEngine.Events.UnityEvent m_onShoot = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void OnEnable()
    {
        this.transform.LookAt(Camera.main.transform.position);
        m_collider = GetComponent<Collider>();
        m_collider.enabled = false;
        m_status = GunEnemyStatus.Idle;
        ResetTimer();
    }

    void Update()
    {
        m_timer += Time.deltaTime;

        switch (m_status)
        {
            case GunEnemyStatus.Idle:
                if (m_timer > m_interval)
                {
                    ResetTimer();
                    m_status = GunEnemyStatus.Ready;
                    m_animator.SetTrigger("Ready");
                    m_collider.enabled = true;
                }
                break;
            case GunEnemyStatus.Ready:
                if (m_timer > m_fireTime)
                {
                    ResetTimer();
                    m_status = GunEnemyStatus.Idle;
                    m_animator.SetTrigger("Fire");
                    m_collider.enabled = false;
                    
                    if (m_onShoot.GetPersistentEventCount() > 0)
                    {
                        m_onShoot.Invoke();
                    }
                    else
                    {
                        Debug.LogWarning("発砲時に呼ばれる処理が指定されていません。");
                    }
                }
                break;
        }
    }

    /// <summary>
    /// タイマーをリセットする
    /// </summary>
    void ResetTimer()
    {
        m_timer = 0;
        m_fireTime = Random.Range(m_minFireTime, m_maxFireTime);    // Idle -> Ready までの時間をランダムに決める
        m_interval = Random.Range(m_minInterval, m_maxInterval);    // Ready -> 攻撃までの時間をランダムに決める
    }

    /// <summary>
    /// プレイヤーの攻撃が当たった時に呼ぶ
    /// Idle 状態（初期状態）に戻す
    /// </summary>
    /// <returns></returns>
    public int Hit()
    {
        ResetTimer();
        m_status = GunEnemyStatus.Idle;
        m_animator.SetTrigger("Hit");
        m_collider.enabled = false;
        return m_score;
    }
}

/// <summary>
/// 敵の状態を表す
/// </summary>
enum GunEnemyStatus
{
    /// <summary>壁の裏に隠れて待機している状態。こちらの攻撃は当たらない
    /// </summary>
    Idle,
    /// <summary>銃を構えてこちらを狙っている状態。この時にこちらの攻撃が当たる。</summary>
    Ready,
}
