# XR - Oculus Notes<br />
###################################### FPS ######################################<br />
To set 90 FPS you need to install: <br />
Xennial Digital/Packages/XR/Meta/Install OpenXR Meta (Required for Mixed Reality)<br />
and then activate the features:<br />
- Meta Quest: AR Session<br />
- Meta Quest: Display Utilities<br />
<br />
XR Origin (XR Rig) contains Player.cs where you can change the FPS value.<br />
###################################### FPS ######################################<br />
<br />
################################## Space Warp ###################################<br />
- Install https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169<br />
- Activate Meta XR Space Warp Feature<br />
- Call MetaXRSpaceWarp.SetSpaceWarp(true);<br />
################################## Space Warp ###################################<br />
<br />
#################################### Issues #####################################<br />
Known Issues: <br />
- We experimented some crashes loading addressable scenes using OpenXR 1.10.0 so you can use OpenXR 1.9.1 instead.