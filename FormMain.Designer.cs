namespace OZIRSALIYE
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Container = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accIrsaliyeler = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accIrsaliyeOlustur = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accIrsaliyeAra = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accIrsaliyeDuzenle = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accSoforler = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accSoforBilgileri = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // Container
            // 
            this.Container.Appearance.BackColor = System.Drawing.Color.LavenderBlush;
            this.Container.Appearance.Options.UseBackColor = true;
            this.Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Container.Location = new System.Drawing.Point(250, 31);
            this.Container.Margin = new System.Windows.Forms.Padding(4);
            this.Container.Name = "Container";
            this.Container.Size = new System.Drawing.Size(989, 878);
            this.Container.TabIndex = 0;
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accIrsaliyeler,
            this.accSoforler});
            this.accordionControl1.Location = new System.Drawing.Point(0, 31);
            this.accordionControl1.Margin = new System.Windows.Forms.Padding(4);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.OptionsMinimizing.NormalWidth = 250;
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl1.Size = new System.Drawing.Size(250, 878);
            this.accordionControl1.TabIndex = 1;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            this.accordionControl1.StateChanged += new System.EventHandler(this.accordionControl1_StateChanged);
            // 
            // accIrsaliyeler
            // 
            this.accIrsaliyeler.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accIrsaliyeOlustur,
            this.accIrsaliyeAra,
            this.accIrsaliyeDuzenle});
            this.accIrsaliyeler.Expanded = true;
            this.accIrsaliyeler.Name = "accIrsaliyeler";
            this.accIrsaliyeler.Text = "İrsaliyeler";
            // 
            // accIrsaliyeOlustur
            // 
            this.accIrsaliyeOlustur.ImageOptions.Image = global::OZIRSALIYE.Properties.Resources.newDoc2;
            this.accIrsaliyeOlustur.ImageOptions.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Squeeze;
            this.accIrsaliyeOlustur.Name = "accIrsaliyeOlustur";
            this.accIrsaliyeOlustur.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accIrsaliyeOlustur.Text = "Yeni İrsaliye Oluştur";
            this.accIrsaliyeOlustur.Click += new System.EventHandler(this.accIrsaliyeOlustur_Click);
            // 
            // accIrsaliyeAra
            // 
            this.accIrsaliyeAra.ImageOptions.Image = global::OZIRSALIYE.Properties.Resources._35972;
            this.accIrsaliyeAra.ImageOptions.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Squeeze;
            this.accIrsaliyeAra.Name = "accIrsaliyeAra";
            this.accIrsaliyeAra.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accIrsaliyeAra.Text = "İrsaliye Ara";
            this.accIrsaliyeAra.Click += new System.EventHandler(this.accIrsaliyeAra_Click);
            // 
            // accIrsaliyeDuzenle
            // 
            this.accIrsaliyeDuzenle.ImageOptions.Image = global::OZIRSALIYE.Properties.Resources.Edit_Document;
            this.accIrsaliyeDuzenle.ImageOptions.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Squeeze;
            this.accIrsaliyeDuzenle.Name = "accIrsaliyeDuzenle";
            this.accIrsaliyeDuzenle.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accIrsaliyeDuzenle.Text = "İrsaliye Düzenle";
            this.accIrsaliyeDuzenle.Click += new System.EventHandler(this.accIrsaliyeDuzenle_Click);
            // 
            // accSoforler
            // 
            this.accSoforler.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accSoforBilgileri});
            this.accSoforler.Expanded = true;
            this.accSoforler.Name = "accSoforler";
            this.accSoforler.Text = "Sürücüler";
            // 
            // accSoforBilgileri
            // 
            this.accSoforBilgileri.ImageOptions.Image = global::OZIRSALIYE.Properties.Resources.driverinfo;
            this.accSoforBilgileri.ImageOptions.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Squeeze;
            this.accSoforBilgileri.Name = "accSoforBilgileri";
            this.accSoforBilgileri.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accSoforBilgileri.Text = "Sürücü Bilgileri";
            this.accSoforBilgileri.Click += new System.EventHandler(this.accSoforBilgileri_Click);
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1});
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Margin = new System.Windows.Forms.Padding(4);
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(1239, 31);
            this.fluentDesignFormControl1.TabIndex = 2;
            this.fluentDesignFormControl1.TabStop = false;
            this.fluentDesignFormControl1.TitleItemLinks.Add(this.barButtonItem1);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Son İrsaliyeyi Görüntüle";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.ItemAppearance.Disabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(127)))), ((int)(((byte)(120)))));
            this.barButtonItem1.ItemAppearance.Disabled.Options.UseBackColor = true;
            this.barButtonItem1.ItemAppearance.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(127)))), ((int)(((byte)(120)))));
            this.barButtonItem1.ItemAppearance.Normal.ForeColor = System.Drawing.Color.White;
            this.barButtonItem1.ItemAppearance.Normal.Options.UseBackColor = true;
            this.barButtonItem1.ItemAppearance.Normal.Options.UseForeColor = true;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 909);
            this.ControlContainer = this.Container;
            this.Controls.Add(this.Container);
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.fluentDesignFormControl1);
            this.FluentDesignFormControl = this.fluentDesignFormControl1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.NavigationControl = this.accordionControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ÖZÜGÜÇER İRSALİYE";
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer Container;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accIrsaliyeler;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accIrsaliyeOlustur;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accIrsaliyeAra;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accIrsaliyeDuzenle;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accSoforler;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accSoforBilgileri;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    }
}