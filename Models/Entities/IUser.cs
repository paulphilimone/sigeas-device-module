using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.Models.Entities {
    public abstract class IUser {

        public int Id { get; set; }
        public Nullable<int> EnrollNumber { get; set; }
        public Nullable<int> Previlege { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CardNumber { get; set; }
        public Nullable<bool> Enabled { get; set; }               
        
        public virtual IDeviceGroupTimezone DeviceGroupTimezone { get; set; }
        public virtual IDeviceTimezone DeviceTimezone1 { get; set; }
        public virtual IDeviceTimezone DeviceTimezone2 { get; set; }
        public virtual IDeviceTimezone DeviceTimezone3 { get; set; }
        public virtual List<IUserFingerprint> UserFingerprints { get; set; }
        public virtual List<IUserFace> UserFaces { get; set; }        

        public IUser() {
            this.UserFingerprints = new List<IUserFingerprint>();
            this.UserFaces = new List<IUserFace>();
        }
        
    }
}
