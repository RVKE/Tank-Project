using UnityEngine;
using UnityEngine.UI;

public class DataMenu : MonoBehaviour
{

    #region Variables

    [Header("References")]
    public Text scrapMetalAmountText;
    public Text woodAmountText;
    public Text coalAmountText;
    public Text unrefinedOilAmountText;

    public Text boltAmountText;
    public Text structureAmountText;
    public Text mechanismAmountText;
    public Text systemAmountText;

    #endregion

    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        scrapMetalAmountText.text = "SCRAPMETAL: " + GameManager.instance.scrapMetalAmount;
        woodAmountText.text = "WOOD: " + GameManager.instance.woodAmount;
        coalAmountText.text = "COAL: " + GameManager.instance.coalAmount;
        unrefinedOilAmountText.text = "UNREFINED OIL: " + GameManager.instance.unrefinedOilAmount;
        boltAmountText.text = "BOLTS: " + GameManager.instance.boltAmount;
        structureAmountText.text = "STRUCTURES: " + GameManager.instance.structureAmount;
        mechanismAmountText.text = "MECHANISMS: " + GameManager.instance.mechanismAmount;
        systemAmountText.text = "SYSTEMS: " + GameManager.instance.systemAmount;
    }
}
