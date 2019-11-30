using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShowHide : MonoBehaviour
{

    private bool show_values = false;


    public void ShowHideValues(GameObject grid)
    {
        show_values = !show_values;

        grid.SetActive(show_values);

    }


}
