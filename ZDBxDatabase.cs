using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mz.betainteractive.sigeas.zdbx.models;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using mz.betainteractive.sigeas.DeviceSystem;

namespace mz.betainteractive.sigeas.zdbx {
    [Serializable]
    public class ZDBxDatabase {
        public List<ZDevice> Devices;
        public List<RawUserClock> Clocks;
        public virtual List<ZUser> Users { get; set; }    

        public ZDBxDatabase() {
            this.Devices = new List<ZDevice>();
            this.Clocks = new List<RawUserClock>();
            this.Users = new List<ZUser>();
        }

        public bool IsEmpty() {
            return this.Devices.Count == 0 && this.Clocks.Count == 0;
        }
    }

    public class DatabaseIO {
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        public string SavedFileName;
        private string OpenedFileName;

        public DatabaseIO() {
            this.openFileDialog = new OpenFileDialog();
            this.saveFileDialog = new SaveFileDialog();
                        
            this.openFileDialog.Filter = "ZK Sigeas DB Files|*.zdbx";
            this.openFileDialog.InitialDirectory = "c:\\";
            this.openFileDialog.Title = "Importar ficheiro com dados biométricos";

            this.SavedFileName = "device_name_dd_mm_aaaa.zdbx";

            this.saveFileDialog.Filter = "ZK Sigeas DB Files|*.zdbx";
            this.saveFileDialog.FileName = this.SavedFileName;
            this.saveFileDialog.Title = "Salvar Ficheiro com dados biométricos";
            this.saveFileDialog.DefaultExt = "zdbx";
        }

        public string GetOpenedFileName() {
            return this.OpenedFileName;
        }

        public bool Write(ZDBxDatabase database) {
            try {

                if (!this.SavedFileName.ToLower().EndsWith(".zdbx")) {
                    this.SavedFileName += ".zdbx";
                }

                this.saveFileDialog.FileName = this.SavedFileName;                               

                DialogResult result = saveFileDialog.ShowDialog();

                if (result == DialogResult.OK) {                  
                    

                    Stream stream = saveFileDialog.OpenFile();
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, database);
                    return true;
                }

            } catch (Exception ex) {
                MessageBox.Show("Não foi possivel gravar o ficheiro!"+Environment.NewLine+""+ex.ToString());
            }

            return false;
        }

        public bool Read(out ZDBxDatabase database) {

            database = null;

            try {

                DialogResult result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK) {
                    this.OpenedFileName = openFileDialog.FileName;

                    Stream stream = openFileDialog.OpenFile();
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Binder = new BindChanger();

                    database = (ZDBxDatabase) binaryFormatter.Deserialize(stream);

                    return database != null;
                }

            } catch (Exception ex) {
                MessageBox.Show("Não foi possivel ler o ficheiro!" + Environment.NewLine + "" + ex.ToString());
            }

            return false;
        }
    }

    public class BindChanger : System.Runtime.Serialization.SerializationBinder {
        public override Type BindToType(string assemblyName, string typeName) {
            // Define the new type to bind to 
            Type typeToDeserialize = null;
            // Get the assembly names

            string currentAssembly = "ZDBxDatabase";
            typeName = "ZKSigeasDB";

            // Create the new type and return it 
            typeToDeserialize = Type.GetType(string.Format("{0}, {1}", typeName, currentAssembly));
            return typeToDeserialize;
        }
    }
}
