using API;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UNIVERSALButton : MonoBehaviour
{
    #region serialize field
    [SerializeField]
    string buttonName;

    [SerializeField]
    GameObject menu;

    [SerializeField]
    GameObject cat;

    [SerializeField]
    GameObject setting;

    [SerializeField]
    string upgradeName;

    [SerializeField]
    int index;
    #endregion
    #region private field
    GameManager gameManager => GameManager.Instance;
    SettingManager settingManager => SettingManager.Instance;
    PressedEvent e;
    AudioSource meow;
    CanvasGroup mask;
    CanvasGroup cg;
    Image icon;
    bool settingOpenning;
    ButtonState state = ButtonState.Unrevealed;
    ButtonState lastState = (ButtonState)(-1);

    public enum ButtonState
    {
        Available,
        NotAffordable,
        Shadow,
        Unrevealed,
    }
    #endregion

    void Awake()
    {
        switch (buttonName)
        {
            case "cat":
                e = GetComponent<PressedEvent>();
                meow = GetComponent<AudioSource>();
                break;
            case "upgrade":
                cg = gameObject.GetComponent<CanvasGroup>();
                mask = transform.GetChild(4).GetComponent<CanvasGroup>();
                icon = transform.GetChild(0).GetComponent<Image>();
                break;
        }
    }

    void FixedUpdate()
    {
        switch (buttonName)
        {
            case "cat":
                transform.Rotate(0, 0, -22.5f * Time.fixedDeltaTime);
                break;
        }
    }

    void Update()
    {
        switch (buttonName)
        {
            case "cat":
                transform.localScale = e.holding ? Vector3.one * 0.8f : Vector3.one;
                meow.volume = settingManager.Sound ? 1 : 0;
                break;
            case "upgradeMenu":
                bool isOpen = UIState.state == UIState.OpenedInterface.Upgrade;
                gameObject.GetComponent<RectTransform>().anchoredPosition = isOpen
                    ? new Vector3(-960, 0)
                    : Vector3.zero;
                cat.GetComponent<RectTransform>().anchoredPosition = isOpen
                    ? new Vector3(-410, 0)
                    : new Vector3(0, 0);
                bool isSettingOpen = UIState.state == UIState.OpenedInterface.Setting;
                gameObject.GetComponent<Image>().color = isSettingOpen ? Color.white : Color.black;
                break;
            case "setting":
                bool isSettingOpen2 = UIState.state == UIState.OpenedInterface.Setting;
                gameObject.GetComponent<Image>().color = isSettingOpen2 ? Color.white : Color.black;
                break;
            case "SAVE":
                bool isSettingOpen3 = UIState.state == UIState.OpenedInterface.Setting;
                gameObject.GetComponent<Image>().color = isSettingOpen3 ? Color.white : Color.black;
                break;
            case "upgrade":
                state = EvaluateState();
                ApplyRevealSideEffect(state);

                if (state != lastState)
                {
                    ApplyState(state);
                    lastState = state;
                }
                break;
        }
    }

    public void OnClicked()
    {
        switch (buttonName)
        {
            #region setting
            case "reset":
                gameManager.ResetGame();
                if (!gameObject.CompareTag("MainMenu"))
                    cat.transform.rotation = Quaternion.Euler(0, 0, 0);
                UIState.state = UIState.OpenedInterface.None;
                break;
            case "setting":
                UIState.state =
                    UIState.state == UIState.OpenedInterface.Setting
                        ? UIState.OpenedInterface.None
                        : UIState.OpenedInterface.Setting;
                break;
            #endregion
            #region upgrade
            case "upgrade":
                if (!IsAffordable())
                    return;
                gameManager.AddCat(-UpgradeAPI.getCalculatedPrice(upgradeName));
                gameManager.AddUpgrade(upgradeName, 1);
                break;
            case "upgradeMenu":
                UIState.state =
                    UIState.state == UIState.OpenedInterface.Upgrade
                        ? UIState.OpenedInterface.None
                        : UIState.OpenedInterface.Upgrade;
                break;
            #endregion
            #region main menu buttons
            case "start":
                SceneManager.LoadScene(1);
                settingOpenning = false;
                break;
            case "msetting":
                setting.SetActive(!settingOpenning);
                settingOpenning = !settingOpenning;
                break;
            case "quit":
                Application.Quit();
                break;
            #endregion

            case "SAVE":
                gameManager.SaveGame();
                break;
            case "cat":
                gameManager.AddCat(1 * gameManager.CPC);
                meow.Play();
                break;
            default:
                Debug.LogError($"No such button named \"{buttonName}\"");
                break;
        }
    }

    #region upgrade button helper
    bool IsAffordable()
    {
        switch (upgradeName)
        {
            case "Sharp Claw":
                return gameManager.Cats >= UpgradeAPI.PriceCalculator(100, gameManager.SharpClaw);
            case "Cozy Spot":
                return gameManager.Cats >= UpgradeAPI.PriceCalculator(20, gameManager.CozySpot);
            case "Fish Bowl":
                return gameManager.Cats >= UpgradeAPI.PriceCalculator(100, gameManager.FishBowl);
            case "TV":
                return gameManager.Cats >= UpgradeAPI.PriceCalculator(1_000, gameManager.TV);
            case "Laser":
                return gameManager.Cats >= UpgradeAPI.PriceCalculator(50_000, gameManager.Laser);
            case "Factory":
                return gameManager.Cats
                    >= UpgradeAPI.PriceCalculator(10_000_000, gameManager.Factory);
            case "Satellite":
                return gameManager.Cats
                    >= UpgradeAPI.PriceCalculator(5_000_000_000, gameManager.Satellite);
            default:
                Debug.LogError($"That upgrade not found ({upgradeName})");
                return false;
        }
    }

    ButtonState EvaluateState()
    {
        double cats = gameManager.Cats;
        double calcPrice = UpgradeAPI.getCalculatedPrice(upgradeName);
        double shadowThreshold = UpgradeAPI.getPrice(upgradeName) * 0.5;
        bool revealed = gameManager.getRevealed(upgradeName);

        if (cats >= calcPrice)
            return ButtonState.Available;

        if (revealed)
            return ButtonState.NotAffordable;

        if (cats >= shadowThreshold)
            return ButtonState.Shadow;

        return ButtonState.Unrevealed;
    }

    void ApplyRevealSideEffect(ButtonState evaluated)
    {
        if (evaluated == ButtonState.Available && !gameManager.getRevealed(upgradeName))
            gameManager.setRevealed(upgradeName, true);
    }

    void ApplyState(ButtonState s)
    {
        switch (s)
        {
            case ButtonState.Available:
                cg.alpha = 1;
                mask.alpha = 0;
                cg.interactable = true;
                icon.color = Color.white;
                transform.SetSiblingIndex(index);
                break;
            case ButtonState.NotAffordable:
                cg.alpha = 1;
                mask.alpha = 1;
                cg.interactable = false;
                icon.color = Color.white;
                transform.SetSiblingIndex(index);
                break;
            case ButtonState.Shadow:
                cg.alpha = 1;
                mask.alpha = 1;
                cg.interactable = false;
                icon.color = Color.black;
                transform.SetSiblingIndex(index);
                break;
            case ButtonState.Unrevealed:
                cg.alpha = 0;
                cg.interactable = false;
                transform.SetSiblingIndex(-1);
                break;
        }
    }
    #endregion
}
