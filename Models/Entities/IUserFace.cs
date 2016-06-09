using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.Models.Entities {

    public partial class IUserFace {
        public int Id { get; set; }
        public int FaceIndex { get; set; }
        public string TemplateData { get; set; }
        
        public virtual IUser User { get; set; }
    }
}
