using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.Models.Entities {

    public partial class IUserFingerprint {
        public int Id { get; set; }
        public int FingerIndex { get; set; }
        public string TemplateData { get; set; }
        
        public virtual IUser User { get; set; }
    }
}
