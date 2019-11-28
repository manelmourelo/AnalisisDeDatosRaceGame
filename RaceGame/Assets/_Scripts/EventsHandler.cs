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
    private List<string[]> sessions_data = new List<string[]>();

    public string username = "manelmm3";

    enum TypeEvent
    {
        EVENT_NONE,
        LAP_DONE,
        END_SESSION
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

        PlayerPrefs.SetString("start_time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

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

        if (System.IO.File.Exists("Assets/CSV/sessions.csv") == false)
        {
            string[] row_data_temp = new string[4];

            row_data_temp[0] = "session_id";
            row_data_temp[1] = "username";
            row_data_temp[2] = "session_start";
            row_data_temp[3] = "session_end";
            sessions_data.Add(row_data_temp);
            Save(TypeEvent.END_SESSION);
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

    private void OnDestroy()
    {
        WriteSessionEnd();
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

    public void WriteSessionEnd()
    {
        PlayerPrefs.SetString("end_time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

        string[] row_data_temp = new string[4];
        row_data_temp[0] = "0";
        row_data_temp[1] = username;
        row_data_temp[2] = PlayerPrefs.GetString("start_time");
        row_data_temp[3] = PlayerPrefs.GetString("end_time");
        sessions_data.Add(row_data_temp);
        Save(TypeEvent.END_SESSION);
    }

    void Save(TypeEvent type)
    {

        string path = "";
        int length = 0;
        string delimiter = ";";
        StringBuilder sb = new StringBuilder();

        switch (type)
        {

            case TypeEvent.EVENT_NONE:
                break;
            case TypeEvent.LAP_DONE:

                path = "Assets/CSV/laps.csv";

                length = laps_data.Count;

                for (int index = 0; index < length; index++) 
                    sb.Append(string.Join(delimiter, laps_data[index]));

                break;

            case TypeEvent.END_SESSION:

                path = "Assets/CSV/sessions.csv";

                length = sessions_data.Count;

                for (int index = 0; index < length; index++)
                    sb.Append(string.Join(delimiter, sessions_data[index]));

                break;

        }
     

        
        StreamWriter outStream = System.IO.File.AppendText(path);
        outStream.WriteLine(sb);
        outStream.Close();
        laps_data.Clear();
        sessions_data.Clear();
    }
}
