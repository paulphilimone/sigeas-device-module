using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.zdbx.models {

    [Serializable]
    public partial class ZUserClock {

        public int Id { get; set; }
        public System.DateTime DateAndTime { get; set; }
        public string CorrectState { get; set; }
        public string Result { get; set; }                       
        public virtual ZDevice Device { get; set; }
        public int InOutMode { get; set; }
        public virtual ZUser User { get; set; }
        public int VerifyMode { get; set; }

    }
}
