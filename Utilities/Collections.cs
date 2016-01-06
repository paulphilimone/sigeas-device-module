using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;

namespace mz.betainteractive.sigeas.Utilities {
    
    public class ObjectItem{
        public Object Value;
        private String name;

        public ObjectItem(Object o, String name) {
            this.Value = o;
            this.name = name;
        }

        public override string ToString() {
            return this.name;
        }
    }

    public class TreeNodeItem<T> : TreeNode{
        public T Value;

        public TreeNodeItem(T value) {
            this.Value = value;
            this.Text = value.ToString();
        }

        public TreeNodeItem(T value, int imageIndex, int selectedImageIndex) {
            this.Value = value;
            this.Text = value.ToString();
            this.ImageIndex = imageIndex;
            this.SelectedImageIndex = selectedImageIndex;
        }

        public override string ToString() {
            return this.Value.ToString();
        }
    }

    /*
    public class TreeNodeDepartamento : TreeNode {
        public Departamento Value;

        public TreeNodeDepartamento(Departamento departamento) {
            this.Value = departamento;
            this.Text = departamento.Nome;
            this.ImageIndex = 1;
            this.SelectedImageIndex = 1;
        }

        public override string ToString() {
            return base.ToString();
        }
    }
    */
        
    public class ListViewObjectItem<T> : ListViewItem {
        public T Value;

        public ListViewObjectItem(T value) {
            this.Value = value;
            this.Text = value.ToString();
        }

        public ListViewObjectItem(T value, int imageIndex) {
            this.Value = value;
            this.Text = value.ToString();
            this.ImageIndex = imageIndex;
        }

        /*
        public void UpdateColumns() {
            this.SubItems.Clear();
            this.Text = Value.GetDoor().ToString();
            this.SubItems.Add(Value.Name);
            this.SubItems.Add(Value.Connected ? "ON" : "OFF");
        }
        */

        public override string ToString() {
            return this.Value.ToString();
        }
    }          

    
    public class ListViewGenericItem<T> : ListViewItem {
        public T Value;
        public bool ReadOnly;

        public delegate void SetSubItemsHandler();

        public ListViewGenericItem(T value) {
            this.Value = value;                        
        }

        public ListViewGenericItem(T value, int imageIndex) : this(value) {            
            this.Text = value.ToString();
            this.ImageIndex = imageIndex;
        }

        public virtual event SetSubItemsHandler SetItems;        
    }

    public class DataGridViewGenericRow<T> : DataGridViewRow
    {
        public T Value;
        public DataGridViewGenericRow(T value) {
            this.Value = value;
        }
    }

    public class DataGridViewCheckBoxColumnGeneric<T> : DataGridViewCheckBoxColumn {
        public T Value;

        public DataGridViewCheckBoxColumnGeneric(T value) {
            this.Value = value;
        }

    }

    public class DataGridViewCheckBoxCellGeneric<T> : DataGridViewCheckBoxCell {
        public T GenericValue;
        public object DefaultValue { get; set; }
        
        public DataGridViewCheckBoxCellGeneric() { 
        }

        public DataGridViewCheckBoxCellGeneric(object defaultValue) {
            this.DefaultValue = defaultValue;
        }

        public DataGridViewCheckBoxCellGeneric(T value, object defaultValue) {
            this.GenericValue = value;
            this.DefaultValue = defaultValue;
        }

    }

    public class EqualityComparerObjectAtt<T> : IEqualityComparer<T>
    {

        #region IEqualityComparer<T> Members

        public bool Equals(T x, T y) {            
            return x.Equals(y);
        }

        public int GetHashCode(T obj) {
            return 0;// obj.GetHashCode();
        }

        #endregion

        #region IEqualityComparer<T> Members

        bool IEqualityComparer<T>.Equals(T x, T y) {
            return x.Equals(y);
        }

        int IEqualityComparer<T>.GetHashCode(T obj) {
            return 0;// obj.GetHashCode();
        }

        #endregion
    }
}
