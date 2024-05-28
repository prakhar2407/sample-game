using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class CarController : MonoBehaviour {


    public float speed = 1500f;
    public float rotationSpeed = 4f;
    private float movement = 0f;
    private float rotation = 0f;

    public WheelJoint2D backWheel;
    public WheelJoint2D frontWheel;

    public Rigidbody2D car;

    public MoveCarActions moveCarAction;

    private void Awake() {
        moveCarAction = new MoveCarActions();
        moveCarAction.Enable();
        moveCarAction.Player.MoveCar.performed += MoveCar;
        moveCarAction.Player.MoveCar.canceled += MoveCar;
    }

    private void FixedUpdate() {
        if (movement == 0f) {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
        }
        else {
            backWheel.useMotor = true;
            frontWheel.useMotor = true;
            JointMotor2D motor = new() { motorSpeed = movement * speed, maxMotorTorque = 1000 };
            backWheel.motor = motor;
            frontWheel.motor = motor;

            car.AddTorque(rotation * rotationSpeed * Time.fixedDeltaTime);
            // if (!car.IsTouching(map.GetComponent<Collider2D>())) {
            // }
        }

    }


    public void MoveCar(InputAction.CallbackContext context) {
        float value = context.ReadValue<float>();
        movement = -value;
        rotation = value;
    }


}
