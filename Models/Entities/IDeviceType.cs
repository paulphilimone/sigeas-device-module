using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.Models.Entities {

    public partial class IDeviceType {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeNumber { get; set; }

        public override string ToString() {
            return this.Name;
        }

        public override bool Equals(object obj) {
            if (obj is IDeviceType) {
                IDeviceType cast = (IDeviceType)obj;
                return this.Id == cast.Id;
            }

            return false;
        }

        public override int GetHashCode() {
            return 0;
        }
    }
}
