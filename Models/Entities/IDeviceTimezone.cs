using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.Models.Entities {

    public partial class IDeviceTimezone {

        public int Id { get; set; }
        public int TimezoneIndex { get; set; }
        public string Name { get; set; }
        public Nullable<System.TimeSpan> SundayIn { get; set; }
        public Nullable<System.TimeSpan> SundayOut { get; set; }
        public Nullable<System.TimeSpan> MondayIn { get; set; }
        public Nullable<System.TimeSpan> MondayOut { get; set; }
        public Nullable<System.TimeSpan> TuesdayIn { get; set; }
        public Nullable<System.TimeSpan> TuesdayOut { get; set; }
        public Nullable<System.TimeSpan> WednesdayIn { get; set; }
        public Nullable<System.TimeSpan> WednesdayOut { get; set; }
        public Nullable<System.TimeSpan> ThursdayIn { get; set; }
        public Nullable<System.TimeSpan> ThursdayOut { get; set; }
        public Nullable<System.TimeSpan> FridayIn { get; set; }
        public Nullable<System.TimeSpan> FridayOut { get; set; }
        public Nullable<System.TimeSpan> SaturdayIn { get; set; }
        public Nullable<System.TimeSpan> SaturdayOut { get; set; }
             
        public IDeviceTimezone() {
        
        }

        public override string ToString() {
            return this.Name;
        }

        public override bool Equals(object obj) {
            if (obj is IDeviceTimezone) {
                IDeviceTimezone cast = (IDeviceTimezone)obj;
                return this.Id == cast.Id;
            }

            return false;
        }

        public override int GetHashCode() {
            return 0;
        }
    }
}
