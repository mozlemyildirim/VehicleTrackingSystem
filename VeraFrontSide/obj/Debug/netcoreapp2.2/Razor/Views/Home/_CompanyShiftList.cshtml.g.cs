#pragma checksum "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "03bd8264e62f1a2754ffab7b005389dcd123241a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home__CompanyShiftList), @"mvc.1.0.view", @"/Views/Home/_CompanyShiftList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/_CompanyShiftList.cshtml", typeof(AspNetCore.Views_Home__CompanyShiftList))]
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
#line 1 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\_ViewImports.cshtml"
using VeraFrontSide;

#line default
#line hidden
#line 2 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\_ViewImports.cshtml"
using VeraFrontSide.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"03bd8264e62f1a2754ffab7b005389dcd123241a", @"/Views/Home/_CompanyShiftList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c58bca9feb3622cf226aa7f72e5ee2d1d4dc7c3a", @"/Views/_ViewImports.cshtml")]
    public class Views_Home__CompanyShiftList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<VeraDAL.Entities.Shift>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(37, 580, true);
            WriteLiteral(@"<table class=""table mt-2 table-bordered table-striped font-10"">
    <thead>
        <tr>
            <th></th>
            <th>Çizelge Adı</th>
            <th>Başlangıç Tarihi</th>
            <th>Bitiş Tarihi</th>
            <th>Başlangıç Saati</th>
            <th>Bitiş Saati</th>
            <th>Pazartesi</th>
            <th>Salı</th>
            <th>Çarşamba</th>
            <th>Perşembe</th>
            <th>Cuma</th>
            <th>Cumartesi</th>
            <th>Pazar</th>
            <th>Tanım Zamanı</th>
        </tr>
    </thead>
    <tbody>
");
            EndContext();
#line 22 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
            BeginContext(666, 59, true);
            WriteLiteral("            <tr>\r\n                <td><button type=\"button\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 725, "\"", 763, 3);
            WriteAttributeValue("", 735, "DeleteCompanyShift(", 735, 19, true);
#line 25 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
WriteAttributeValue("", 754, item.Id, 754, 8, false);

#line default
#line hidden
            WriteAttributeValue("", 762, ")", 762, 1, true);
            EndWriteAttribute();
            BeginContext(764, 91, true);
            WriteLiteral(" style=\"color:white;\" class=\"btn btn-dark btn-xs\"> SİL </button></td>\r\n                <td>");
            EndContext();
            BeginContext(856, 9, false);
#line 26 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
               Write(item.Name);

#line default
#line hidden
            EndContext();
            BeginContext(865, 28, true);
            WriteLiteral(" </td>\r\n                <td>");
            EndContext();
            BeginContext(894, 14, false);
#line 27 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
               Write(item.StartDate);

#line default
#line hidden
            EndContext();
            BeginContext(908, 28, true);
            WriteLiteral(" </td>\r\n                <td>");
            EndContext();
            BeginContext(937, 12, false);
#line 28 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
               Write(item.EndDate);

#line default
#line hidden
            EndContext();
            BeginContext(949, 28, true);
            WriteLiteral(" </td>\r\n                <td>");
            EndContext();
            BeginContext(978, 14, false);
#line 29 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
               Write(item.StartHour);

#line default
#line hidden
            EndContext();
            BeginContext(992, 28, true);
            WriteLiteral(" </td>\r\n                <td>");
            EndContext();
            BeginContext(1021, 12, false);
#line 30 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
               Write(item.EndHour);

#line default
#line hidden
            EndContext();
            BeginContext(1033, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1062, 42, false);
#line 31 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
                Write(item.Pazartesi == true ? "ACIK" : "KAPALI");

#line default
#line hidden
            EndContext();
            BeginContext(1105, 28, true);
            WriteLiteral(" </td>\r\n                <td>");
            EndContext();
            BeginContext(1135, 37, false);
#line 32 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
                Write(item.Sali == true ? "ACIK" : "KAPALI");

#line default
#line hidden
            EndContext();
            BeginContext(1173, 28, true);
            WriteLiteral(" </td>\r\n                <td>");
            EndContext();
            BeginContext(1203, 41, false);
#line 33 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
                Write(item.Carsamba == true ? "ACIK" : "KAPALI");

#line default
#line hidden
            EndContext();
            BeginContext(1245, 28, true);
            WriteLiteral(" </td>\r\n                <td>");
            EndContext();
            BeginContext(1275, 41, false);
#line 34 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
                Write(item.Persembe == true ? "ACIK" : "KAPALI");

#line default
#line hidden
            EndContext();
            BeginContext(1317, 28, true);
            WriteLiteral(" </td>\r\n                <td>");
            EndContext();
            BeginContext(1347, 37, false);
#line 35 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
                Write(item.Cuma == true ? "ACIK" : "KAPALI");

#line default
#line hidden
            EndContext();
            BeginContext(1385, 28, true);
            WriteLiteral(" </td>\r\n                <td>");
            EndContext();
            BeginContext(1415, 42, false);
#line 36 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
                Write(item.Cumartesi == true ? "ACIK" : "KAPALI");

#line default
#line hidden
            EndContext();
            BeginContext(1458, 28, true);
            WriteLiteral(" </td>\r\n                <td>");
            EndContext();
            BeginContext(1488, 38, false);
#line 37 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
                Write(item.Pazar == true ? "ACIK" : "KAPALI");

#line default
#line hidden
            EndContext();
            BeginContext(1527, 28, true);
            WriteLiteral(" </td>\r\n                <td>");
            EndContext();
            BeginContext(1556, 17, false);
#line 38 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
               Write(item.CreationDate);

#line default
#line hidden
            EndContext();
            BeginContext(1573, 26, true);
            WriteLiteral("</td>\r\n            </tr>\r\n");
            EndContext();
#line 40 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_CompanyShiftList.cshtml"
        }

#line default
#line hidden
            BeginContext(1610, 24, true);
            WriteLiteral("\r\n    </tbody>\r\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<VeraDAL.Entities.Shift>> Html { get; private set; }
    }
}
#pragma warning restore 1591
