using UnityEngine;

public class UtilityMenu : MonoBehaviour
{
    #region Variables

    [Header("UI GameObjects")]
    public GameObject craftMenuObject;
    public GameObject upgradeMenuObject;
    public GameObject replenishMenuObject;
    public GameObject utilityMenuObject;

    #endregion


    public void GoToCraftMenu()
    {
        utilityMenuObject.SetActive(false);
        craftMenuObject.SetActive(true);
    }

    public void GoToUpgradeMenu()
    {
        utilityMenuObject.SetActive(false);
        upgradeMenuObject.SetActive(true);
    }

    public void GoToReplenishMenu()
    {
        utilityMenuObject.SetActive(false);
        replenishMenuObject.SetActive(true);
    }

    public void GoToUtilityMenu()
    {
        if (craftMenuObject.activeInHierarchy)
            craftMenuObject.SetActive(false);
        if (upgradeMenuObject.activeInHierarchy)
            upgradeMenuObject.SetActive(false);
        if (replenishMenuObject.activeInHierarchy)
            replenishMenuObject.SetActive(false);

        utilityMenuObject.SetActive(true);
    }
}
