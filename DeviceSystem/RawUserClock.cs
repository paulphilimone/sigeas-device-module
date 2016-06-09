using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mz.betainteractive.sigeas.DeviceSystem {
    [Serializable]
    public class RawUserClock {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EnrollNumber { get; set; }        
        public string DeviceSerialNumber { get; set; }

        public DateTime DateAndTime { get; set; }                      
        public int InOutMode { get; set; }        
        public int VerifyMode { get; set; }
    }
}
