using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace SP.Title
{
    public class SPSetttingsProvider: IDisposable
    {

        SPWeb _web = null;

        public Exception exception {get; set;}

        public SPSetttingsProvider(SPWeb web)
        {
            _web = web;
        }

        public SPSetttingsProvider()
        {


        }


        public bool LoadSPSetting(TitleWebpart.TitleWebpart webpart)
        {
            try
            {
                SPSite site = SPContext.Current.Site; 
                using (SPWeb web = site.RootWeb)
                {
                    SPList listInstance = web.Lists.TryGetList(Constants.SettingsListName);

                    if (listInstance == null)
                        throw new Exception("Titlewebpartpropertylist is not found");
                    else
                    {
                        SPListItemCollection items = listInstance.Items;
                        foreach (SPListItem item in items)
                        {
                            // init first
                            if (item[Constants.SettingsListPropertyName].ToString().Equals("FirstBackColorString"))
                                webpart.FirstBackColorString = item[Constants.SettingsListPropertyValue] == null ? string.Empty : item[Constants.SettingsListPropertyValue].ToString();
                            else if (item[Constants.SettingsListPropertyName].ToString().Equals("FirstFontBold"))
                                webpart.FirstFontBold = item[Constants.SettingsListPropertyValue].ToString().Equals("true") ? true : false;
                            else if (item[Constants.SettingsListPropertyName].ToString().Equals("FirstFontItallic"))
                                webpart.FirstFontItallic = item[Constants.SettingsListPropertyValue].ToString().Equals("true") ? true : false;
                            else if (item[Constants.SettingsListPropertyName].ToString().Equals("FirstFontName"))
                                webpart.FirstFontName = item[Constants.SettingsListPropertyValue] == null ? string.Empty : item[Constants.SettingsListPropertyValue].ToString();
                            else if (item[Constants.SettingsListPropertyName].ToString().Equals("FirstFontSize"))
                                webpart.FirstFontSize = item[Constants.SettingsListPropertyValue] == null ? 0 : int.Parse(item[Constants.SettingsListPropertyValue].ToString());
                            else if (item[Constants.SettingsListPropertyName].ToString().Equals("FirstFontUnderline"))
                                webpart.FirstFontUnderline = item[Constants.SettingsListPropertyValue].ToString().Equals("true") ? true : false;
                            else if (item[Constants.SettingsListPropertyName].ToString().Equals("FirstForeColorString"))
                                webpart.FirstForeColorString = item[Constants.SettingsListPropertyValue]== null ? string.Empty : item[Constants.SettingsListPropertyValue].ToString();

                            // init second
                            if (item[Constants.SettingsListPropertyName].ToString().Equals("SecondBackColorString"))
                                webpart.SecondBackColorString = item[Constants.SettingsListPropertyValue] == null ? string.Empty : item[Constants.SettingsListPropertyValue].ToString();
                            else if (item[Constants.SettingsListPropertyName].ToString().Equals("SecondFontBold"))
                                webpart.SecondFontBold = item[Constants.SettingsListPropertyValue].ToString().Equals("true") ? true : false;
                            else if (item[Constants.SettingsListPropertyName].ToString().Equals("SecondFontItallic"))
                                webpart.SecondFontItallic = item[Constants.SettingsListPropertyValue].ToString().Equals("true") ? true : false;
                            else if (item[Constants.SettingsListPropertyName].ToString().Equals("SecondFontName"))
                                webpart.SecondFontName = item[Constants.SettingsListPropertyValue] == null ? string.Empty : item[Constants.SettingsListPropertyValue].ToString();
                            else if (item[Constants.SettingsListPropertyName].ToString().Equals("SecondFontSize"))
                                webpart.SecondFontSize = item[Constants.SettingsListPropertyValue] == null ? 0 : int.Parse(item[Constants.SettingsListPropertyValue].ToString());
                            else if (item[Constants.SettingsListPropertyName].ToString().Equals("SecondFontUnderline"))
                                webpart.SecondFontUnderline = item[Constants.SettingsListPropertyValue].ToString().Equals("true") ? true : false;
                            else if (item[Constants.SettingsListPropertyName].ToString().Equals("SecondForeColorString"))
                                webpart.SecondForeColorString = item[Constants.SettingsListPropertyValue] == null ? string.Empty : item[Constants.SettingsListPropertyValue].ToString();

                            if (item[Constants.SettingsListPropertyName].ToString().Equals("ImageStyle"))
                                webpart.ImageStyle = item[Constants.SettingsListPropertyValue] == null ? string.Empty : item[Constants.SettingsListPropertyValue].ToString();
                            else if (item[Constants.SettingsListPropertyName].ToString().Equals("ImageUrl"))
                                webpart.ImageUrl = item[Constants.SettingsListPropertyValue] == null ? string.Empty : item[Constants.SettingsListPropertyValue].ToString();
                            
                        }                        
                    }
                }
                return true; 
            }
            catch (Exception ex)
            {
                exception = ex;
                return false; 
            }
            return false; 
        }

        public bool SaveSPSetting(TitleWebpart.TitleWebpart webpart)
        {
            try
            {                
                var webID = SPContext.Current == null ? _web.ID : SPContext.Current.Site.RootWeb.ID;

                using (SPWeb web = SPContext.Current == null ? _web : SPContext.Current.Site.OpenWeb(webID))
                {
                    bool oldUnsafe = web.AllowUnsafeUpdates;
                    web.AllowUnsafeUpdates = true;
                    SPList listInstance = web.Lists.TryGetList(Constants.SettingsListName);

                    if (listInstance == null)
                        throw new Exception("Titlewebpartpropertylist is not found");
                    else
                    {
                        SPListItemCollection items = listInstance.GetItems(); 
                          
                        try
                        {
                            foreach (SPListItem item in items)
                            {
                                // init first
                                if (item[Constants.SettingsListPropertyName].ToString().Equals("FirstBackColorString"))
                                    item[Constants.SettingsListPropertyValue] = webpart.FirstBackColorString;
                                else if (item[Constants.SettingsListPropertyName].ToString().Equals("FirstFontBold"))
                                    item[Constants.SettingsListPropertyValue] = webpart.FirstFontBold ? "true" : "false";
                                else if (item[Constants.SettingsListPropertyName].ToString().Equals("FirstFontItallic"))
                                    item[Constants.SettingsListPropertyValue] = webpart.FirstFontItallic ? "true" : "false";
                                else if (item[Constants.SettingsListPropertyName].ToString().Equals("FirstFontName"))
                                    item[Constants.SettingsListPropertyValue] = webpart.FirstFontName.ToString();
                                else if (item[Constants.SettingsListPropertyName].ToString().Equals("FirstFontSize"))
                                    item[Constants.SettingsListPropertyValue] = webpart.FirstFontSize.ToString();
                                else if (item[Constants.SettingsListPropertyName].ToString().Equals("FirstFontUnderline"))
                                    item[Constants.SettingsListPropertyValue] = webpart.FirstFontUnderline ? "true" : "false";
                                else if (item[Constants.SettingsListPropertyName].ToString().Equals("FirstForeColorString"))
                                    item[Constants.SettingsListPropertyValue] = webpart.FirstForeColorString;

                                // init second
                                if (item[Constants.SettingsListPropertyName].ToString().Equals("SecondBackColorString"))
                                    item[Constants.SettingsListPropertyValue] = webpart.SecondBackColorString;
                                else if (item[Constants.SettingsListPropertyName].ToString().Equals("SecondFontBold"))
                                    item[Constants.SettingsListPropertyValue] = webpart.SecondFontBold ? "true" : "false";
                                else if (item[Constants.SettingsListPropertyName].ToString().Equals("SecondFontItallic"))
                                    item[Constants.SettingsListPropertyValue] = webpart.SecondFontItallic ? "true" : "false";
                                else if (item[Constants.SettingsListPropertyName].ToString().Equals("SecondFontName"))
                                    item[Constants.SettingsListPropertyValue] = webpart.SecondFontName;
                                else if (item[Constants.SettingsListPropertyName].ToString().Equals("SecondFontSize"))
                                    item[Constants.SettingsListPropertyValue] = webpart.SecondFontSize;
                                else if (item[Constants.SettingsListPropertyName].ToString().Equals("SecondFontUnderline"))
                                    item[Constants.SettingsListPropertyValue] = webpart.SecondFontUnderline ? "true" : "false";
                                else if (item[Constants.SettingsListPropertyName].ToString().Equals("SecondForeColorString"))
                                    item[Constants.SettingsListPropertyValue] = webpart.SecondForeColorString;

                                if (item[Constants.SettingsListPropertyName].ToString().Equals("ImageStyle"))
                                    item[Constants.SettingsListPropertyValue] = webpart.ImageStyle;
                                else if (item[Constants.SettingsListPropertyName].ToString().Equals("ImageUrl"))
                                    item[Constants.SettingsListPropertyValue] = webpart.ImageUrl;

                                item.SystemUpdate();
                            }
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = oldUnsafe;
                        }

                        //listInstance.Update();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                //throw ex;
                return false;
            }

            return false;
        }

        public bool InitSPSettings()
        {
            try
            {
                //SPSite site = SPContext.Current.Site;
                //using (SPWeb web = site.OpenWeb())
                //{
                    SPList listInstance = _web.Lists.TryGetList(Constants.SettingsListName);

                    if (listInstance == null)
                        throw new Exception("Titlewebpartpropertylist is not found");
                    else
                    {

                        // deleting all from the list
                        SPListItemCollection items = listInstance.Items;

                        for (int x = items.Count - 1; x >= 0; x--)
                        {
                            items[x].Delete();
                        }

                        listInstance.Update();

                        // adding default properties

                        SPListItem newItem1 = listInstance.AddItem();
                        newItem1["Title"] = "FirstBackColorString";
                        newItem1[Constants.SettingsListPropertyName] = "FirstBackColorString";
                        newItem1[Constants.SettingsListPropertyValue] = "";
                        newItem1.Update();

                        SPListItem newItem2 = listInstance.AddItem();
                        newItem2["Title"] = "FirstFontBold";
                        newItem2[Constants.SettingsListPropertyName] = "FirstFontBold";
                        newItem2[Constants.SettingsListPropertyValue] = "false";
                        newItem2.Update();

                        SPListItem newItem3 = listInstance.AddItem();
                        newItem3["Title"] = "FirstFontItallic";
                        newItem3[Constants.SettingsListPropertyName] = "FirstFontItallic";
                        newItem3[Constants.SettingsListPropertyValue] = "false";
                        newItem3.Update();

                        SPListItem newItem4 = listInstance.AddItem();
                        newItem4["Title"] = "FirstFontName";
                        newItem4[Constants.SettingsListPropertyName] = "FirstFontName";
                        newItem4[Constants.SettingsListPropertyValue] = "";
                        newItem4.Update();

                        SPListItem newItem5 = listInstance.AddItem();
                        newItem5["Title"] = "FirstFontSize";
                        newItem5[Constants.SettingsListPropertyName] = "FirstFontSize";
                        newItem5[Constants.SettingsListPropertyValue] = "10";
                        newItem5.Update();

                        SPListItem newItem6 = listInstance.AddItem();
                        newItem6["Title"] = "FirstFontUnderline";
                        newItem6[Constants.SettingsListPropertyName] = "FirstFontUnderline";
                        newItem6[Constants.SettingsListPropertyValue] = "false";
                        newItem6.Update();

                        SPListItem newItem7 = listInstance.AddItem();
                        newItem7["Title"] = "FirstForeColorString";
                        newItem7[Constants.SettingsListPropertyName] = "FirstForeColorString";
                        newItem7[Constants.SettingsListPropertyValue] = "";
                        newItem7.Update();

                        SPListItem newItem8 = listInstance.AddItem();
                        newItem8["Title"] = "SecondBackColorString";
                        newItem8[Constants.SettingsListPropertyName] = "SecondBackColorString";
                        newItem8[Constants.SettingsListPropertyValue] = "";
                        newItem8.Update();

                        SPListItem newItem9 = listInstance.AddItem();
                        newItem9["Title"] = "SecondFontBold";
                        newItem9[Constants.SettingsListPropertyName] = "SecondFontBold";
                        newItem9[Constants.SettingsListPropertyValue] = "false";
                        newItem9.Update();

                        SPListItem newItem10 = listInstance.AddItem();
                        newItem10["Title"] = "SecondFontItallic";
                        newItem10[Constants.SettingsListPropertyName] = "SecondFontItallic";
                        newItem10[Constants.SettingsListPropertyValue] = "false";
                        newItem10.Update();

                        SPListItem newItem11 = listInstance.AddItem();
                        newItem11["Title"] = "SecondFontName";
                        newItem11[Constants.SettingsListPropertyName] = "SecondFontName";
                        newItem11[Constants.SettingsListPropertyValue] = "";
                        newItem11.Update();

                        SPListItem newItem12 = listInstance.AddItem();
                        newItem12["Title"] = "SecondFontSize";
                        newItem12[Constants.SettingsListPropertyName] = "SecondFontSize";
                        newItem12[Constants.SettingsListPropertyValue] = "10";
                        newItem12.Update();

                        SPListItem newItem13 = listInstance.AddItem();
                        newItem13["Title"] = "SecondFontUnderline";
                        newItem13[Constants.SettingsListPropertyName] = "SecondFontUnderline";
                        newItem13[Constants.SettingsListPropertyValue] = "false";
                        newItem13.Update();

                        SPListItem newItem14 = listInstance.AddItem();
                        newItem14["Title"] = "SecondForeColorString";
                        newItem14[Constants.SettingsListPropertyName] = "SecondForeColorString";
                        newItem14[Constants.SettingsListPropertyValue] = "";
                        newItem14.Update();

                        SPListItem newItem15 = listInstance.AddItem();
                        newItem15["Title"] = "ImageStyle";
                        newItem15[Constants.SettingsListPropertyName] = "ImageStyle";
                        newItem15[Constants.SettingsListPropertyValue] = "";
                        newItem15.Update();

                        SPListItem newItem16 = listInstance.AddItem();
                        newItem16["Title"] = "ImageUrl";
                        newItem16[Constants.SettingsListPropertyName] = "ImageUrl";
                        newItem16[Constants.SettingsListPropertyValue] = "";
                        newItem16.Update();

                        listInstance.Update();

                    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                //throw ex;
                return false;
            }

            return false;
        }

        public bool CreateSPSettingsList(SPWeb web)
        {
            try
            {
                SPList instance = null;
                Guid id = web.Lists.Add(Constants.SettingsListName, Constants.SettingsListName, SPListTemplateType.GenericList);
                instance = web.Lists[id];
                instance.Fields.Add(Constants.SettingsListPropertyName, SPFieldType.Text, true);
                instance.Fields.Add(Constants.SettingsListPropertyValue, SPFieldType.Text, true);
                instance.Hidden = false;
                instance.Update();

                InitSPSettings();
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw ex;
                return false;
            }

            return false;
        }

        //public bool hasUserWriteRight(SPUser user)
        //{ 
        //    //SPList instance = _web.Lists.TryGetList(Constants.SettingsListName);
        //    //if (instance != null)
        //    //{
        //    //    foreach (SPRoleAssignment assignment in instance.RoleAssignments)
        //    //    {
        //    //        foreach (SPRoleDefinition definition in assignment.RoleDefinitionBindings)
        //    //        { 
        //    //            if(definition.BasePermissions == SPBasePermissions.

        //    //        }
        //    //    }

        //    //} 
        //}

        public bool DestroySPSettingsList()
        {
            try
            {
                SPList instance = _web.Lists.TryGetList(Constants.SettingsListName);

                if (instance != null)
                    _web.Lists.Delete(instance.ID);    
            }
            catch (Exception ex)
            {
                exception = ex;
                throw ex;
            }
            return false;
        }

        public void Dispose()
        {
            if (_web != null)
                _web.Dispose();
        }
    }
}
