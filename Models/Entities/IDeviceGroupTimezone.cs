using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.Models.Entities {
    public partial class IDeviceGroupTimezone {

        public int Id { get; set; }
        public int GroupIndex { get; set; }
        public string Name { get; set; }
        public Nullable<int> ValidHolyday { get; set; }
        public Nullable<int> VerifyStyle { get; set; }        

        public virtual IDeviceTimezone DeviceTimezone1 { get; set; }
        public virtual IDeviceTimezone DeviceTimezone2 { get; set; }
        public virtual IDeviceTimezone DeviceTimezone3 { get; set; }
        public virtual List<IUser> Users { get; set; }
        
        public IDeviceGroupTimezone() {
            this.Users = new List<IUser>();
        }

        public override string ToString() {
            return this.Name;
        }

        public override bool Equals(object obj) {
            if (obj is IDeviceGroupTimezone) {
                IDeviceGroupTimezone cast = (IDeviceGroupTimezone)obj;
                return this.Id == cast.Id;
            }

            return false;
        }

        public override int GetHashCode() {
            return 0;
        }
        
    }
}
