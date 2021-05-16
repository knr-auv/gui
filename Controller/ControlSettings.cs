using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Controller
{
    [Serializable]
    public class ControlSettings
    {

        public int ControllerInterval = 50;

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

        public string submergeKey_pad;
        public string emergeKey_pad;
        public string throttleKey_pad;
        public string yawKey_pad;
        public string rollKey_pad;
 
        public string pitchKey_pad;

        [XmlIgnoreAttribute]
        public Dictionary<string, Key> KeyboardAssignment;
        [XmlIgnoreAttribute]
        public Dictionary<string, string> PadAssignment;

        public void BeforeSerialization()
        {
            submergeKey = KeyboardAssignment["submerge"];
            emergeKey = KeyboardAssignment["emerge"];
            forwardKey = KeyboardAssignment["forward"];
            backwardKey = KeyboardAssignment["backward"];
            yawLeftKey = KeyboardAssignment["yawLeft"];
            yawRightKey = KeyboardAssignment["yawRight"];
            rollLeftKey = KeyboardAssignment["rollLeft"];
            rollRightKey = KeyboardAssignment["rollRight"];
            pitchForwardKey = KeyboardAssignment["pitchForward"];
            pitchBackwardKey = KeyboardAssignment["pitchBackward"];

            submergeKey_pad = PadAssignment["submerge"];
            emergeKey_pad = PadAssignment["emerge"];
            throttleKey_pad = PadAssignment["throttle"];
            yawKey_pad = PadAssignment["yaw"];
            rollKey_pad = PadAssignment["roll"];
            pitchKey_pad = PadAssignment["pitch"];


    }

        public void AfterSerialization()
        {
            KeyboardAssignment = new Dictionary<string, Key>();
            KeyboardAssignment.Add("submerge", submergeKey);
            KeyboardAssignment.Add("emerge", emergeKey);
            KeyboardAssignment.Add("forward", forwardKey);
            KeyboardAssignment.Add("backward", backwardKey);
            KeyboardAssignment.Add("yawLeft", yawLeftKey);
            KeyboardAssignment.Add("yawRight", yawRightKey);
            KeyboardAssignment.Add("rollLeft", rollLeftKey);
            KeyboardAssignment.Add("rollRight", rollRightKey);
            KeyboardAssignment.Add("pitchForward", pitchForwardKey);
            KeyboardAssignment.Add("pitchBackward", pitchBackwardKey);

            PadAssignment.Add("yaw", yawKey_pad);
            PadAssignment.Add("throttle", throttleKey_pad);
            PadAssignment.Add("roll", rollKey_pad);
            PadAssignment.Add("pitch", pitchKey_pad);
            PadAssignment.Add("submerge", submergeKey_pad);
            PadAssignment.Add("emerge", emergeKey_pad);
        }

        public ControlSettings()
        {
            submergeKey = Key.LeftControl;
            emergeKey = Key.LeftShift;
            forwardKey = Key.W;
            backwardKey = Key.S;
            yawLeftKey = Key.A;
            yawRightKey = Key.D;
            rollLeftKey = Key.Left;
            rollRightKey = Key.Right;
            pitchForwardKey = Key.Up;
            pitchBackwardKey = Key.Down;

            KeyboardAssignment = new Dictionary<string, Key>();
            KeyboardAssignment.Add("submerge", Key.LeftControl);
            KeyboardAssignment.Add("emerge", Key.LeftShift);
            KeyboardAssignment.Add("forward", Key.W);
            KeyboardAssignment.Add("backward", Key.S);
            KeyboardAssignment.Add("yawLeft", Key.A);
            KeyboardAssignment.Add("yawRight", Key.D);
            KeyboardAssignment.Add("rollLeft", Key.Left);
            KeyboardAssignment.Add("rollRight", Key.Right);
            KeyboardAssignment.Add("pitchForward", Key.Up);
            KeyboardAssignment.Add("pitchBackward", Key.Down);

            PadAssignment = new Dictionary<string, string>();
            PadAssignment.Add("yaw", "leftThumbX");
            PadAssignment.Add("throttle", "leftThumbY");
            PadAssignment.Add("roll", "rightThumbX");
            PadAssignment.Add("pitch", "rightThumbY");
            PadAssignment.Add("submerge", "leftTrigger");
            PadAssignment.Add("emerge", "rightTrigger");
        }
    }
}
