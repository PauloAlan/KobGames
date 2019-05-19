using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    //Obstacles types
    public enum ObsType {RotHorizontal,RotVertical,PingPong,Pendulum };
    public ObsType type;

    public float speed=10;



    Transform pos1, pos2;
    bool comeBack = false;
	// Use this for initialization
	void Start ()
    {
        switch (type.ToString())
        {
            case "RotHorizontal":
               
                break;
            case "PingPong":
                pos1 = transform.parent.GetChild(0);
                pos2 = transform.parent.GetChild(1);

                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        switch (type.ToString())
        {
            case "RotHorizontal":
                transform.Rotate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
                break;
            case "RotVertical":
                transform.Rotate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
                break;
            case "PingPong":
                
                if (Vector3.Distance(transform.position, pos2.position) < .05f || Vector3.Distance(transform.position, pos1.position) < .05f)
                {
                    comeBack = !comeBack;
                    
                }
                if(!comeBack)
                    transform.position = Vector3.MoveTowards(transform.position, pos2.position, speed * Time.deltaTime);
                else
                    transform.position = Vector3.MoveTowards(transform.position, pos1.position, speed * Time.deltaTime);
                break;
        }

    }
}
