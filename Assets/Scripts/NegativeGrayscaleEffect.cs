/*==============================================================================
Copyright (c) 2012 QUALCOMM Austria Research Center GmbH.
All Rights Reserved.
Qualcomm Confidential and Proprietary
==============================================================================*/

using UnityEngine;
using System;

[RequireComponent(typeof(VideoTextureBehaviour))]
[RequireComponent(typeof(GLErrorHandler))]
public class NegativeGrayscaleEffect : MonoBehaviour
{
    #region PRIVATE_MEMBER_VARIABLES

    private bool mErrorOccurred = false;

    private const string ERROR_TEXT = "The BackgroundTextureAccess sample requires OpenGL ES 2.0";
    private const string CHECK_STRING = "OpenGL ES 2.0";

    #endregion // PRIVATE_MEMBER_VARIABLES



    #region UNITY_MONOBEHAVIOUR_METHODS

    // Use this for initialization
    void Start()
    {
        // This sample requires OpenGL ES 2.0 otherwise it won't work.
        mErrorOccurred = !IsOpenGLES2();

        if (mErrorOccurred)
        {
            Debug.LogError(ERROR_TEXT);

            // Show a dialog box with an error message.
            GLErrorHandler.SetError(ERROR_TEXT);

            // Turn off renderer to make sure the unsupported shader is not used.
            renderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float touchX = 2.0F;
        float touchY = 2.0F;

        if (Input.touches.Length > 0)
        {
            Touch maintouch = Input.touches[0];

            // Adjust the touch point for the current orientation
            if (Screen.orientation == ScreenOrientation.Landscape)
            {
                touchX = (maintouch.position.x / Screen.width) - 0.5F;
                touchY = (maintouch.position.y / Screen.height) - 0.5F;
            }
            else if (Screen.orientation == ScreenOrientation.Portrait)
            {
                touchX = ((maintouch.position.y / Screen.height) - 0.5F) * -1;
                touchY = (maintouch.position.x / Screen.width) - 0.5F;
            }
            else if (Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                touchX = ((maintouch.position.x / Screen.width) - 0.5F) * -1;
                touchY = ((maintouch.position.y / Screen.height) - 0.5F) * -1;
            }
            else if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                touchX = (maintouch.position.y / Screen.height) - 0.5F;
                touchY = ((maintouch.position.x / Screen.width) - 0.5F) * -1;
            }
        }

        renderer.material.SetFloat("_TouchX", touchX);
        renderer.material.SetFloat("_TouchY", touchY);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS



    #region PRIVATE_METHODS

    // This method checks if we are using OpenGL ES 2.0.
    private bool IsOpenGLES2()
    {
        string graphicsDeviceVersion = SystemInfo.graphicsDeviceVersion;

        Debug.Log("Sample using " + graphicsDeviceVersion);

        return (graphicsDeviceVersion.IndexOf(CHECK_STRING, StringComparison.Ordinal) >= 0);
    }

    #endregion // PRIVATE_METHODS
}
