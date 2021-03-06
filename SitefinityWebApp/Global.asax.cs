﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using CoverFlowWidget;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Samples.Common;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Data;

namespace SitefinityWebApp
{
    public class Global : System.Web.HttpApplication
    {
        private const string SamplesThemeName = "SamplesTheme";
        private const string SamplesThemePath = "~/App_Data/Sitefinity/WebsiteTemplates/Samples/App_Themes/Samples";

        private const string SamplesTemplateId = "015b4db0-1d4f-4938-afec-5da59749e0e8";
        private const string SamplesTemplateName = "SamplesMasterPage";
        private const string SamplesTemplatePath = "~/App_Data/Sitefinity/WebsiteTemplates/Samples/App_Master/Samples.master";

        private const string CoverFlowWidgetPageId = "b210e2f7-3a43-4621-a729-9c13b463a6c4";
        private const string CoverFlowWidgetPageName = "CoverFlow Sample";

        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Initialized += Bootstrapper_Initialized;
        }

        private void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
        {
            if ((Bootstrapper.IsDataInitialized) && (e.CommandName == "Bootstrapped"))
            {
                SystemManager.RunWithElevatedPrivilegeDelegate worker = new SystemManager.RunWithElevatedPrivilegeDelegate(this.CreateSample);
                SystemManager.RunWithElevatedPrivilege(worker);
            }
        }

        private void CreateSample(object[] args)
        {         
            SampleUtilities.RegisterToolboxWidget("CoverFlowWidget", typeof(CoverFlow), "Samples");
            SampleUtilities.RegisterTheme(SamplesThemeName, SamplesThemePath);
            SampleUtilities.RegisterTemplate(new Guid(SamplesTemplateId), SamplesTemplateName, SamplesTemplateName, SamplesTemplatePath, SamplesThemeName);

            SampleUtilities.UploadImages(HttpRuntime.AppDomainAppPath + "Images", "CoverFlowImages");

            var result = SampleUtilities.CreatePage(new Guid(CoverFlowWidgetPageId), CoverFlowWidgetPageName, true);

            if (result)
            {
                SampleUtilities.SetTemplateToPage(new Guid(CoverFlowWidgetPageId), new Guid(SamplesTemplateId));

                CoverFlow myWidget = new CoverFlow();
                myWidget.AlbumTitle = "CoverFlowImages";

                SampleUtilities.AddControlToPage(new Guid(CoverFlowWidgetPageId), myWidget, "Content", "CoverFlowWidget");
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}