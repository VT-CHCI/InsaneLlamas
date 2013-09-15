The BackgroundTextureAccess sample shows two approaches to rendering the camera image in Unity:

1) Using a separate orthographic camera, render the camera image with a special shader to produce a negative greyscale effect that warps the image in response to touch events.  Note that this shader only works with OpenGL ES 2.0 selected.  This is the default mode.

2) Using the ARCamera, render the camera image as a plane at the far end of the perspective frustum.  To enable this mode, deactivate the BackgroundCamera (and its child object) and activate the VideoBackground child of the ARCamera.
