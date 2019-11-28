using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    //public GameObject Events_Handler;
    bool _finished;
    float _time;
    public EventsHandler eventhandler;
    public int lap_count = 0;

    // Use this for initialization
    void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if ((col.tag == "Player" || col.transform.root.tag == "Player" ) && !_finished)
        {

            lap_count++;
            _finished = true;            
            StartCoroutine(ResetTag());
            Debug.Log(_time);

            if (eventhandler)
            {
                eventhandler.WriteGoal(_time, lap_count);
            }


            _time = 0;

           
        }
    }
    

    private IEnumerator ResetTag()
    {
        yield return new WaitForSeconds(3);
        _finished = false;
    }
}
