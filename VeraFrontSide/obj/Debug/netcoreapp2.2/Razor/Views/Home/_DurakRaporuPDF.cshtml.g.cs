#pragma checksum "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_DurakRaporuPDF.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9e980b9dd14c6c0a6826615fbd5e32aacd59fb92"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home__DurakRaporuPDF), @"mvc.1.0.view", @"/Views/Home/_DurakRaporuPDF.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/_DurakRaporuPDF.cshtml", typeof(AspNetCore.Views_Home__DurakRaporuPDF))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9e980b9dd14c6c0a6826615fbd5e32aacd59fb92", @"/Views/Home/_DurakRaporuPDF.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c58bca9feb3622cf226aa7f72e5ee2d1d4dc7c3a", @"/Views/_ViewImports.cshtml")]
    public class Views_Home__DurakRaporuPDF : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Dictionary<VeraDAL.DB.DRKeyClass, List<VeraDAL.DB.DurakRaporuObject>>>
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
#line 2 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_DurakRaporuPDF.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(178, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(180, 68, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9e980b9dd14c6c0a6826615fbd5e32aacd59fb923832", async() => {
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
            BeginContext(248, 900, true);
            WriteLiteral(@"


<!--replacepoint-->
<div id=""exportDRDiv"">
    <style>
        body {
            font-family: Verdana !important;
            color: black;
        }

        h5 {
            color: black;
        }
    </style>

    <div style=""width: 100%; font-family: Verdana;"" class=""col-md-12"">
        <!--replacepoint1-->
        <button id=""pdfBtn"" type=""button"" onclick=""$('#exportDRDiv').printThis();""
                style=""float:right; width:200px; border:none;
    color:black; padding:20px; text-align:center;
    text-decoration:none; display:inline-block;
    font-size:16px;margin:4px 2px;cursor:pointer;
    border-radius:8px; font-weight:bold"">
            PDF İNDİR
        </button>
        <!--replacepoint2-->
        <h2 style=""text-align:center;"">Durak Raporu</h2>

        <div class=""row"">
            <div class=""col-md-12"">
                <h5>Tarih :");
            EndContext();
            BeginContext(1149, 13, false);
#line 37 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_DurakRaporuPDF.cshtml"
                      Write(ViewBag.Tarih);

#line default
#line hidden
            EndContext();
            BeginContext(1162, 35, true);
            WriteLiteral("</h5>\r\n                <h5>Süre :  ");
            EndContext();
            BeginContext(1198, 12, false);
#line 38 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_DurakRaporuPDF.cshtml"
                       Write(ViewBag.Sure);

#line default
#line hidden
            EndContext();
            BeginContext(1210, 41, true);
            WriteLiteral(" </h5>\r\n                <p>Kayıt Sayısı: ");
            EndContext();
            BeginContext(1252, 11, false);
#line 39 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_DurakRaporuPDF.cshtml"
                            Write(Model.Count);

#line default
#line hidden
            EndContext();
            BeginContext(1263, 44, true);
            WriteLiteral("</p>\r\n            </div>\r\n        </div>\r\n\r\n");
            EndContext();
#line 43 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_DurakRaporuPDF.cshtml"
         foreach (var item1 in Model)
        {

#line default
#line hidden
            BeginContext(1357, 682, true);
            WriteLiteral(@"            <div style=""background-color:#a9a9a961; border-top-left-radius:7px; border-bottom-right-radius:7px; margin-top:15px;"">
                <table class=""table-bordered table"" border=""1"" style=""font-size: 12px; border-collapse: collapse; font-family: Verdana; width: 100%; border:1px solid black; text-align: center;"">
                    <tbody>
                        <tr>
                            <td style=""font-size:20px;"" colspan=""1"">Grup</td>
                            <td style=""font-size:20px;"" colspan=""2"">Mobil Tanımı</td>
                        </tr>
                        <tr>
                            <td style=""font-size:18px;"" colspan=""1"">");
            EndContext();
            BeginContext(2040, 14, false);
#line 53 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_DurakRaporuPDF.cshtml"
                                                               Write(item1.Key.Grup);

#line default
#line hidden
            EndContext();
            BeginContext(2054, 75, true);
            WriteLiteral("</td>\r\n                            <td style=\"font-size:18px;\" colspan=\"2\">");
            EndContext();
            BeginContext(2130, 21, false);
#line 54 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_DurakRaporuPDF.cshtml"
                                                               Write(item1.Key.MobilTanimi);

#line default
#line hidden
            EndContext();
            BeginContext(2151, 321, true);
            WriteLiteral(@"</td>
                        </tr>
                        <tr style=""font-size:16px;"">
                            <td>Durak</td>
                            <td>Sefer Sayısı</td>
                            <td>Şoför</td>
                        </tr>
                    </tbody>
                    <tbody>
");
            EndContext();
#line 63 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_DurakRaporuPDF.cshtml"
                          
                            foreach (var item in item1.Value)
                            {

#line default
#line hidden
            BeginContext(2594, 124, true);
            WriteLiteral("                                <tr style=\"font-size:14px;\">\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(2719, 10, false);
#line 67 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_DurakRaporuPDF.cshtml"
                                                         Write(item.Durak);

#line default
#line hidden
            EndContext();
            BeginContext(2729, 69, true);
            WriteLiteral("</td>\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(2799, 16, false);
#line 68 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_DurakRaporuPDF.cshtml"
                                                         Write(item.SeferSayisi);

#line default
#line hidden
            EndContext();
            BeginContext(2815, 69, true);
            WriteLiteral("</td>\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(2885, 11, false);
#line 69 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_DurakRaporuPDF.cshtml"
                                                         Write(item.Driver);

#line default
#line hidden
            EndContext();
            BeginContext(2896, 46, true);
            WriteLiteral("</td>\r\n                                </tr>\r\n");
            EndContext();
#line 71 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_DurakRaporuPDF.cshtml"
                            }
                        

#line default
#line hidden
            BeginContext(3000, 76, true);
            WriteLiteral("                    </tbody>\r\n                </table>\r\n            </div>\r\n");
            EndContext();
#line 76 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_DurakRaporuPDF.cshtml"
        }

#line default
#line hidden
            BeginContext(3087, 201, true);
            WriteLiteral("    </div>\r\n</div>\r\n<script>\r\n    $(\'#pdfBtn\').click(function () {\r\n\r\n        $(\'#pdfBtn\').hide();\r\n\r\n        setTimeout(() => {\r\n            $(\'#pdfBtn\').show();\r\n        }, 1000);\r\n    });\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Dictionary<VeraDAL.DB.DRKeyClass, List<VeraDAL.DB.DurakRaporuObject>>> Html { get; private set; }
    }
}
#pragma warning restore 1591