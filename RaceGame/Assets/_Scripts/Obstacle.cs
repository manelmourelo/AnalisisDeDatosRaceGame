using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public EventsHandler eventhandler = null;
    public int collision_obj_id = 0;
    float timer = 0.0f;
    bool has_crashed = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (has_crashed == true)
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f)
            {
                has_crashed = false;
                timer = 0.0f;
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (eventhandler != null && has_crashed == false)
        {
            eventhandler.WriteCrash(collision_obj_id, transform.position);
            has_crashed = true;
        }
    }

}
