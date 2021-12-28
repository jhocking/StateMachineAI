using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float speed = 8;

    private CharacterController charController;

    // Start is called before the first frame update
    void Start() {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var xMov = -Input.GetAxis("Vertical") * speed * Time.deltaTime;
        var zMov = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        charController.Move(new Vector3(xMov, 0, zMov));
    }
}
