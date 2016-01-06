using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mz.betainteractive.sigeas.DeviceSystem {
    [Serializable]
    public class RawUser {
        public int Id { get; set; }
        public string EnrollNumber { get; set; }
        public int Previlege { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CardNumber { get; set; }
        public bool Enabled { get; set; }

        public int DeviceGroupTimezone { get; set; }
        public int DeviceTimezone1 { get; set; }
        public int DeviceTimezone2 { get; set; }
        public int DeviceTimezone3 { get; set; }        
    }
}
