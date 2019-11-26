using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class EventsHandler : MonoBehaviour
{
    private List<string[]> level_events_data = new List<string[]>();

    enum TypeEvent
    {
        EVENT_NONE,
        LAP_DONE
    }

    // Start is called before the first frame update
    void Start()
    {
        //StringBuilder sb = new StringBuilder();

        //StreamWriter outStream = System.IO.File.CreateText("Assets/CSV/test.csv");

        //outStream.WriteLine(sb);
        //outStream.Close();


        

        string[] row_data_temp = new string[3];


        //string[] file = System.IO.File.ReadAllLines("Assets/CSV/level_events.csv"); 




        if (System.IO.File.Exists("Assets/CSV/level_events.csv")==false)
        {
            Debug.Log("File exists");
            row_data_temp[0] = "type";
            row_data_temp[1] = "time";
            row_data_temp[2] = "session_id";
            level_events_data.Add(row_data_temp);
        }
        else
        {
            Debug.Log("File doesn't exist");
        }
       // level_events_data.ToString().Replace("\n\n", "\n");
       



        

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void WriteGoal(float time, int lap)
    {

        string[] row_data_temp = new string[3];
        row_data_temp[0] = "1";
        row_data_temp[1] = time.ToString();
        row_data_temp[2] = lap.ToString();
        level_events_data.Add(row_data_temp);
        Save(TypeEvent.LAP_DONE);

    }

    void Save(TypeEvent type)
    {

        string path = "";
        StringBuilder sb = new StringBuilder();

        switch (type)
        {

            case TypeEvent.EVENT_NONE:
                break;
            case TypeEvent.LAP_DONE:

                path = "Assets/CSV/level_events.csv";

                int length = level_events_data.Count;
                string delimiter = ";";



                for (int index = 0; index < length; index++) 
                    sb.Append(string.Join(delimiter, level_events_data[index]));
                    
                   
        

                break;
        }
     

        
        StreamWriter outStream = System.IO.File.AppendText(path);
        outStream.WriteLine(sb);
        outStream.Close();
        level_events_data.Clear();
    }
}
