using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class EventsHandler : MonoBehaviour
{
    private List<string[]> level_events_data = new List<string[]>();

    private List<string[]> laps_data = new List<string[]>();

    public string username = "manelmm3";

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





        //string[] row_data_temp = new string[3];


        //string[] file = System.IO.File.ReadAllLines("Assets/CSV/level_events.csv"); 

        if (System.IO.File.Exists("Assets/CSV/laps.csv") == false)
        {
            string[] row_data_temp = new string[4];

            row_data_temp[0] = "lap_id";
            row_data_temp[1] = "session_id";
            row_data_temp[2] = "username";
            row_data_temp[3] = "time";
            laps_data.Add(row_data_temp);
            Save(TypeEvent.LAP_DONE);
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

        PlayerPrefs.SetString("time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

        string[] row_data_temp = new string[4];
        row_data_temp[0] = lap.ToString();
        row_data_temp[1] = "0";
        row_data_temp[2] = username;
        row_data_temp[3] = PlayerPrefs.GetString("time");
        laps_data.Add(row_data_temp);
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

                path = "Assets/CSV/laps.csv";

                int length = laps_data.Count;
                string delimiter = ";";




                for (int index = 0; index < length; index++) 
                    sb.Append(string.Join(delimiter, laps_data[index]));
                    
                   
        


                break;
        }
     

        
        StreamWriter outStream = System.IO.File.AppendText(path);
        outStream.WriteLine(sb);
        outStream.Close();
        laps_data.Clear();
    }
}
