using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mz.betainteractive.sigeas.DeviceSystem.IO {
    class BWSeriesSdk : ISeriesSdk {

        private Models.Entities.IDevice device;

        public BWSeriesSdk(Models.Entities.IDevice iDevice) {
            this.device = iDevice;
        }

        #region Users Information

        public void GetAvaiableUsersID(out SortedSet<int> avaiables, out int refMaxUsers) {

            if (this.device.Connected == false) {
                avaiables = new SortedSet<int>();
                refMaxUsers = -1;
                return;
            }

            int maxUsers = 0;
            int enrollNumber = -1;				// User ID      			
            string name = "";
            string password = "";
            int privilege = 0;
            bool enabled = false;

            List<int> users = new List<int>();
            avaiables = new SortedSet<int>();

            bool readed = this.device.BiometricSDK.ReadAllUserID(1);
            this.device.BiometricSDK.GetDeviceStatus(1, 8, ref maxUsers);

            refMaxUsers = maxUsers;

            while (this.device.BiometricSDK.GetAllUserInfo(1, ref enrollNumber, ref name, ref password, ref privilege, ref enabled)) {
                users.Add(enrollNumber);
            }

            for (int i = 1; i <= maxUsers; i++) {
                if (!users.Contains(i)) {
                    avaiables.Add(i);
                }
            }

        }

        public bool StartEnroll(string enrollNumber, int fingerIndex) {
            if (this.device.Connected) {
                return this.device.BiometricSDK.StartEnroll(Int32.Parse(enrollNumber), fingerIndex);
            }

            return false;
        }

        public bool SetUserInfo(string enrollNumber, string cardNumber, string userName, string password, int privilege, bool enabled) {
            if (this.device.Connected) {

                if (cardNumber != null && cardNumber.Length > 0) {
                    this.device.BiometricSDK.SetStrCardNumber(cardNumber);
                }

                bool resul = this.device.BiometricSDK.SetUserInfo(1, Int32.Parse(enrollNumber), userName, password, privilege, enabled);

                return resul;
            }

            return false;
        }

        public bool SetUserTmp(string enrollNumber, int fingerIndex, string templateData) {
            if (this.device.Connected) {
                if (this.device.IsZKFinger10()) {
                    return this.device.BiometricSDK.SetUserTmpExStr(1, enrollNumber, fingerIndex, 1, templateData);
                } else {
                    return this.device.BiometricSDK.SetUserTmpStr(1, Int32.Parse(enrollNumber), fingerIndex, templateData);
                }                
            }
            return false;
        }

        public bool GetUserTmp(string enrollNumber, int fingerIndex, out string templateData, out int templateLength) {
            if (this.device.Connected) {
                bool resul = false;
                int flag = 0;

                if (this.device.IsZKFinger10()) {
                    resul = this.device.BiometricSDK.GetUserTmpExStr(1, enrollNumber, fingerIndex, out flag, out templateData, out templateLength);
                } else {
                    string tmpData = "";
                    int tmpLen = -1;

                    resul = this.device.BiometricSDK.GetUserTmpStr(1, Int32.Parse(enrollNumber), fingerIndex, ref tmpData, ref tmpLen);

                    templateData = tmpData;
                    templateLength = tmpLen;
                }

                return resul;
            }

            templateData = null;
            templateLength = 0;

            return false;
        }

        public bool DeleteUserTmp(string enrollNumber, int fingerIndex) {
            if (this.device.Connected) {
                return this.device.BiometricSDK.DelUserTmp(1, Int32.Parse(enrollNumber), fingerIndex);
            }

            return false;
        }

        public bool GetUserTmps(string enrollNumber, out List<RawFingerprint> fingerprints) {

            fingerprints = new List<RawFingerprint>();

            if (this.device.Connected) {

                string templateData = "";
                int templateLength = 0;
                int flag = 0;
                int enrollNumberInt = Int32.Parse(enrollNumber);

                this.device.BiometricSDK.EnableDevice(1, false);
                this.device.BiometricSDK.ReadAllTemplate(1);

                for (int fingerIndex = 0; fingerIndex < 9; fingerIndex++) {
                    if (this.device.IsZKFinger10()) {
                        if (this.device.BiometricSDK.GetUserTmpExStr(1, enrollNumber, fingerIndex, out flag, out templateData, out templateLength)) {
                            fingerprints.Add(new RawFingerprint(Int32.Parse(enrollNumber), fingerIndex, templateData));
                        }
                    } else {
                        if (this.device.BiometricSDK.GetUserTmpStr(1, enrollNumberInt, fingerIndex, ref templateData, ref templateLength)) {
                            fingerprints.Add(new RawFingerprint(enrollNumberInt, fingerIndex, templateData));
                        }
                    }                    
                }

                this.device.BiometricSDK.EnableDevice(1, true);
            }

            return true;
        }

        public bool GetAllUserTmp(List<string> users, out List<RawFingerprint> fingerprints) {

            fingerprints = new List<RawFingerprint>();

            if (this.device.Connected) {

                string templateData = "";
                int templateLength = 0;
                int flag = 0;
                
                this.device.BiometricSDK.EnableDevice(1, false);
                this.device.BiometricSDK.ReadAllTemplate(1);

                foreach (var enrollNumber in users) {
                    int enrollNumberInt = Int32.Parse(enrollNumber);

                    for (int fingerIndex = 0; fingerIndex < 9; fingerIndex++) {
                        if (this.device.IsZKFinger10()) {
                            if (this.device.BiometricSDK.GetUserTmpExStr(1, enrollNumber, fingerIndex, out flag, out templateData, out templateLength)) {
                                fingerprints.Add(new RawFingerprint(enrollNumberInt, fingerIndex, templateData));
                            }
                        } else {
                            if (this.device.BiometricSDK.GetUserTmpStr(1, enrollNumberInt, fingerIndex, ref templateData, ref templateLength)) {
                                fingerprints.Add(new RawFingerprint(enrollNumberInt, fingerIndex, templateData));
                            }
                        }
                    }
                }

                this.device.BiometricSDK.EnableDevice(1, true);

            }

            return true;
        }

        public bool DeleteUserInfo(string enrollNumber) {
            if (this.device.Connected) {
                return this.device.BiometricSDK.DeleteEnrollData(1, Int32.Parse(enrollNumber), 1, 12);
            }

            return false;
        }

        public void GetBasicUserInfo(out List<RawUser> users) {
            users = new List<RawUser>();

            if (!this.device.Connected) return;

            int enrollNumber = 0;				// User ID      			
            string cardNumber = "";
            string name = "";
            string password = "";
            int privilege = 0;
            bool enabled = false;

            bool readed = this.device.BiometricSDK.ReadAllUserID(1);

            while (this.device.BiometricSDK.GetAllUserInfo(1, ref enrollNumber, ref name, ref password, ref privilege, ref enabled)) {
                this.device.BiometricSDK.GetStrCardNumber(out cardNumber);

                RawUser ruser = new RawUser {
                    EnrollNumber = enrollNumber.ToString(),
                    CardNumber = cardNumber,
                    UserName = name,
                    Password = password,
                    Previlege = privilege,
                    Enabled = enabled
                };

                users.Add(ruser);
            }

        }

        #endregion

        #region Attendance Data

        public void DownloadAttendanceData(out List<RawUserClock> clocks) {
            clocks = new List<RawUserClock>();

            string srEnrollNumber = "";            
            int dwEnrollNumber = -1;
            int dwVerifyMode = -1;
            int dwInOutMode = -1;
            int dwYear = -1, dwMonth = -1, dwDay = -1, dwHour = -1, dwMinute = -1, dwSecond = -1;
            int dwWorkcode = -1;
            int dwReserved = -1;
            int id = 1;
            string serialNumber = "";

            device.BiometricSDK.EnableDevice(1, false);

            serialNumber = device.SerialNumber;

            Console.WriteLine("Reading all data " + serialNumber);
            device.BiometricSDK.ReadGeneralLogData(1);


            while (device.BiometricSDK.GetGeneralExtLogData(1, ref dwEnrollNumber, ref dwVerifyMode, ref dwInOutMode, ref dwYear, ref dwMonth, ref dwDay, ref dwHour, ref dwMinute, ref dwSecond, ref dwWorkcode, ref dwReserved)) {

                try {

                    srEnrollNumber = dwEnrollNumber.ToString();
                    DateTime dateAndTime = new DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond);

                    RawUserClock userClock = new RawUserClock();
                    userClock.Id = id;
                    userClock.EnrollNumber = srEnrollNumber;
                    userClock.DeviceSerialNumber = serialNumber;
                    userClock.VerifyMode = dwVerifyMode;
                    userClock.InOutMode = dwInOutMode;
                    userClock.DateAndTime = dateAndTime;

                    clocks.Add(userClock);
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
                id++;
            }

            device.BiometricSDK.EnableDevice(1, true);//enable the device

            Console.WriteLine("Finished reading");
        }

        public bool DeleteAllAttendaceData() {
            device.BiometricSDK.EnableDevice(1, false);//disable the device

            if (device.BiometricSDK.ClearGLog(1)) {
                device.BiometricSDK.RefreshData(1);
                return true;
            }

            device.BiometricSDK.EnableDevice(1, true);//enable the device

            return false;
        }

        public bool DeleteAllUserData() {
            bool r1 = device.BiometricSDK.ClearData(1, 1);
            bool r2 = device.BiometricSDK.ClearData(1, 5);

            return r1 && r2;
        }

        public bool DeleteAllData() {
            return device.BiometricSDK.ClearKeeperData(1);            
        }

        #endregion
    }
}
