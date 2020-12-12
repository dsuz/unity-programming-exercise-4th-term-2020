using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Exercise4-2 のタンクを動かすコンポーネント
/// </summary>
public class TankController4_2 : MonoBehaviour
{
    [SerializeField] float m_moveSpeed = 1f;
    [SerializeField] float m_rotateSpeed = 1f;
    [SerializeField] ExplosionController m_explosionObject = null;
    [SerializeField] Transform m_muzzle = null;
    [SerializeField] float m_maxFireDistance = 100f;
    Rigidbody m_rb = null;
    LineRenderer m_line = null;
    Vector3 m_rayCastHitPosition;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // クリックしたら爆発する
        if (Input.GetButtonDown("Fire1"))
        {
            Fire1();
        }

        // レイキャストして「レーザーポインターがどこに当たっているか」を調べる
        Ray ray = new Ray(m_muzzle.position, this.transform.forward);   // muzzle から正面に ray を飛ばす
        RaycastHit hit;

        // 課題: 以下で Physics.Raycast() を使って Ray が衝突する座標を取得し、レーザーが障害物に衝突した時はそこでレーザーが止まるように修正せよ
        m_rayCastHitPosition = m_muzzle.position + this.transform.forward * m_maxFireDistance;

        // Line Renderer を使ってレーザーを描く
        m_line.SetPosition(0, m_muzzle.position);
        m_line.SetPosition(1, m_rayCastHitPosition);

        // 以下、上下左右でタンクを動かす

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0)
        {
            this.transform.Rotate(Vector3.up, h * m_rotateSpeed);
        }

        if (v != 0)
        {
            m_rb.velocity = v * this.transform.forward * m_moveSpeed;
        }
    }

    void Fire1()
    {
        // 課題: 以下のコードでは Muzzle の場所で爆発するが、レーザーが障害物に衝突した位置で爆発するように修正せよ
        m_explosionObject.Explode(m_muzzle.position);
    }
}
