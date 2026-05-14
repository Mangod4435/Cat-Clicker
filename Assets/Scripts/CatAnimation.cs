using UnityEngine;

public class NikoAnimation : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(0, 0, -22.5f * Time.fixedDeltaTime);
    }
}
