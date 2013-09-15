/*==============================================================================
Copyright (c) 2012 QUALCOMM Austria Research Center GmbH.
All Rights Reserved.
Qualcomm Confidential and Proprietary
==============================================================================*/

using UnityEngine;
using System.Collections;

public class GLErrorHandler : MonoBehaviour
{
    #region PRIVATE_MEMBER_VARIABLES

    private static string mErrorText = "";
    private static bool mErrorOccurred = false;

    private const string WINDOW_TITLE = "Sample Error";

    #endregion // PRIVATE_MEMBER_VARIABLES



    #region PUBLIC_METHODS

    public static void SetError(string errorText)
    {
        mErrorText = errorText;
        mErrorOccurred = true;
    }

    #endregion // PUBLIC_METHODS



    #region UNTIY_MONOBEHAVIOUR_METHODS

    // In this method we draw an error window in case something happened.
    void OnGUI()
    {
        // On error, create a full screen window.
        if (mErrorOccurred)
        {
            GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height),
                DrawWindowContent, WINDOW_TITLE);
        }
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS



    #region PRIVATE_METHODS

    // This method draws an error-dialog on the screen.
    private void DrawWindowContent(int id)
    {
        // Create text area with a 10 pixel distance from other controls and
        // window border.
        GUI.Label(new Rect(10, 25, Screen.width - 20, Screen.height - 95),
                    mErrorText);

        // Create centered button with 50/50 size and 10 pixel distance from
        // other controls and window border.
        if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height - 60,
                                    150, 50), "Close"))
            Application.Quit();
    }

    #endregion // PRIVATE_METHODS
}
