using UnityEngine;

public class ResetButton : MonoBehaviour
{
    GameManager manager => GameManager.instance;

    public void OnClicked()
    {
        manager.ResetGame();
    }
}
