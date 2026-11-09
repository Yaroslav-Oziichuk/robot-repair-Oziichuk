using UnityEngine;
using UnityEngine.UIElements;
public class UIHandler : MonoBehaviour
{
    private VisualElement m_Healthbar;
    public static UIHandler instance { get; private set; }
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement
        .Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);
    }
    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }
    private void Awake()
    {
        instance = this;
    }
}
