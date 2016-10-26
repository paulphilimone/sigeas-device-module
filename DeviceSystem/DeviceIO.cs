using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mz.betainteractive.sigeas.Models.Entities;
using mz.betainteractive.sigeas.Models;
using mz.betainteractive.sigeas.DeviceSystem.IO;

namespace mz.betainteractive.sigeas.DeviceSystem {
    public class DeviceIO {
        /* For Now is for ZK Fingerprint Devices, B&W, iFace, TFT*/
        private IDevice device;
        private ISeriesSdk deviceSdk;

        public DeviceIO(IDevice device) {
            this.device = device;

            if (this.device.IsTFTMachine()) {
                this.deviceSdk = new TFTSeriesSdk(this.device);
            } else {
                this.deviceSdk = new BWSeriesSdk(this.device);
            }
        }
                
        #region Device System Information and Managment - Used on BW and TFT

        public void GetSerialNumber(out string serialNumber) {
            serialNumber = "";

            if (this.device.Connected) {
                this.device.BiometricSDK.GetSerialNumber(1, out serialNumber);
            }
        }

        public void GetDeviceStatus(int status, ref int value) {

            if (this.device.Connected) {
                this.device.BiometricSDK.GetDeviceStatus(1, status, ref value);
            }
        }

        public void GetVendor(ref string vendor) {
            if (this.device.Connected) {
                this.device.BiometricSDK.GetVendor(ref vendor);
            }
        }

        public void GetProductCode(out string productCode) {
            productCode = "";
            if (this.device.Connected) {
                this.device.BiometricSDK.GetProductCode(1, out productCode);
            }
        }

        public void GetFirmwareVersion(ref string firmwareVersion) {
            if (this.device.Connected) {
                this.device.BiometricSDK.GetFirmwareVersion(1, ref firmwareVersion);
            }
        }

        public bool IsTFTMachine() {            /*if false is a Black n White Machine*/
            return this.device.IsTFTMachine();            
        }

        public string GetFPVersion() {
            return device.IsZKFinger10() ? "ZKFinger10.0" : "ZKFinger9.0";
        }

        public void GetLastError(out string error){
            int idwErrorCode = -1;
            this.device.BiometricSDK.GetLastError(ref idwErrorCode);
            error = idwErrorCode.ToString();
        }

        public void CancelOperation() {
            if (this.device.Connected) {
                this.device.BiometricSDK.CancelOperation();
            }
        }

        public void BlockDevice() {
            if (this.device.Connected) {
                this.device.BiometricSDK.CancelOperation();
                this.device.BiometricSDK.EnableDevice(1, false);
            }
        }

        public void StartIdentify() {
            if (this.device.Connected) {
                this.device.BiometricSDK.EnableDevice(1, true);
                this.device.BiometricSDK.StartIdentify();
            }
        }

        public void RefreshData() {
            if (this.device.Connected) {
                this.device.BiometricSDK.RefreshData(1);
            }
        }
        
        #endregion       
        
        #region events - Used on BW and TFT

        public void AddOnEnrollFinger(zkemkeeper._IZKEMEvents_OnEnrollFingerEventHandler onEnrollFinger) {
            this.device.BiometricSDK.OnEnrollFinger += onEnrollFinger;
        }

        public void RemoveOnEnrollFinger(zkemkeeper._IZKEMEvents_OnEnrollFingerEventHandler onEnrollFinger) {
            this.device.BiometricSDK.OnEnrollFinger -= onEnrollFinger;
        }

        public void AddOnFingerFeature(zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler onFingerFeature) {
            this.device.BiometricSDK.OnFingerFeature += onFingerFeature;
        }

        public void RemoveOnFingerFeature(zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler onFingerFeature) {
            this.device.BiometricSDK.OnFingerFeature -= onFingerFeature;
        }

        public void AddOnHIDNum(zkemkeeper._IZKEMEvents_OnHIDNumEventHandler onHidNum) {
            this.device.BiometricSDK.OnHIDNum += onHidNum;
        }

        public void RemoveOnHIDNum(zkemkeeper._IZKEMEvents_OnHIDNumEventHandler onHidNum) {
            this.device.BiometricSDK.OnHIDNum -= onHidNum;
        }

        #endregion

        #region Users Information

        public void GetAvaiableUsersID(out SortedSet<int> avaiables, out int refMaxUsers) {
            this.deviceSdk.GetAvaiableUsersID(out avaiables, out refMaxUsers);
        }

        public bool StartEnroll(string enrollNumber, int fingerIndex) {
            return this.deviceSdk.StartEnroll(enrollNumber, fingerIndex);
        }

        public bool SetUserInfo(string enrollNumber, string cardNumber, string userName, string password, int privilege, bool enabled) {
            return this.deviceSdk.SetUserInfo(enrollNumber, cardNumber, userName, password, privilege, enabled);
        }

        public bool SetUserTmp(string enrollNumber, int fingerIndex, string templateData) {
            return this.deviceSdk.SetUserTmp(enrollNumber, fingerIndex, templateData);
        }

        public bool GetUserTmp(string enrollNumber, int fingerIndex, out string templateData, out int templateLength) {
            return this.deviceSdk.GetUserTmp(enrollNumber, fingerIndex, out templateData, out templateLength);
        }

        public bool DeleteUserTmp(string enrollNumber, int fingerIndex) {
            return this.deviceSdk.DeleteUserTmp(enrollNumber, fingerIndex);
        }

        public bool GetUserTmps(string enrollNumber, out List<RawFingerprint> fingerprints) {
            return this.deviceSdk.GetUserTmps(enrollNumber, out fingerprints);
        }

        public bool GetAllUserTmp(List<string> users, out List<RawFingerprint> fingerprints) {
            return this.deviceSdk.GetAllUserTmp(users, out fingerprints);
        }

        public bool DeleteUserInfo(string enrollNumber) {
            return this.deviceSdk.DeleteUserInfo(enrollNumber);
        }

        public void GetBasicUserInfo(out List<RawUser> users) {
            this.deviceSdk.GetBasicUserInfo(out users);
        }

        #endregion

        #region Attendance Data

        public void DownloadAttendanceData(out List<RawUserClock> clocks) {
            this.deviceSdk.DownloadAttendanceData(out clocks);
        }

        public bool DeleteAllAttendaceData() {
            return this.deviceSdk.DeleteAllAttendaceData();
        }

        public bool DeleteAllUserData() {
            return this.deviceSdk.DeleteAllUserData();
        }

        public bool DeleteAllData() {
            return this.deviceSdk.DeleteAllData();
        }

        #endregion
    }
}
