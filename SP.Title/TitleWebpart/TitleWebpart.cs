using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Drawing;
using System.Xml.Serialization;
using System.Threading;


namespace SP.Title.TitleWebpart
{
    [ToolboxItemAttribute(false)]
    public class TitleWebpart : WebPart
    {
        #region UI_Elements

        Label lblTitleFirst = new Label();
        Label lblTitleSecond = new Label();
        Label lblError = new Label();

        #endregion

        #region Properties_First

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public string FirstString {get; set;}

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public string FirstBackColorString { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public string FirstForeColorString { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public string FirstFontName { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public int FirstFontSize { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public bool FirstFontItallic { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public bool FirstFontUnderline { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public bool FirstFontBold { get; set; }

        #endregion

        #region Properties_Second

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]        
        public string SecondString { get; set;}

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public string SecondForeColorString { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public string SecondBackColorString { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public string SecondFontName { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public int SecondFontSize { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public bool SecondFontItallic { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public bool SecondFontUnderline { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public bool SecondFontBold { get; set; }

        #endregion

        #region Image_settings

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public string ImageUrl { get; set; }


        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public string ImageStyle { get; set; } 

        #endregion

        #region Properties_general

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false), DefaultValue(true)]
        public bool IsSyncronised {get; set;}

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public string Type { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public string Error { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(false)]
        public bool IsDebug { get; set; }

        #endregion

        #region Services

        protected Label setControlLayout(Label control, bool firstOrSecond)
        {
            if (firstOrSecond)
            {
                // settgins first label properties
                control.Text = FirstString;

                if ((FirstBackColorString != null) && (!FirstBackColorString.Equals(string.Empty)))
                {
                    int colorCode = Int32.Parse(FirstBackColorString, System.Globalization.NumberStyles.HexNumber);
                    control.BackColor = Color.FromArgb(colorCode);
                }

                if ((FirstForeColorString != null) && (!FirstForeColorString.Equals(string.Empty)))
                {
                    int colorCode = Int32.Parse(FirstForeColorString, System.Globalization.NumberStyles.HexNumber);
                    control.ForeColor = Color.FromArgb(colorCode);
                }

                if (FirstFontName != null)
                    control.Font.Name = FirstFontName;
                if (FirstFontItallic != null)
                    control.Font.Italic = FirstFontItallic;
                if (FirstFontUnderline != null)
                    control.Font.Underline = FirstFontUnderline;
                if (FirstFontBold != null)
                    control.Font.Bold = FirstFontBold;
                    control.Font.Size = FirstFontSize;

            }
            else
            {

                // setting second label properties
                control.Text = SecondString;

                if ((SecondBackColorString != null) && (!SecondBackColorString.Equals(string.Empty)))
                {
                    int colorCode = Int32.Parse(SecondBackColorString, System.Globalization.NumberStyles.HexNumber);
                    control.BackColor = Color.FromArgb(colorCode);
                }

                if ((SecondForeColorString != null) && (!SecondForeColorString.Equals(string.Empty)))
                {
                    int colorCode = Int32.Parse(SecondForeColorString, System.Globalization.NumberStyles.HexNumber);
                    control.ForeColor = Color.FromArgb(colorCode);
                }

                if (SecondFontName != null)
                    control.Font.Name = SecondFontName;
                if (SecondFontItallic != null)
                    control.Font.Italic = SecondFontItallic;
                if (SecondFontUnderline != null)
                    control.Font.Underline = SecondFontUnderline;
                if (SecondFontBold != null)
                    control.Font.Bold = SecondFontBold;
                    control.Font.Size = SecondFontSize;

            }

            return control;
        }


        #endregion

        protected override void CreateChildControls()
        {


            base.CreateChildControls();

            if (IsSyncronised)
            {
                using (SPSetttingsProvider provider = new SPSetttingsProvider())
                {
                    provider.LoadSPSetting(this);                    
                }
            }

            // add image depending of the style
            if (this.ImageStyle == null)
            {
                this.Controls.Add(setControlLayout(lblTitleFirst, true));
                this.Controls.Add(setControlLayout(lblTitleSecond, false));                

            }
            else if (this.ImageStyle.Equals(Constants.ImageStyle.After.ToString()))
            {
                this.Controls.Add(setControlLayout(lblTitleFirst, true));
                this.Controls.Add(setControlLayout(lblTitleSecond, false));
                LiteralControl imgLit = new LiteralControl(string.Format("<img id=\"img1\" class=\"img_after_style\" src=\"{0}\"> </img>", this.ImageUrl));
                this.Controls.Add(imgLit);

            }
            else if (this.ImageStyle.Equals(Constants.ImageStyle.Before.ToString()))
            {

                LiteralControl imgLit = new LiteralControl(string.Format("<img id=\"img1\" class=\"img_before_style\" src=\"{0}\"> </img>", this.ImageUrl));
                this.Controls.Add(imgLit);
                this.Controls.Add(setControlLayout(lblTitleFirst, true));
                this.Controls.Add(setControlLayout(lblTitleSecond, false));

            }
            else if (this.ImageStyle.Equals(Constants.ImageStyle.Continues.ToString()))
            {

                LiteralControl imgLitStart = new LiteralControl(string.Format("<div style=\"background-image:url({0}); background-repeat:repeat-x; width:100%\">", this.ImageUrl));
                this.Controls.Add(imgLitStart);


                this.Controls.Add(setControlLayout(lblTitleFirst, true));
                this.Controls.Add(setControlLayout(lblTitleSecond, false));

                LiteralControl imgLitEnd = new LiteralControl(string.Format("</div>", this.ImageUrl));
                this.Controls.Add(imgLitEnd);

            }
            else
            {
                this.Controls.Add(setControlLayout(lblTitleFirst, true));
                this.Controls.Add(setControlLayout(lblTitleSecond, false));
            }

            #region Error_Stuff

            if ((Error != null) && (!Error.Equals(string.Empty)) && (IsDebug != null) && (IsDebug))
            {
                lblError.Text = Error;
                this.Controls.Add(lblError);
            }

            #endregion

        }


        //protected override void OnPreRender(EventArgs e)
        //{
        //    base.OnPreRender(e);

        //    Label lblTitleFirst = new Label();
        //    lblTitleFirst.Text = FirstText;
        //    this.Controls.Add(lblTitleFirst);

        //    Label lblTitleSecond = new Label();
        //    lblTitleSecond.Text = SecondText;
        //    this.Controls.Add(lblTitleSecond);

        //}


        public override EditorPartCollection CreateEditorParts()
        {
            var newEditorPart = new TitleWebPartEditor
            {
                ID = ID + "_titleEditorPart",
                Title = "Custom Settings"
            };

            var newEditorPartCollection = new EditorPartCollection(new[] { newEditorPart });

          return new EditorPartCollection(newEditorPartCollection, base.CreateEditorParts());
        }


        public override object WebBrowsableObject
        {
            // Return a reference to the Web Part instance.
            get { return this; }
        }

        //public override EditorPartCollection CreateEditorParts()
        //{
        //    TabConfigurationEditorPart editorPart = new TabConfigurationEditorPart();

        //    // The ID of the editor part should be unique. So prefix it with the ID of the Web Part.
        //    editorPart.ID = this.ID + "_TabConfigurationEditorPart";

        //    // Create a collection of editor parts and add them to the EditorPart collection.
        //    List<EditorPart> editors = new List<EditorPart> { editorPart };
        //    return new EditorPartCollection(editors);
        //}

    }
}
