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

    public void CraftBolt()
    {
        var gameManager = GameManager.instance;
        if (gameManager.scrapMetalAmount - 4 >= 0)
        {
            gameManager.boltAmount += 1;
            gameManager.scrapMetalAmount -= 4;
        }
    }

    public void CraftStructure()
    {
        var gameManager = GameManager.instance;
        if (gameManager.scrapMetalAmount - 12 >= 0)
        {
            gameManager.structureAmount += 1;
            gameManager.scrapMetalAmount -= 12;
        }
    }

    public void CraftMechanism()
    {
        var gameManager = GameManager.instance;
        if (gameManager.scrapMetalAmount - 36 >= 0)
        {
            gameManager.mechanismAmount += 1;
            gameManager.scrapMetalAmount -= 36;
        }
    }

    public void CraftSystem()
    {
        var gameManager = GameManager.instance;
        if (gameManager.scrapMetalAmount - 108 >= 0)
        {
            gameManager.systemAmount += 1;
            gameManager.scrapMetalAmount -= 108;
        }
    }

    public void Upgrade()
    {
        
    }
}
