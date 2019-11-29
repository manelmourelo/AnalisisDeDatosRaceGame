using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMap : MonoBehaviour
{
    private Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(100, 100, 5f);
        grid.SetCSVValues();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            grid.SetValue(new Vector3(27, 27, 27), 50);//Pass the pos of the csv
        }

        if (Input.GetMouseButtonDown(1))
        {

            Debug.Log(grid.GetValue(new Vector3(27, 27, 27)));      //Pass the pos of the csv
        }
    }
}
