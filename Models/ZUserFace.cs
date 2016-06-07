using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.zdbx.models {

    [Serializable]
    public partial class ZUserFace {
        public int Id { get; set; }
        public int FaceIndex { get; set; }
        public string TemplateData { get; set; }        
        public virtual ZUser User { get; set; }
    }
}
