using UnityEngine;

public class ResetButton : MonoBehaviour
{
    GameManager manager => GameManager.Instance;

    [SerializeField]
    GameObject cat;

    public void OnClicked()
    {
        manager.ResetGame();
        if (!gameObject.CompareTag("MainMenu"))
            cat.transform.rotation = Quaternion.Euler(0, 0, 0);
        UIState.state = UIState.OpenedInterface.None;
    }
}
