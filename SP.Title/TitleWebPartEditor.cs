using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;
using System.Web.UI;
using SP.Title.TitleWebpart;
using Microsoft.SharePoint;
using System.Threading;

namespace SP.Title
{
    public class TitleWebPartEditor : EditorPart
    {
        private string _Title = "Custom Settings";
        private TextBox txtTitle1 = new TextBox();
        private TextBox txtTitle2 = new TextBox();
        private TextBox txtFirstBackColorBox = new TextBox();
        private TextBox txtFirstForeColorBox = new TextBox();
        private TextBox txtSecondBackColorBox = new TextBox();
        private TextBox txtSecondForeColorBox = new TextBox();
        private DropDownList drpFirstFonts = new DropDownList();
        private DropDownList drpSecondFonts = new DropDownList();
        private DropDownList drpFirstSize = new DropDownList();
        private DropDownList drpSecondSize = new DropDownList();
        private CheckBox chkFirstItallic = new CheckBox();
        private CheckBox chkSecondItallic = new CheckBox();
        private CheckBox chkFirstUnderline = new CheckBox();
        private CheckBox chkSecondUnderline = new CheckBox();
        private CheckBox chkFirstBold = new CheckBox();
        private CheckBox chkSecondBold = new CheckBox();
        private DropDownList drpFirstItallic = new DropDownList();
        private DropDownList drpSecondItallic = new DropDownList();
        private DropDownList drpFirstUnderline = new DropDownList();
        private DropDownList drpSecondUnderline = new DropDownList();
        private DropDownList drpFirstBold = new DropDownList();
        private DropDownList drpSecondBold = new DropDownList();
        private Button btnSyncronise = new Button();
        private DropDownList drpImageStyle = new DropDownList();
        private TextBox txtImageUrl = new TextBox();
        private CheckBox chkDebug = new CheckBox();
        

        private DropDownList drpSyncWithStore = new DropDownList();


        public override string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                base.Title = value;
            }
        }


        protected override void CreateChildControls()
        {
            base.CreateChildControls();

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.RenderControls();
        }

        public void RenderControls()
        {
            this.Controls.Clear();

            #region First_Text_Parameters

            LiteralControl pnlFisrtStart = new LiteralControl("<div class=\"panel_style\">");
            this.Controls.Add(pnlFisrtStart);

            Label lblTitle1 = new Label();
            lblTitle1.Text = "First text to display :";
            this.Controls.Add(lblTitle1);
            this.Controls.Add(new LiteralControl("</br>"));

            this.Controls.Add(txtTitle1);

            this.Controls.Add(new LiteralControl("</br>"));

            Label lblTitle3 = new Label();
            lblTitle3.Text = "Pick up background color for first text:";
            this.Controls.Add(lblTitle3);
            this.Controls.Add(new LiteralControl("</br>"));

            string colorPickupWidget = "<div id=\"colorSelector\"><div style=\"background-color: rgb(0, 255, 0);\"/></div>";
            string colorPickupString = "<input id=\"colorpickerField1\" type=\"text\" size=\"6\" maxLength=\"6\" value=\"00ff00\"/>";
            //this.Controls.Add(new LiteralControl(colorPickupWidget));
            //this.Controls.Add(new LiteralControl(colorPickupString));

            txtFirstBackColorBox.CssClass = "colorpickerField1";
            this.Controls.Add(txtFirstBackColorBox);
            this.Controls.Add(new LiteralControl("</br>"));

            Label lblTitle4 = new Label();
            lblTitle4.Text = "Pick up forground color for first text:";
            this.Controls.Add(lblTitle4);
            this.Controls.Add(new LiteralControl("</br>"));

            txtFirstForeColorBox.CssClass = "colorpickerField2";
            this.Controls.Add(txtFirstForeColorBox);
            this.Controls.Add(new LiteralControl("</br>"));


            // init fonts
            Label lblTitle7 = new Label();
            lblTitle7.Text = "Choose a font type";
            this.Controls.Add(lblTitle7);
            this.Controls.Add(new LiteralControl("</br>"));
            drpFirstFonts.Items.Clear();
            foreach (System.Drawing.FontFamily family in System.Drawing.FontFamily.Families)
            {
                drpFirstFonts.Items.Add(family.Name);
            }
            this.Controls.Add(drpFirstFonts);
            this.Controls.Add(new LiteralControl("</br>"));

            Label lblTitle8 = new Label();
            lblTitle8.Text = "Choose a font size";
            this.Controls.Add(lblTitle8);
            this.Controls.Add(new LiteralControl("</br>"));
            drpFirstSize.Items.Clear();
            for (int i = 8; i < 41; i++)
            {
                drpFirstSize.Items.Add(i.ToString());
            }
            this.Controls.Add(drpFirstSize);
            this.Controls.Add(new LiteralControl("</br>"));

            Label lblTitle12 = new Label();
            lblTitle12.Text = "Choose a font style";
            this.Controls.Add(lblTitle12);

            this.Controls.Add(new LiteralControl("</br>"));

            drpFirstItallic.Items.Clear();
            //this.Controls.Add(chkItallic);
            drpFirstItallic.Items.Add("true");
            drpFirstItallic.Items.Add("false");
            this.Controls.Add(drpFirstItallic);

            Label lblTitle9 = new Label();
            lblTitle9.Text = " - Itallic";
            this.Controls.Add(lblTitle9);
            this.Controls.Add(new LiteralControl("</br>"));

            drpFirstUnderline.Items.Clear();
            //this.Controls.Add(chkUnderline);
            drpFirstUnderline.Items.Add("true");
            drpFirstUnderline.Items.Add("false");
            this.Controls.Add(drpFirstUnderline);

            Label lblTitle10 = new Label();
            lblTitle10.Text = " - Underline";
            this.Controls.Add(lblTitle10);
            this.Controls.Add(new LiteralControl("</br>"));

            drpFirstBold.Items.Clear();
            //this.Controls.Add(chkItallic);
            drpFirstBold.Items.Add("true");
            drpFirstBold.Items.Add("false");
            this.Controls.Add(drpFirstBold);

            Label lblTitle11 = new Label();
            lblTitle11.Text = " - Bold";
            this.Controls.Add(lblTitle11);
            this.Controls.Add(new LiteralControl("</br>"));

            LiteralControl pnlFirstEnd = new LiteralControl("</div>");
            this.Controls.Add(pnlFirstEnd);

            #endregion

            #region Second_Text_Parameters

            LiteralControl pnlSecondStart = new LiteralControl("<div class=\"panel_style\">");
            this.Controls.Add(pnlSecondStart);

            Label lblTitle2 = new Label();
            lblTitle2.Text = "Second text to display :";
            this.Controls.Add(lblTitle2);
            this.Controls.Add(new LiteralControl("</br>"));
            this.Controls.Add(txtTitle2);

            this.Controls.Add(new LiteralControl("</br>"));

            Label lblTitle5 = new Label();
            lblTitle5.Text = "Pick up background color for second text:";
            this.Controls.Add(lblTitle5);
            this.Controls.Add(new LiteralControl("</br>"));

            txtSecondBackColorBox.CssClass = "colorpickerField3";
            this.Controls.Add(txtSecondBackColorBox);

            this.Controls.Add(new LiteralControl("</br>"));

            Label lblTitle6 = new Label();
            lblTitle6.Text = "Pick up foreground color for second text:";
            this.Controls.Add(lblTitle6);
            this.Controls.Add(new LiteralControl("</br>"));

            txtSecondForeColorBox.CssClass = "colorpickerField4";
            this.Controls.Add(txtSecondForeColorBox);
            this.Controls.Add(new LiteralControl("</br>"));

            // init fonts
            Label lblTitle27 = new Label();
            lblTitle27.Text = "Choose a font type";
            this.Controls.Add(lblTitle27);
            this.Controls.Add(new LiteralControl("</br>"));
            drpSecondFonts.Items.Clear();
            foreach (System.Drawing.FontFamily family in System.Drawing.FontFamily.Families)
            {
                drpSecondFonts.Items.Add(family.Name);
            }
            this.Controls.Add(drpSecondFonts);
            this.Controls.Add(new LiteralControl("</br>"));

            Label lblTitle28 = new Label();
            lblTitle28.Text = "Choose a font size";
            this.Controls.Add(lblTitle28);
            this.Controls.Add(new LiteralControl("</br>"));
            drpSecondSize.Items.Clear();
            for (int i = 8; i < 41; i++)
            {
                drpSecondSize.Items.Add(i.ToString());
            }
            this.Controls.Add(drpSecondSize);
            this.Controls.Add(new LiteralControl("</br>"));

            Label lblTitle212 = new Label();
            lblTitle212.Text = "Choose a font style";
            this.Controls.Add(lblTitle212);

            this.Controls.Add(new LiteralControl("</br>"));

            drpSecondItallic.Items.Clear();
            drpSecondItallic.Items.Add("true");
            drpSecondItallic.Items.Add("false");
            this.Controls.Add(drpSecondItallic);

            Label lblTitle29 = new Label();
            lblTitle29.Text = " - Itallic";
            this.Controls.Add(lblTitle29);
            this.Controls.Add(new LiteralControl("</br>"));

            drpSecondUnderline.Items.Clear();
            //this.Controls.Add(chkUnderline);
            drpSecondUnderline.Items.Add("true");
            drpSecondUnderline.Items.Add("false");
            this.Controls.Add(drpSecondUnderline);

            Label lblTitle210 = new Label();
            lblTitle210.Text = " - Underline";
            this.Controls.Add(lblTitle210);
            this.Controls.Add(new LiteralControl("</br>"));

            drpSecondBold.Items.Clear();
            //this.Controls.Add(chkItallic);
            drpSecondBold.Items.Add("true");
            drpSecondBold.Items.Add("false");
            this.Controls.Add(drpSecondBold);

            Label lblTitle211 = new Label();
            lblTitle211.Text = " - Bold";
            this.Controls.Add(lblTitle211);
            this.Controls.Add(new LiteralControl("</br>"));

            LiteralControl pnlSecondEnd = new LiteralControl("</div>");
            this.Controls.Add(pnlSecondEnd);

            #endregion

            #region Image_Information

            LiteralControl pnlImgStart = new LiteralControl("<div class=\"panel_style\">");
            this.Controls.Add(pnlImgStart);

            Label lblTitle221 = new Label();
            lblTitle221.Text = "Image Information";
            this.Controls.Add(lblTitle221);
            this.Controls.Add(new LiteralControl("</br>"));

            drpImageStyle.Items.Clear();
            drpImageStyle.Items.Add(Constants.ImageStyle.None.ToString());
            drpImageStyle.Items.Add(Constants.ImageStyle.Before.ToString());
            drpImageStyle.Items.Add(Constants.ImageStyle.After.ToString());
            drpImageStyle.Items.Add(Constants.ImageStyle.Continues.ToString());

            this.Controls.Add(new LiteralControl("</br>"));

            this.Controls.Add(drpImageStyle);

            Label lblTitle231 = new Label();
            lblTitle231.Text = "Image Url:";
            this.Controls.Add(lblTitle231);
            this.Controls.Add(new LiteralControl("</br>"));

            this.Controls.Add(txtImageUrl);

            this.Controls.Add(new LiteralControl("</br>"));

            LiteralControl pnlImgEnd = new LiteralControl("</div>");
            this.Controls.Add(pnlImgEnd);

            #endregion

            #region Sync_Information

            LiteralControl pnlSyncStart = new LiteralControl("<div class=\"panel_style\">");
            this.Controls.Add(pnlSyncStart);

            drpSyncWithStore.Items.Clear();

            drpSyncWithStore.Items.Add("true");
            drpSyncWithStore.Items.Add("false");
            this.Controls.Add(drpSyncWithStore);
            this.Controls.Add(new LiteralControl("</br>"));

            Label lblTitle13 = new Label();
            lblTitle13.Text = " - Syncronised";
            this.Controls.Add(lblTitle13);
            this.Controls.Add(new LiteralControl("</br>"));

            btnSyncronise.Text = "Load Store Settings";
            btnSyncronise.Click += new EventHandler(btnSyncronise_Click);
            this.Controls.Add(btnSyncronise);

            LiteralControl syncVersionEnd = new LiteralControl("</div>");
            this.Controls.Add(syncVersionEnd);

            #endregion

            #region Versioning_Information

            LiteralControl pnlVersionStart = new LiteralControl("<div class=\"panel_style\">");
            this.Controls.Add(pnlVersionStart);

            Label lblTitle14 = new Label();
            lblTitle14.Text = VersionProvider.VersionNumber + VersionProvider.GetBuild();
            this.Controls.Add(lblTitle14);
            this.Controls.Add(new LiteralControl("</br>"));

            Label lblTitle114 = new Label();
            lblTitle114.Text = VersionProvider.ProviderInfo;
            this.Controls.Add(lblTitle114);
            this.Controls.Add(new LiteralControl("</br>"));

            chkDebug.Text = "Debug";
            this.Controls.Add(chkDebug);
            this.Controls.Add(new LiteralControl("</br>"));

            LiteralControl pnlVersionEnd = new LiteralControl("</div>");
            this.Controls.Add(pnlVersionEnd);

            #endregion
  
        }

        void btnSyncronise_Click(object sender, EventArgs e)
        {
            TitleWebpart.TitleWebpart webPartToEditt = (TitleWebpart.TitleWebpart)this.WebPartToEdit;
            //if (drpSyncWithStore.SelectedValue.Equals("true"))
            //{
                using (SPSetttingsProvider provider = new SPSetttingsProvider())
                {
                    bool error = provider.LoadSPSetting(webPartToEditt);
                    if (!error && provider.exception != null)
                        webPartToEditt.Error = provider.exception.ToString();
                }
                this.RenderControls();
                this.SyncChanges();
            //}

        }

        public override bool ApplyChanges()
        {
            EnsureChildControls();

            TitleWebpart.TitleWebpart webPartToEdit = (TitleWebpart.TitleWebpart)this.WebPartToEdit;
            if (webPartToEdit != null)
            {
                webPartToEdit.FirstString = txtTitle1.Text;
                webPartToEdit.SecondString = txtTitle2.Text;
                webPartToEdit.FirstBackColorString = txtFirstBackColorBox.Text;
                webPartToEdit.FirstForeColorString = txtFirstForeColorBox.Text;
                webPartToEdit.SecondBackColorString = txtSecondBackColorBox.Text;
                webPartToEdit.SecondForeColorString =  txtSecondForeColorBox.Text;
                webPartToEdit.FirstFontName = drpFirstFonts.SelectedValue;
                webPartToEdit.FirstFontSize = int.Parse(drpFirstSize.SelectedValue);
                webPartToEdit.FirstFontItallic = drpFirstItallic.SelectedIndex == 0 ? true : false;
                webPartToEdit.FirstFontBold = drpFirstBold.SelectedIndex == 0 ? true : false;
                webPartToEdit.FirstFontUnderline = drpFirstUnderline.SelectedIndex == 0 ? true : false;
                webPartToEdit.SecondFontName = drpSecondFonts.SelectedValue;
                webPartToEdit.SecondFontSize = int.Parse(drpSecondSize.SelectedValue);
                webPartToEdit.SecondFontItallic = drpSecondItallic.SelectedIndex == 0 ? true : false;
                webPartToEdit.SecondFontBold = drpSecondBold.SelectedIndex == 0 ? true : false;
                webPartToEdit.SecondFontUnderline = drpSecondUnderline.SelectedIndex == 0 ? true : false;
                webPartToEdit.IsSyncronised = drpSyncWithStore.SelectedIndex == 0 ? true : false;
                webPartToEdit.ImageUrl = txtImageUrl.Text;
                webPartToEdit.ImageStyle = drpImageStyle.SelectedValue;
                webPartToEdit.IsDebug = chkDebug.Checked;

                // sync with main storage
                if (drpSyncWithStore.SelectedValue.ToString().Equals("true"))
                {
                    using (SPSetttingsProvider prov = new SPSetttingsProvider())
                    {
                        bool error = prov.SaveSPSetting(webPartToEdit);
                        if (!error && prov.exception != null)
                            webPartToEdit.Error = prov.exception.ToString();
                    }

                    //SPSite site = SPContext.Current.Site;
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(saveSPSettings), new ContextWrapper { _webPartToEdit = webPartToEdit, _webID = site.RootWeb.ID, _siteID = site.ID});
                }

            }
            return true;
        }

        public void saveSPSettings(object input)
        {
            TitleWebpart.TitleWebpart webPartToEdit = ((ContextWrapper)input)._webPartToEdit;
            Guid siteID = ((ContextWrapper)input)._siteID;
            Guid webID = ((ContextWrapper)input)._webID;

            using (SPSite site = new SPSite(siteID))
            {
                using (SPWeb web = site.OpenWeb(webID))
                {
                    using (SPSetttingsProvider prov = new SPSetttingsProvider(site.RootWeb))
                    {
                        bool error = prov.SaveSPSetting(webPartToEdit);
                        if (!error && prov.exception != null)
                            webPartToEdit.Error = prov.exception.ToString();
                    }
                }
            }
        }


        public override void SyncChanges()
        {
            EnsureChildControls();

            TitleWebpart.TitleWebpart webPartToEdit = (TitleWebpart.TitleWebpart)this.WebPartToEdit;
            if (webPartToEdit != null)
            {
                txtTitle1.Text = webPartToEdit.FirstString;
                txtTitle2.Text = webPartToEdit.SecondString;
                txtFirstBackColorBox.Text = webPartToEdit.FirstBackColorString;
                txtFirstForeColorBox.Text = webPartToEdit.FirstForeColorString;
                txtSecondBackColorBox.Text = webPartToEdit.SecondBackColorString;
                txtSecondForeColorBox.Text = webPartToEdit.SecondForeColorString;

                if (webPartToEdit.FirstFontItallic)
                    drpFirstItallic.SelectedIndex = 0;
                else
                    drpFirstItallic.SelectedIndex = 1;


                if (webPartToEdit.FirstFontBold)
                    drpFirstBold.SelectedIndex = 0;
                else
                    drpFirstBold.SelectedIndex = 1;


                if (webPartToEdit.FirstFontUnderline)
                    drpFirstUnderline.SelectedIndex = 0;
                else
                    drpFirstUnderline.SelectedIndex = 1;


                int fontIndex = 0;
                foreach (ListItem item in drpFirstFonts.Items)
                {
                    if (item.Text.Equals(webPartToEdit.FirstFontName))
                        break;
                    fontIndex++;
                }
                drpFirstFonts.SelectedIndex = fontIndex >= drpFirstFonts.Items.Count ? 0 : fontIndex;

                int sizeIndex = 0;
                foreach (ListItem item in drpFirstSize.Items)
                {
                    if (item.Text.Equals(webPartToEdit.FirstFontSize.ToString()))
                        break;
                    sizeIndex++;
                }
                drpFirstSize.SelectedIndex = sizeIndex >= drpFirstSize.Items.Count ? 0 : sizeIndex;


                // second stuff

                if (webPartToEdit.SecondFontItallic)
                    drpSecondItallic.SelectedIndex = 0;
                else
                    drpSecondItallic.SelectedIndex = 1;


                if (webPartToEdit.SecondFontBold)
                    drpSecondBold.SelectedIndex = 0;
                else
                    drpSecondBold.SelectedIndex = 1;

                if (webPartToEdit.SecondFontUnderline)
                    drpSecondUnderline.SelectedIndex = 0;
                else
                    drpSecondUnderline.SelectedIndex = 1;

                fontIndex = 0;
                foreach (ListItem item in drpSecondFonts.Items)
                {
                    if (item.Text.Equals(webPartToEdit.SecondFontName))
                        break;
                    fontIndex++;
                }
                drpSecondFonts.SelectedIndex = fontIndex >= drpSecondFonts.Items.Count ? 0 : fontIndex;

                sizeIndex = 0;
                foreach (ListItem item in drpSecondSize.Items)
                {
                    if (item.Text.Equals(webPartToEdit.SecondFontSize.ToString()))
                        break;
                    sizeIndex++;
                }
                drpSecondSize.SelectedIndex = sizeIndex >= drpSecondSize.Items.Count ? 0 : sizeIndex;

                if (webPartToEdit.IsSyncronised)
                    drpSyncWithStore.SelectedIndex = 0;
                else
                    drpSyncWithStore.SelectedIndex = 1; 
                
                // color value

                txtImageUrl.Text = webPartToEdit.ImageUrl;

                int imageIndex = 0;
                foreach (ListItem item in drpImageStyle.Items)
                {
                    if (item.Text.Equals(webPartToEdit.ImageStyle))
                        break;
                    imageIndex++;
                }

                drpImageStyle.SelectedIndex = (webPartToEdit.ImageStyle == null || webPartToEdit.ImageStyle.Equals(string.Empty)) ? 0 : imageIndex;

                chkDebug.Checked = webPartToEdit.IsDebug;
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {

            base.RenderContents(writer);

            string selfUrl = SPContext.Current.Site.RootWeb.Url;

            string styleSheetString = string.Format("<link rel=\"stylesheet\" media=\"screen\" type=\"text/css\" href=\"{1}/SiteAssets/{0}/css/colorpicker.css\" />", Constants.AssetString, selfUrl);
            string jqueryString = string.Format("<script type=\"text/javascript\" src=\"{1}/SiteAssets/{0}/js/jquery.js\"></script>", Constants.AssetString, selfUrl);
            string colorPickerString = string.Format("<script type=\"text/javascript\" src=\"{1}/SiteAssets/{0}/js/colorpicker.js\"></script>", Constants.AssetString, selfUrl);
            string initColorPicker = string.Format("<script language=\"javascript\" type=\"text/javascript\" src=\"{1}/SiteAssets/{0}/InitColorPicker.js\"></script>", Constants.AssetString, selfUrl);


            //writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
            //writer.AddAttribute(HtmlTextWriterAttribute.Src, "//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js");
            //writer.RenderBeginTag(HtmlTextWriterTag.Script);
            //writer.RenderEndTag();

            //writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
            //writer.RenderBeginTag(HtmlTextWriterTag.Script);
            //writer.WriteLine(js.ToString());
            //writer.RenderEndTag();  

            //base.RenderContents(writer);

            //writer.Write("<script language=\"javascript\" type=\"text/javascript\" src=\"/SiteAssets/Assets/InitColorPicker.js\"></script>\")");

            writer.Write(jqueryString);
            writer.Write(styleSheetString);
            writer.Write(colorPickerString);
            writer.Write(initColorPicker);
        }
    }
}
