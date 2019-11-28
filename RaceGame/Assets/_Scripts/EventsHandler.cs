using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class EventsHandler : MonoBehaviour
{

    public GameObject goal = null;

    private List<string[]> level_events_data = new List<string[]>();

    private List<string[]> laps_data = new List<string[]>();
    private List<string[]> sessions_data = new List<string[]>();
    private List<string[]> crashes_data = new List<string[]>();

    private int last_lap_id = 0;
    private int last_session_id = 0;
    private int last_crash_id = 0;

    public string username = "ricardogl";

    enum TypeEvent
    {
        EVENT_NONE,
        LAP_DONE,
        END_SESSION,
        CRASH
    }

    // Start is called before the first frame update
    void Start()
    {
        

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

        else
        {
            Debug.Log("File exist");

            //**If file exists then we look for the highest session_id to follow from there.**

            //Read the file and put data into stringList
            List<string> stringList = new List<string>();
            List<string[]> parsedList = new List<string[]>();
            List<int> lap_ids = new List<int>();
          
            StreamReader str_reader = new StreamReader("Assets/CSV/laps.csv");
            while (!str_reader.EndOfStream)
            {
                string line = str_reader.ReadLine();
                stringList.Add(line);

            }
            str_reader.Close();

            for (int i = 1; i < stringList.Count; i++)
            {
                string[] temp = stringList[i].Split(';');
             
                for (int j = 0; j < temp.Length; j++)
                {
                    temp[j] = temp[j].Trim();
                 
                    if (j == 0)
                        lap_ids.Add(int.Parse(temp[j]));

                 
                }

                parsedList.Add(temp);

            }

            //Get highest lap id
            lap_ids.Sort();
            last_lap_id = lap_ids[lap_ids.Count - 1];
            

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
            Debug.Log("File exist");

            //**If file exists then we look for the highest session_id to follow from there.**

            //Read the file and put data into stringList
            List<string> stringList = new List<string>();
            List<string[]> parsedList = new List<string[]>();
            List<int> session_ids = new List<int>();

            StreamReader str_reader = new StreamReader("Assets/CSV/sessions.csv");
            while (!str_reader.EndOfStream)
            {
                string line = str_reader.ReadLine();
                stringList.Add(line);

            }
            str_reader.Close();

            for (int i = 1; i < stringList.Count; i++)
            {
                string[] temp = stringList[i].Split(';');
           
                for (int j = 0; j < temp.Length; j++)
                {
                    temp[j] = temp[j].Trim();

                    if(j == 0)
                        session_ids.Add(int.Parse(temp[j]));
                  

                }

                parsedList.Add(temp);

            }

           
            //Get highest session id 
            session_ids.Sort();
            last_session_id = session_ids[session_ids.Count - 1];
        }
        

        if (System.IO.File.Exists("Assets/CSV/crashes.csv") == false)
        {
            string[] row_data_temp = new string[6];

            row_data_temp[0] = "crash_id";
            row_data_temp[1] = "position";
            row_data_temp[2] = "current_lap";
            row_data_temp[3] = "time";
            row_data_temp[4] = "session_id";
            row_data_temp[5] = "collision_obj_id";
            crashes_data.Add(row_data_temp);
            Save(TypeEvent.CRASH);
        }
        else
        {
            Debug.Log("File exist");

            //**If file exists then we look for the highest crash_id to follow from there.**

            //Read the file and put data into stringList
            List<string> stringList = new List<string>();
            List<string[]> parsedList = new List<string[]>();
            List<int> crash_ids = new List<int>();

            StreamReader str_reader = new StreamReader("Assets/CSV/crashes.csv");
            while (!str_reader.EndOfStream)
            {
                string line = str_reader.ReadLine();
                stringList.Add(line);

            }
            str_reader.Close();

            for (int i = 1; i < stringList.Count; i++)
            {
                string[] temp = stringList[i].Split(';');

                for (int j = 0; j < temp.Length; j++)
                {
                    temp[j] = temp[j].Trim();

                    if (j == 0)
                        crash_ids.Add(int.Parse(temp[j]));


                }

                parsedList.Add(temp);

            }


            //Get highest session id 
            crash_ids.Sort();
            last_crash_id = crash_ids[crash_ids.Count - 1];
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

    public void WriteCrash(int collision_obj_id, Vector3 pos)
    {
        PlayerPrefs.SetString("crash_time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

        string[] row_data_temp = new string[6];
        row_data_temp[0] = "0";
        row_data_temp[1] = username;
        row_data_temp[2] = goal.GetComponent<Goal>().lap_count.ToString();
        row_data_temp[3] = PlayerPrefs.GetString("crash_time");
        row_data_temp[4] = "0";
        row_data_temp[5] = collision_obj_id.ToString();
        crashes_data.Add(row_data_temp);
        Save(TypeEvent.CRASH);
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

            case TypeEvent.CRASH:

                path = "Assets/CSV/crashes.csv";

                length = crashes_data.Count;

                for (int index = 0; index < length; index++)
                    sb.Append(string.Join(delimiter, crashes_data[index]));

                break;

        }
     

        
        StreamWriter outStream = System.IO.File.AppendText(path);
        outStream.WriteLine(sb);
        outStream.Close();
        laps_data.Clear();
        sessions_data.Clear();
        crashes_data.Clear();
    }
}
