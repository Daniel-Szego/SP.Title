using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace SP.Title
{
    /// <summary>
    /// Sharepoint context
    /// </summary>
    public class ContextWrapper
    {
        public TitleWebpart.TitleWebpart _webPartToEdit;
        public Guid _webID;
        public Guid _siteID;

    }
}
