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
    public enum FeedBackType
    {
        Unity,
        Cubic,
        Exponential
    }

    public class FeedBack
    {
        public double Run(double positionVariable)
        {
            return positionVariable;
        }
    }

    public class Unity : FeedBack
    {
        new public double Run(double positionVariable)
        {
            return positionVariable;
        }
    }

    public class Cubic : FeedBack
    {
        new public double Run(double positionVariable)
        {
            return positionVariable * positionVariable * positionVariable;
        }
    }

    public class Exponential : FeedBack
    {
        new public double Run(double positionVariable)
        {
            return Math.Pow(MathHelper.E, positionVariable);
        }
    }
}
