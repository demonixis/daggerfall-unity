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

using UnityEngine;
using UnityEngine.XR;

namespace DaggerfallWorkshop.Game
{
    public class PlayerVR : MonoBehaviour
    {
        public static bool XREnabled
        {
            get { return XRSettings.enabled; }
        }

        void Start()
        {
            if (XRSettings.enabled)
            {
                var camera = Camera.main;
                camera.fieldOfView = 90.0f;
                camera.clearFlags = CameraClearFlags.SolidColor;

                XRDevice.SetTrackingSpaceType(TrackingSpaceType.Stationary);
                InputTracking.Recenter();
            }
            else
                Destroy(this);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
                InputTracking.Recenter();
        }
    }
}