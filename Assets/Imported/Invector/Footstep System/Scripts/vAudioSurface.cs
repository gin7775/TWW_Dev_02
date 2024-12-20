﻿using FMODUnity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Invector.FootstepSystem
{
    public class vAudioSurface : ScriptableObject
    {
        public AudioSource audioSource;
        public AudioMixerGroup audioMixerGroup;                 // The AudioSource that will play the clips.   
        public List<string> TextureOrMaterialNames;             // The tag on the surfaces that play these sounds.
        public List<AudioClip> audioClips;                      // The different clips that can be played on this surface.    
        public GameObject particleObject;
        public List<EventReference> fmodEvents;
        private vBetterRandom randomSource = new vBetterRandom();       // For randomly reordering clips.   

        public GameObject stepMark;

        public LayerMask stepLayer;

        public float timeToDestroyFootstep = 5f;

        public vAudioSurface()
        {
            audioClips = new List<AudioClip>();
            TextureOrMaterialNames = new List<string>();
        }
        /// <summary>
        /// Spawn surface effect
        /// </summary>
        /// <param name="footStepObject">step object surface info</param>
        /// <param name="playSound">Spawn sound effect</param>
        /// <param name="spawnParticle">Spawn particle effect</param>
        /// <param name="spawnStepMark">Spawn step Mark effect</param>

        public virtual void SpawnSurfaceEffect(FootstepObject footStepObject)
        {
            // initialize variable if not already started
            if (randomSource == null)
            {
                randomSource = new vBetterRandom();
            }
            ///Create audio Effect
            if (footStepObject.spawnSoundEffect)
            {
                PlaySound(footStepObject);
            }
            ///Create particle Effect
            if (footStepObject.spawnParticleEffect && particleObject && footStepObject.ground && stepLayer.ContainsLayer(footStepObject.ground.gameObject.layer))
            {
                SpawnParticle(footStepObject);
            }
            ///Create Step Mark Effect
            if (footStepObject.spawnStepMarkEffect && stepMark != null)
            {
                StepMark(footStepObject);
            }
        }

        /// <summary>
        /// Spawn Sound effect
        /// </summary>
        /// <param name="footStepObject">Step object surface info</param>      
        protected virtual void PlaySound(FootstepObject footStepObject)
        {
            // Si no hay eventos de FMOD para reproducir, retorna.
            if (fmodEvents == null || fmodEvents.Count == 0)
            {
                return;
            }

            int index = randomSource.Next(fmodEvents.Count);
            var instance = RuntimeManager.CreateInstance(fmodEvents[index]);
            instance.set3DAttributes(RuntimeUtils.To3DAttributes(footStepObject.sender.position));
            instance.start();
            instance.release();
        }

        /// <summary>
        /// Spawn Particle effect
        /// </summary>
        /// <param name="footStepObject">Step object surface info</param>
        protected virtual void SpawnParticle(FootstepObject footStepObject)
        {
            var obj = Instantiate(particleObject, footStepObject.sender.position, footStepObject.sender.rotation);
            obj.transform.SetParent(vFootstepContainer.root, true);
            Destroy(obj, timeToDestroyFootstep);
        }

        /// <summary>
        /// Spawn Step Mark effect
        /// </summary>
        /// <param name="footStepObject">Step object surface info</param>
        protected virtual void StepMark(FootstepObject footStep)
        {
            RaycastHit hit;
            if (Physics.Raycast(footStep.sender.transform.position + new Vector3(0, 0.25f, 0), Vector3.down, out hit, 1f, stepLayer))
            {
                if (stepMark)
                {
                    var angle = Quaternion.FromToRotation(footStep.sender.up, hit.normal);
                    var step = Instantiate(stepMark, hit.point, angle * footStep.sender.rotation);
                    step.transform.SetParent(vFootstepContainer.root, true);
                    Destroy(step, timeToDestroyFootstep);
                }
            }
        }
    }



    public static class vFootstepExtensions
    {
        public static bool ContainsLayer(this LayerMask layermask, int layer)
        {
            return layermask == (layermask | (1 << layer));
        }
    }

}