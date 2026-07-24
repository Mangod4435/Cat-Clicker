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

    private static readonly int SizeProp = Shader.PropertyToID("_Size");

    void OnEnable()
    {
        rt = (RectTransform)transform;
        img = GetComponent<Image>();
        EnsureInstanceMaterial();
        SyncSize(force: true);
    }

    void EnsureInstanceMaterial()
    {
        if (img.material == null) return;

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
        if (rt == null) rt = (RectTransform)transform;
        SyncSize(force: true);
    }

    void SyncSize(bool force)
    {
        if (rt == null || img == null || img.material == null) return;

        Vector2 size = rt.rect.size;
        if (!force && size == lastSize) return;

        lastSize = size;
        img.material.SetVector(SizeProp, new Vector4(size.x, size.y, 0f, 0f));
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
