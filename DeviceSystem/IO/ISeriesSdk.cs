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

        void GetAvaiableUsersID(out SortedSet<int> avaiables, out int refMaxUsers);

        bool StartEnroll(string enrollNumber, int fingerIndex);

        bool SetUserInfo(string enrollNumber, string cardNumber, string userName, string password, int privilege, bool enabled);

        bool SetUserTmp(string enrollNumber, int fingerIndex, string templateData);

        bool GetUserTmp(string enrollNumber, int fingerIndex, out string templateData, out int templateLength);

        bool DeleteUserTmp(string enrollNumber, int fingerIndex);

        bool GetUserTmps(string enrollNumber, out List<RawFingerprint> fingerprints);

        bool GetAllUserTmp(List<string> users, out List<RawFingerprint> fingerprints);

        bool DeleteUserInfo(string enrollNumber);

        void GetBasicUserInfo(out List<RawUser> users);

        #endregion

        #region Attendance Data

        void DownloadAttendanceData(out List<RawUserClock> clocks);

        bool DeleteAllAttendaceData();

        #endregion

    }
}
