#pragma checksum "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e61708ae4f456522b7ad0dbffc6c93bcf487b9d5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Email__MesafeOzetRaporuPDF), @"mvc.1.0.view", @"/Views/Email/_MesafeOzetRaporuPDF.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Email/_MesafeOzetRaporuPDF.cshtml", typeof(AspNetCore.Views_Email__MesafeOzetRaporuPDF))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e61708ae4f456522b7ad0dbffc6c93bcf487b9d5", @"/Views/Email/_MesafeOzetRaporuPDF.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c58bca9feb3622cf226aa7f72e5ee2d1d4dc7c3a", @"/Views/_ViewImports.cshtml")]
    public class Views_Email__MesafeOzetRaporuPDF : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Dictionary<VeraDAL.DB.MORKeyClass, List<VeraDAL.DB.MesafeOzetRaporuObjectRepo>>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/assets/plugins/jquery/printThis.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(88, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(117, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(194, 68, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e61708ae4f456522b7ad0dbffc6c93bcf487b9d53992", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(262, 866, true);
            WriteLiteral(@"



<!--replacepoint-->
<div id=""exportMORDiv"">
    <style>
        body {
            font-family: Verdana !important;
            color: black;
        }

        h5 {
            color: black;
        }
    </style>

    <div class=""col-md-12"">
        <!--replacepoint1-->
        <button id=""pdfBtn"" type=""button"" onclick=""$('#exportMORDiv').printThis();""
                style=""float:right; width:200px; border:none;
    color:black; padding:20px; text-align:center;
    text-decoration:none; display:inline-block;
    font-size:16px;margin:4px 2px;cursor:pointer;
    border-radius:8px; font-weight:bold"">
            PDF İNDİR
        </button>
        <!--replacepoint2-->
        <h1 style=""text-align:center;"">MESAFE ÖZETİ RAPORU</h1>
        <div class=""row"">
            <div class=""col-md-12"">
                <h5>Tarih :");
            EndContext();
            BeginContext(1129, 13, false);
#line 39 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
                      Write(ViewBag.Tarih);

#line default
#line hidden
            EndContext();
            BeginContext(1142, 35, true);
            WriteLiteral("</h5>\r\n                <h5>Süre :  ");
            EndContext();
            BeginContext(1178, 12, false);
#line 40 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
                       Write(ViewBag.Sure);

#line default
#line hidden
            EndContext();
            BeginContext(1190, 41, true);
            WriteLiteral(" </h5>\r\n                <p>Kayıt Sayısı: ");
            EndContext();
            BeginContext(1232, 11, false);
#line 41 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
                            Write(Model.Count);

#line default
#line hidden
            EndContext();
            BeginContext(1243, 58, true);
            WriteLiteral("</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n\r\n");
            EndContext();
#line 47 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
      
        foreach (var item in Model)
        {

#line default
#line hidden
            BeginContext(1357, 682, true);
            WriteLiteral(@"            <div style=""background-color:#a9a9a961; border-top-left-radius:7px; border-bottom-right-radius:7px; margin-top:15px;"">
                <table class=""table-bordered table"" border=""1"" style=""font-size: 12px; border-collapse: collapse; font-family: Verdana; width: 100%; border:1px solid black; text-align: center;"">
                    <tbody>
                        <tr>
                            <td style=""font-size:20px;"" colspan=""2"">Grup</td>
                            <td style=""font-size:20px;"" colspan=""3"">Mobil Tanımı</td>
                        </tr>
                        <tr>
                            <td style=""font-size:17px;"" colspan=""3"">");
            EndContext();
            BeginContext(2040, 13, false);
#line 58 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
                                                               Write(item.Key.Grup);

#line default
#line hidden
            EndContext();
            BeginContext(2053, 75, true);
            WriteLiteral("</td>\r\n                            <td style=\"font-size:17px;\" colspan=\"4\">");
            EndContext();
            BeginContext(2129, 20, false);
#line 59 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
                                                               Write(item.Key.MobilTanimi);

#line default
#line hidden
            EndContext();
            BeginContext(2149, 420, true);
            WriteLiteral(@"</td>
                        </tr>
                        <tr style=""font-size:16px;"">
                            <td>Tarih</td>
                            <td>Mesafe</td>
                            <td>Mesai Dışı (km)</td>
                            <td>Bölge İçi (km)</td>
                            <td>Gün</td>
                        </tr>
                    </tbody>
                    <tbody>
");
            EndContext();
#line 70 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
                          
                            foreach (var item2 in item.Value)
                            {

#line default
#line hidden
            BeginContext(2691, 124, true);
            WriteLiteral("                                <tr style=\"font-size:14px;\">\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(2816, 85, false);
#line 74 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
                                                         Write(Html.Raw(Convert.ToDateTime(item2.Tarih).ToString("dd.MM.yyyy").Replace(" ", "<br>")));

#line default
#line hidden
            EndContext();
            BeginContext(2901, 69, true);
            WriteLiteral("</td>\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(2971, 14, false);
#line 75 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
                                                         Write(item2.MesafeKm);

#line default
#line hidden
            EndContext();
            BeginContext(2985, 69, true);
            WriteLiteral("</td>\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(3055, 17, false);
#line 76 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
                                                         Write(item2.MesaiDisiKm);

#line default
#line hidden
            EndContext();
            BeginContext(3072, 69, true);
            WriteLiteral("</td>\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(3142, 23, false);
#line 77 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
                                                         Write(item2.BolgeDisiMesafeKm);

#line default
#line hidden
            EndContext();
            BeginContext(3165, 69, true);
            WriteLiteral("</td>\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(3235, 9, false);
#line 78 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
                                                         Write(item2.Gun);

#line default
#line hidden
            EndContext();
            BeginContext(3244, 48, true);
            WriteLiteral("</td>\r\n\r\n                                </tr>\r\n");
            EndContext();
#line 81 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
                            }
                        

#line default
#line hidden
            BeginContext(3350, 76, true);
            WriteLiteral("                    </tbody>\r\n                </table>\r\n            </div>\r\n");
            EndContext();
#line 86 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_MesafeOzetRaporuPDF.cshtml"
        }
    

#line default
#line hidden
            BeginContext(3444, 189, true);
            WriteLiteral("</div>\r\n<script>\r\n    $(\'#pdfBtn\').click(function () {\r\n\r\n        $(\'#pdfBtn\').hide();\r\n\r\n        setTimeout(() => {\r\n            $(\'#pdfBtn\').show();\r\n        }, 1000);\r\n    });\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Dictionary<VeraDAL.DB.MORKeyClass, List<VeraDAL.DB.MesafeOzetRaporuObjectRepo>>> Html { get; private set; }
    }
}
#pragma warning restore 1591