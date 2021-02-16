using UnityEngine;
using UnityEngine.UI;

public class AttackController : MonoBehaviour
{
    [SerializeField] Transform m_gunPivot = null;
    [SerializeField] Image m_crosshair = null;
    [SerializeField] float m_shootRange = 30f;
    [SerializeField] int m_attackPower = 30;
    [SerializeField] LayerMask m_layerMask;
    Color m_defaultCrosshairColor;
    [SerializeField] Color m_targetedCrosshairColor = Color.red;
    [SerializeField] Transform m_muzzle = null;
    [SerializeField] LineRenderer m_laserRenderer = null;
    [SerializeField] AudioClip m_shootSfx = null;
    ConeController m_targetCone = null;

    void Start()
    {
        m_defaultCrosshairColor = m_crosshair.color;
        ResetLaser();
    }
        
    void Update()
    {
        ResetLaser();

        Ray ray = Camera.main.ScreenPointToRay(m_crosshair.rectTransform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, m_shootRange, m_layerMask))
        {
            ConeController cone = hit.collider.gameObject.GetComponent<ConeController>();
            m_targetCone = cone;
        }
        else
        {
            m_targetCone = null;
        }

        if (m_targetCone)
        {
            m_crosshair.color = m_targetedCrosshairColor;

            if (Input.GetButtonDown("Fire1"))
            {
                Shoot(hit.point);
            }
        }
        else if (m_crosshair.color == m_targetedCrosshairColor)
        {
            m_crosshair.color = m_defaultCrosshairColor;
            m_gunPivot.transform.forward = this.transform.forward;
        }
    }

    void Shoot(Vector3 hitPoint)
    {
        Vector3 dir = m_targetCone.transform.position - this.transform.position;
        dir.y = 0;
        m_gunPivot.transform.forward = dir;

        m_targetCone.Hit(m_attackPower);
        m_laserRenderer.SetPosition(0, m_muzzle.position);
        m_laserRenderer.SetPosition(1, hitPoint);

        if (m_shootSfx)
        {
            AudioSource.PlayClipAtPoint(m_shootSfx, m_muzzle.position);
        }
    }

    void ResetLaser()
    {
        m_laserRenderer.SetPosition(0, m_muzzle.position);
        m_laserRenderer.SetPosition(1, m_muzzle.position);
    }
}
