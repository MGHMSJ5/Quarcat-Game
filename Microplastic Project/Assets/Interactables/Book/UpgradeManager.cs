using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    private Color _buttonUpgradedColor; 
    [SerializeField]
    private Color _buttonUpgradeColor;
    [SerializeField]
    private TextMeshProUGUI _upgradeText;

    [Serializable]
    public class CostButtons
    {
        public GameObject buttons;
        public int costValue;
    }

    [SerializeField]
    private List<CostButtons> _upgradeButtons = new List<CostButtons>();

    public static int upgradePoints = 0;
    public static bool addedList = false;

    private void OnEnable()
    {
        if (!addedList)
        {
            print("add");
            ButtonValue.AddToList(_upgradeButtons.Count);
            addedList = true;
        }
        CheckPoints();
    }
    public void AddRemoveUpgradePoint(int points)
    {
        upgradePoints += points;
        _upgradeText.text = "Upgrade Points = " + upgradePoints.ToString();
    }

    public void ButtonUpgraded(GameObject go)
    {
        int index = 0;
        for (int i = 0; i < _upgradeButtons.Count; i++)
        {
            if (_upgradeButtons[i].buttons == go)
            {
                index = i;
                break;
            }
        }

        ButtonValue.SetUpgradeButton(index, true);
        AddRemoveUpgradePoint(-_upgradeButtons[index].costValue);

        CantUpgrade(go);
        HasUpgraded(go);

        CheckPoints();
    }

    private void CheckPoints()
    {
        for (int i = 0; i < _upgradeButtons.Count; i++)
        {
            if (ButtonValue.GetIfUpgraded(i))
            {
                HasUpgraded(_upgradeButtons[i].buttons);
                continue;
            }
            if (upgradePoints >= _upgradeButtons[i].costValue)
            {
                CanUpgrade(_upgradeButtons[i].buttons);
            }
            else
            {
                CantUpgrade(_upgradeButtons[i].buttons);
            }
        }
    }

    private void HasUpgraded(GameObject go)
    {
        CantUpgrade(go);
        TextMeshProUGUI text = go.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = "Upgraded!";
        go.transform.GetChild(1).gameObject.SetActive(false);
    }

    private void CanUpgrade(GameObject go)
    {
        Button button = go.GetComponent<Button>();
        button.enabled = true;
        Image image = button.GetComponent<Image>();
        image.color = _buttonUpgradeColor;
    }

    private void CantUpgrade(GameObject go)
    {
        Button button = go.GetComponent<Button>();
        button.enabled = false;
        Image image = button.GetComponent<Image>();
        image.color = _buttonUpgradedColor;
    }
}
