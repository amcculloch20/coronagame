using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskShieldMesh : MonoBehaviour
{
    public delegate void DestroyMaskDelegate();

    public DestroyMaskDelegate destroyDelegate;

    void DestroyMask()
    {
        destroyDelegate();
    }
}
