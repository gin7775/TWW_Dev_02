using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource AudioSource;

    [SerializeField] private AudioClip grass;
    [SerializeField] private AudioClip concrete;

    RaycastHit hit;

    public Transform RayStart;

    public float range;

    public LayerMask layerMask;

    void Start()
    {
        
    }

    void PlayFootstepSound(AudioClip audio)
    {
        AudioSource.clip = grass;
      
        AudioSource.Play();
    }

   

    // Update is called once per frame
    //void Update()
    //{
    //    if (Physics.Raycast(RayStart.position, RayStart.transform.up * -1, out hit, range, layerMask))
    //    {
    //        if (hit.collider.CompareTag("Grass"))
    //        {
    //            PlayFootstepSound(grass);
    //        }

    //    }
    //}
}
