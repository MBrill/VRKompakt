using UnityEngine;

/// <summary>
/// Controller für das Prefab Command Control Cube.
/// </summary>
/// <remarks>
/// Die 26 Würfel werden mit drei Indices ijk benannt.
/// Dabei bezeichnt i die "Zeile", j die "spalte" pro Schicht,
/// und der Index k ist die Schicht.
///
/// Alle Indices laufen von 1 bis 3.
/// </remarks>
public class CCC : MonoBehaviour
{
    protected virtual void Awake()
    {
        if (gameObject.transform.childCount != 0) 
            foreach (Transform t in gameObject.transform)
            {
                if (t.gameObject.name == "Schicht1")
                    m_layer[0] = t.gameObject;
                if (t.gameObject.name == "Schicht2")
                    m_layer[1] = t.gameObject;
                if (t.gameObject.name == "Schicht3")
                    m_layer[2] = t.gameObject;
            }

        isCCCVisible = true;
        ActiveLayer = 2;
        ShowLayer2();
        //ShowLayer(ActiveLayer);
    }

    
    /// <summary>
    /// C3 anzeigen.
    /// </summary>
    /// <remarks>
    /// Als Default zeigen wir die mittlere Schicht an.
    /// </remarks>
    public void Show()
    {
        Debug.Log("Show");
        Debug.Log(m_isCCCVisible);

        Debug.Log(ActiveLayer);
        if (isCCCVisible)
            switch (ActiveLayer)
            {
                case 1:
                    ShowLayer1();
                    break;
                case 2:
                    ShowLayer2();
                    break;
                default:
                case 3:
                    ShowLayer3();
                    break;
            }
    }

    void ShowLayer(uint l)
    {
        if (l > 3)
            Debug.Log("Schichtindex zu groß in ShowLayer");
        ShowLayer(l);
    }


    public void ShowLayer1()
    {
        if (isCCCVisible)
        {
            m_layer[0].SetActive(true);
            m_layer[1].SetActive(false);
            m_layer[2].SetActive(false);           
        }
        else
        {
            m_layer[0].SetActive(false);
            m_layer[1].SetActive(false);
            m_layer[2].SetActive(false);           
        }
    }
    
    public void ShowLayer2()
    {
        if (isCCCVisible)
        {
            m_layer[0].SetActive(false);
            m_layer[1].SetActive(true);
            m_layer[2].SetActive(false);         
        }
        else
        {
            m_layer[0].SetActive(false);
            m_layer[1].SetActive(false);
            m_layer[2].SetActive(false);      
        }
    }
    
    public void ShowLayer3()
    {
        if (isCCCVisible)
        {
            m_layer[0].SetActive(false);
            m_layer[1].SetActive(false);
            m_layer[2].SetActive(true);         
        }
        else
        {
            m_layer[0].SetActive(false);
            m_layer[1].SetActive(false);
            m_layer[2].SetActive(false);      
        }
    }
/// <summary>
/// Ist CCC sichtbar in der Szene?
/// </summary>
    private bool m_isCCCVisible;
    public bool isCCCVisible
    {
        get => m_isCCCVisible;
        set => m_isCCCVisible = value;
    }

    private uint m_activeLayer;

    public uint ActiveLayer
    {
        get => m_activeLayer;
        set => m_activeLayer = ActiveLayer;
    }
    
    private GameObject[]  m_layer = new GameObject[3];
}
