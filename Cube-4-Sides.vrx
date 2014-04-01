<?xml version="1.0" ?>
<MiddleVR>
    <Kernel LogLevel="2" LogInSimulationFolder="0" EnableCrashHandler="0" Version="1.4.0.f2" />
    <DeviceManager WandAxis="0" WandHorizontalAxis="0" WandHorizontalAxisScale="1" WandVerticalAxis="1" WandVerticalAxisScale="1" WandButtons="0" WandButton0="0" WandButton1="1" WandButton2="2" WandButton3="3" WandButton4="4" WandButton5="5">
        <Driver Type="vrDriverDirectInput" />
    </DeviceManager>
    <DisplayManager Fullscreen="0" WindowBorders="0" ShowMouseCursor="0" VSync="1" AntiAliasing="0" ForceHideTaskbar="0" SaveRenderTarget="0">
        <Node3D Name="CenterNode" Parent="VRRootNode" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
        <Node3D Name="HandNode" Tag="Hand" Parent="CenterNode" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
        <Node3D Name="Screens" Parent="CenterNode" Tracker="0" PositionLocal="0.000000,0.000000,1.160000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
        <Screen Name="Front" Parent="Screens" Tracker="0" PositionLocal="0.000000,1.200000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" Width="3.2" Height="2.4" />
        <Screen Name="Left" Parent="Screens" Tracker="0" PositionLocal="-1.600000,-0.400000,0.000000" OrientationLocal="0.000000,0.000000,0.707107,0.707107" Width="3.2" Height="2.4" />
        <Screen Name="Right" Parent="Screens" Tracker="0" PositionLocal="1.600000,-0.400000,0.000000" OrientationLocal="0.000000,0.000000,-0.707107,0.707107" Width="3.2" Height="2.4" />
        <Screen Name="Floor" Parent="Screens" Tracker="0" PositionLocal="0.000000,0.000000,-1.200000" OrientationLocal="-0.707107,0.000000,0.000000,0.707107" Width="3.2" Height="2.4" />
        <Node3D Name="HeadNode" Parent="CenterNode" Tracker="0" PositionLocal="0.000000,0.000000,1.700000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
        <CameraStereo Name="CameraStereo_Front" Parent="HeadNode" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" VerticalFOV="60" Near="0.1" Far="3000" Screen="Front" ScreenDistance="1" UseViewportAspectRatio="0" AspectRatio="1.25" InterEyeDistance="0.065" LinkConvergence="1" />
        <Node3D Name="Base_Floor" Parent="HeadNode" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
        <CameraStereo Name="CameraStereo_Floor" Parent="Base_Floor" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" VerticalFOV="60" Near="0.1" Far="3000" Screen="Floor" ScreenDistance="1" UseViewportAspectRatio="0" AspectRatio="1" InterEyeDistance="0.065" LinkConvergence="1" />
        <CameraStereo Name="CameraStereo_Right" Parent="HeadNode" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" VerticalFOV="60" Near="0.1" Far="3000" Screen="Right" ScreenDistance="1" UseViewportAspectRatio="0" AspectRatio="1.25" InterEyeDistance="0.065" LinkConvergence="1" />
        <CameraStereo Name="CameraStereo_Left" Parent="HeadNode" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" VerticalFOV="60" Near="0.1" Far="3000" Screen="Left" ScreenDistance="1" UseViewportAspectRatio="0" AspectRatio="1.25" InterEyeDistance="0.065" LinkConvergence="1" />
        <Viewport Name="Viewport_Front" Left="0" Top="0" Width="1400" Height="1050" Camera="CameraStereo_Front" Stereo="1" StereoMode="0" CompressSideBySide="0" StereoInvertEyes="0" OculusRiftWarping="0" />
        <Viewport Name="Viewport_Left" Left="0" Top="0" Width="1400" Height="1050" Camera="CameraStereo_Left" Stereo="1" StereoMode="0" CompressSideBySide="0" StereoInvertEyes="0" OculusRiftWarping="0" />
        <Viewport Name="Viewport_Right" Left="0" Top="0" Width="1400" Height="1050" Camera="CameraStereo_Right" Stereo="1" StereoMode="0" CompressSideBySide="0" StereoInvertEyes="0" OculusRiftWarping="0" />
        <Viewport Name="Viewport_Floor" Left="0" Top="0" Width="1400" Height="1050" Camera="CameraStereo_Floor" Stereo="1" StereoMode="0" CompressSideBySide="0" StereoInvertEyes="0" OculusRiftWarping="0" />
    </DisplayManager>
    <ClusterManager NVidiaSwapLock="0" DisableVSyncOnServer="1" ForceOpenGLConversion="0" BigBarrier="0" SimulateClusterLag="0">
        <ClusterServer Address="Cube_Master" Viewports="Viewport_Front" CPUAffinity="-1" GPUAffinity="-1" />
        <ClusterClient Address="Cube_Front" ClusterID="Client_Front" Viewports="Viewport_Front" CPUAffinity="-1" GPUAffinity="-1" />
        <ClusterClient Address="Cube_Left" ClusterID="Client_Left" Viewports="Viewport_Left" CPUAffinity="-1" GPUAffinity="-1" />
        <ClusterClient Address="Cube_Right" ClusterID="Client_Right" Viewports="Viewport_Right" CPUAffinity="-1" GPUAffinity="-1" />
        <ClusterClient Address="Cube_Floor" ClusterID="Client_Floor" Viewports="Viewport_Floor" CPUAffinity="-1" GPUAffinity="-1" />
    </ClusterManager>
</MiddleVR>
