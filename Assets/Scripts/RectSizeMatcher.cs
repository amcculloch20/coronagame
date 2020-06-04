using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RectSizeMatcher : MonoBehaviour
{
    public RectTransform dependant;

    public bool matchWidth;
    public bool matchHeight;

    private Rect _rect;
    private RectTransform _rectTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rect = _rectTransform.rect;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 size = _rectTransform.sizeDelta;
        
        if (matchHeight) size.y = 100 * dependant.rect.height / _rect.height;
        if (matchWidth) size.x = 100 * dependant.rect.width / _rect.width;

        _rectTransform.sizeDelta = size;
    }
}
