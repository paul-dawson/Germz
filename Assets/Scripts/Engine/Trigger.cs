using UnityEngine;

public class Trigger : MonoBehaviour
{
    protected Transform m_transform;

    public void Awake()
    {
        m_transform = this.gameObject.transform;
    }
}
