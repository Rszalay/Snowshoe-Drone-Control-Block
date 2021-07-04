using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls
{
    public class Controller
    {
        public double Sp { get; protected set; }
        public double Pv { get; protected set; }
        public double Gain { get; protected set; }
        protected int ticksSinceLastRun = 0;

        public Controller(double gain, double setpoint = 0) { Gain = gain; Sp = setpoint; }
        public void Set(double setpoint) { Sp = setpoint; }
        public void Load(double positionVariable) { Pv = positionVariable; }
        public double Run(double positionVariable = 0)
        {
            Pv = positionVariable;
            ticksSinceLastRun = 0;
            return (Sp - Pv) * Gain;
        }
        public void Reset() { }
        public void Tick() { ticksSinceLastRun++; }
    }

    public class Proportional : Controller
    {
        public Proportional(double kp, double setpoint = 0) : base(kp, setpoint = 0)
        {
            Gain = kp; Sp = setpoint; 
        }
    }

    public class Integral : Controller
    {
        private double acc;

        public Integral(double ki, double setpoint = 0) : base(ki, setpoint = 0)
        {
            Gain = ki; Sp = setpoint;
        }

        new public double Run(double positionVariable = 0)
        {
            Pv = positionVariable;
            acc = (Sp - Pv) * ticksSinceLastRun;
            ticksSinceLastRun = 0;
            return acc * Gain;
        }

        new public void Reset() { acc = 0; }
    }

    public class Derivative : Controller
    {
        private double lastError;
        public Derivative(double kd, double setpoint = 0) : base(kd, setpoint = 0)
        {
            Gain = kd; Sp = setpoint;
        }

        new public double Run(double positionVariable = 0)
        {
            Pv = positionVariable;
            double error = (Sp - Pv);
            double derivative = (error - lastError) / ticksSinceLastRun;
            ticksSinceLastRun = 0;
            return derivative * Gain;
        }

        new public void Reset() { lastError = 0; }
    }
}
