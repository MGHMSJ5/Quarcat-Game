using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    private Color _buttonUpgradedColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonUpgraded(GameObject go)
    {
        Button button = go.GetComponent<Button>();
        button.enabled = false;
        Image image = button.GetComponent<Image>();
        image.color = _buttonUpgradedColor;

        Transform parent = go.transform;
        TextMeshProUGUI text = parent.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = "Upgraded!";

        parent.GetChild(1).gameObject.SetActive(false);
    }
}
