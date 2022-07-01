using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScreenController : MonoBehaviour
{
    public GameObject CreditPanel;

    public void OnCreditButtonClicked()
    {
        CreditPanel.SetActive(true);
    }

    public void OnCloseCreditButtonClicked()
    {
        CreditPanel.SetActive(false);
    }
}
