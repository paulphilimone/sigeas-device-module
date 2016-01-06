using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.zdbx.models {

    [Serializable]
    public partial class ZUser {

        public int Id { get; set; }
        public int EnrollNumber { get; set; }
        public int Previlege { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CardNumber { get; set; }
        public bool Enabled { get; set; }               
        
        public virtual ZDeviceGroupTimezone DeviceGroupTimezone { get; set; }
        public virtual ZDeviceTimezone DeviceTimezone1 { get; set; }
        public virtual ZDeviceTimezone DeviceTimezone2 { get; set; }
        public virtual ZDeviceTimezone DeviceTimezone3 { get; set; }
        public virtual List<ZUserFingerprint> UserFingerprints { get; set; }        

        public ZUser() {
            this.UserFingerprints = new List<ZUserFingerprint>();
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return 0;
        }

        public bool HasNewFingerprints { get; set; }
        public bool HasNewCardNumber { get; set; }
    }
}
