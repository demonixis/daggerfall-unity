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

using DaggerfallWorkshop.Game.Serialization;
using System.Collections;
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

        private IEnumerator Start()
        {
            if (XRSettings.enabled)
            {
                var camera = Camera.main;
                camera.fieldOfView = 90.0f;
                camera.clearFlags = CameraClearFlags.Skybox;

                XRDevice.SetTrackingSpaceType(TrackingSpaceType.Stationary);

                yield return new WaitForEndOfFrame();

                InputTracking.Recenter();
            }
            else
                Destroy(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
                InputTracking.Recenter();

            if (Input.GetKeyDown(KeyCode.P))
            {
                var saveManager = FindObjectOfType<SaveLoadManager>();
                saveManager.EnumerateSaves();
                saveManager.QuickLoad();
            }
        }
    }
}