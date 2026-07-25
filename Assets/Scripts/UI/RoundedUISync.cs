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
    private RectTransform canvasRect;

    private static readonly int SizeProp = Shader.PropertyToID("_Size");
    private static readonly int OffsetProp = Shader.PropertyToID("_Offset");
    private static readonly int CornerProp = Shader.PropertyToID("_Radius");

    void OnEnable()
    {
        rt = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        var canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas != null ? canvas.rootCanvas.transform as RectTransform : null;
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

        // UGUI batches same-material Graphics into one draw call, which means
        // the vertex positions the shader receives are pre-baked into CANVAS
        // space, not this object's own local mesh space. So _Offset has to be
        // this rect's visual center expressed in canvas-local coordinates,
        // walking the FULL hierarchy up to the Canvas -- not just rect.center
        // (pivot-only, ignores position) and not just localPosition
        // (only accounts for the immediate parent, not grandparents).
        Vector2 offset;
        if (canvasRect != null)
        {
            Vector3 worldOrigin = rt.TransformPoint(rt.rect.center);
            offset = canvasRect.InverseTransformPoint(worldOrigin);
        }
        else
        {
            offset = rt.rect.center;
        }

        if (!force && size == lastSize && offset == lastPivot)
            return;

        lastSize = size;
        lastPivot = offset;

        img.material.SetVector(SizeProp, new Vector4(size.x, size.y, 0f, 0f));
        img.material.SetVector(OffsetProp, (Vector4)offset);
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
