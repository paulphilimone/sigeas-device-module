using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mz.betainteractive.sigeas.DeviceSystem {
    public class RawFingerprint {
        public int EnrollNumber;
        public int FingerIndex { get; set; }
        public string TemplateData { get; set; }

        public RawFingerprint(int enrollNumber, int fingerIndex, string tmpData) {
            this.EnrollNumber = enrollNumber;
            this.FingerIndex = fingerIndex;
            this.TemplateData = tmpData;
        }

        public override bool Equals(object obj) {
            if (obj is RawFingerprint) {
                var fg = obj as RawFingerprint;
                return this.EnrollNumber==fg.EnrollNumber && this.FingerIndex == fg.FingerIndex;
            } else {
                return false;
            }
        }

        public override int GetHashCode() {
            return 0;// base.GetHashCode();
        }

        public RawFingerprint Copy() {
            return new RawFingerprint(this.EnrollNumber, this.FingerIndex, this.TemplateData);
        }
    }
}
