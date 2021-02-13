using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController _charController;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // бесконечное перемещение
        // transform.Translate(0, speed, 0);

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        //transform.Translate(deltaX * Time.deltaTime, 0, deltaZ * Time.deltaTime);
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        // ограничение скорости по диагонали
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;

        // ограничение по FPS
        movement *= Time.deltaTime;
        // преобразование координат
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
    }
}
