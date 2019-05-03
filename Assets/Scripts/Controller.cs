using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update

    CharacterController _ctrl;

    [SerializeField]
    Animator _humanAnim;

    [SerializeField]
    Animator _wolfAnim;

    [SerializeField]
    float MoveSpeed;

    [SerializeField]
    float RotSpeed;


    void Start()
    {
        _ctrl = GetComponentInChildren<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _ctrl.SimpleMove(transform.forward * Input.GetAxis("Vertical") * MoveSpeed);
        transform.Rotate(0, Input.GetAxis("Horizontal") * RotSpeed * Time.deltaTime, 0);
        if (Input.GetAxis("Vertical") != 0)
        {
            _humanAnim.SetBool("running", true);
            _wolfAnim.SetBool("running", true);
        }
        else
        {
            _humanAnim.SetBool("running", false);
            _wolfAnim.SetBool("running", false);
        }
    }
}
