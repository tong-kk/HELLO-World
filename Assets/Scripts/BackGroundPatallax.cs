﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundPatallax : MonoBehaviour
{
    public Transform[] backgrounds;
    //相机移动时，背景相对移动的比例
    public float parallaxScale = 0.5f;
    //层间的运动比例
    public float layerScale = 0.5f;
    public float smooth = 5f;

    private Transform CamTransform;
    private Vector3 previousCamPos;

    private void Awake()
    {
        //CamTransform = GameObject.FindGameObjectsWithTag("MainCamera").transform;
        CamTransform = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = CamTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float parallax = (previousCamPos.x - CamTransform.position.x) * parallaxScale;
        for(int i=0; i < backgrounds.Length; i++)
        {
            float targetX = backgrounds[i].position.x + parallax * (1 + i * layerScale);
            Vector3 targetPos = new Vector3(targetX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPos, smooth * Time.deltaTime);
        }
        previousCamPos = CamTransform.position;
    }
}
