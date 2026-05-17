using UnityEngine;

[CreateAssetMenu(fileName = "ButtonRef", menuName = "Scriptable Objects/ButtonRef")]
public class ButtonRef : ScriptableObject
{
    [SerializeField]
    PlayerStats stat;

    public void NikoClicked()
    {
        stat.cats += 1;
    }
}
