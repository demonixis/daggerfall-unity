// Project:         Daggerfall Tools For Unity
// Copyright:       Copyright (C) 2009-2016 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Gavin Clayton (interkarma@dfworkshop.net)
// Contributors:    Yannick Comte (comte.yannick@gmail.com)
// 
// Notes:
//

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;

public class OnGUIVR : MonoBehaviour
{
    private static OnGUIVR instance;
    private RenderTexture renderTexture;
    private bool vrEnabled;

    [SerializeField]
    private Texture2D _cursor = null;

    IEnumerator Start()
    {
        vrEnabled = VRSettings.enabled;

        if (vrEnabled)
        {
            var camera = Camera.main;
            var width = 1024;
            var height = 600;

            renderTexture = new RenderTexture(width, height, 24, RenderTextureFormat.Default);
            renderTexture.Create();

            var canvasGO = new GameObject("VRCanvas");
            var canvasTransform = canvasGO.AddComponent<RectTransform>();
            canvasTransform.SetParent(camera.transform.parent);
            canvasTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            canvasTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            canvasTransform.localPosition = new Vector3(0.0f, 0.0f, 1.0f);
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
            rawImage.texture = renderTexture;

            yield return new WaitForSeconds(0.5f);

            canvasTransform.localPosition = new Vector3(0.0f, Camera.main.transform.localPosition.y, canvasTransform.localPosition.z);
        }

        instance = this;
    }

    void OnGUI()
    {
        Begin();
        GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, _cursor.width, _cursor.height), _cursor);
        End();
    }

    public static void Begin()
    {
        if (instance != null && instance.vrEnabled)
            RenderTexture.active = instance.renderTexture;
    }

    public static void End()
    {
        if (instance != null && instance.vrEnabled)
            RenderTexture.active = null;
    }
}