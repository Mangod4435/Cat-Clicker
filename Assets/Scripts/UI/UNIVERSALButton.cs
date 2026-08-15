using API;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UNIVERSALButton : MonoBehaviour
{
    #region enum
    public enum ButtonState
    {
        Available,
        NotAffordable,
        Shadow,
        Unrevealed,
    }
    public enum ButtonType
    {
        reset,
        cat,
        setting,
        save,
        upgradeMenu,
        upgrade,
        quit,
        start,
        mainMenuSetting
    }
    #endregion
    #region serialize field
    [SerializeField] ButtonType buttonName;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject cat;
    [SerializeField] GameObject setting;
    [SerializeField] string upgradeName;
    [SerializeField] int index;
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
    #endregion

    void Awake()
    {
        switch (buttonName)
        {
            case ButtonType.cat:
                e = GetComponent<PressedEvent>();
                meow = GetComponent<AudioSource>();
                break;
            case ButtonType.upgrade:
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
            case ButtonType.cat:
                transform.Rotate(0, 0, -22.5f * Time.fixedDeltaTime);
                break;
        }
    }

    void Update()
    {
        switch (buttonName)
        {
            case ButtonType.cat:
                transform.localScale = e.holding ? Vector3.one * 0.8f : Vector3.one;
                meow.volume = settingManager.Sound ? 1 : 0;
                break;
            case ButtonType.upgradeMenu:
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
            case ButtonType.setting:
                bool isSettingOpen2 = UIState.state == UIState.OpenedInterface.Setting;
                gameObject.GetComponent<Image>().color = isSettingOpen2 ? Color.white : Color.black;
                break;
            case ButtonType.save:
                bool isSettingOpen3 = UIState.state == UIState.OpenedInterface.Setting;
                gameObject.GetComponent<Image>().color = isSettingOpen3 ? Color.white : Color.black;
                break;
            case ButtonType.upgrade:
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
            case ButtonType.reset:
                gameManager.ResetGame();
                if (!gameObject.CompareTag("MainMenu"))
                    cat.transform.rotation = Quaternion.Euler(0, 0, 0);
                UIState.state = UIState.OpenedInterface.None;
                break;
            case ButtonType.setting:
                UIState.state =
                    UIState.state == UIState.OpenedInterface.Setting
                        ? UIState.OpenedInterface.None
                        : UIState.OpenedInterface.Setting;
                break;
            #endregion
            #region upgrade
            case ButtonType.upgrade:
                if (!IsAffordable())
                    return;
                gameManager.AddCat(-UpgradeAPI.getCalculatedPrice(upgradeName));
                gameManager.AddUpgrade(upgradeName, 1);
                break;
            case ButtonType.upgradeMenu:
                UIState.state =
                    UIState.state == UIState.OpenedInterface.Upgrade
                        ? UIState.OpenedInterface.None
                        : UIState.OpenedInterface.Upgrade;
                break;
            #endregion
            #region main menu buttons
            case ButtonType.start:
                SceneManager.LoadScene(1);
                settingOpenning = false;
                break;
            case ButtonType.mainMenuSetting:
                setting.SetActive(!settingOpenning);
                settingOpenning = !settingOpenning;
                break;
            case ButtonType.quit:
                Application.Quit();
                break;
            #endregion

            case ButtonType.save:
                gameManager.SaveGame();
                break;
            case ButtonType.cat:
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
