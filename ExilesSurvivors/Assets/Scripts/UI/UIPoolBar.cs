using UnityEngine;
using UnityEngine.UI;

public class UIPoolBar : MonoBehaviour
{
    public Image bar; // Reference to the Image component for the health bar fill
    public ValuePool targetPool;

    private void Update()
    {
        // Check if the targetPool or its maxValue is null
        if (targetPool == null || targetPool.maxValue == null)
        {
            Debug.LogWarning("TargetPool or maxValue is null!");
            return;
        }

        // Ensure the bar reference is valid
        if (bar == null)
        {
            Debug.LogError("Bar Image is not assigned in UIPoolBar!");
            return;
        }

        // Update the health bar fill amount
        bar.fillAmount = (float)targetPool.currentValue / (float)targetPool.maxValue.value;
    }

    public void Show(ValuePool targetPool)
    {
        this.targetPool = targetPool;
    }

    public void Clear()
    {
        this.targetPool = null;
    }
}