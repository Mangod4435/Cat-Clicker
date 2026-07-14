using API;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UpgradeButtons : MonoBehaviour
    {
        [SerializeField]
        string upgradeName;

        [SerializeField]
        int index;

        CanvasGroup mask;
        CanvasGroup cg;
        Image icon;
        double Cats => manager.Cats;

        public enum ButtonState
        {
            Available,
            NotAffordable,
            Shadow,
            Unrevealed,
        }

        ButtonState state = ButtonState.Unrevealed;
        ButtonState lastState = (ButtonState)(-1);
        private GameManager manager => GameManager.Instance;

        void Awake()
        {
            cg = gameObject.GetComponent<CanvasGroup>();
            mask = transform.GetChild(4).GetComponent<CanvasGroup>();
            icon = transform.GetChild(0).GetComponent<Image>();
        }

        void Update()
        {
            state = EvaluateState();
            ApplyRevealSideEffect(state);

            if (state != lastState)
            {
                ApplyState(state);
                lastState = state;
            }
        }

        private bool IsAffordable(string upgradeName)
        {
            switch (upgradeName)
            {
                case "Sharp Claw":
                    if (manager.Cats >= UpgradeAPI.PriceCalculator(100, manager.SharpClaw))
                        return true;
                    break;
                case "Cozy Spot":
                    if (manager.Cats >= UpgradeAPI.PriceCalculator(20, manager.CozySpot))
                        return true;
                    break;
                case "Fish Bowl":
                    if (manager.Cats >= UpgradeAPI.PriceCalculator(100, manager.FishBowl))
                        return true;
                    break;
                case "TV":
                    if (manager.Cats >= UpgradeAPI.PriceCalculator(1_000, manager.TV))
                        return true;
                    break;
                case "Laser":
                    if (manager.Cats >= UpgradeAPI.PriceCalculator(50_000, manager.Laser))
                        return true;
                    break;
                case "Factory":
                    if (manager.Cats >= UpgradeAPI.PriceCalculator(10_000_000, manager.Factory))
                        return true;
                    break;
                case "Satellite":
                    if (
                        manager.Cats >= UpgradeAPI.PriceCalculator(5_000_000_000, manager.Satellite)
                    )
                        return true;
                    break;
            }
            return false;
        }

        public void OnClicked()
        {
            if (!IsAffordable(upgradeName))
                return;
            manager.AddCat(-UpgradeAPI.getCalculatedPrice(upgradeName));
            manager.AddUpgrade(upgradeName, 1);
        }

        ButtonState EvaluateState()
        {
            double cats = Cats;
            double calcPrice = UpgradeAPI.getCalculatedPrice(upgradeName);
            double shadowThreshold = UpgradeAPI.getPrice(upgradeName) * 0.9;
            bool revealed = manager.getRevealed(upgradeName);

            if (cats >= calcPrice)
                return ButtonState.Available;

            if (revealed)
                return ButtonState.NotAffordable;

            if (cats >= shadowThreshold)
                return ButtonState.Shadow;

            return ButtonState.Unrevealed;
        }

        // Only place allowed to WRITE shared state, and only once condition truly met.
        void ApplyRevealSideEffect(ButtonState evaluated)
        {
            if (evaluated == ButtonState.Available && !manager.getRevealed(upgradeName))
                manager.setRevealed(upgradeName, true);
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
    }
}
