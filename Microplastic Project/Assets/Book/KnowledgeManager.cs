using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnowledgeManager : MonoBehaviour
{
    [Serializable]
    public class EntriesAndButtons
    {
        public int id;
        public GameObject information;
        public TextMeshProUGUI buttonText;
        public string name;
        public float size;
    }

    [SerializeField]
    private List<EntriesAndButtons> _entriesAndButtons = new List<EntriesAndButtons>();

    private BookContentButtons _knowledgeBookContentButtons;

    private void Awake()
    {
        _knowledgeBookContentButtons = GetComponent<BookContentButtons>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _entriesAndButtons.Count; i++)
        {
            ChangeEntryIfReplaced(_entriesAndButtons[i].id, i); //add + 1 so that it matches with the Microplastic id
            ChangeButtonName(_entriesAndButtons[i].id, i);
        }
    }
    private void ChangeEntryIfReplaced(int id, int index)
    {
        if (SpawnBoolManager.GetHasReplaced(id) || NotesManager.CheckContainsId(id))
        {
            _knowledgeBookContentButtons._informationObjects[index].SetActive(false); //deactivate Entry X just to be sure
            _knowledgeBookContentButtons._informationObjects[index] = _entriesAndButtons[index].information;
        }
    }

    private void ChangeButtonName(int id, int index)
    {
        if (SpawnBoolManager.GetHasReplaced(id) || NotesManager.CheckContainsId(id))
        {   //change the text name, and size
            TextMeshProUGUI buttonText = _entriesAndButtons[index].buttonText;
            buttonText.text = _entriesAndButtons[index].name;
            buttonText.fontSize = _entriesAndButtons[index].size;
        }
    }
}
