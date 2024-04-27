using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource src;
    public AudioClip[] randomClips;
    public AudioClip[] keyClips;
    public AudioClip[] deathClips;
    public AudioClip[] nearbyClips;
    public AudioClip[] winClips;

    public StreamerNavigation sn;

    public float delayInSeconds = 12f;
    private float timer = 0f;

    public bool canPlay;

    void Start()
    {
        canPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delayInSeconds && canPlay)
        {
            if (sn.isClose)
            {
                PlayNearbySound();
            } else
            {
                PlayRandomSound();
            }
            
            timer = 0f;
        }
    }

    private void PlayRandomSound()
    {
        if (randomClips.Length == 0)
        {
            Debug.LogWarning("No noise clips assigned.");
            return;
        }

        int randomIndex = Random.Range(0, randomClips.Length);
        //AudioClip selectedClip = clips[randomIndex];

        src.clip = randomClips[randomIndex];
        src.Play();

        //AudioSource.PlayClipAtPoint(selectedClip, transform.position);
    }


    public void PlayKeySound()
    {
        if (canPlay)
        {
            int randomIndex = Random.Range(0, keyClips.Length);
            src.clip = keyClips[randomIndex];
            src.Play();
            timer = 0f;
        }
    }
    
    public void PlayDeathSound()
    {
        if (canPlay)
        {
            canPlay = false;
            int randomIndex = Random.Range(0, deathClips.Length);
            //src.clip = deathClips[randomIndex];
            src.PlayOneShot(deathClips[randomIndex]);
        }
    }
    
    public void PlayNearbySound()
    {
        if (canPlay)
        {
            int randomIndex = Random.Range(0, nearbyClips.Length);
            src.clip = nearbyClips[randomIndex];
            src.Play();
            timer = 0f;
        }
    }

    public void PlayWinSound()
    {
        if (canPlay)
        {
            canPlay = false;
            int randomIndex = Random.Range(0, winClips.Length);
            //src.clip = deathClips[randomIndex];
            src.PlayOneShot(winClips[randomIndex]);
        }
    }
}
