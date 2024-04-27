using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Camera monsterCam;
    public Camera streamerCam;

    private CharacterController characterController;

    [SerializeField]
    private float speed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        monsterCam.enabled = true;
        streamerCam.enabled = false;

        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            monsterCam.enabled = !monsterCam.enabled;
            streamerCam.enabled = !streamerCam.enabled;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        characterController.Move(move * Time.deltaTime * speed);
    }
}
