using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject CreditPanel;


    public void ShowCreditPanel()
    {
        CreditPanel.SetActive(true);
    }
    public void HideCreditPanel() {
        CreditPanel.SetActive(false);
    }
    public void ClickOnCreditButton()
    {
        if (CreditPanel.activeSelf)
        {
            HideCreditPanel();
        }
        else
        {
            ShowCreditPanel();
        }
    }

}
