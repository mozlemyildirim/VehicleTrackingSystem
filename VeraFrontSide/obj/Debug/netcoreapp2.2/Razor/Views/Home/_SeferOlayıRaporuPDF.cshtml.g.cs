#pragma checksum "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOlayıRaporuPDF.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "60c12c5b65e5d05eb2f57fc3fe9c895745b4cc51"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home__SeferOlayıRaporuPDF), @"mvc.1.0.view", @"/Views/Home/_SeferOlayıRaporuPDF.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/_SeferOlayıRaporuPDF.cshtml", typeof(AspNetCore.Views_Home__SeferOlayıRaporuPDF))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"60c12c5b65e5d05eb2f57fc3fe9c895745b4cc51", @"/Views/Home/_SeferOlayıRaporuPDF.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c58bca9feb3622cf226aa7f72e5ee2d1d4dc7c3a", @"/Views/_ViewImports.cshtml")]
    public class Views_Home__SeferOlayıRaporuPDF : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Dictionary<VeraDAL.DB.SOLRKeyClass, List<VeraDAL.DB.SeferOlayıRaporuObjectRepo>>>
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
#line 2 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOlayıRaporuPDF.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(189, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(191, 68, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "60c12c5b65e5d05eb2f57fc3fe9c895745b4cc513883", async() => {
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
            BeginContext(259, 880, true);
            WriteLiteral(@"

<script>
</script>
<!--replacepoint-->
<div id=""exportSOLRDiv"">
    <style>
       body {
            font-family: Verdana !important;
            color:black;
        }
        h5{
            color:black;
        }
    </style>


    <div class=""col-md-12"">
        <!--replacepoint1-->
        <button id=""pdfBtn"" type=""button"" onclick=""$('#exportSOLRDiv').printThis();""
                style=""float:right; width:200px; border:none;
    color:black; padding:20px; text-align:center;
    text-decoration:none; display:inline-block;
    font-size:16px;margin:4px 2px;cursor:pointer;
    border-radius:8px; font-weight:bold"">
            PDF İNDİR
        </button>
        <!--replacepoint2-->
        <h1 style=""text-align:center;"">SEFER OLAYI RAPORU</h1>
        <div class=""row"">
            <div class=""col-md-12"">
                <h5>Tarih :");
            EndContext();
            BeginContext(1140, 13, false);
#line 37 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOlayıRaporuPDF.cshtml"
                      Write(ViewBag.Tarih);

#line default
#line hidden
            EndContext();
            BeginContext(1153, 35, true);
            WriteLiteral("</h5>\r\n                <h5>Süre :  ");
            EndContext();
            BeginContext(1189, 12, false);
#line 38 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOlayıRaporuPDF.cshtml"
                       Write(ViewBag.Sure);

#line default
#line hidden
            EndContext();
            BeginContext(1201, 60, true);
            WriteLiteral(" </h5>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n\r\n");
            EndContext();
#line 44 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOlayıRaporuPDF.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
            BeginContext(1302, 672, true);
            WriteLiteral(@"        <div style=""background-color:#a9a9a961; border-top-left-radius:7px; border-bottom-right-radius:7px; margin-top:15px;"">
            <table class=""table-bordered table"" border=""1"" style=""font-size: 12px; border-collapse: collapse; font-family: Verdana; width: 100%; border:1px solid black; text-align: center; background-color:#e8e7e4;"">
                <tbody>
                    <tr>
                        <td style=""font-size:20px;"" colspan=""2"">Grup</td>
                        <td style=""font-size:20px;"" colspan=""4"">Mobil Tanımı</td>
                    </tr>
                    <tr>
                        <td style=""font-size:18px;"" colspan=""2"">");
            EndContext();
            BeginContext(1975, 13, false);
#line 54 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOlayıRaporuPDF.cshtml"
                                                           Write(item.Key.Grup);

#line default
#line hidden
            EndContext();
            BeginContext(1988, 71, true);
            WriteLiteral("</td>\r\n                        <td style=\"font-size:18px;\" colspan=\"4\">");
            EndContext();
            BeginContext(2060, 20, false);
#line 55 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOlayıRaporuPDF.cshtml"
                                                           Write(item.Key.MobilTanimi);

#line default
#line hidden
            EndContext();
            BeginContext(2080, 355, true);
            WriteLiteral(@"</td>
                    </tr>
                    <tr style=""font-size:17px; font-weight:bolder;"">
                        <td>Olay Zamanı</td>
                        <td>Açıklama</td>
                        <td>Pozisyon</td>
                        <td>Sürücü</td>
                    </tr>
                </tbody>
                <tbody>
");
            EndContext();
#line 65 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOlayıRaporuPDF.cshtml"
                      
                        foreach (var item2 in item.Value)
                        {

#line default
#line hidden
            BeginContext(2545, 116, true);
            WriteLiteral("                            <tr style=\"font-size:13px;\">\r\n                                <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(2662, 16, false);
#line 69 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOlayıRaporuPDF.cshtml"
                                                     Write(item2.OlayZamani);

#line default
#line hidden
            EndContext();
            BeginContext(2678, 66, true);
            WriteLiteral(" </td>\r\n                                <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(2745, 14, false);
#line 70 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOlayıRaporuPDF.cshtml"
                                                     Write(item2.Aciklama);

#line default
#line hidden
            EndContext();
            BeginContext(2759, 66, true);
            WriteLiteral(" </td>\r\n                                <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(2826, 14, false);
#line 71 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOlayıRaporuPDF.cshtml"
                                                     Write(item2.Pozisyon);

#line default
#line hidden
            EndContext();
            BeginContext(2840, 66, true);
            WriteLiteral(" </td>\r\n                                <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(2907, 12, false);
#line 72 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOlayıRaporuPDF.cshtml"
                                                     Write(item2.Surucu);

#line default
#line hidden
            EndContext();
            BeginContext(2919, 43, true);
            WriteLiteral(" </td>\r\n                            </tr>\r\n");
            EndContext();
#line 74 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOlayıRaporuPDF.cshtml"

                        }
                    

#line default
#line hidden
            BeginContext(3014, 64, true);
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n");
            EndContext();
#line 80 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOlayıRaporuPDF.cshtml"

    }  

#line default
#line hidden
            BeginContext(3089, 193, true);
            WriteLiteral("    </div>\r\n<script>\r\n    $(\'#pdfBtn\').click(function () {\r\n\r\n        $(\'#pdfBtn\').hide();\r\n\r\n        setTimeout(() => {\r\n            $(\'#pdfBtn\').show();\r\n        }, 1000);\r\n    });\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Dictionary<VeraDAL.DB.SOLRKeyClass, List<VeraDAL.DB.SeferOlayıRaporuObjectRepo>>> Html { get; private set; }
    }
}
#pragma warning restore 1591
