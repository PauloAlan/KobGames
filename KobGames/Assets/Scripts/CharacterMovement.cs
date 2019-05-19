using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour {
    public Transform goal,checkPoint;
    NavMeshAgent agent;

    public bool alive = true, boost = false;
    public float energy=1;

    public GameObject explosionParticle;


    //UI
    public Image energyBar;

	// Use this for initialization
	void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;
        agent.isStopped = true;
    }
	
	// Update is called once per frame
	void Update () {

        //Stop and run
        if (Input.GetMouseButton(0) && !GameController.Gc.pause &&alive)
        {
            //Debug.Log("Finger Down");
            agent.destination = goal.position;
            agent.isStopped = false;
            if (boost && energy > 0)
            {
                energy -= Time.deltaTime/2;
                agent.speed = 8;
                Debug.Log("Sprinting");
            }
            else
            {
                agent.speed = 6;

            }
        }

        if (Input.GetMouseButtonUp(0) && !GameController.Gc.pause&& alive)
        {
            //Debug.Log("Finger Up");
            agent.isStopped = true;
            agent.speed = 6;
            boost = false;
        }

        //Recharge Energy
        if (!boost && energy<1)
        {
            energy += Time.deltaTime/4;
        }
        Ui();
    }

    public void Booster(bool value)
    {
        boost = value;
        //Debug.Log("Boost= "+boost);
    }

    public void Ui()
    {
        Color energyBarColor = energyBar.color ;
        if (energy != 1)
        {
            energyBar.enabled = true;
            energyBarColor.g = energy;
            energyBarColor.r = 1 - energyBarColor.g;
            energyBar.color = energyBarColor;
            energyBar.fillAmount = energy;
        }
        else
        {
            energyBar.enabled = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        switch (other.tag)
        {
            case "Obstacle":
                if (alive)
                {
                    alive = false;
                    explosionParticle.transform.position = transform.position;
                    GameController.Gc.PlayAudio("Death");
                    explosionParticle.GetComponent<ParticleSystem>().Play();
                    GetComponent<MeshRenderer>().enabled = false;
                    StartCoroutine(BackToCheckPoint());
                }
                break;
            case "CheckPoint":
                if (checkPoint != other.transform)
                {
                    GameController.Gc.PlayAudio("CheckPoint");
                }
                checkPoint = other.transform;
                break;
            case "CameraTrigger":
                Debug.Log("Camara!");
                GameObject.Find("Main Camera").transform.parent=null ;
                Camera.aerealView=other.transform.parent.transform;
                Camera.thirdPerson = false;
                break;
            case"CameraExit":
                GameObject.Find("Main Camera").transform.parent = gameObject.transform;
                Camera.thirdPerson = true;
                break;
            //Goal Reached
            case "Finish":
                GameController.Gc.LevelCleared();
                break;
        }
    }

    IEnumerator BackToCheckPoint()
    {
        GameObject.Find("Main Camera").transform.parent = gameObject.transform;
        Camera.thirdPerson = true;
        agent.isStopped = true;
        agent.ResetPath();
        yield return new WaitForSeconds(2);
        GetComponent<MeshRenderer>().enabled = true;
        alive = true;
        transform.position = checkPoint.position;
        //agent.destination = goal.position;
        //agent.isStopped = false;
    }
}
