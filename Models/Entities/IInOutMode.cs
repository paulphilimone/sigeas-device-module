using System;
using System.Collections.Generic;

namespace mz.betainteractive.sigeas.Models.Entities {
    
    public partial class IInOutMode {
        public static string CHECK_IN_NAME = "Check In";
        public static string CHECK_OUT_NAME = "Check Out";
        public static string BREAK_OUT_NAME = "Break Out";
        public static string BREAK_IN_NAME = "Break In";
        public static string OVERTIME_IN_NAME = "Overtime In";
        public static string OVERTIME_OUT_NAME = "Overtime Out";
        public static int CHECK_IN_NUMBER = 0;
        public static int CHECK_OUT_NUMBER = 1;
        public static int BREAK_OUT_NUMBER = 2;
        public static int BREAK_IN_NUMBER = 3;
        public static int OVERTIME_IN_NUMBER = 4;
        public static int OVERTIME_OUT_NUMBER = 5;

        /* Properties */
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        /* Properties */

        public override string ToString() {
            return this.Name;
        }

        public override bool Equals(object obj) {
            if (obj is IInOutMode) {
                IInOutMode cast = (IInOutMode)obj;
                return this.Id == cast.Id;
            }

            return false;
        }

        public override int GetHashCode() {
            return 0;
        }
    }
}
