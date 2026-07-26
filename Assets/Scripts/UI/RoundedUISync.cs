using UnityEngine;
using UnityEngine.UI;

// Attach this to the same GameObject as the Image using the UI/RoundedRect material.
// It creates a per-instance material (so multiple buttons don't share the same
// _Size value) and keeps _Size synced to the RectTransform's actual width/height.
[RequireComponent(typeof(RectTransform), typeof(Image)), ExecuteAlways]
public class RoundedUISync : MonoBehaviour
{
    [SerializeField, Range(0, 100)]
    float cornerRadius;
    private RectTransform rt;
    private Image img;
    private Material instanceMat;
    private Vector2 lastSize;
    private Vector2 lastPivot;

    private static readonly int SizeProp = Shader.PropertyToID("_Size");
    private static readonly int OffsetProp = Shader.PropertyToID("_PivotOffset");
    private static readonly int CornerProp = Shader.PropertyToID("_Radius");

    void OnEnable()
    {
        rt = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        EnsureInstanceMaterial();
        SyncSize(force: true);
        img.material.SetFloat(CornerProp, cornerRadius);
    }

    void EnsureInstanceMaterial()
    {
        // Avoid re-instancing an already-instanced material (name ends with "(Instance)")
        if (!img.material.name.EndsWith("(Instance)"))
        {
            instanceMat = new Material(img.material)
            {
                name = img.material.name + " (Instance)",
                shader = Shader.Find("UI/RoundedRect"),
            };
            img.material = instanceMat;
        }
        else
            instanceMat = img.material;
    }

    void Update()
    {
        SyncSize(force: false);
        img.material.SetFloat(CornerProp, cornerRadius);
    }

    // Also catches resizes from layout groups / anchors without waiting a frame
    void OnRectTransformDimensionsChange()
    {
        if (rt == null)
            rt = (RectTransform)transform;
        SyncSize(force: true);
    }

    void SyncSize(bool force)
    {
        if (rt == null || img == null || img.material == null)
            return;

        Vector2 size = rt.rect.size;

        // Each element gets its own instanced material (see EnsureInstanceMaterial),
        // so UGUI never batches these Graphics together -- meaning v.vertex in the
        // shader really is this object's own local mesh space, already symmetric
        // around the pivot-adjusted rect. The ONLY correction ever needed is for
        // non-centered pivots, which is exactly rect.center. No hierarchy walk,
        // no canvas space involved.
        Vector2 pivotOffset = rt.rect.center;

        if (!force && size == lastSize && pivotOffset == lastPivot)
            return;

        lastSize = size;
        lastPivot = pivotOffset;

        img.material.SetVector(SizeProp, new Vector4(size.x, size.y, 0f, 0f));
        img.material.SetVector(OffsetProp, (Vector4)pivotOffset);
    }

    void OnDestroy()
    {
        // Clean up the instanced material to avoid leaks when the object is destroyed
        if (instanceMat != null)
        {
            if (Application.isPlaying)
                Destroy(instanceMat);
            else
                DestroyImmediate(instanceMat);
        }
    }
}
