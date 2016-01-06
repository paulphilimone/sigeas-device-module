using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.Models.Entities {

    public partial class IUserClock {

        public int Id { get; set; }
        public System.DateTime DateAndTime { get; set; }
        public string CorrectState { get; set; }
        public string Result { get; set; }
        
        public Nullable<System.DateTime> CreationDate { get; set; }        
        public Nullable<System.DateTime> UpdatedDate { get; set; }
                
        public virtual IDevice Device { get; set; }
        public virtual IInOutMode InOutMode { get; set; }
        //public virtual Beneficiario Beneficiario { get; set; }
        public virtual IVerifyMode VerifyMode { get; set; }
    }
}
