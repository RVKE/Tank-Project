using UnityEngine;
using UnityEngine.UI;

public class PurchaseCardDisplay : MonoBehaviour
{

    #region Variables

    public PurchaseCard purchaseCard;

    public Text nameText;

    public Text scrapMetalCostText;
    public Text coalCostText;
    public Text woodCostText;
    public Text unrefinedOilCostText;

    public Text boltCostText;
    public Text structureCostText;
    public Text mechanismCostText;
    public Text systemCostText;

    #endregion

    void Start()
    {
        if (nameText)
            nameText.text = purchaseCard.acquirableItem.ToString() + " " + purchaseCard.description;
        if (scrapMetalCostText)
            scrapMetalCostText.text = purchaseCard.scrapMetalCost.ToString() + " SCRAPMETAL";
        if (coalCostText)
            coalCostText.text = purchaseCard.coalCost.ToString() + " COAL";
        if (woodCostText)
            woodCostText.text = purchaseCard.woodCost.ToString() + " WOOD";
        if (unrefinedOilCostText)
            unrefinedOilCostText.text = purchaseCard.unrefinedOilCost.ToString() + " UNREF. OIL";
        if (boltCostText)
            boltCostText.text = purchaseCard.boltCost.ToString() + " BOLTS";
        if (structureCostText)
            structureCostText.text = purchaseCard.structureCost.ToString() + " STRUCTURES";
        if (mechanismCostText)
            mechanismCostText.text = purchaseCard.mechanismCost.ToString() + " MECHANISMS";
        if (systemCostText)
            systemCostText.text = purchaseCard.systemCost.ToString() + " SYSTEMS";
    }

    public void BuyItem()
    {
        GameManager.instance.MakePurchase(purchaseCard);
    }
}
