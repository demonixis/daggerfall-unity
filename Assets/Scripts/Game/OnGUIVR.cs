// Project:         Daggerfall Tools For Unity
// Copyright:       Copyright (C) 2009-2016 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Yannick Comte (comte.yannick@gmail.com)
// Contributors:
// 
// Notes:
//
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class OnGUIVR : MonoBehaviour
{
    private static OnGUIVR instance;
    private RenderTexture m_VRRenderTexture;
    private bool vrEnabled;

    [SerializeField]
    private Texture2D _cursor = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        vrEnabled = XRSettings.enabled;

        if (vrEnabled)
        {
            var camera = Camera.main;
            var width = Screen.width;
            var height = Screen.height;

            m_VRRenderTexture = new RenderTexture(width, height, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default);
            m_VRRenderTexture.Create();

            var canvasGO = new GameObject("VRCanvas");
            var canvasTransform = canvasGO.AddComponent<RectTransform>();
            canvasTransform.SetParent(camera.transform.parent);
            canvasTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            canvasTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            canvasTransform.localPosition = new Vector3(0.0f, 0.5f, 1.0f);
            canvasTransform.localRotation = Quaternion.identity;
            canvasTransform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

            var canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = camera;

            var rawImageGO = new GameObject("RawImage");
            var rectTransform = rawImageGO.AddComponent<RectTransform>();
            rectTransform.SetParent(canvasTransform, false);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            rectTransform.localPosition = Vector3.zero;

            var rawImage = rawImageGO.AddComponent<RawImage>();
            rawImage.texture = m_VRRenderTexture;

            StartCoroutine(ClearRenderTexture());
        }
    }

    void OnGUI()
    {
        var prev = RenderTexture.active;

        RenderTexture.active = m_VRRenderTexture;

        GUI.depth = -100;
        var rect = new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, _cursor.width, _cursor.height);
        GUI.DrawTexture(rect, _cursor);

        RenderTexture.active = prev;
    }

    private IEnumerator ClearRenderTexture()
    {
        var endOfFrame = new WaitForFixedUpdate();

        while (true)
        {
            yield return endOfFrame;
            var prev = RenderTexture.active;
            RenderTexture.active = m_VRRenderTexture;
            GL.Clear(true, true, new Color(1, 1, 1, 0));
            RenderTexture.active = prev;
        }
    }

    public static void Begin()
    {
        if (instance != null && instance.vrEnabled)
            RenderTexture.active = instance.m_VRRenderTexture;
    }

    public static void End()
    {
        if (instance != null && instance.vrEnabled)
            RenderTexture.active = null;
    }
}