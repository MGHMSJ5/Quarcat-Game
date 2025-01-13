using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("For Upgrade References")]
    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    private Catching _catching;
    [Header("Upgrade vairables")]
    [Tooltip("The order goes by Beadie version. So index 0 = Beadie V1. Index 1 = Beadie V2, etc")]
    [SerializeField]
    private List<float> _newBeadieSlowedSpeed = new List<float>();
    private float _newCatchSpeed = 2f;
    private float _newSpeed = 8;
    private float _newTurnSpeed = 1000;
    private float _newInvulnerabilityTime = 3.5f;

    [Header("Book References")]
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
        //add to the list only once
        if (!addedList)
        {
            ButtonValue.AddToList(_upgradeButtons.Count);
            addedList = true;
        }
        CheckPoints();
    }
    //UPGRADES START
    public void VacuumPower()
    {
        //values to change: catchSpeed in Catching, slowed speed in microplastic prefabs in BeadieCatchingManager
        _catching.CatchSpeed = _newCatchSpeed;
        for (int i = 0; i < _newBeadieSlowedSpeed.Count; i++)
        {
            BeadieStaticManager.SetSlowedSpeed(i, _newBeadieSlowedSpeed[i]);
        }
        
    }

    public void PlayerSpeed()
    {
        //values to change: player speed + turnspeed
        _playerController.speed = _newSpeed;
        _playerController.turnspeed = _newTurnSpeed;
    }

    public void PlayerInvulnerability()
    {
        //values to change: variable in Catching script in the Enumerator in the bottom
        _catching.InvulnerabilityTime = _newInvulnerabilityTime;
    }

    //UPGRADES END
    //remove/add upgrade points
    public void AddRemoveUpgradePoint(int points)
    {
        upgradePoints += points;
        _upgradeText.text = "Upgrade Points = " + upgradePoints.ToString();
    }
    
    //will be called if you want to upgrade
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

        HasUpgraded(go);

        CheckPoints();
    }
    //compares the upgradepoints and sees what you can upgrade
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
    //changes to button if already upgraded
    private void HasUpgraded(GameObject go)
    {
        CantUpgrade(go);
        TextMeshProUGUI text = go.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = "Upgraded!";
        go.transform.GetChild(1).gameObject.SetActive(false);
    }
    //changes to button if you can upgrade
    private void CanUpgrade(GameObject go)
    {
        Button button = go.GetComponent<Button>();
        button.enabled = true;
        Image image = button.GetComponent<Image>();
        image.color = _buttonUpgradeColor;
    }
    //changes to button if you can't upgrade
    private void CantUpgrade(GameObject go)
    {
        Button button = go.GetComponent<Button>();
        button.enabled = false;
        Image image = button.GetComponent<Image>();
        image.color = _buttonUpgradedColor;
    }
}
