#pragma checksum "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_SonKonumRaporuPDF.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1300ee444ba5331f6a595e7282923ef39203068a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Email__SonKonumRaporuPDF), @"mvc.1.0.view", @"/Views/Email/_SonKonumRaporuPDF.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Email/_SonKonumRaporuPDF.cshtml", typeof(AspNetCore.Views_Email__SonKonumRaporuPDF))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1300ee444ba5331f6a595e7282923ef39203068a", @"/Views/Email/_SonKonumRaporuPDF.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c58bca9feb3622cf226aa7f72e5ee2d1d4dc7c3a", @"/Views/_ViewImports.cshtml")]
    public class Views_Email__SonKonumRaporuPDF : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Dictionary<string, List<VeraDAL.DB.SonKonumRaporuObjectRepo>>>
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
            BeginContext(70, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_SonKonumRaporuPDF.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(99, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(176, 68, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1300ee444ba5331f6a595e7282923ef39203068a3957", async() => {
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
            BeginContext(244, 863, true);
            WriteLiteral(@"



<!--replacepoint-->
<div id=""exportSKRDiv"">
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
        <button id=""pdfBtn"" type=""button"" onclick=""$('#exportSKRDiv').printThis();""
                style=""float:right; width:200px; border:none;
    color:black; padding:20px; text-align:center;
    text-decoration:none; display:inline-block;
    font-size:16px;margin:4px 2px;cursor:pointer;
    border-radius:8px; font-weight:bold"">
            PDF İNDİR
        </button>
        <!--replacepoint2-->
        <h1 style=""text-align:center;"">SON KONUM RAPORU</h1>
        <div class=""row"">
            <div class=""col-md-12"">
                <h5>Tarih :");
            EndContext();
            BeginContext(1108, 13, false);
#line 39 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_SonKonumRaporuPDF.cshtml"
                      Write(ViewBag.Tarih);

#line default
#line hidden
            EndContext();
            BeginContext(1121, 39, true);
            WriteLiteral("</h5>\r\n                <p>Grup Sayısı: ");
            EndContext();
            BeginContext(1161, 11, false);
#line 40 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_SonKonumRaporuPDF.cshtml"
                           Write(Model.Count);

#line default
#line hidden
            EndContext();
            BeginContext(1172, 54, true);
            WriteLiteral("</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
            EndContext();
#line 44 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_SonKonumRaporuPDF.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
            BeginContext(1267, 563, true);
            WriteLiteral(@"        <div style=""background-color:#a9a9a961; border-top-left-radius:7px; border-bottom-right-radius:7px; margin-top:15px;"">
            <table class=""table-bordered table"" border=""1"" style=""font-size: 12px; border-collapse: collapse; font-family: Verdana; width: 100%; border:1px solid black; text-align: center;"">
                <tbody>
                    <tr>
                        <td style=""font-size:20px;"" colspan=""5"">Grup</td>
                    </tr>
                    <tr>
                        <td style=""font-size:17px;"" colspan=""5"">");
            EndContext();
            BeginContext(1831, 8, false);
#line 53 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_SonKonumRaporuPDF.cshtml"
                                                           Write(item.Key);

#line default
#line hidden
            EndContext();
            BeginContext(1839, 381, true);
            WriteLiteral(@"</td>
                    </tr>
                    <tr style=""font-size:16px;"">
                        <td>Grup Adı</td>
                        <td>Tarih</td>
                        <td>Mobil Tanımı</td>
                        <td>Toplam(Km)</td>
                        <td>Son Konum</td>
                    </tr>
                </tbody>
                <tbody>
");
            EndContext();
#line 64 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_SonKonumRaporuPDF.cshtml"
                      
                        foreach (var item2 in item.Value)
                        {

#line default
#line hidden
            BeginContext(2330, 116, true);
            WriteLiteral("                            <tr style=\"font-size:13px;\">\r\n                                <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(2447, 10, false);
#line 68 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_SonKonumRaporuPDF.cshtml"
                                                     Write(item2.Grup);

#line default
#line hidden
            EndContext();
            BeginContext(2457, 65, true);
            WriteLiteral("</td>\r\n                                <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(2523, 91, false);
#line 69 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_SonKonumRaporuPDF.cshtml"
                                                     Write(Html.Raw(Convert.ToDateTime(item2.Tarih).ToString("dd.MM.yyyy HH:mm").Replace(" ", "<br>")));

#line default
#line hidden
            EndContext();
            BeginContext(2614, 65, true);
            WriteLiteral("</td>\r\n                                <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(2680, 17, false);
#line 70 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_SonKonumRaporuPDF.cshtml"
                                                     Write(item2.MobilTanimi);

#line default
#line hidden
            EndContext();
            BeginContext(2697, 65, true);
            WriteLiteral("</td>\r\n                                <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(2763, 14, false);
#line 71 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_SonKonumRaporuPDF.cshtml"
                                                     Write(item2.ToplamKm);

#line default
#line hidden
            EndContext();
            BeginContext(2777, 65, true);
            WriteLiteral("</td>\r\n                                <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(2843, 11, false);
#line 72 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_SonKonumRaporuPDF.cshtml"
                                                     Write(item2.Konum);

#line default
#line hidden
            EndContext();
            BeginContext(2854, 42, true);
            WriteLiteral("</td>\r\n                            </tr>\r\n");
            EndContext();
#line 74 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_SonKonumRaporuPDF.cshtml"

                        }
                    

#line default
#line hidden
            BeginContext(2948, 64, true);
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n");
            EndContext();
#line 80 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Email\_SonKonumRaporuPDF.cshtml"
    }

#line default
#line hidden
            BeginContext(3019, 189, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Dictionary<string, List<VeraDAL.DB.SonKonumRaporuObjectRepo>>> Html { get; private set; }
    }
}
#pragma warning restore 1591
