<?xml version="1.0" ?>
<MiddleVR>
    <Kernel LogLevel="2" LogInSimulationFolder="0" EnableCrashHandler="0" Version="1.4.0.f2" />
    <DeviceManager WandAxis="0" WandHorizontalAxis="0" WandHorizontalAxisScale="1" WandVerticalAxis="1" WandVerticalAxisScale="1" WandButtons="0" WandButton0="0" WandButton1="1" WandButton2="2" WandButton3="3" WandButton4="4" WandButton5="5">
        <Driver Type="vrDriverDirectInput" />
    </DeviceManager>
    <DisplayManager Fullscreen="0" WindowBorders="0" ShowMouseCursor="1" VSync="0" AntiAliasing="0" ForceHideTaskbar="0" SaveRenderTarget="0">
        <Node3D Name="CenterNode" Parent="VRRootNode" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
        <Node3D Name="HandNode" Tag="Hand" Parent="CenterNode" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
        <Node3D Name="HeadNode" Tag="Head" Parent="CenterNode" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
        <Camera Name="FrontCamera" Parent="HeadNode" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" VerticalFOV="60" Near="0.1" Far="1000" Screen="FrontScreen" ScreenDistance="1" UseViewportAspectRatio="0" AspectRatio="2" />
        <Camera Name="RightCamera" Parent="HeadNode" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" VerticalFOV="60" Near="0.1" Far="1000" Screen="RightScreen" ScreenDistance="1" UseViewportAspectRatio="0" AspectRatio="1.5" />
        <Camera Name="LeftCamera" Parent="HeadNode" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" VerticalFOV="60" Near="0.1" Far="1000" Screen="LeftScreen" ScreenDistance="1" UseViewportAspectRatio="0" AspectRatio="1.5" />
        <Camera Name="FloorCamera" Parent="HeadNode" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" VerticalFOV="60" Near="0.1" Far="1000" Screen="FloorScreen" ScreenDistance="1" UseViewportAspectRatio="0" AspectRatio="1.5" />
        <Node3D Name="Screen" Parent="CenterNode" Tracker="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
        <Screen Name="FrontScreen" Parent="Screen" Tracker="0" PositionLocal="0.000000,0.750000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" Width="1.5" Height="1" />
        <Screen Name="RightScreen" Parent="Screen" Tracker="0" PositionLocal="0.750000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,-0.707107,0.707107" Width="1.5" Height="1" />
        <Screen Name="LeftScreen" Parent="Screen" Tracker="0" PositionLocal="-0.750000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.707107,0.707107" Width="1.5" Height="1" />
        <Screen Name="FloorScreen" Parent="Screen" Tracker="0" PositionLocal="0.000000,0.250000,-0.500000" OrientationLocal="-0.707107,0.000000,0.000000,0.707107" Width="1.5" Height="1" />
        <Viewport Name="FrontViewport" Left="1366" Top="0" Width="1366" Height="768" Camera="FrontCamera" Stereo="0" StereoMode="3" CompressSideBySide="0" StereoInvertEyes="0" OculusRiftWarping="0" />
        <Viewport Name="RightViewport" Left="2732" Top="0" Width="1366" Height="768" Camera="RightCamera" Stereo="0" StereoMode="3" CompressSideBySide="0" StereoInvertEyes="0" OculusRiftWarping="0" />
        <Viewport Name="LeftViewport" Left="0" Top="0" Width="1366" Height="768" Camera="LeftCamera" Stereo="0" StereoMode="3" CompressSideBySide="0" StereoInvertEyes="0" OculusRiftWarping="0" />
        <Viewport Name="FloorViewport" Left="1366" Top="768" Width="1366" Height="768" Camera="FloorCamera" Stereo="0" StereoMode="3" CompressSideBySide="0" StereoInvertEyes="0" OculusRiftWarping="0" />
    </DisplayManager>
    <ClusterManager NVidiaSwapLock="0" DisableVSyncOnServer="1" ForceOpenGLConversion="0" BigBarrier="0" SimulateClusterLag="0">
        <ClusterServer Address="localhost" Viewports="FrontViewport" CPUAffinity="-1" GPUAffinity="-1" />
        <ClusterClient Address="localhost" ClusterID="RightClient" Viewports="RightViewport" CPUAffinity="-1" GPUAffinity="-1" />
        <ClusterClient Address="localhost" ClusterID="LeftClient" Viewports="LeftViewport" CPUAffinity="-1" GPUAffinity="-1" />
        <ClusterClient Address="localhost" ClusterID="FloorClient" Viewports="FloorViewport" CPUAffinity="-1" GPUAffinity="-1" />
    </ClusterManager>
</MiddleVR>
