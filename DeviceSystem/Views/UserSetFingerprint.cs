using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using mz.betainteractive.sigeas;
using mz.betainteractive.sigeas.Models.Entities;


namespace mz.betainteractive.sigeas.DeviceSystem.Views {
    public partial class UserSetFingerprint : Form {

        private IDevice device;
        private Polygon[] fing = new Polygon[10];
        private int onHoverIndex = -1;
        private bool[] drawFinger = new bool[10];
        
        private Pen regstdPen = Pens.SeaGreen;
        private Pen toregPen = Pens.Fuchsia;
        private Brush regstdBrush = Brushes.SeaGreen;
        private Brush toregBrush = Brushes.Fuchsia;

        private bool isEnrolling;
        private bool selectedFinger;
        private int selectedIndex;
        private int verifyTimes;
        public int UserIdToEnroll = -1;
        private bool hasEnrolled;
        /*
         * Usado para caso de registrar e obter fingerprints de dispositivos temporarios
         */
        public bool AfterEnrollDelete;

        private zkemkeeper.CZKEMClass BioAccess;

        private FingerprintResult registrationResult;

        public UserSetFingerprint() {
            
            for (int i = 0; i < fing.Length; i++) {
                fing[i] = new Polygon();                
            }

            initPolyFingers();            
            InitializeComponent();

            registrationResult = new FingerprintResult();            
        }        

        public void StartForm(IDevice dev) {

            //var SDB = FrmMainProgram.SystemDatabase;
            this.device = dev;
                        
            if (this.device == null) {
                MessageBox.Show(this, "Não existem dispositivos conectados ao sistema!\nConecte um dispositivo primeiro");
                return;
            }

            if (this.device.Connected == false){
                MessageBox.Show(this, "O dispositivo selecionado não está conectado ao sistema!\nConecte o dispositivo primeiro");
                return;
            }

            BioAccess = this.device.BiometricSDK;
            AddListeners();
            
            //this.Visible = true;
            this.btOk.Enabled = false;

            labMsg.Text = "Sistema desligado";
            selectedFinger = false;
                                                
            if (UserIdToEnroll == -1){
                MessageBox.Show(this, "Selecione o usuário primeiro!!!", "Informação");
                this.Hide();
                return;
            }

            RefreshFingerPrints();

            labMsg.Text = "Selecione o dedo";

            ShowDialog();
            //this.Visible = true;
        }

        private void ClearFingerPrints() {
            for (int i = 0; i < drawFinger.Length; i++) {
                drawFinger[i] = false;
            }
            registrationResult.GetFingerPrints().Clear();
        }

        private void RefreshFingerPrints() {
            
            ClearFingerPrints();

            BioAccess.RefreshData(1);
            BioAccess.EnableDevice(1, false);            
            BioAccess.ReadAllUserID(1);
            BioAccess.ReadAllTemplate(1);
            
            int machineNumber = 1;
            string sEnroll = UserIdToEnroll.ToString();
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;

            bool resul = BioAccess.SSR_GetUserInfo(machineNumber, sEnroll, out sName, out sPassword, out iPrivilege, out bEnabled);

            //Console.WriteLine("Data: "+resul);
            if (resul) {
                registrationResult.EnrolledUserId = UserIdToEnroll;
            }

            for (int fingerIndex = 0; fingerIndex < drawFinger.Length; fingerIndex++) {

                string TmpData = "";
                int TmpLength = 0;
                int Flag = -1;

                if (BioAccess.SSR_GetUserTmpStr(1, sEnroll, fingerIndex, out TmpData, out TmpLength)) {
                    
                    registrationResult.Add(fingerIndex, TmpData);
                    registrationResult.EnrolledUserId = UserIdToEnroll;

                    drawFinger[fingerIndex] = true;
                } else {
                    drawFinger[fingerIndex] = false;
                }
            }
                        
            BioAccess.EnableDevice(1, true);
        }
                
        private void DeleteUserAndFingerPrint(int EnrollNumber) {
            if (AfterEnrollDelete) {
                bool delete = BioAccess.SSR_DeleteEnrollData(1, EnrollNumber.ToString(), 12);
                //Console.WriteLine("dELETE: " + delete);
                BioAccess.RefreshData(1);
            }
        }

        private void GravarFingerprintNoResult(int EnrollNumber, int FingerIndex) {            
            string TmpData = "";
            int TmpLength = 0;                        

            int machineNumber = 1;
            string sEnroll = UserIdToEnroll.ToString();
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;
            int Flag = 0;

            //BioAccess.RefreshData(1);
            BioAccess.EnableDevice(1, false);            
            BioAccess.ReadAllTemplate(1);

            bool resul = BioAccess.SSR_GetUserInfo(machineNumber, sEnroll, out sName, out sPassword, out iPrivilege, out bEnabled);
            Console.WriteLine("GetUserInfo: " + resul);

            if (resul) {
                bool readed = BioAccess.SSR_GetUserTmpStr(1, EnrollNumber.ToString(), FingerIndex, out TmpData, out TmpLength);

                Console.WriteLine("GetUserTmpExStr: "+readed+", tmpData: "+TmpData);

                if (readed) {
                    Fingerprint fingerPrint = new Fingerprint(FingerIndex, TmpData);
                    registrationResult.Add(fingerPrint);
                    registrationResult.EnrolledUserId = EnrollNumber;
                }
            }

            BioAccess.EnableDevice(1, true);
        }
                
        public FingerprintResult GetCurrentFingerprintResult() {
            return registrationResult;
        }
        
        public void GetCurrentFingerprints(out List<Fingerprint> fingers) {
            fingers = new List<Fingerprint>();

            foreach (Fingerprint fg in this.registrationResult.GetFingerPrints()) {
                fingers.Add(fg.Copy());
            }
        }

        private void ExecuteEnrollment() {            
            verifyTimes = 0;                       

            BioAccess.CancelOperation();
            BioAccess.SSR_DelUserTmpExt(1, UserIdToEnroll.ToString(), selectedIndex);         
            isEnrolling = true;

            if (BioAccess.StartEnrollEx(UserIdToEnroll.ToString(), selectedIndex, 1)) {                
                labMsg.Text = "Coloque o dedo";                
            } else {
                int idwErrorCode = -1;
                BioAccess.GetLastError(ref idwErrorCode);
                MessageBox.Show(this, "Ocorreu um falha, não é possivel registrar, Error nº=" + idwErrorCode.ToString(), "Error");
                isEnrolling = false;
            }                        
        }

        private void RemoveListeners() {
            BioAccess.OnEnrollFinger -= new zkemkeeper._IZKEMEvents_OnEnrollFingerEventHandler(BioAccess_OnEnrollFinger);            
            BioAccess.OnFingerFeature -= new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(BioAccess_OnFingerFeature);
        }

        private void AddListeners() {
            BioAccess.OnEnrollFinger += new zkemkeeper._IZKEMEvents_OnEnrollFingerEventHandler(BioAccess_OnEnrollFinger);            
            BioAccess.OnFingerFeature += new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(BioAccess_OnFingerFeature);
        }

        private void BioAccess_OnFingerFeature(int Score) {
            if (isEnrolling) {
                verifyTimes++;

                if (verifyTimes == 1) {
                    labMsg.Text = "Confirme a segunda vez";
                }
                if (verifyTimes == 2) {
                    labMsg.Text = "Confirme a terceira vez";
                }
            }
        }

        private void BioAccess_OnEnrollFinger(int EnrollNumber, int FingerIndex, int ActionResult, int TemplateLength) {
            //In the finish after enroll            
            if (isEnrolling) {
                bool isOk = false;
                BioAccess.StartIdentify();

                if (ActionResult == 0) {
                    labMsg.Text = "Registrado com sucesso";
                    isOk = true;
                    hasEnrolled = true;
                } else {

                    labMsg.Text = "Ocorreu erro no registro";
                }

                //RefreshFingerPrints();                
                isEnrolling = false;
                btOk.Enabled = isOk;
                this.Update();
            }
        }

        private void initPolyFingers() {
            int mx = 128;

            fing[0].Add(18, 18);
            fing[0].Add(16, 19);
            fing[0].Add(13, 22);
            fing[0].Add(12, 25);
            fing[0].Add(12, 35);
            fing[0].Add(12, 39);
            fing[0].Add(11, 51);
            fing[0].Add(11, 60);
            fing[0].Add(14, 63);
            fing[0].Add(16, 64);
            fing[0].Add(17, 65);
            fing[0].Add(22, 65);
            fing[0].Add(23, 64);
            fing[0].Add(25, 63);
            fing[0].Add(26, 62);
            fing[0].Add(27, 60);
            fing[0].Add(28, 58);
            fing[0].Add(28, 24);
            fing[0].Add(27, 22);
            fing[0].Add(24, 19);
            fing[0].Add(22, 18);
            
            fing[1].Add(new Point(44, 4));
            fing[1].Add(new Point(41, 7));
            fing[1].Add(new Point(41, 8));
            fing[1].Add(new Point(40, 9));
            fing[1].Add(new Point(40, 14));
            fing[1].Add(new Point(39, 15));
            fing[1].Add(new Point(39, 21));
            fing[1].Add(new Point(38, 22));
            fing[1].Add(new Point(38, 27));
            fing[1].Add(new Point(37, 28));
            fing[1].Add(new Point(37, 34));
            fing[1].Add(new Point(36, 35));
            fing[1].Add(new Point(36, 40));
            fing[1].Add(new Point(35, 41));
            fing[1].Add(new Point(35, 47));
            fing[1].Add(new Point(34, 48));
            fing[1].Add(new Point(34, 53));
            fing[1].Add(new Point(33, 54));
            fing[1].Add(new Point(33, 59));
            fing[1].Add(new Point(34, 60));
            fing[1].Add(new Point(34, 61));
            fing[1].Add(new Point(37, 64));
            fing[1].Add(new Point(39, 64));
            fing[1].Add(new Point(39, 65));
            fing[1].Add(new Point(44, 65));
            fing[1].Add(new Point(45, 64));
            fing[1].Add(new Point(46, 64));
            fing[1].Add(new Point(49, 61));
            fing[1].Add(new Point(49, 59));
            fing[1].Add(new Point(50, 59));
            fing[1].Add(new Point(50, 52));
            fing[1].Add(new Point(51, 51));
            fing[1].Add(new Point(51, 43));
            fing[1].Add(new Point(52, 42));
            fing[1].Add(new Point(52, 34));
            fing[1].Add(new Point(53, 33));
            fing[1].Add(new Point(53, 25));
            fing[1].Add(new Point(54, 24));
            fing[1].Add(new Point(54, 16));
            fing[1].Add(new Point(55, 15));
            fing[1].Add(new Point(55, 8));
            fing[1].Add(new Point(52, 5));
            fing[1].Add(new Point(51, 5));
            fing[1].Add(new Point(50, 4));

            fing[2].Add(new Point(72, 4));
            fing[2].Add(new Point(69, 7));
            fing[2].Add(new Point(68, 8));
            fing[2].Add(new Point(68, 11));
            fing[2].Add(new Point(67, 12));
            fing[2].Add(new Point(67, 15));
            fing[2].Add(new Point(66, 16));
            fing[2].Add(new Point(66, 19));
            fing[2].Add(new Point(65, 20));
            fing[2].Add(new Point(65, 22));
            fing[2].Add(new Point(64, 23));
            fing[2].Add(new Point(64, 26));
            fing[2].Add(new Point(63, 27));
            fing[2].Add(new Point(63, 30));
            fing[2].Add(new Point(62, 31));
            fing[2].Add(new Point(62, 34));
            fing[2].Add(new Point(61, 35));
            fing[2].Add(new Point(61, 38));
            fing[2].Add(new Point(60, 39));
            fing[2].Add(new Point(60, 41));
            fing[2].Add(new Point(59, 42));
            fing[2].Add(new Point(59, 45));
            fing[2].Add(new Point(58, 46));
            fing[2].Add(new Point(58, 49));
            fing[2].Add(new Point(57, 50));
            fing[2].Add(new Point(57, 53));
            fing[2].Add(new Point(56, 54));
            fing[2].Add(new Point(56, 56));
            fing[2].Add(new Point(55, 57));
            fing[2].Add(new Point(55, 65));
            fing[2].Add(new Point(59, 69));
            fing[2].Add(new Point(60, 69));
            fing[2].Add(new Point(61, 70));
            fing[2].Add(new Point(65, 70));
            fing[2].Add(new Point(66, 69));
            fing[2].Add(new Point(67, 69));
            fing[2].Add(new Point(70, 66));
            fing[2].Add(new Point(70, 65));
            fing[2].Add(new Point(71, 64));
            fing[2].Add(new Point(71, 61));
            fing[2].Add(new Point(72, 60));
            fing[2].Add(new Point(72, 56));
            fing[2].Add(new Point(73, 55));
            fing[2].Add(new Point(73, 52));
            fing[2].Add(new Point(74, 51));
            fing[2].Add(new Point(74, 47));
            fing[2].Add(new Point(75, 46));
            fing[2].Add(new Point(75, 43));
            fing[2].Add(new Point(76, 42));
            fing[2].Add(new Point(76, 38));
            fing[2].Add(new Point(77, 37));
            fing[2].Add(new Point(77, 34));
            fing[2].Add(new Point(78, 33));
            fing[2].Add(new Point(78, 29));
            fing[2].Add(new Point(79, 28));
            fing[2].Add(new Point(79, 24));
            fing[2].Add(new Point(80, 23));
            fing[2].Add(new Point(80, 20));
            fing[2].Add(new Point(81, 19));
            fing[2].Add(new Point(81, 15));
            fing[2].Add(new Point(82, 14));
            fing[2].Add(new Point(82, 8));
            fing[2].Add(new Point(79, 5));
            fing[2].Add(new Point(78, 5));
            fing[2].Add(new Point(77, 4));

            fing[3].Add(new Point(97, 21));
            fing[3].Add(new Point(96, 22));
            fing[3].Add(new Point(95, 22));
            fing[3].Add(new Point(92, 25));
            fing[3].Add(new Point(92, 27));
            fing[3].Add(new Point(91, 28));
            fing[3].Add(new Point(91, 30));
            fing[3].Add(new Point(90, 30));
            fing[3].Add(new Point(90, 32));
            fing[3].Add(new Point(90, 33));
            fing[3].Add(new Point(88, 35));
            fing[3].Add(new Point(88, 37));
            fing[3].Add(new Point(87, 38));
            fing[3].Add(new Point(87, 40));
            fing[3].Add(new Point(86, 40));
            fing[3].Add(new Point(86, 42));
            fing[3].Add(new Point(85, 43));
            fing[3].Add(new Point(85, 45));
            fing[3].Add(new Point(84, 45));
            fing[3].Add(new Point(84, 48));
            fing[3].Add(new Point(83, 48));
            fing[3].Add(new Point(83, 50));
            fing[3].Add(new Point(82, 50));
            fing[3].Add(new Point(82, 52));
            fing[3].Add(new Point(81, 52));
            fing[3].Add(new Point(81, 55));
            fing[3].Add(new Point(80, 55));
            fing[3].Add(new Point(80, 57));
            fing[3].Add(new Point(79, 57));
            fing[3].Add(new Point(79, 60));
            fing[3].Add(new Point(78, 60));
            fing[3].Add(new Point(78, 62));
            fing[3].Add(new Point(77, 62));
            fing[3].Add(new Point(77, 65));
            fing[3].Add(new Point(76, 65));
            fing[3].Add(new Point(76, 67));
            fing[3].Add(new Point(75, 67));
            fing[3].Add(new Point(75, 73));
            fing[3].Add(new Point(76, 73));
            fing[3].Add(new Point(76, 75));
            fing[3].Add(new Point(79, 78));
            fing[3].Add(new Point(81, 78));
            fing[3].Add(new Point(82, 79));
            fing[3].Add(new Point(85, 79));
            fing[3].Add(new Point(85, 78));
            fing[3].Add(new Point(87, 78));
            fing[3].Add(new Point(90, 75));
            fing[3].Add(new Point(90, 73));
            fing[3].Add(new Point(91, 73));
            fing[3].Add(new Point(91, 70));
            fing[3].Add(new Point(92, 70));
            fing[3].Add(new Point(92, 67));
            fing[3].Add(new Point(93, 67));
            fing[3].Add(new Point(93, 65));
            fing[3].Add(new Point(94, 65));
            fing[3].Add(new Point(94, 62));
            fing[3].Add(new Point(95, 62));
            fing[3].Add(new Point(95, 59));
            fing[3].Add(new Point(96, 59));
            fing[3].Add(new Point(96, 56));
            fing[3].Add(new Point(97, 56));
            fing[3].Add(new Point(97, 53));
            fing[3].Add(new Point(98, 51));
            fing[3].Add(new Point(99, 51));
            fing[3].Add(new Point(99, 48));
            fing[3].Add(new Point(100, 48));
            fing[3].Add(new Point(100, 45));
            fing[3].Add(new Point(101, 45));
            fing[3].Add(new Point(101, 42));
            fing[3].Add(new Point(102, 42));
            fing[3].Add(new Point(102, 39));
            fing[3].Add(new Point(103, 39));
            fing[3].Add(new Point(103, 37));
            fing[3].Add(new Point(104, 37));
            fing[3].Add(new Point(104, 34));
            fing[3].Add(new Point(105, 34));
            fing[3].Add(new Point(105, 31));
            fing[3].Add(new Point(106, 31));
            fing[3].Add(new Point(106, 26));
            fing[3].Add(new Point(102, 22));
            fing[3].Add(new Point(101, 22));
            fing[3].Add(new Point(100, 21));

            fing[4].Add(new Point(113, 97));
            fing[4].Add(new Point(111, 99));
            fing[4].Add(new Point(110, 99));
            fing[4].Add(new Point(109, 100));
            fing[4].Add(new Point(108, 100));
            fing[4].Add(new Point(108, 101));
            fing[4].Add(new Point(106, 101));
            fing[4].Add(new Point(105, 102));
            fing[4].Add(new Point(104, 102));
            fing[4].Add(new Point(103, 103));
            fing[4].Add(new Point(102, 104));
            fing[4].Add(new Point(101, 104));
            fing[4].Add(new Point(100, 105));
            fing[4].Add(new Point(99, 105));
            fing[4].Add(new Point(98, 106));
            fing[4].Add(new Point(97, 106));
            fing[4].Add(new Point(96, 107));
            fing[4].Add(new Point(95, 108));
            fing[4].Add(new Point(94, 108));
            fing[4].Add(new Point(93, 109));
            fing[4].Add(new Point(92, 109));
            fing[4].Add(new Point(91, 110));
            fing[4].Add(new Point(90, 110));
            fing[4].Add(new Point(90, 111));
            fing[4].Add(new Point(88, 111));
            fing[4].Add(new Point(83, 116));
            fing[4].Add(new Point(83, 124));
            fing[4].Add(new Point(88, 129));
            fing[4].Add(new Point(90, 129));
            fing[4].Add(new Point(90, 130));
            fing[4].Add(new Point(95, 130));
            fing[4].Add(new Point(96, 129));
            fing[4].Add(new Point(97, 129));
            fing[4].Add(new Point(100, 126));
            fing[4].Add(new Point(101, 126));
            fing[4].Add(new Point(106, 121));
            fing[4].Add(new Point(107, 121));
            fing[4].Add(new Point(111, 117));
            fing[4].Add(new Point(112, 117));
            fing[4].Add(new Point(116, 113));
            fing[4].Add(new Point(117, 113));
            fing[4].Add(new Point(122, 108));
            fing[4].Add(new Point(122, 103));
            fing[4].Add(new Point(121, 103));
            fing[4].Add(new Point(121, 101));
            fing[4].Add(new Point(118, 98));
            fing[4].Add(new Point(117, 98));
            fing[4].Add(new Point(116, 97));
            
            //simetria
            
            //5 -> 4            
            fing[5].Add(new Point(2 * mx + 1 - 113, 97));
            fing[5].Add(new Point(2 * mx + 1 - 111, 99));
            fing[5].Add(new Point(2 * mx + 1 - 110, 99));
            fing[5].Add(new Point(2 * mx + 1 - 109, 100));
            fing[5].Add(new Point(2 * mx + 1 - 108, 100));
            fing[5].Add(new Point(2 * mx + 1 - 108, 101));
            fing[5].Add(new Point(2 * mx + 1 - 106, 101));
            fing[5].Add(new Point(2 * mx + 1 - 105, 102));
            fing[5].Add(new Point(2 * mx + 1 - 104, 102));
            fing[5].Add(new Point(2 * mx + 1 - 103, 103));
            fing[5].Add(new Point(2 * mx + 1 - 102, 104));
            fing[5].Add(new Point(2 * mx + 1 - 101, 104));
            fing[5].Add(new Point(2 * mx + 1 - 100, 105));
            fing[5].Add(new Point(2 * mx + 1 - 99, 105));
            fing[5].Add(new Point(2 * mx + 1 - 98, 106));
            fing[5].Add(new Point(2 * mx + 1 - 97, 106));
            fing[5].Add(new Point(2 * mx + 1 - 96, 107));
            fing[5].Add(new Point(2 * mx + 1 - 95, 108));
            fing[5].Add(new Point(2 * mx + 1 - 94, 108));
            fing[5].Add(new Point(2 * mx + 1 - 93, 109));
            fing[5].Add(new Point(2 * mx + 1 - 92, 109));
            fing[5].Add(new Point(2 * mx + 1 - 91, 110));
            fing[5].Add(new Point(2 * mx + 1 - 90, 110));
            fing[5].Add(new Point(2 * mx + 1 - 90, 111));
            fing[5].Add(new Point(2 * mx + 1 - 88, 111));
            fing[5].Add(new Point(2 * mx + 1 - 83, 116));
            fing[5].Add(new Point(2 * mx + 1 - 83, 124));
            fing[5].Add(new Point(2 * mx + 1 - 88, 129));
            fing[5].Add(new Point(2 * mx + 1 - 90, 129));
            fing[5].Add(new Point(2 * mx + 1 - 90, 130));
            fing[5].Add(new Point(2 * mx + 1 - 95, 130));
            fing[5].Add(new Point(2 * mx + 1 - 96, 129));
            fing[5].Add(new Point(2 * mx + 1 - 97, 129));
            fing[5].Add(new Point(2 * mx + 1 - 100, 126));
            fing[5].Add(new Point(2 * mx + 1 - 101, 126));
            fing[5].Add(new Point(2 * mx + 1 - 106, 121));
            fing[5].Add(new Point(2 * mx + 1 - 107, 121));
            fing[5].Add(new Point(2 * mx + 1 - 111, 117));
            fing[5].Add(new Point(2 * mx + 1 - 112, 117));
            fing[5].Add(new Point(2 * mx + 1 - 116, 113));
            fing[5].Add(new Point(2 * mx + 1 - 117, 113));
            fing[5].Add(new Point(2 * mx + 1 - 122, 108));
            fing[5].Add(new Point(2 * mx + 1 - 122, 103));
            fing[5].Add(new Point(2 * mx + 1 - 121, 103));
            fing[5].Add(new Point(2 * mx + 1 - 121, 101));
            fing[5].Add(new Point(2 * mx + 1 - 118, 98));
            fing[5].Add(new Point(2 * mx + 1 - 117, 98));
            fing[5].Add(new Point(2 * mx + 1 - 116, 97));

            //6 -> 3
            fing[6].Add(new Point(2 * mx + 1 - 97, 21));
            fing[6].Add(new Point(2 * mx + 1 - 96, 22));
            fing[6].Add(new Point(2 * mx + 1 - 95, 22));
            fing[6].Add(new Point(2 * mx + 1 - 92, 25));
            fing[6].Add(new Point(2 * mx + 1 - 92, 27));
            fing[6].Add(new Point(2 * mx + 1 - 91, 28));
            fing[6].Add(new Point(2 * mx + 1 - 91, 30));
            fing[6].Add(new Point(2 * mx + 1 - 90, 30));
            fing[6].Add(new Point(2 * mx + 1 - 90, 32));
            fing[6].Add(new Point(2 * mx + 1 - 90, 33));
            fing[6].Add(new Point(2 * mx + 1 - 88, 35));
            fing[6].Add(new Point(2 * mx + 1 - 88, 37));
            fing[6].Add(new Point(2 * mx + 1 - 87, 38));
            fing[6].Add(new Point(2 * mx + 1 - 87, 40));
            fing[6].Add(new Point(2 * mx + 1 - 86, 40));
            fing[6].Add(new Point(2 * mx + 1 - 86, 42));
            fing[6].Add(new Point(2 * mx + 1 - 85, 43));
            fing[6].Add(new Point(2 * mx + 1 - 85, 45));
            fing[6].Add(new Point(2 * mx + 1 - 84, 45));
            fing[6].Add(new Point(2 * mx + 1 - 84, 48));
            fing[6].Add(new Point(2 * mx + 1 - 83, 48));
            fing[6].Add(new Point(2 * mx + 1 - 83, 50));
            fing[6].Add(new Point(2 * mx + 1 - 82, 50));
            fing[6].Add(new Point(2 * mx + 1 - 82, 52));
            fing[6].Add(new Point(2 * mx + 1 - 81, 52));
            fing[6].Add(new Point(2 * mx + 1 - 81, 55));
            fing[6].Add(new Point(2 * mx + 1 - 80, 55));
            fing[6].Add(new Point(2 * mx + 1 - 80, 57));
            fing[6].Add(new Point(2 * mx + 1 - 79, 57));
            fing[6].Add(new Point(2 * mx + 1 - 79, 60));
            fing[6].Add(new Point(2 * mx + 1 - 78, 60));
            fing[6].Add(new Point(2 * mx + 1 - 78, 62));
            fing[6].Add(new Point(2 * mx + 1 - 77, 62));
            fing[6].Add(new Point(2 * mx + 1 - 77, 65));
            fing[6].Add(new Point(2 * mx + 1 - 76, 65));
            fing[6].Add(new Point(2 * mx + 1 - 76, 67));
            fing[6].Add(new Point(2 * mx + 1 - 75, 67));
            fing[6].Add(new Point(2 * mx + 1 - 75, 73));
            fing[6].Add(new Point(2 * mx + 1 - 76, 73));
            fing[6].Add(new Point(2 * mx + 1 - 76, 75));
            fing[6].Add(new Point(2 * mx + 1 - 79, 78));
            fing[6].Add(new Point(2 * mx + 1 - 81, 78));
            fing[6].Add(new Point(2 * mx + 1 - 82, 79));
            fing[6].Add(new Point(2 * mx + 1 - 85, 79));
            fing[6].Add(new Point(2 * mx + 1 - 85, 78));
            fing[6].Add(new Point(2 * mx + 1 - 87, 78));
            fing[6].Add(new Point(2 * mx + 1 - 90, 75));
            fing[6].Add(new Point(2 * mx + 1 - 90, 73));
            fing[6].Add(new Point(2 * mx + 1 - 91, 73));
            fing[6].Add(new Point(2 * mx + 1 - 91, 70));
            fing[6].Add(new Point(2 * mx + 1 - 92, 70));
            fing[6].Add(new Point(2 * mx + 1 - 92, 67));
            fing[6].Add(new Point(2 * mx + 1 - 93, 67));
            fing[6].Add(new Point(2 * mx + 1 - 93, 65));
            fing[6].Add(new Point(2 * mx + 1 - 94, 65));
            fing[6].Add(new Point(2 * mx + 1 - 94, 62));
            fing[6].Add(new Point(2 * mx + 1 - 95, 62));
            fing[6].Add(new Point(2 * mx + 1 - 95, 59));
            fing[6].Add(new Point(2 * mx + 1 - 96, 59));
            fing[6].Add(new Point(2 * mx + 1 - 96, 56));
            fing[6].Add(new Point(2 * mx + 1 - 97, 56));
            fing[6].Add(new Point(2 * mx + 1 - 97, 53));
            fing[6].Add(new Point(2 * mx + 1 - 98, 51));
            fing[6].Add(new Point(2 * mx + 1 - 99, 51));
            fing[6].Add(new Point(2 * mx + 1 - 99, 48));
            fing[6].Add(new Point(2 * mx + 1 - 100, 48));
            fing[6].Add(new Point(2 * mx + 1 - 100, 45));
            fing[6].Add(new Point(2 * mx + 1 - 101, 45));
            fing[6].Add(new Point(2 * mx + 1 - 101, 42));
            fing[6].Add(new Point(2 * mx + 1 - 102, 42));
            fing[6].Add(new Point(2 * mx + 1 - 102, 39));
            fing[6].Add(new Point(2 * mx + 1 - 103, 39));
            fing[6].Add(new Point(2 * mx + 1 - 103, 37));
            fing[6].Add(new Point(2 * mx + 1 - 104, 37));
            fing[6].Add(new Point(2 * mx + 1 - 104, 34));
            fing[6].Add(new Point(2 * mx + 1 - 105, 34));
            fing[6].Add(new Point(2 * mx + 1 - 105, 31));
            fing[6].Add(new Point(2 * mx + 1 - 106, 31));
            fing[6].Add(new Point(2 * mx + 1 - 106, 26));
            fing[6].Add(new Point(2 * mx + 1 - 102, 22));
            fing[6].Add(new Point(2 * mx + 1 - 101, 22));
            fing[6].Add(new Point(2 * mx + 1 - 100, 21));

            //7 -> 2
            fing[7].Add(new Point(2 * mx + 1 - 72, 4));
            fing[7].Add(new Point(2 * mx + 1 - 69, 7));
            fing[7].Add(new Point(2 * mx + 1 - 68, 8));
            fing[7].Add(new Point(2 * mx + 1 - 68, 11));
            fing[7].Add(new Point(2 * mx + 1 - 67, 12));
            fing[7].Add(new Point(2 * mx + 1 - 67, 15));
            fing[7].Add(new Point(2 * mx + 1 - 66, 16));
            fing[7].Add(new Point(2 * mx + 1 - 66, 19));
            fing[7].Add(new Point(2 * mx + 1 - 65, 20));
            fing[7].Add(new Point(2 * mx + 1 - 65, 22));
            fing[7].Add(new Point(2 * mx + 1 - 64, 23));
            fing[7].Add(new Point(2 * mx + 1 - 64, 26));
            fing[7].Add(new Point(2 * mx + 1 - 63, 27));
            fing[7].Add(new Point(2 * mx + 1 - 63, 30));
            fing[7].Add(new Point(2 * mx + 1 - 62, 31));
            fing[7].Add(new Point(2 * mx + 1 - 62, 34));
            fing[7].Add(new Point(2 * mx + 1 - 61, 35));
            fing[7].Add(new Point(2 * mx + 1 - 61, 38));
            fing[7].Add(new Point(2 * mx + 1 - 60, 39));
            fing[7].Add(new Point(2 * mx + 1 - 60, 41));
            fing[7].Add(new Point(2 * mx + 1 - 59, 42));
            fing[7].Add(new Point(2 * mx + 1 - 59, 45));
            fing[7].Add(new Point(2 * mx + 1 - 58, 46));
            fing[7].Add(new Point(2 * mx + 1 - 58, 49));
            fing[7].Add(new Point(2 * mx + 1 - 57, 50));
            fing[7].Add(new Point(2 * mx + 1 - 57, 53));
            fing[7].Add(new Point(2 * mx + 1 - 56, 54));
            fing[7].Add(new Point(2 * mx + 1 - 56, 56));
            fing[7].Add(new Point(2 * mx + 1 - 55, 57));
            fing[7].Add(new Point(2 * mx + 1 - 55, 65));
            fing[7].Add(new Point(2 * mx + 1 - 59, 69));
            fing[7].Add(new Point(2 * mx + 1 - 60, 69));
            fing[7].Add(new Point(2 * mx + 1 - 61, 70));
            fing[7].Add(new Point(2 * mx + 1 - 65, 70));
            fing[7].Add(new Point(2 * mx + 1 - 66, 69));
            fing[7].Add(new Point(2 * mx + 1 - 67, 69));
            fing[7].Add(new Point(2 * mx + 1 - 70, 66));
            fing[7].Add(new Point(2 * mx + 1 - 70, 65));
            fing[7].Add(new Point(2 * mx + 1 - 71, 64));
            fing[7].Add(new Point(2 * mx + 1 - 71, 61));
            fing[7].Add(new Point(2 * mx + 1 - 72, 60));
            fing[7].Add(new Point(2 * mx + 1 - 72, 56));
            fing[7].Add(new Point(2 * mx + 1 - 73, 55));
            fing[7].Add(new Point(2 * mx + 1 - 73, 52));
            fing[7].Add(new Point(2 * mx + 1 - 74, 51));
            fing[7].Add(new Point(2 * mx + 1 - 74, 47));
            fing[7].Add(new Point(2 * mx + 1 - 75, 46));
            fing[7].Add(new Point(2 * mx + 1 - 75, 43));
            fing[7].Add(new Point(2 * mx + 1 - 76, 42));
            fing[7].Add(new Point(2 * mx + 1 - 76, 38));
            fing[7].Add(new Point(2 * mx + 1 - 77, 37));
            fing[7].Add(new Point(2 * mx + 1 - 77, 34));
            fing[7].Add(new Point(2 * mx + 1 - 78, 33));
            fing[7].Add(new Point(2 * mx + 1 - 78, 29));
            fing[7].Add(new Point(2 * mx + 1 - 79, 28));
            fing[7].Add(new Point(2 * mx + 1 - 79, 24));
            fing[7].Add(new Point(2 * mx + 1 - 80, 23));
            fing[7].Add(new Point(2 * mx + 1 - 80, 20));
            fing[7].Add(new Point(2 * mx + 1 - 81, 19));
            fing[7].Add(new Point(2 * mx + 1 - 81, 15));
            fing[7].Add(new Point(2 * mx + 1 - 82, 14));
            fing[7].Add(new Point(2 * mx + 1 - 82, 8));
            fing[7].Add(new Point(2 * mx + 1 - 79, 5));
            fing[7].Add(new Point(2 * mx + 1 - 78, 5));
            fing[7].Add(new Point(2 * mx + 1 - 77, 4));

            //8 -> 1
            fing[8].Add(new Point(2 * mx + 1 - 44, 4));
            fing[8].Add(new Point(2 * mx + 1 - 41, 7));
            fing[8].Add(new Point(2 * mx + 1 - 41, 8));
            fing[8].Add(new Point(2 * mx + 1 - 40, 9));
            fing[8].Add(new Point(2 * mx + 1 - 40, 14));
            fing[8].Add(new Point(2 * mx + 1 - 39, 15));
            fing[8].Add(new Point(2 * mx + 1 - 39, 21));
            fing[8].Add(new Point(2 * mx + 1 - 38, 22));
            fing[8].Add(new Point(2 * mx + 1 - 38, 27));
            fing[8].Add(new Point(2 * mx + 1 - 37, 28));
            fing[8].Add(new Point(2 * mx + 1 - 37, 34));
            fing[8].Add(new Point(2 * mx + 1 - 36, 35));
            fing[8].Add(new Point(2 * mx + 1 - 36, 40));
            fing[8].Add(new Point(2 * mx + 1 - 35, 41));
            fing[8].Add(new Point(2 * mx + 1 - 35, 47));
            fing[8].Add(new Point(2 * mx + 1 - 34, 48));
            fing[8].Add(new Point(2 * mx + 1 - 34, 53));
            fing[8].Add(new Point(2 * mx + 1 - 33, 54));
            fing[8].Add(new Point(2 * mx + 1 - 33, 59));
            fing[8].Add(new Point(2 * mx + 1 - 34, 60));
            fing[8].Add(new Point(2 * mx + 1 - 34, 61));
            fing[8].Add(new Point(2 * mx + 1 - 37, 64));
            fing[8].Add(new Point(2 * mx + 1 - 39, 64));
            fing[8].Add(new Point(2 * mx + 1 - 39, 65));
            fing[8].Add(new Point(2 * mx + 1 - 44, 65));
            fing[8].Add(new Point(2 * mx + 1 - 45, 64));
            fing[8].Add(new Point(2 * mx + 1 - 46, 64));
            fing[8].Add(new Point(2 * mx + 1 - 49, 61));
            fing[8].Add(new Point(2 * mx + 1 - 49, 59));
            fing[8].Add(new Point(2 * mx + 1 - 50, 59));
            fing[8].Add(new Point(2 * mx + 1 - 50, 52));
            fing[8].Add(new Point(2 * mx + 1 - 51, 51));
            fing[8].Add(new Point(2 * mx + 1 - 51, 43));
            fing[8].Add(new Point(2 * mx + 1 - 52, 42));
            fing[8].Add(new Point(2 * mx + 1 - 52, 34));
            fing[8].Add(new Point(2 * mx + 1 - 53, 33));
            fing[8].Add(new Point(2 * mx + 1 - 53, 25));
            fing[8].Add(new Point(2 * mx + 1 - 54, 24));
            fing[8].Add(new Point(2 * mx + 1 - 54, 16));
            fing[8].Add(new Point(2 * mx + 1 - 55, 15));
            fing[8].Add(new Point(2 * mx + 1 - 55, 8));
            fing[8].Add(new Point(2 * mx + 1 - 52, 5));
            fing[8].Add(new Point(2 * mx + 1 - 51, 5));
            fing[8].Add(new Point(2 * mx + 1 - 50, 4));

            //9 -> 0
            fing[9].Add(new Point(2 * mx + 1 - 18, 18));
            fing[9].Add(new Point(2 * mx + 1 - 16, 19));
            fing[9].Add(new Point(2 * mx + 1 - 13, 22));
            fing[9].Add(new Point(2 * mx + 1 - 12, 25));
            fing[9].Add(new Point(2 * mx + 1 - 12, 35));
            fing[9].Add(new Point(2 * mx + 1 - 12, 39));
            fing[9].Add(new Point(2 * mx + 1 - 11, 51));
            fing[9].Add(new Point(2 * mx + 1 - 11, 60));
            fing[9].Add(new Point(2 * mx + 1 - 14, 63));
            fing[9].Add(new Point(2 * mx + 1 - 16, 64));
            fing[9].Add(new Point(2 * mx + 1 - 17, 65));
            fing[9].Add(new Point(2 * mx + 1 - 22, 65));
            fing[9].Add(new Point(2 * mx + 1 - 23, 64));
            fing[9].Add(new Point(2 * mx + 1 - 25, 63));
            fing[9].Add(new Point(2 * mx + 1 - 26, 62));
            fing[9].Add(new Point(2 * mx + 1 - 27, 60));
            fing[9].Add(new Point(2 * mx + 1 - 28, 58));
            fing[9].Add(new Point(2 * mx + 1 - 28, 24));
            fing[9].Add(new Point(2 * mx + 1 - 27, 22));
            fing[9].Add(new Point(2 * mx + 1 - 24, 19));
            fing[9].Add(new Point(2 * mx + 1 - 22, 18));
        }

        private void picHand_MouseMove(object sender, MouseEventArgs e) {
            
            if (chkBox.Checked) {
                return;
            }

            for (int i = 0; i < fing.Length; i++) {
                if (fing[i].hasPoint(e.X, e.Y)) {
                    picHand.Cursor = Cursors.Hand;
                    onHoverIndex = i;
                    break;
                } else {
                    picHand.Cursor = Cursors.Default;
                    onHoverIndex = -1;
                }
            }
        }                       

        private void picHand_MouseClick(object sender, MouseEventArgs e) {
            if (chkBox.Checked) {
                Console.WriteLine("fing0.Add(new Point(" + e.X + ", " + e.Y + "));");
            } else {                
                if (onHoverIndex != -1) {

                    if (drawFinger[onHoverIndex] == true) {
                        DialogResult result = MessageBox.Show(this, "A impressão digital do dedo selecionado será apagado. Deseja Continuar?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == System.Windows.Forms.DialogResult.No) {
                            return;
                        }
                    }

                    selectedFinger = true;
                    selectedIndex = onHoverIndex;
                    picHand.Refresh();
                    ExecuteEnrollment();
                }
                picHand.Refresh();
            }
        }                

        private void picHand_Paint(object sender, PaintEventArgs e) {
            
            for (int i = 0; i < fing.Length; i++) {
                if (drawFinger[i]) {
                    //Console.WriteLine("drawfinger true");
                    //e.Graphics.DrawPolygon(regstdPen, fing[i].getPoints());
                    e.Graphics.FillPolygon(regstdBrush, fing[i].getPoints());
                }                
            }

            if (selectedFinger) {
                //e.Graphics.DrawPolygon(toregPen, fing[selectedIndex].getPoints());
                e.Graphics.FillPolygon(toregBrush, fing[selectedIndex].getPoints());
            }
        }

        private void btCancel_Click(object sender, EventArgs e) {
            cancel();
            this.Close();
        }

        private void cancel() {
            if (isEnrolling) {
                BioAccess.CancelOperation();
                BioAccess.StartIdentify();
                labMsg.Text = "Operação cancelada";
            }
        }

        private void btOk_Click(object sender, EventArgs e) {            
            RefreshFingerPrints();
            DeleteUserAndFingerPrint(UserIdToEnroll);
            this.Close();
        }

        private void FrmSetFingerprint_FormClosing(object sender, FormClosingEventArgs e) {            
            cancel();            
        }

        public class FingerprintResult {
            public int EnrolledUserId;
            private List<Fingerprint> Fingerprints;

            public FingerprintResult(){
                Fingerprints = new List<Fingerprint>();
            }

            public List<Fingerprint> GetFingerPrints() {
                return this.Fingerprints;
            }

            public void Add(Fingerprint fingerPrint) {
                
                foreach (Fingerprint fg in Fingerprints) {
                    if (fg.FingerIndex == fingerPrint.FingerIndex) {
                        fg.TemplateData = fingerPrint.TemplateData;
                    }
                }

                if (!Fingerprints.Contains(fingerPrint)) {
                    this.Fingerprints.Add(fingerPrint);
                }            
            }

            public void Add(int fingerIndex, string tmpData) {
                Fingerprint fingerPrint = new Fingerprint(fingerIndex, tmpData);
                Add(fingerPrint);
            }
        }

        public class Fingerprint {
            public int FingerIndex { get; set; }
            public string TemplateData { get; set; }

            public Fingerprint(int fingerIndex, string tmpData) {                
                this.FingerIndex = fingerIndex;
                this.TemplateData = tmpData;
            }            

            public override bool Equals(object obj) {
                if (obj is Fingerprint) {
                    Fingerprint fg = obj as Fingerprint;                    
                    return this.FingerIndex==fg.FingerIndex;
                } else {
                    return false;
                }
            }

            public override int GetHashCode() {
                return 0;// base.GetHashCode();
            }

            public Fingerprint Copy() {
                return new Fingerprint(this.FingerIndex, this.TemplateData);
            }
        }
                
    }

    class Polygon {
        private List<Point> points;
        //private int count;
        
        public Polygon() {
            points = new List<Point>();
        }

        public Polygon(Point[] p) {
            points = new List<Point>();
            points.AddRange(p);
        }

        public void Add(int x, int y) {
            this.Add(new Point(x, y));
        }

        public void Add(Point p) {
            points.Add(p);
        }

        public void Add(Point[] p) {
            points.AddRange(p);
        }

        public Point[] getPoints() {
            return points.ToArray();
        }

        public bool hasPoint(Point p) {
            //Point[] poly = points.ToArray();

            Point p1, p2;
            bool inside = false;

            //if (poly.Length < 3) {
            if (points.Count < 3) {
                return inside;
            }
            
            Point oldPoint = points.Last();
            //Point oldPoint = new Point(poly[poly.Length - 1].X, poly[poly.Length - 1].Y);
            
            //for (int i = 0; i < poly.Length; i++) {
            foreach (Point newPoint in points){
                //Point newPoint = new Point(poly[i].X, poly[i].Y);

                if (newPoint.X > oldPoint.X) {
                    p1 = oldPoint;
                    p2 = newPoint;
                } else {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X) && ((long)p.Y - (long)p1.Y) * (long)(p2.X - p1.X) < ((long)p2.Y - (long)p1.Y) * (long)(p.X - p1.X)) {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }


            return inside;
        }

        public bool hasPoint(int x, int y) {
            return hasPoint(new Point(x, y));
        }

    }
}
