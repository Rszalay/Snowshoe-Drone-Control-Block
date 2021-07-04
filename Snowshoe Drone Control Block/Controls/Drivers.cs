using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace Controls
{
    public class GyroDriver
    {
        Dictionary<MyBlockOrientation, List<IMyGyro>> Gyros = new Dictionary<MyBlockOrientation, List<IMyGyro>>();
        Ideal Pitch, Yaw, Roll;
        Vector3D TangentVector, NormalVector;

    }

    public class ThrustDriver
    {

    }
}
