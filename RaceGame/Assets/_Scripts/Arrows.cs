using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Globalization;

public class Arrows : MonoBehaviour
{
    public GameObject arrow = null;

    Vector3 position = Vector3.one;
    Quaternion rotation = Quaternion.identity;
    int current_lap = 0;
    int prev_lap = 0;
    Color mycolor;
    // Start is called before the first frame update
    void Start()
    {
        mycolor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        GetCSVInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCSVInfo()
    {

        if (System.IO.File.Exists("Assets/CSV/positions.csv"))
        {

            List<string> stringList = new List<string>();
            List<string[]> parsedList = new List<string[]>();
            // List<Vector3> pos_list = new List<Vector3>();

            StreamReader str_reader = new StreamReader("Assets/CSV/positions.csv");
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

                    if (j == 3)
                    {
                        string[] aux = temp[j].Split(',');


                        Vector3 pos = new Vector3
                        (
                            (float)double.Parse(aux[0], CultureInfo.InvariantCulture.NumberFormat),
                            (float)double.Parse(aux[1], CultureInfo.InvariantCulture.NumberFormat),
                            (float)double.Parse(aux[2], CultureInfo.InvariantCulture.NumberFormat)
                        );

                        position = pos;

                    }

                    if (j == 5)
                    {
                        string[] aux = temp[j].Split(',');


                        Quaternion rot = new Quaternion
                        (
                            (float)double.Parse(aux[0], CultureInfo.InvariantCulture.NumberFormat),
                            (float)double.Parse(aux[1], CultureInfo.InvariantCulture.NumberFormat),
                            (float)double.Parse(aux[2], CultureInfo.InvariantCulture.NumberFormat),
                            (float)double.Parse(aux[3], CultureInfo.InvariantCulture.NumberFormat)
                        );

                        rotation = rot;

                    }

                    if (j == 6)
                    {

                        int lap = int.Parse(temp[j]);

                        current_lap = lap;

                    }

                    if (current_lap != prev_lap)
                    {
                        mycolor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                        prev_lap = current_lap;
                    }
                    position.y += 1.5f;
                    GameObject tmp_arrow = Instantiate(arrow, position, rotation);
                    tmp_arrow.GetComponentInChildren<Renderer>().material.color = mycolor;

                }

                parsedList.Add(temp);

            }
        }
    }

}
