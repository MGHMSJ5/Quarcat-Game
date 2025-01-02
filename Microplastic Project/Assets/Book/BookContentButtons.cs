using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookContentButtons : MonoBehaviour
{
    [SerializeField]
    private List<Image> _contentButtons = new List<Image>();
    [SerializeField]
    private Color _notActiveColor;
    [SerializeField]
    private Color _activeColor;
    public List<GameObject> _informationObjects = new List<GameObject>();

    [SerializeField]
    private Button _enableActive; //put in which part of the book will be active

    private void OnEnable()
    {
        _enableActive.onClick.Invoke();
    }

    public void GreyOutExcept(Image im)
    {
        for (int i = 0; i < _contentButtons.Count; i++)
        {
            if (im != _contentButtons[i])
            {
                _contentButtons[i].color = _notActiveColor;
            }
            else
            {
                _contentButtons[i].color= _activeColor;
            }
        }
    }

    public void DeactivateExcept(int index)
    {
        for (int i = 0; i < _informationObjects.Count; i++)
        {
            if ((index) != i)
            {
                _informationObjects[i].SetActive(false);
            }
            else
            {
                _informationObjects[i].SetActive(true);
            }
        }
    }
}
