// Project:         Daggerfall Tools For Unity
// Copyright:       Copyright (C) 2009-2016 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Gavin Clayton (interkarma@dfworkshop.net)
// Contributors:    Yannick Comte
// 
// Notes:
//

using UnityEngine;
using UnityEngine.VR;

namespace DaggerfallWorkshop.Game
{
    public class PlayerVR : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            if (VRSettings.enabled)
            {
                var camera = Camera.main;
                camera.fieldOfView = 90.0f;

                var fogEffect = camera.GetComponent<UnityStandardAssets.ImageEffects.GlobalFog>();
                if (fogEffect != null)
                    fogEffect.enabled = false;
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
                InputTracking.Recenter();
        }
    }
}