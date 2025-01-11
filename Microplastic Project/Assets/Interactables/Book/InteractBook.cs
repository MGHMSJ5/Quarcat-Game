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
        // Check if the recycling dialogue has been completed before allowing interaction
        if (!RecyclingDialogueListener.HasDialoguePlayed())
        {
            Debug.Log("You must first complete the recycling dialogue to interact with the book.");
            return; // Exit early if the dialogue hasn't been played
        }

        _canvasBook.SetActive(true); // If the dialogue has been played, open the book

        // interact code...


        //_basicInteractLayout.ExitANDNotInteract(true); //in case you want to only interact once
    }
}
