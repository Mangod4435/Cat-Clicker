using UnityEngine;
using UnityEngine.UI;

// Attach this to the same GameObject as the Image using the UI/RoundedRect material.
// It creates a per-instance material (so multiple buttons don't share the same
// _Size value) and keeps _Size synced to the RectTransform's actual width/height.
[RequireComponent(typeof(RectTransform), typeof(Image))]
[ExecuteAlways]
public class RoundedUISync : MonoBehaviour
{
    private RectTransform rt;
    private Image img;
    private Material instanceMat;
    private Vector2 lastSize;
    private Vector2 lastPivot;

    private static readonly int SizeProp = Shader.PropertyToID("_Size");
    private static readonly int OffsetProp = Shader.PropertyToID("_Offset");

    void OnEnable()
    {
        rt = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        EnsureInstanceMaterial();
        SyncSize(force: true);
    }

    void EnsureInstanceMaterial()
    {
        if (img.material == null)
            return;

        // Avoid re-instancing an already-instanced material (name ends with "(Instance)")
        if (!img.material.name.EndsWith("(Instance)"))
        {
            instanceMat = new Material(img.material);
            instanceMat.name = img.material.name + " (Instance)";
            img.material = instanceMat;
        }
        else
        {
            instanceMat = img.material;
        }
    }

    void Update()
    {
        SyncSize(force: false);
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
        Vector2 pivot = rt.pivot;
        if (!force && size == lastSize && pivot == lastPivot)
            return;

        lastSize = size;
        lastPivot = pivot;

        img.material.SetVector(SizeProp, new Vector4(size.x, size.y, 0f, 0f));

        // Unity's mesh vertices are generated relative to the pivot, so a
        // pivot away from (0.5, 0.5) shifts the visual center away from
        // local (0,0). This offset corrects the shader's SDF center to match.
        float offsetX = (0.5f - pivot.x) * size.x;
        float offsetY = (0.5f - pivot.y) * size.y;
        img.material.SetVector(OffsetProp, new Vector4(offsetX, offsetY, 0f, 0f));
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
