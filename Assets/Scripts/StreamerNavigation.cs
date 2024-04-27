using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StreamerNavigation : MonoBehaviour
{
    //public GameObject target1;
    //public GameObject target2;
    public GameObject[] targets;

    public NavMeshAgent agent;

    public AudioPlayer audioPlayer;

    public GameObject monster;
    public float triggerDistance = 11f;
    public bool isClose = false;

    [SerializeField]
    private int numKeys = 0;

    public GameManager gm;

    //Screen shake
    //public float maxDistance = 10f;
    //public float maxShakeIntensity = 0.5f;
    //public float shakeSpeed = 1f;

    //private Vector3 initialPosition;
    //private float shakeIntensity = 0f;

    //public GameObject cam;



    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(targets[numKeys].transform.position);
        audioPlayer = GetComponentInChildren<AudioPlayer>();
        //initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target1.transform.position);

        float distance = Vector3.Distance(transform.position, monster.transform.position);

        if (distance <= triggerDistance && !isClose)
        {
            audioPlayer.PlayNearbySound();
            isClose = true;
        }
        else if (distance > triggerDistance && isClose)
        {
            isClose = false;
        }

        //Screen shake
        //float normalizedDistance = Mathf.Clamp01(distance / maxDistance);
        //shakeIntensity = normalizedDistance * maxShakeIntensity;

        //Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
        //cam.transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition + shakeOffset, shakeSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            if (numKeys < targets.Length - 1)
            {
                numKeys++;
                agent.SetDestination(targets[numKeys].transform.position);
            }
            //numKeys++;
            //agent.SetDestination(targets[numKeys].transform.position);
            audioPlayer.PlayKeySound();
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Exit" && numKeys == targets.Length - 1)
        {
            //End game
            gm.EndGame(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            audioPlayer.PlayDeathSound();
            gm.EndGame(true);
        }
    }
}
