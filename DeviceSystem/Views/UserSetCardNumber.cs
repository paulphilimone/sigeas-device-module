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
using System.Threading;
using mz.betainteractive.sigeas.Models.Entities;


namespace mz.betainteractive.sigeas.DeviceSystem.Views {
    public partial class UserSetCardNumber : Form {

        private IDevice device;        
        private zkemkeeper.CZKEMClass BioAccess;

        private bool isEnrolling { get; set; }
        private bool hasEnrolled;
        private string cardNumberStr;

        public UserSetCardNumber() {                        
            InitializeComponent();            
        }        

        public void StartForm(IDevice dev) {
            
            this.device = dev;
            this.hasEnrolled = false;
                        
            if (this.device == null) {
                MessageBox.Show(this, "Não existem dispositivos conectados ao sistema!\nConecte um dispositivo primeiro");
                return;
            }

            if (this.device.Connected == false){
                MessageBox.Show(this, "O dispositivo selecionado não está conectado ao sistema!\nConecte o dispositivo primeiro");
                return;
            }

            isEnrolling = true;
            BioAccess = this.device.BiometricSDK;
            
            BioAccess.CancelOperation();
            BioAccess.StartIdentify();

            AddListeners();

            this.btOk.Enabled = false;
            this.ShowDialog();            
        }

     
        private void RemoveListeners() {
            BioAccess.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(BioAccess_OnHIDNum);
        }

        private void AddListeners() {
            BioAccess.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(BioAccess_OnHIDNum);            
        }

        private void BioAccess_OnHIDNum(int CardNumber) {
            //Do something
            String cardNumber = CardNumber.ToString();                        

            this.LabelSerialNumber.Text = cardNumber;
            this.LabelSerialNumber.Update();

            this.cardNumberStr = cardNumber;

            hasEnrolled = true;

            this.btOk.Enabled = true;

            RemoveListeners();
            BioAccess.StartIdentify();
        }               

        private void btCancel_Click(object sender, EventArgs e) {
            cancel();
            this.Close();
        }

        private void cancel() {
            if (isEnrolling) {
                RemoveListeners();
                BioAccess.CancelOperation();
                BioAccess.StartIdentify();
                labMsg.Text = "Operação cancelada";
            }
        }

        private void btOk_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void FrmSetFingerprint_FormClosing(object sender, FormClosingEventArgs e) {            
            cancel();            
        }
                
        public string GetCardNumber() {
            if (hasEnrolled) {
                return cardNumberStr;
            } else {
                return null;
            }
        }
    }
        
}
