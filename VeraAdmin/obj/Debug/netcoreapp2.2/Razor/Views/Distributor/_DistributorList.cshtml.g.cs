#pragma checksum "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ecf1b91e6c1fff8496b9fcb3bd656a85ef83111f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Distributor__DistributorList), @"mvc.1.0.view", @"/Views/Distributor/_DistributorList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Distributor/_DistributorList.cshtml", typeof(AspNetCore.Views_Distributor__DistributorList))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\_ViewImports.cshtml"
using VeraAdmin;

#line default
#line hidden
#line 2 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\_ViewImports.cshtml"
using VeraAdmin.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ecf1b91e6c1fff8496b9fcb3bd656a85ef83111f", @"/Views/Distributor/_DistributorList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e98273b5ba9e1a8f6da536bfe2b73f48a9bb8a6", @"/Views/_ViewImports.cshtml")]
    public class Views_Distributor__DistributorList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<VeraDAL.Models.DistributorRepo>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(45, 529, true);
            WriteLiteral(@"<table class=""table table-striped table-bordered  table-hover"" id=""sample_1"" style=""border:solid; border-color:lightgray"">
	<thead>
		<tr>
			<th><b>Güncelle</b></th>
			<th><b>Sil</b></th>
			<th> Bayi Adı</th>
			<th> İş Ortağı </th>
			<th> Bayi Giriş Tarihi </th>
			<th> Bayi Çıkış Tarihi </th>
			<th> Telefon 1 </th>
			<th> Telefon 2 </th>
			<th> Fax </th>
			<th> Şehir </th>
			<th> Bayi Yetkilisi </th>
			<th> Aktiflik </th>
			<th> Bayi Kodu </th>
			<th> Adres </th>
		</tr>
	</thead>
	<tbody>
");
            EndContext();
#line 22 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
         foreach (var item in Model)
		{

#line default
#line hidden
            BeginContext(611, 19, true);
            WriteLiteral("\t\t\t<tr>\r\n\t\t\t\t<td><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 630, "\"", 689, 3);
            WriteAttributeValue("", 637, "javascript:GetDistributorByIdForDisplaying(", 637, 43, true);
#line 25 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
WriteAttributeValue("", 680, item.Id, 680, 8, false);

#line default
#line hidden
            WriteAttributeValue("", 688, ")", 688, 1, true);
            EndWriteAttribute();
            BeginContext(690, 110, true);
            WriteLiteral("><i style=\"color:green; margin-left:34px; margin-top:12px;\" class=\"fa fa-edit fa-2x\"></i></a></td>\r\n\t\t\t\t<td><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 800, "\"", 845, 3);
            WriteAttributeValue("", 807, "javascript:DeleteDistributor(", 807, 29, true);
#line 26 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
WriteAttributeValue("", 836, item.Id, 836, 8, false);

#line default
#line hidden
            WriteAttributeValue("", 844, ")", 844, 1, true);
            EndWriteAttribute();
            BeginContext(846, 112, true);
            WriteLiteral("><i style=\" color: red;margin-left:5px; margin-top:10px;\"  class=\"fa fa-trash-o  fa-2x\"></i></a></td>\r\n\t\t\t\t<td> ");
            EndContext();
            BeginContext(959, 9, false);
#line 27 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
                Write(item.Name);

#line default
#line hidden
            EndContext();
            BeginContext(968, 16, true);
            WriteLiteral("</td>\r\n\t\t\t\t<td> ");
            EndContext();
            BeginContext(986, 49, false);
#line 28 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
                 Write(item.CompanyPartner==null?"-":item.CompanyPartner);

#line default
#line hidden
            EndContext();
            BeginContext(1036, 16, true);
            WriteLiteral("</td>\r\n\t\t\t\t<td> ");
            EndContext();
            BeginContext(1054, 94, false);
#line 29 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
                 Write(item.EntranceDate == null ? "-" : Convert.ToDateTime(item.EntranceDate).ToString("dd/MM/yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(1149, 16, true);
            WriteLiteral("</td>\r\n\t\t\t\t<td> ");
            EndContext();
            BeginContext(1167, 86, false);
#line 30 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
                 Write(item.ExitDate == null ? "-" : Convert.ToDateTime(item.ExitDate).ToString("dd/MM/yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(1254, 16, true);
            WriteLiteral("</td>\r\n\t\t\t\t<td> ");
            EndContext();
            BeginContext(1271, 11, false);
#line 31 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
                Write(item.Phone1);

#line default
#line hidden
            EndContext();
            BeginContext(1282, 16, true);
            WriteLiteral("</td>\r\n\t\t\t\t<td> ");
            EndContext();
            BeginContext(1299, 11, false);
#line 32 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
                Write(item.Phone2);

#line default
#line hidden
            EndContext();
            BeginContext(1310, 16, true);
            WriteLiteral("</td>\r\n\t\t\t\t<td> ");
            EndContext();
            BeginContext(1327, 8, false);
#line 33 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
                Write(item.Fax);

#line default
#line hidden
            EndContext();
            BeginContext(1335, 16, true);
            WriteLiteral("</td>\r\n\t\t\t\t<td> ");
            EndContext();
            BeginContext(1352, 9, false);
#line 34 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
                Write(item.City);

#line default
#line hidden
            EndContext();
            BeginContext(1361, 16, true);
            WriteLiteral("</td>\r\n\t\t\t\t<td> ");
            EndContext();
            BeginContext(1378, 19, false);
#line 35 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
                Write(item.PersonInCharge);

#line default
#line hidden
            EndContext();
            BeginContext(1397, 16, true);
            WriteLiteral("</td>\r\n\t\t\t\t<td> ");
            EndContext();
            BeginContext(1415, 39, false);
#line 36 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
                 Write(item.Activity == true?"Aktif" : "Pasif");

#line default
#line hidden
            EndContext();
            BeginContext(1455, 16, true);
            WriteLiteral("</td>\r\n\t\t\t\t<td> ");
            EndContext();
            BeginContext(1472, 9, false);
#line 37 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
                Write(item.Code);

#line default
#line hidden
            EndContext();
            BeginContext(1481, 16, true);
            WriteLiteral("</td>\r\n\t\t\t\t<td> ");
            EndContext();
            BeginContext(1498, 12, false);
#line 38 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
                Write(item.Address);

#line default
#line hidden
            EndContext();
            BeginContext(1510, 17, true);
            WriteLiteral("</td>\r\n\t\t\t</tr>\r\n");
            EndContext();
#line 40 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\_DistributorList.cshtml"
		} 

#line default
#line hidden
            BeginContext(1533, 188, true);
            WriteLiteral("\t</tbody> \r\n</table> \r\n<script>\r\n\t//$(\"#sample_1\").dataTable({\r\n\t//\t\"language\": {\r\n\t//\t\t\"url\": \"https://cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json\"\r\n\t//\t}\r\n\t//});\r\n</script>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<VeraDAL.Models.DistributorRepo>> Html { get; private set; }
    }
}
#pragma warning restore 1591
