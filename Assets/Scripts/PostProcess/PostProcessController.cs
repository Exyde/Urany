using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System;

public class PostProcessController : MonoBehaviour
{
    [Header("PostProcessing")]
    [HideInInspector]
    public GameObject PostProcessing;
    [HideInInspector]
    public Volume volume;
    Bloom bloom;
    ChromaticAberration chrom;
    ColorAdjustments colorAdj;
    LensDistortion lensDist;


    [Header("Base Values")]
    MinFloatParameter startBloomIntensity;

    [Header("Attack")]
    public MinFloatParameter AttackBloomIntensity;

	private void Awake()
	{
        //PostProcessing = GameObject.FindGameObjectWithTag("PostProcessing");
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out chrom);
        volume.profile.TryGet(out colorAdj);
        volume.profile.TryGet(out lensDist);
    }

	private void Start()
	{
        startBloomIntensity = bloom.intensity;
        bloom.active = true;
	}
	public void SetDashPostProcess()
	{
        chrom.active = true;
	}

    public void SetAttackPostProcess()
	{
        StartCoroutine(AttackFx());
	}

    public void ResetPostProcess()
	{
        chrom.active = false;
        bloom.active = true;
        colorAdj.active = false;
        bloom.intensity = startBloomIntensity;

        lensDist.active = false;
	}

    IEnumerator AttackFx()
	{
        //chrom.active = true;
        colorAdj.active = true;
        bloom.active = true;
        bloom.intensity = AttackBloomIntensity;
        
        yield return new WaitForSeconds(.2f);

        ResetPostProcess();
	}

	public void SetHitPostProcess()
	{
        chrom.active = true;
        colorAdj.active = true;
        colorAdj.saturation.SetValue(new MinFloatParameter(3, 3));

        lensDist.active = true;
	}
}
