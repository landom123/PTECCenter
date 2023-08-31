<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    CodeBehind="rptComtract.aspx.vb" Inherits="PTECCENTER.rptComtract" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<system.web>
  <httpHandlers>
    <add verb=" * "  path="Reserved.ReportViewerWebControl.axd" 
         type="Microsoft.Reporting.WebForms.HttpHandler,
               Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral,
               PublicKeyToken=b03f5f7f11d50a3a" />
  </httpHandlers>
</system.web>
<system.webServer>
  <handlers>
    <add name="ReportViewerWebControlHandler" preCondition="integratedMode"
         verb="*" path="Reserved.ReportViewerWebControl.axd" 
         type="Microsoft.Reporting.WebForms.HttpHandler, 
               Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral,
               PublicKeyToken=b03f5f7f11d50a3a"/>
  </handlers>
</system.webServer>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin: 0px; padding: 0px;width:100%;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
<%--        <rsweb:ReportViewer ID="ReportViewer1" runat="server" >
            <LocalReport ReportPath="Report.rdlc">  
                <DataSources>  
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />  
                </DataSources>  
            </LocalReport>  
        </rsweb:ReportViewer>  --%>

        <div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"  Height="1000px">
            </rsweb:ReportViewer>
        </div>

    </form>
</body>
</html>