using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicInteractLayout))]
public class InteractWindow : MonoBehaviour
{
    private BasicInteractLayout _basicInteractLayout;
    
    void Awake()
    {
        _basicInteractLayout = GetComponent<BasicInteractLayout>();
        _basicInteractLayout._interactAction += OpenWindow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenWindow()
    {
        print("OpenWindow");
    }
}
