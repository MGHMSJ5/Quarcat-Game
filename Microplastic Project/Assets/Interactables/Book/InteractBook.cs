using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicInteractLayout))] //ALSO NEEDED
public class InteractBook : MonoBehaviour
{
    [SerializeField]
    private GameObject _canvasBook;
    //NEEDED INTERACT LAYOUT for every interact script
    private BasicInteractLayout _basicInteractLayout;

    void Awake()
    {
        _basicInteractLayout = GetComponent<BasicInteractLayout>();
        _basicInteractLayout._interactAction += OpenBook; //Depends on function name
    }
    //END NEEDED INTERACT LAYOUT

    //unique function/interaction based on script/object
    void OpenBook()
    {
        _canvasBook.SetActive(true);

        //interact code...


        //_basicInteractLayout.ExitANDNotInteract(true); //in case you want to only interact once
    }
}
