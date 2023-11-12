using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class SetQuality : MonoBehaviour
{
    [SerializeField] private Dropdown qualityDropDown;
    [SerializeField] private UniversalRenderPipelineAsset urpQualityLow;
    [SerializeField] private UniversalRenderPipelineAsset urpQualityMedium;
    [SerializeField] private UniversalRenderPipelineAsset urpQualityHigh;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetQualityLevel(int index)
    {

        switch (index)
        {
            case 0:
                urpQualityLow.shadowDistance = 20;
                break;
            case 1:
                urpQualityMedium.shadowDistance = 50;
                break;
            case 2:
                urpQualityHigh.shadowDistance = 100;
                break;
        }

        QualitySettings.SetQualityLevel(index, false);
    }

}
