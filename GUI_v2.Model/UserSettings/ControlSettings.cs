using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using System.Windows.Input;
using System.Runtime.Serialization;

namespace GUI_v2.Model.UserSettings
{
    [Serializable]
    public class ControlSettings
    {
        
        public Key submergeKey;
        public Key emergeKey;
        public Key forwardKey;
        public Key backwardKey;
        public Key yawLeftKey;
        public Key yawRightKey;
        public Key rollLeftKey;
        public Key rollRightKey;
        public Key pitchForwardKey;
        public Key pitchBackwardKey;

        [XmlIgnoreAttribute]
        public Dictionary<string, Key> Assignment;


        public void BeforeSerialization()
        {
            submergeKey = Assignment["submerge"];
            emergeKey = Assignment["emerge"];
            forwardKey = Assignment["forward"];
            backwardKey = Assignment["backward"];
            yawLeftKey = Assignment["yawLeft"];
            yawRightKey = Assignment["yawRight"];
            rollLeftKey = Assignment["rollLeft"];
            rollRightKey = Assignment["rollRight"];
            pitchForwardKey = Assignment["pitchForward"];
            pitchBackwardKey = Assignment["pitchBackward"];
        }

        public void AfterSerialization()
        {
            Assignment = new Dictionary<string, Key>();
            Assignment.Add("submerge", submergeKey);
            Assignment.Add("emerge", emergeKey);
            Assignment.Add("forward", forwardKey);
            Assignment.Add("backward", backwardKey);
            Assignment.Add("yawLeft", yawLeftKey);
            Assignment.Add("yawRight", yawRightKey);
            Assignment.Add("rollLeft", rollLeftKey);
            Assignment.Add("rollRight", rollRightKey);
            Assignment.Add("pitchForward", pitchForwardKey);
            Assignment.Add("pitchBackward", pitchBackwardKey);
        }

        public ControlSettings()
        {
            submergeKey = Key.LeftCtrl;
            emergeKey = Key.LeftShift;
            forwardKey = Key.W;
            backwardKey = Key.S;
            yawLeftKey = Key.A;
            yawRightKey = Key.D;
            rollLeftKey = Key.Left;
            rollRightKey = Key.Right;
            pitchForwardKey = Key.Up;
            pitchBackwardKey = Key.Down;

            Assignment = new Dictionary<string, Key>();
            Assignment.Add("submerge", Key.LeftCtrl);
            Assignment.Add("emerge", Key.LeftShift);
            Assignment.Add("forward", Key.W);
            Assignment.Add("backward", Key.S);
            Assignment.Add("yawLeft", Key.A);
            Assignment.Add("yawRight", Key.D);
            Assignment.Add("rollLeft", Key.Left);
            Assignment.Add("rollRight", Key.Right);
            Assignment.Add("pitchForward", Key.Up);
            Assignment.Add("pitchBackward", Key.Down);
        }
    }
}
