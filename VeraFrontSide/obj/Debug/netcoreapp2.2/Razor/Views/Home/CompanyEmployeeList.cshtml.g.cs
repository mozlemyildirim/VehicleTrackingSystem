#pragma checksum "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\CompanyEmployeeList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "353a2210f2dc308a31fdceca8df36911a27d5439"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_CompanyEmployeeList), @"mvc.1.0.view", @"/Views/Home/CompanyEmployeeList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/CompanyEmployeeList.cshtml", typeof(AspNetCore.Views_Home_CompanyEmployeeList))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"353a2210f2dc308a31fdceca8df36911a27d5439", @"/Views/Home/CompanyEmployeeList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c58bca9feb3622cf226aa7f72e5ee2d1d4dc7c3a", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_CompanyEmployeeList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<VeraDAL.Entities.Employee>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(40, 442, true);
            WriteLiteral(@"<table id=""companyEmployeeTable"" class=""table table-bordered table-striped font-10"">
    <thead>
        <tr>
            <th></th>
            <th></th>
            <th>Görevli Adı</th>
            <th>Görevli Soyadı</th>
            <th>Görevli No</th>
            <th>Doğum Tarihi</th>
            <th>Cep Telefonu</th>
            <th>Ev Telefonu</th>
            <th>Görev Tipi</th>
        </tr>
    </thead>
    <tbody>
");
            EndContext();
#line 17 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\CompanyEmployeeList.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
            BeginContext(531, 60, true);
            WriteLiteral("            <tr>\r\n                <td><a href=\"javascript:;\"");
            EndContext();
            BeginWriteAttribute("onclick", "  onclick=\"", 591, "\"", 633, 3);
            WriteAttributeValue("", 602, "DeleteCompanyEmployee(", 602, 22, true);
#line 20 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\CompanyEmployeeList.cshtml"
WriteAttributeValue("", 624, item.Id, 624, 8, false);

#line default
#line hidden
            WriteAttributeValue("", 632, ")", 632, 1, true);
            EndWriteAttribute();
            BeginContext(634, 124, true);
            WriteLiteral("><i style=\"color:red\" class=\"fa fa-trash fa-lg\" aria-hidden=\"true\"></i></a></td>\r\n                <td><a href=\"javascript:;\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 758, "\"", 804, 3);
            WriteAttributeValue("", 768, "GetCompanyEmployeeToUpdate(", 768, 27, true);
#line 21 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\CompanyEmployeeList.cshtml"
WriteAttributeValue("", 795, item.Id, 795, 8, false);

#line default
#line hidden
            WriteAttributeValue("", 803, ")", 803, 1, true);
            EndWriteAttribute();
            BeginContext(805, 63, true);
            WriteLiteral("><i class=\"fa fa-cog fa-lg\"></i></a></td>\r\n                <td>");
            EndContext();
            BeginContext(869, 9, false);
#line 22 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\CompanyEmployeeList.cshtml"
               Write(item.Name);

#line default
#line hidden
            EndContext();
            BeginContext(878, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(906, 12, false);
#line 23 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\CompanyEmployeeList.cshtml"
               Write(item.Surname);

#line default
#line hidden
            EndContext();
            BeginContext(918, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(946, 15, false);
#line 24 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\CompanyEmployeeList.cshtml"
               Write(item.EmployeeNo);

#line default
#line hidden
            EndContext();
            BeginContext(961, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(989, 14, false);
#line 25 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\CompanyEmployeeList.cshtml"
               Write(item.BirthDate);

#line default
#line hidden
            EndContext();
            BeginContext(1003, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1031, 16, false);
#line 26 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\CompanyEmployeeList.cshtml"
               Write(item.PhoneNumber);

#line default
#line hidden
            EndContext();
            BeginContext(1047, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1075, 20, false);
#line 27 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\CompanyEmployeeList.cshtml"
               Write(item.HomePhoneNumber);

#line default
#line hidden
            EndContext();
            BeginContext(1095, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1123, 15, false);
#line 28 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\CompanyEmployeeList.cshtml"
               Write(item.Occupation);

#line default
#line hidden
            EndContext();
            BeginContext(1138, 26, true);
            WriteLiteral("</td>\r\n            </tr>\r\n");
            EndContext();
#line 30 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\CompanyEmployeeList.cshtml"
        }

#line default
#line hidden
            BeginContext(1175, 546, true);
            WriteLiteral(@"    </tbody>
</table>
<script>

     function DeleteCompanyEmployee(empId)
            {
                swal({
                title: ""Emin misiniz?"",
                text: ""Bu görevliyi silerseniz geri alamayacaksınız!!"",
                icon: ""warning"",
                buttons: [
                'Hayır, iptal et!',
                'Evet, eminim!'
                ],
              dangerMode: true,
              }).then(function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    url: '");
            EndContext();
            BeginContext(1722, 43, false);
#line 49 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\CompanyEmployeeList.cshtml"
                     Write(Url.Action("DeleteCompanyEmployee", "Home"));

#line default
#line hidden
            EndContext();
            BeginContext(1765, 567, true);
            WriteLiteral(@"',
                    type: 'POST',
                    dataType: 'json',
                        data: {
                            '_employeeId': empId
                        },
                    success: function (data) {
                         if (data) {
                             swal('Başarılı!', 'Görevli Silindi!', 'success');
                             getEmployeeList();
                         } 
                        }
                     });
                 }
              })
           
            }
    
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<VeraDAL.Entities.Employee>> Html { get; private set; }
    }
}
#pragma warning restore 1591