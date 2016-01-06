using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.zdbx.models {

    [Serializable]
    public partial class ZDeviceTimezone {

        public int Id { get; set; }
        public int TimezoneIndex { get; set; }
        public string Name { get; set; }
        public System.TimeSpan SundayIn { get; set; }
        public System.TimeSpan SundayOut { get; set; }
        public System.TimeSpan MondayIn { get; set; }
        public System.TimeSpan MondayOut { get; set; }
        public System.TimeSpan TuesdayIn { get; set; }
        public System.TimeSpan TuesdayOut { get; set; }
        public System.TimeSpan WednesdayIn { get; set; }
        public System.TimeSpan WednesdayOut { get; set; }
        public System.TimeSpan ThursdayIn { get; set; }
        public System.TimeSpan ThursdayOut { get; set; }
        public System.TimeSpan FridayIn { get; set; }
        public System.TimeSpan FridayOut { get; set; }
        public System.TimeSpan SaturdayIn { get; set; }
        public System.TimeSpan SaturdayOut { get; set; }
             
        public ZDeviceTimezone() {
        
        }
                
    }
}
