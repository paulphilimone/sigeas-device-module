using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.zdbx.models {

    [Serializable]
    public partial class ZDeviceUser {
        public int Id { get; set; }
        public int EnrollNumber { get; set; }
        public string CardNumber { get; set; }
        public virtual ZDevice Device { get; set; }
        public virtual ZUser User { get; set; }
    }

}
