using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicInteractLayout))] //ALSO NEEDED
public class InteractNotes : MonoBehaviour
{
    //NEEDED INTERACT LAYOUT for every interact script
    private BasicInteractLayout _basicInteractLayout;

    [SerializeField]
    private GameObject _canvasUINotes;
    [SerializeField]
    private int _id;

    public void Awake()
    {
        _basicInteractLayout = GetComponent<BasicInteractLayout>();
        _basicInteractLayout._interactAction += NoteInteract; //Depends on function name
    }
    //END NEEDED INTERACT LAYOUT

    //unique function/interaction based on script/object
    void NoteInteract()
    {
        _canvasUINotes.SetActive(true);
        NotesManager.AddToList(_id);
        //interact code...


        //_basicInteractLayout.ExitANDNotInteract(true); //in case you want to only interact once
    }
}
