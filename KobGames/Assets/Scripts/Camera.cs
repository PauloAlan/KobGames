using UnityEngine;

public class Camera : MonoBehaviour
{
    public static bool thirdPerson = true;

    public Transform target,anchor;
    public static Transform aerealView;

    public float smoothSpeed = 0.125f;
    public Vector3 offset,normalView;

    private void Start()
    {
    }

    void FixedUpdate()
    {

        normalView = anchor.transform.position;

        if (thirdPerson)
        {
            transform.position = Vector3.MoveTowards(transform.position, normalView, 35 * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, aerealView.position, 19 * Time.deltaTime);
        }
        transform.LookAt(target);
    }


}
