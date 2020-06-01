using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class GunMenuController : MonoBehaviour
{
    public Image fill;
    public Image background;

    public TMP_Text text;

    public float fillAmount;
    public float smoothSpeed;

    private Color _color;

    public Color GunColor
    {
        get => _color;
        set => UpdateColor(value);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set to default
        _color = fill.color;
    }

    // Update is called once per frame
    void Update()
    {
        fill.fillAmount = Mathf.Lerp(fill.fillAmount,fillAmount, smoothSpeed * Time.deltaTime);
    }

    private void UpdateColor(Color color)
    {
        fill.color = color;
        Color.RGBToHSV(color, out float h, out float s, out float v);
        v /= 2;
        
        Color darker = Color.HSVToRGB(h, s, v);
        darker.a = 0.8f;

        background.color = darker;

        text.color = color;
        text.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, darker);
    }
}
