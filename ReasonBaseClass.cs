using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

using System.Drawing.Drawing2D;

namespace Microsoft.Samples.Kinect.BodyBasics
{

    public enum FailReason { KneeNotOverFoot, NotDeepEnough }
    public abstract class ReasonBase
    {
        public FailReason reason { get; protected set; }
        protected IEnumerable<JointType> involvedJoints;
        public abstract void drawGuide(DrawingContext context, IDictionary<JointType, Point> jointPoints);
        public abstract IEnumerable<JointType> getFailedJoints();
    }

    public class NotDeepEnough : ReasonBase
    {
        public NotDeepEnough()
        {
            this.reason = FailReason.NotDeepEnough;
            this.involvedJoints = new List<JointType> { JointType.SpineBase, JointType.HipLeft, JointType.HipRight, JointType.KneeLeft, JointType.KneeRight };
        }
        public override void drawGuide(DrawingContext context, IDictionary<JointType, Point> jointPoints)
        {
            var arrow = new Pen(Brushes.Red , 2);
            var startPoint = jointPoints[JointType.SpineBase];
            context.DrawLine(arrow, startPoint, new Point(startPoint.X, jointPoints[JointType.KneeLeft].Y));
        }
        public override IEnumerable<JointType> getFailedJoints()
        {
            return involvedJoints;
        }
    }
}