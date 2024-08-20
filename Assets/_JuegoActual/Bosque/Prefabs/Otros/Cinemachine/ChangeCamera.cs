using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    
    CinemachineTargetGroup targetGroup;
    EnemyLock player;
    public float radius;
    public float weight;

    void Start()
    {
        
        targetGroup = GetComponent<CinemachineTargetGroup>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EnemyLock>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isLockOnMode == false)
        {
            if(targetGroup.m_Targets.Length > 1)
            {
              targetGroup.RemoveMember(targetGroup.m_Targets[1].target);
            }
           

        }

        if (player.isLockOnMode == true)
        {
            if (player.currentTarget != null)
            {
                if (targetGroup.m_Targets.Length < 2)
                {
                    // Agrega el nuevo miembro del CinemachineTargetGroup.
                    targetGroup.AddMember(player.currentTarget.transform, weight, radius);
                }
                else
                {
                    // Si ya hay un segundo miembro, actualiza su transform con el nuevo objetivo.
                    targetGroup.m_Targets[1].target = player.currentTarget.transform;
                    targetGroup.m_Targets[1].weight = weight;
                    targetGroup.m_Targets[1].radius = radius;
                }
            }
        }


    }

   
}
