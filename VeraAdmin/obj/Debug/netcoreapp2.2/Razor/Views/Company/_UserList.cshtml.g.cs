#pragma checksum "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Company\_UserList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0c3341f662149995949ea2d122ee2136215b7a40"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Company__UserList), @"mvc.1.0.view", @"/Views/Company/_UserList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Company/_UserList.cshtml", typeof(AspNetCore.Views_Company__UserList))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0c3341f662149995949ea2d122ee2136215b7a40", @"/Views/Company/_UserList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e98273b5ba9e1a8f6da536bfe2b73f48a9bb8a6", @"/Views/_ViewImports.cshtml")]
    public class Views_Company__UserList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<VeraDAL.Models.UserRepo>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(38, 421, true);
            WriteLiteral(@"	<button onclick=""ShowUserModal()"" class=""btn btn-primary rounded-4 "" style="" margin-bottom: 10px;  width: 100%;  font-size: large;""> Kullanıcı Ekle </button>
<table class=""table table-striped table-bordered table-hover"" id=""sample_1"">
	
	<thead>
		<tr>
			<th></th>
			<th></th>
			<th>Kullanıcı Kodu</th>
			<th>Adı</th>
			<th>Soyadı</th>
			<th>Şirket</th>
			<th>Yetki</th>
		</tr>
	</thead>
	<tbody>
");
            EndContext();
#line 17 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Company\_UserList.cshtml"
         foreach (var item in Model)
		{

#line default
#line hidden
            BeginContext(496, 17, true);
            WriteLiteral("\t\t<tr>\r\n\t\t\t<td><a");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 513, "\"", 555, 3);
            WriteAttributeValue("", 523, "GetUserIdForDisplaying(", 523, 23, true);
#line 20 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Company\_UserList.cshtml"
WriteAttributeValue("", 546, item.Id, 546, 8, false);

#line default
#line hidden
            WriteAttributeValue("", 554, ")", 554, 1, true);
            EndWriteAttribute();
            BeginContext(556, 111, true);
            WriteLiteral("><i style=\"color:green; margin-left: 25px;margin-top: 7px;\" class=\"fa fa-edit fa-lg\"></i> </a></td> \r\n\t\t\t<td><a");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 667, "\"", 697, 3);
            WriteAttributeValue("", 677, "DeleteUser(", 677, 11, true);
#line 21 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Company\_UserList.cshtml"
WriteAttributeValue("", 688, item.Id, 688, 8, false);

#line default
#line hidden
            WriteAttributeValue("", 696, ")", 696, 1, true);
            EndWriteAttribute();
            BeginContext(698, 109, true);
            WriteLiteral("><i style=\"color:red; margin-left: 25px;margin-top: 7px;\" class=\"fa fa-trash  fa-lg\"></i> </a></td> \r\n\t\t\t<td>");
            EndContext();
            BeginContext(808, 13, false);
#line 22 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Company\_UserList.cshtml"
           Write(item.UserCode);

#line default
#line hidden
            EndContext();
            BeginContext(821, 15, true);
            WriteLiteral(" </td>\r\n\t\t\t<td>");
            EndContext();
            BeginContext(837, 9, false);
#line 23 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Company\_UserList.cshtml"
           Write(item.Name);

#line default
#line hidden
            EndContext();
            BeginContext(846, 14, true);
            WriteLiteral("</td>\r\n\t\t\t<td>");
            EndContext();
            BeginContext(861, 12, false);
#line 24 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Company\_UserList.cshtml"
           Write(item.Surname);

#line default
#line hidden
            EndContext();
            BeginContext(873, 14, true);
            WriteLiteral("</td>\r\n\t\t\t<td>");
            EndContext();
            BeginContext(888, 16, false);
#line 25 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Company\_UserList.cshtml"
           Write(item.CompanyName);

#line default
#line hidden
            EndContext();
            BeginContext(904, 14, true);
            WriteLiteral("</td>\r\n\t\t\t<td>");
            EndContext();
            BeginContext(919, 18, false);
#line 26 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Company\_UserList.cshtml"
           Write(item.Authorization);

#line default
#line hidden
            EndContext();
            BeginContext(937, 18, true);
            WriteLiteral(" </td> \r\n\t\t</tr>\r\n");
            EndContext();
#line 28 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Company\_UserList.cshtml"
		}

#line default
#line hidden
            BeginContext(960, 184, true);
            WriteLiteral("\r\n\t</tbody>\r\n</table>\r\n\r\n<script>\r\n\t$(\"#sample_1\").dataTable({\r\n\t\t\"language\": {\r\n\t\t\t\"url\": \"https://cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json\"\r\n\t\t}\r\n\t});\r\n</script>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<VeraDAL.Models.UserRepo>> Html { get; private set; }
    }
}
#pragma warning restore 1591