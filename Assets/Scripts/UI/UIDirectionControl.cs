using UnityEngine;

public class UIDirectionControl : MonoBehaviour
{
    public bool m_UseRelativeRotation = true;

    private Quaternion m_RelativeRotation;
    private Transform m_transform;

    private void Start()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_RelativeRotation = m_transform.parent.localRotation;
    }


    private void Update()
    {
        if (m_UseRelativeRotation)
            m_transform.rotation = m_RelativeRotation;
    }
}
