using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 5f;

    private CharacterController controller;
    private Camera playerCamera;
    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;

    public Camera monsterCam;
    public Camera streamerCam;
    public GameObject lowRes;
    public GameObject lowRes2;

    public GameObject backGame;
    public GameObject backStream;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        //playerCamera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;

        monsterCam.enabled = true;
        streamerCam.enabled = false;
        lowRes.SetActive(false);
        lowRes2.SetActive(true);
        backGame.SetActive(true);
        backStream.SetActive(false);
    }

    private void Update()
    {
        // Rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        horizontalRotation += mouseX;
        horizontalRotation = horizontalRotation % 360f;

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, horizontalRotation, 0f);
        //transform.Rotate(Vector3.up * mouseX);

        // Movement
        float moveX = Input.GetAxis("Horizontal") * movementSpeed;
        float moveZ = Input.GetAxis("Vertical") * movementSpeed;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * Time.deltaTime);

        // Camera View
        if (Input.GetKeyDown("tab"))
        {
            monsterCam.enabled = !monsterCam.enabled;
            streamerCam.enabled = !streamerCam.enabled;
            lowRes.SetActive(!lowRes.activeSelf);
            lowRes2.SetActive(!lowRes2.activeSelf);
            backGame.SetActive(!backGame.activeSelf);
            backStream.SetActive(!backStream.activeSelf);
        }
    }

}
