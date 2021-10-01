using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followplayer : MonoBehaviour
{
    private GameObject playertarget;
    public Rigidbody groupPlayerFollow;
    // Start is called before the first frame update
    void Start()
    {
        playertarget = GameObject.Find("player");
        groupPlayerFollow = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (playertarget.transform.position - groupPlayerFollow.position).normalized;
        if (direction.magnitude > 0.5)
        {
            groupPlayerFollow.AddForce(direction);
        }
    }
}
