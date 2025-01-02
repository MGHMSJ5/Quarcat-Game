using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnowledgeManager : MonoBehaviour
{
    [Serializable]
    public class ButtonChanges
    {
        public TextMeshProUGUI buttonText;
        public string name;
        public float size;
    }
    [Header("Replacing")]
    [SerializeField]
    private List<ButtonChanges> _buttonChanges = new List<ButtonChanges>();

    [SerializeField]
    private List<GameObject> _entriesWInformation = new List<GameObject>();

    private BookContentButtons _knowledgeBookContentButtons;

    private void Awake()
    {
        _knowledgeBookContentButtons = GetComponent<BookContentButtons>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _entriesWInformation.Count; i++)
        {
            ChangeEntryIfReplaced(i + 1); //add + 1 so that it matches with the Microplastic id
            ChangeButtonName(i + 1);
        }
    }
    private void ChangeEntryIfReplaced(int id)
    {
        if (SpawnBoolManager.GetHasReplaced(id))
        {
            _knowledgeBookContentButtons._informationObjects[id - 1].SetActive(false); //deactivate Entry X just to be sure
            _knowledgeBookContentButtons._informationObjects[id - 1] = _entriesWInformation[id - 1];
        }
    }

    private void ChangeButtonName(int id)
    {
        if (SpawnBoolManager.GetHasReplaced(id))
        {   //change the text name, and size
            TextMeshProUGUI buttonText = _buttonChanges[id - 1].buttonText;
            buttonText.text = _buttonChanges[id - 1].name;
            buttonText.fontSize = _buttonChanges[id - 1].size;
        }
    }
}
