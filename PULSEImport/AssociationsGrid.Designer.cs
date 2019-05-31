namespace PULSEImport
{
    partial class AssociationsGrid
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtgAssociations = new System.Windows.Forms.DataGridView();
            this.ObjectType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Object = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.AssociationType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ObjectType2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Object2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAssociations)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgAssociations
            // 
            this.dtgAssociations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgAssociations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAssociations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ObjectType,
            this.Object,
            this.AssociationType,
            this.ObjectType2,
            this.Object2});
            this.dtgAssociations.Location = new System.Drawing.Point(0, 0);
            this.dtgAssociations.Name = "dtgAssociations";
            this.dtgAssociations.Size = new System.Drawing.Size(752, 352);
            this.dtgAssociations.TabIndex = 1;
            // 
            // ObjectType
            // 
            this.ObjectType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.ObjectType.HeaderText = "Object Type";
            this.ObjectType.Items.AddRange(new object[] {
            "Device",
            "Equipment",
            "Vehicle",
            "Person"});
            this.ObjectType.Name = "ObjectType";
            // 
            // Object
            // 
            this.Object.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Object.HeaderText = "Object";
            this.Object.Name = "Object";
            // 
            // AssociationType
            // 
            this.AssociationType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.AssociationType.HeaderText = "Association";
            this.AssociationType.Items.AddRange(new object[] {
            "Driver Of",
            "Installed In"});
            this.AssociationType.Name = "AssociationType";
            // 
            // ObjectType2
            // 
            this.ObjectType2.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.ObjectType2.HeaderText = "Object Type";
            this.ObjectType2.Items.AddRange(new object[] {
            "Device",
            "Equipment",
            "Vehicle",
            "Person"});
            this.ObjectType2.Name = "ObjectType2";
            // 
            // Object2
            // 
            this.Object2.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Object2.HeaderText = "Object";
            this.Object2.Name = "Object2";
            // 
            // AssociationsGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.dtgAssociations);
            this.Name = "AssociationsGrid";
            this.Size = new System.Drawing.Size(755, 355);
            ((System.ComponentModel.ISupportInitialize)(this.dtgAssociations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgAssociations;
        private System.Windows.Forms.DataGridViewComboBoxColumn ObjectType;
        private System.Windows.Forms.DataGridViewComboBoxColumn Object;
        private System.Windows.Forms.DataGridViewComboBoxColumn AssociationType;
        private System.Windows.Forms.DataGridViewComboBoxColumn ObjectType2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Object2;
    }
}
