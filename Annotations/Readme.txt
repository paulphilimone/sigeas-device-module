Ao terminar o projecto colocar database user e password
Durante o projecto usamos root

The Database of this software, it is to be reusable in differents softwares that can implement Biometrics Users

1. O UserId de um "User" é relativo, para cada biometrico, pode ser registrado com um valor diferente, então a tabela
   "DeviceUser" ira conter a associacao entre o dispositivo e o user, e o respectivo "UserId"

   Na hora da pesquisa procuramos pela associacao, Device.SerialNumber=? e UserId na tabela "DeviceUser" ai podemos obter o "User"





Codes:
- Font do Sistema "Calibri", "Segoe UI", "Sans Serif"

- Dont forget to use Attach for object entity, that are disconnect from the Context

- Delete User And Fingerprints
  BioAccess.SSR_DeleteEnrollData(1, EnrollNumber.ToString(), 12);

#Date firmat
d' de 'MMMM' de 'yyyy

#Code For DBContext Template File
#In the project we cant see the files in path
foreach (var entity in ItemCollection.GetItems<EntityType>().OrderBy(e => e.Name))
{
    fileManager.StartNewFile("Entities\\Generated\\"entity.Name + ".cs");
    BeginNamespace(namespaceName, code);
#>


#
AutoCarro car = context.AutoCarro.FirstOrDefault(u => u.Id != SelectedAutoCarro.Id && u.Nome == carName);
            if (car != null) {
                MessageBox.Show(this, "Já existe um autocarro com o nome (" + carName + "), introduza outro por favor!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtCarNome.Focus();
                return;
            }

            //verificar se existe um outro device com o novo nome


            DialogResult dr = MessageBox.Show(this, "Tem certeza que deseja atualizar os dados do autocarro " + carName + ")?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.No) {
                return;
            }

#Code For Read CARD NUMBER
 private void btnGetStrCardNumber_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show(this, "Please connect the device first!", "Error");
                return;
            }

            string sdwEnrollNumber = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;
            string sCardnumber = "";

            lvCard.Items.Clear();
            lvCard.BeginUpdate();
            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get user information from memory
            {
                if (axCZKEM1.GetStrCardNumber(out sCardnumber))//get the card number from the memory
                {
                    ListViewItem list = new ListViewItem();
                    list.Text = sdwEnrollNumber;
                    list.SubItems.Add(sName);
                    list.SubItems.Add(sCardnumber);
                    list.SubItems.Add(iPrivilege.ToString());
                    list.SubItems.Add(sPassword);
                    if (bEnabled == true)
                    {
                        list.SubItems.Add("true");
                    }
                    else
                    {
                        list.SubItems.Add("false");
                    }
                    lvCard.Items.Add(list);
                }
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            lvCard.EndUpdate();
            Cursor = Cursors.Default;
        }

        //Upload the cardnumber as part of the user information.
        private void btnSetStrCardNumber_Click(object sender, EventArgs e)
        {

            if (bIsConnected == false)
            {
                MessageBox.Show(this, "Please connect the device first!", "Error");
                return;
            }

            if (txtUserID.Text.Trim() == "" || cbPrivilege.Text.Trim() == "" || txtCardnumber.Text.Trim() == "")
            {
                MessageBox.Show(this, "UserID,Privilege,Cardnumber must be inputted first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            bool bEnabled = true;
            if (chbEnabled.Checked)
            {
                bEnabled = true;
            }
            else
            {
                bEnabled = false;
            }
            string sdwEnrollNumber = txtUserID.Text.Trim();
            string sName = txtName.Text.Trim();
            string sPassword = txtPassword.Text.Trim();
            int iPrivilege = Convert.ToInt32(cbPrivilege.Text.Trim());
            string sCardnumber = txtCardnumber.Text.Trim();

            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            axCZKEM1.SetStrCardNumber(sCardnumber);//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device
            if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload the user's information(card number included)
            {
                MessageBox.Show(this, "(SSR_)SetUserInfo,UserID:" + sdwEnrollNumber + " Privilege:" + iPrivilege.ToString() + " Enabled:" + bEnabled.ToString(), "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show(this, "Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;
        }
