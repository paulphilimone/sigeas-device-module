using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.Models.Entities {

    public partial class IVerifyMode {
        public static string PASSWORD_NAME = "Password Verification";
        public static string FINGERPRINT_NAME = "Fingerprint Verification";
        public static string CARD_NAME = "Card Verification";
        public static int PASSWORD_NUMBER = 0;
        public static int FINGERPRINT_NUMBER = 1;
        public static int CARD_NUMBER = 2;
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }


        public override string ToString() {
            return this.Name;
        }

        public override bool Equals(object obj) {
            if (obj is IVerifyMode) {
                IVerifyMode cast = (IVerifyMode)obj;
                return this.Id == cast.Id;
            }

            return false;
        }

        public override int GetHashCode() {
            return 0;
        }
    }

}
