using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mz.betainteractive.sigeas.DeviceSystem.IO {
    /**
     * This interface has the methods that can be used on TFT or B&W specific devices, 
     **/
    interface ISeriesSdk {

        #region Users Information

        public void GetAvaiableUsersID(out SortedSet<int> avaiables, out int refMaxUsers);

        public bool StartEnroll(string enrollNumber, int fingerIndex);

        public bool SetUserInfo(string enrollNumber, string cardNumber, string userName, string password, int privilege, bool enabled);

        public bool SetUserTmp(string enrollNumber, int fingerIndex, string templateData);

        public bool GetUserTmp(string enrollNumber, int fingerIndex, out string templateData, out int templateLength);

        public bool DeleteUserTmp(string enrollNumber, int fingerIndex);

        public bool GetUserTmps(string enrollNumber, out List<RawFingerprint> fingerprints);

        public bool GetAllUserTmp(List<string> users, out List<RawFingerprint> fingerprints);

        public bool DeleteUserInfo(string enrollNumber);

        public void GetBasicUserInfo(out List<RawUser> users);

        #endregion

        #region Attendance Data

        public void DownloadAttendanceData(out List<RawUserClock> clocks);

        public bool DeleteAllAttendaceData();

        #endregion

    }
}
