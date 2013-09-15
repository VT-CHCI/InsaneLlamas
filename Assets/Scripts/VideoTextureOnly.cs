using UnityEngine;
using System.Collections;

public class VideoTextureOnly : MonoBehaviour {
	#region PRIVATE_MEMBER_VARIABLES

    private Texture2D mTexture = null;
    private QCARRenderer.VideoTextureInfo mTextureInfo;

    #endregion // PRIVATE_MEMBER_VARIABLES
	
	// Use this for initialization
	void Start () {
		// Ask the renderer to stop drawing the videobackground.
        QCARRenderer.Instance.DrawVideoBackground = false;

        // Create texture of size 0 that will be updated in the plugin (we allocate buffers in native code)
        mTexture = new Texture2D(0, 0, TextureFormat.RGB565, false);
        mTexture.filterMode = FilterMode.Bilinear;
        mTexture.wrapMode = TextureWrapMode.Clamp;

        // Assign texture to the renderer
        renderer.material.mainTexture = mTexture;

        // Set the native texture ID:
        int nativeTextureID = mTexture.GetNativeTextureID();
        if (!QCARRenderer.Instance.SetVideoBackgroundTextureID(nativeTextureID))
        {
            Debug.Log("Failed to setVideoBackgroundTextureID " + nativeTextureID);
        }
        else
        {
            Debug.Log("Successfully setVideoBackgroundTextureID " + nativeTextureID);
        }
	}
	
	// Update is called once per frame
	void Update () {
		// Setup the geometry and orthographic camera as soon as the video
        // background info is available.
        if (QCARRenderer.Instance.IsVideoBackgroundInfoAvailable())
        {
            // Check if we need to update the texture:
            QCARRenderer.VideoTextureInfo texInfo = QCARRenderer.Instance.GetVideoTextureInfo();
            if (!mTextureInfo.imageSize.Equals(texInfo.imageSize) ||
                !mTextureInfo.textureSize.Equals(texInfo.textureSize))
            {
                // Cache the info:
                mTextureInfo = texInfo;

                Debug.Log("VideoTextureInfo " + texInfo.textureSize.x + " " +
                    texInfo.textureSize.y + " " + texInfo.imageSize.x + " " + texInfo.imageSize.y);
			}
		}

	}
}
