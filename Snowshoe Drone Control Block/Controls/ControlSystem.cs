using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls
{
    public class ControlSystem
    {
        public double Sp { get; protected set; }
        public double Pv { get; protected set; }
        protected FeedBack feedBack;
        protected List<Controller> controlOperators = new List<Controller>();

        public ControlSystem(FeedBackType feedBackType, List<double> gains)
        {
            if (feedBackType == FeedBackType.Unity) { feedBack = new Unity(); }
            else if (feedBackType == FeedBackType.Cubic) { feedBack = new Cubic(); }
            else if (feedBackType == FeedBackType.Exponential) { feedBack = new Exponential(); }

            controlOperators[0] = new Controller(gains[0]);
        }

        public double Run(double positionVariable)
        {
            Pv = feedBack.Run(positionVariable);
            double acc = 0;
            foreach (var operat in controlOperators) { acc += operat.Run(Pv); }
            return acc;
        }

        public void Set(double setpoint)
        {
            Sp = setpoint;
            foreach(var operat in controlOperators) { operat.Set(setpoint); }
        }

        public void Reset()
        {
            foreach (var operat in controlOperators) { operat.Reset(); }
        }

        public void Tick()
        {
            foreach (var operat in controlOperators) { operat.Tick(); }
        }
    }

    public class PID : ControlSystem
    {
        public PID(FeedBackType feedBackType, List<double> gains) : base(feedBackType, gains)
        {
            if (feedBackType == FeedBackType.Unity) { feedBack = new Unity(); }
            else if (feedBackType == FeedBackType.Cubic) { feedBack = new Cubic(); }
            else if (feedBackType == FeedBackType.Exponential) { feedBack = new Exponential(); }
            controlOperators.Add(new Proportional(gains[0]));
            controlOperators.Add(new Integral(gains[1]));
            controlOperators.Add(new Derivative(gains[2]));
        }
    }

    public class Ideal : ControlSystem
    {
        public Ideal(FeedBackType feedBackType, List<double> gains) : base(feedBackType, gains)
        {
            if (feedBackType == FeedBackType.Unity) { feedBack = new Unity(); }
            else if (feedBackType == FeedBackType.Cubic) { feedBack = new Cubic(); }
            else if (feedBackType == FeedBackType.Exponential) { feedBack = new Exponential(); }
            controlOperators.Add(new Proportional(gains[0]));
            controlOperators.Add(new Integral(gains[1]));
            controlOperators.Add(new Derivative(gains[2]));
        }

        new public double Run(double positionVariable)
        {
            Pv = feedBack.Run(positionVariable);
            double acc = 0;
            double proportional = controlOperators[0].Run(Pv);
            acc += controlOperators[0].Run(proportional);
            acc += controlOperators[0].Run(proportional);
            acc += proportional;
            return acc;
        }

        new public void Set(double setpoint)
        {
            Sp = setpoint;
            foreach (var operat in controlOperators) { operat.Set(0); }
            controlOperators[0].Set(Sp);
        }
    }
}
