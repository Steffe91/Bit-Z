using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackgrounds : MonoBehaviour
{
    private Transform m_CameraTransform;
    private Transform[] Layers;
    private float m_ViewZone = 10;
    private int m_LeftIndex;
    private int m_RightIndex;

    public float m_BackgroundSize;

    private void Start()
    {
        m_CameraTransform = Camera.main.transform;
        Layers = new Transform[transform.childCount];
        
        for(int i = 0; i < transform.childCount; i++)
        {
            Layers[i] = transform.GetChild(i);
        }

        m_LeftIndex = 0;
        m_RightIndex = Layers.Length - 1;

    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            ScrollLeft();
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            ScrollRigth();
        }
    }

    private void ScrollLeft()
    {
        int lastRight = m_RightIndex;
        Layers[m_RightIndex].position = Vector3.right * (Layers[m_LeftIndex].position.x - m_BackgroundSize);

        m_LeftIndex = m_RightIndex;
        m_RightIndex--;

        if(m_RightIndex < 0)
        {
            m_RightIndex = Layers.Length - 1;
        }

    }

    private void ScrollRigth()
    {
        int lastLeft = m_LeftIndex;
        Layers[m_LeftIndex].position = Vector3.right * (Layers[m_RightIndex].position.x + m_BackgroundSize);

        m_RightIndex = m_LeftIndex;
        m_LeftIndex++;

        if (m_LeftIndex == Layers.Length)
        {
            m_LeftIndex = 0;
        }
    }
}
