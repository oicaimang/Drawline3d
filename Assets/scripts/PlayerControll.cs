using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private float InHorizontal;
    private float InVertical;
    public Rigidbody playerRb;

    [SerializeField] float speedHorizonta = 5;
    [SerializeField] float speedVertical = 5;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InHorizontal = Input.GetAxis("Horizontal");
        InVertical = Input.GetAxis("Vertical");
        playerRb.transform.Translate(Vector3.left * InHorizontal * Time.deltaTime*speedHorizonta);
        playerRb.transform.Translate(Vector3.back * InVertical * Time.deltaTime*speedVertical);

    }
}
