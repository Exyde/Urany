using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PostProcessController : MonoBehaviour
{
    [Header("PostProcessing")]
    [HideInInspector]
    public GameObject PostProcessing;
    [HideInInspector]
    public Volume volume;
    Bloom bloom;
    ChromaticAberration chrom;
    //To Add
    LensDistortion lensDist;
    ColorAdjustments colorAdj;


	private void Awake()
	{
        //PostProcessing = GameObject.FindGameObjectWithTag("PostProcessing");
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out chrom);
    }

    public void SetDashPostProcess()
	{
        chrom.active = true;
	}

    public void ResetPostProcess()
	{
        chrom.active = false;
	}
}
