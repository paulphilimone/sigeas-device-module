using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.zdbx.models {

    [Serializable]
    public partial class ZUserFingerprint {
        public int Id { get; set; }
        public int FingerIndex { get; set; }
        public string TemplateData { get; set; }        
        public virtual ZUser User { get; set; }
    }
}
