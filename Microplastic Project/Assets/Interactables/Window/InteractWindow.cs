using UnityEngine;

[RequireComponent(typeof(BasicInteractLayout))] //ALSO NEEDED
public class InteractWindow : MonoBehaviour
{
    //NEEDED INTERACT LAYOUT for every interact script
    private BasicInteractLayout _basicInteractLayout;
    
    void Awake()
    {
        _basicInteractLayout = GetComponent<BasicInteractLayout>();
        _basicInteractLayout._interactAction += OpenWindow; //Depends on function name
    }
    //END NEEDED INTERACT LAYOUT

    //unique function/interaction based on script/object
    void OpenWindow()
    {
        print("OpenWindow");

        //interact code...


        //_basicInteractLayout.ExitANDNotInteract(true); //in case you want to only interact once
    }
}
