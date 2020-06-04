using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformChildScaler : MonoBehaviour
{
    public RectTransform parent;

    private RectTransform _parent;
    private Vector2 _initialRatio;
    private float _initialZ;
    
    // Start is called before the first frame update
    void Start()
    {
        if (parent)
        {
            _parent = parent;
        }
        else
        {
            _parent = transform.parent.GetComponent<RectTransform>();
        }

        _initialRatio = transform.localScale;
        _initialZ = transform.localScale.z;
        _initialRatio.Scale(new Vector2(1 / _parent.rect.width, 1 / _parent.rect.height));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 size = _initialRatio;
        size.Scale(_parent.rect.size);
        
        Vector3 newScale = new Vector3(size.x, size.y, _initialZ);

        transform.localScale = newScale;
    }
}
