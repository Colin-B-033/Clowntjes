using UnityEngine;
using UnityEngine.UI;

public class SlowUIController : MonoBehaviour
{
    public Slider slider;
    public Image sliderFillImage;
    public Image sliderBackgroundImage;
    public Image slowmogradient;

    [Header("Slider Alpha")]
    public float sliderInactiveAlpha = 0.3f;
    public float sliderActiveAlpha = 1f;

    [Header("Gradient Fade")]
    public float slowmoGradientActiveAlpha = 0.8f;
    public float slowmoGradientFadeSpeed = 2f;

    private float targetGradientAlpha = 0f;

    public void UpdateSlider(float value, float maxValue, bool isSlowMo)
    {
        if (slider != null)
        {
            slider.maxValue = maxValue;
            slider.value = value;
        }
        if (sliderFillImage != null)
        {
            Color c = sliderFillImage.color;
            c.a = isSlowMo ? sliderActiveAlpha : sliderInactiveAlpha;
            sliderFillImage.color = c;
        }
        if (sliderBackgroundImage != null)
        {
            Color bgColor = sliderBackgroundImage.color;
            bgColor.a = isSlowMo ? sliderActiveAlpha : sliderInactiveAlpha;
            sliderBackgroundImage.color = bgColor;
        }
    }

    public void SetGradientTarget(bool isSlowMo)
    {
        targetGradientAlpha = isSlowMo ? slowmoGradientActiveAlpha : 0f;
    }

    private void Update()
    {
        if (slowmogradient != null)
        {
            Color color = slowmogradient.color;
            color.a = Mathf.MoveTowards(color.a, targetGradientAlpha, slowmoGradientFadeSpeed * Time.unscaledDeltaTime);
            slowmogradient.color = color;
        }
    }
}