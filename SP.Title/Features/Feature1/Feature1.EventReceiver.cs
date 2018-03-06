using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using System.Collections.Generic;
using System.Linq;

namespace SP.Title.Features.Feature1
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("3625815c-8b1b-4159-bf70-fc41ad5a436f")]
    public class Feature1EventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            // create storage library


            SPSite site = properties.Feature.Parent as SPSite;
            using (SPWeb web = site.RootWeb)
            {
                //web.Lists.EnsureSiteAssetsLibrary();

                SPList instance = web.Lists.TryGetList(Constants.SettingsListName);

                if (instance == null)
                {
                    // initialising content of the list
                    using (SPSetttingsProvider provider = new SPSetttingsProvider(web))
                    {
                        provider.CreateSPSettingsList(web);
                    }
                }
            }
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            // TODO:  garbage collection

            try
            {
                var featureParentSite = properties.Feature.Parent as SPSite;
                if (featureParentSite != null)
                {
                    RollbackFeature(featureParentSite, false);
                    // delete param list
                    using (SPWeb web = featureParentSite.RootWeb)
                    {
                        using (SPSetttingsProvider provider = new SPSetttingsProvider(web))
                        {
                            provider.DestroySPSettingsList();
                        }
                    }
                }
            }
            finally
            {
                base.FeatureDeactivating(properties);
            }
        }


        // Uncomment the method below to handle the event raised after a feature has been installed.

        public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        {
                //SPSite site = properties.Feature.Parent as SPSite;
                //using (SPWeb web = site.OpenWeb())
                //{
                //    web.Lists.EnsureSiteAssetsLibrary();
                //}

        }


        private static void RollbackFeature(SPSite site, bool fullRollback)
        {
            if (site == null)
                throw new Exception("Parent Site should not be null.");

            var solutionFolders = new List<String>();
            var solutionWebParts = new List<String>();

            //add all style library sub directories.
            solutionFolders.Add(string.Format("SiteAssets/{0}",Constants.AssetString));

            //add all list web parts added
            solutionWebParts.Add(Constants.Webpartname);

            //Delete style library files
            try
            {
                if (fullRollback && solutionFolders.Count > 0)
                {
                    //go through each list
                    foreach (var subDir in solutionFolders)
                    {
                        try
                        {
                            var subDirs = subDir.Split(new[] { '/' });
                            switch (subDirs.Length)
                            {
                                case 1:
                                    DeleteSubFoldersAndFiles(site.RootWeb.Folders[subDir]);
                                    break;
                                case 2:
                                    {
                                        var folder = site.RootWeb.Folders[subDirs[0]];
                                        if (folder != null && folder.Exists)
                                        {
                                            DeleteSubFoldersAndFiles(folder.SubFolders[subDirs[1]]);
                                        }
                                    }
                                    break;
                            }
                        }
                        catch (Exception se)
                        {
                        }
                    } //end going through each primary sub folder added by feature
                }
            }
            catch (Exception se)
            {
            }

            //Delete Web part
            if (solutionWebParts.Count <= 0) return;
            try
            {
                var webPartGallery = site.RootWeb.Lists.TryGetList("Web Part Gallery") ??
                                     site.RootWeb.Lists.TryGetList("Webpartkatalog");
                if (webPartGallery == null) return;
                var webParts = webPartGallery.GetItems();
                var filesToDelete = (from SPListItem webPartTemplateFile in webParts
                                     from sWebPart in solutionWebParts
                                     where webPartTemplateFile.File.Name.Contains(sWebPart)
                                     select webPartTemplateFile.File).ToList();
                // delete Web Part template files
                foreach (var file in filesToDelete)
                {
                    file.Delete();
                }
            }
            catch (Exception se)
            {
            }
        }

        private static void DeleteSubFoldersAndFiles(SPFolder spFolder)
        {
            for (var i = spFolder.SubFolders.Count - 1; i >= 0; i--)
            {
                //delete the subfolder first
                DeleteSubFoldersAndFiles(spFolder.SubFolders[i]);
            }

            //now delete all of these files
            for (var i = spFolder.Files.Count - 1; i >= 0; i--)
            {
                try
                {
                    spFolder.Files[i].Delete();
                }
                catch (Exception se)
                {
                    /*Error("Deactivate: Unable to delete: " + subsubFolder.Name + " - " + i.ToString() + " " + ex.Message);*/
                }
            }

            //then delete the sub directory
            try
            {
                spFolder.Update();
                spFolder.Delete();
            }
            catch (Exception se)
            {
                /*Error("Deactivate: Unable to delete folder: " + subFolder.Folder.Name + " - " + j.ToString() + " " + ex.Message);*/
            }
        }

        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        {
            try
            {
                var featureParentSite = properties.UserCodeSite ?? properties.Feature.Parent as SPSite;
                if (featureParentSite != null)
                {
                    RollbackFeature(featureParentSite, true);
                }
            }
            finally
            {
                base.FeatureUninstalling(properties);
            }

        }

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
