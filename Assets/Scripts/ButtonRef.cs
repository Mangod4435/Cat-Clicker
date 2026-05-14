using UnityEngine;

[CreateAssetMenu(fileName = "ButtonRef", menuName = "Scriptable Objects/ButtonRef")]
public class ButtonRef : ScriptableObject
{
    public void NikoClicked()
    {
        Debug.Log("+1$");
    }
}
