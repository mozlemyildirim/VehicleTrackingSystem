#pragma checksum "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6e9841a32be85cdab238d963cc1b26ff63eed536"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home__SeferOzetiRaporuPDF), @"mvc.1.0.view", @"/Views/Home/_SeferOzetiRaporuPDF.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/_SeferOzetiRaporuPDF.cshtml", typeof(AspNetCore.Views_Home__SeferOzetiRaporuPDF))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e9841a32be85cdab238d963cc1b26ff63eed536", @"/Views/Home/_SeferOzetiRaporuPDF.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c58bca9feb3622cf226aa7f72e5ee2d1d4dc7c3a", @"/Views/_ViewImports.cshtml")]
    public class Views_Home__SeferOzetiRaporuPDF : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Dictionary<VeraDAL.DB.SORKeyClass, List<VeraDAL.DB.SeferOzetiRaporuObjectRepo>>>
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
#line 3 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(192, 68, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6e9841a32be85cdab238d963cc1b26ff63eed5363881", async() => {
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
            BeginContext(260, 987, true);
            WriteLiteral(@"

<script>
</script>
<!--replacepoint-->

<div id=""exportSORDiv"" style=""width: 100%; height: 100%;"" frameborder=""0"">

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
        <button id=""pdfBtn"" type=""button"" onclick=""$('#exportSORDiv').printThis();""
                style=""float:right; width:200px; border:none;
                color:black; padding:20px; text-align:center;
                text-decoration:none; display:inline-block;
                font-size:16px;margin:4px 2px;cursor:pointer;
                border-radius:8px; font-weight:bold"">
            PDF İNDİR
        </button>
        <!--replacepoint2-->
        <h1 style=""text-align:center;"">SEFER ÖZETİ RAPORU</h1>
        <div class=""row"">
            <div class=""col-md-12"">
                <h5>Tarih :");
            EndContext();
            BeginContext(1248, 13, false);
#line 41 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                      Write(ViewBag.Tarih);

#line default
#line hidden
            EndContext();
            BeginContext(1261, 35, true);
            WriteLiteral("</h5>\r\n                <h5>Süre :  ");
            EndContext();
            BeginContext(1297, 12, false);
#line 42 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                       Write(ViewBag.Sure);

#line default
#line hidden
            EndContext();
            BeginContext(1309, 40, true);
            WriteLiteral(" </h5>\r\n                <p>Araç Sayısı: ");
            EndContext();
            BeginContext(1350, 11, false);
#line 43 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                           Write(Model.Count);

#line default
#line hidden
            EndContext();
            BeginContext(1361, 60, true);
            WriteLiteral("</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n\r\n\r\n");
            EndContext();
#line 50 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
      
        foreach (var item in Model)
        {

#line default
#line hidden
            BeginContext(1477, 708, true);
            WriteLiteral(@"            <div style=""background-color:#a9a9a961; border-top-left-radius:7px; border-bottom-right-radius:7px; margin-top:15px;"">
                <table class=""table-bordered table"" border=""1"" style=""font-size: 12px; border-collapse: collapse; font-family: Verdana; width: 100%; border:1px solid black; text-align: center; background-color:#e8e7e4;"">
                    <tbody>
                        <tr>
                            <td style=""font-size:20px;"" colspan=""3"">Grup</td>
                            <td style=""font-size:20px;"" colspan=""5"">Mobil Tanımı</td>
                        </tr>
                        <tr>
                            <td style=""font-size:17px;"" colspan=""3"">");
            EndContext();
            BeginContext(2186, 13, false);
#line 61 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                                                               Write(item.Key.Grup);

#line default
#line hidden
            EndContext();
            BeginContext(2199, 75, true);
            WriteLiteral("</td>\r\n                            <td style=\"font-size:17px;\" colspan=\"5\">");
            EndContext();
            BeginContext(2275, 20, false);
#line 62 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                                                               Write(item.Key.MobilTanimi);

#line default
#line hidden
            EndContext();
            BeginContext(2295, 557, true);
            WriteLiteral(@"</td>
                        </tr>
                        <tr style=""font-size:15px;"">
                            <td>Kalkış</td>
                            <td>Varış</td>
                            <td>Süre</td>
                            <td>Mesafe</td>
                            <td>Toplam KM</td>
                            <td>Kalkış Noktası</td>
                            <td>Varış Noktası</td>
                            <td>Sürücü</td>
                        </tr>
                    </tbody>
                    <tbody>
");
            EndContext();
#line 76 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                          
                            foreach (var item2 in item.Value.Where(x => x.Mesafe.Trim() != "0").ToList())
                            {

#line default
#line hidden
            BeginContext(3018, 124, true);
            WriteLiteral("                                <tr style=\"font-size:13px;\">\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(3143, 98, false);
#line 80 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                                                         Write(Html.Raw(Convert.ToDateTime(item2.KalkisZamani).ToString("dd.MM.yyyy HH:mm").Replace(" ", "<br>")));

#line default
#line hidden
            EndContext();
            BeginContext(3241, 69, true);
            WriteLiteral("</td>\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(3311, 97, false);
#line 81 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                                                         Write(Html.Raw(Convert.ToDateTime(item2.VarisZamani).ToString("dd.MM.yyyy HH:mm").Replace(" ", "<br>")));

#line default
#line hidden
            EndContext();
            BeginContext(3408, 69, true);
            WriteLiteral("</td>\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(3478, 10, false);
#line 82 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                                                         Write(item2.Sure);

#line default
#line hidden
            EndContext();
            BeginContext(3488, 69, true);
            WriteLiteral("</td>\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(3558, 12, false);
#line 83 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                                                         Write(item2.Mesafe);

#line default
#line hidden
            EndContext();
            BeginContext(3570, 72, true);
            WriteLiteral(" km</td>\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(3643, 14, false);
#line 84 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                                                         Write(item2.ToplamKm);

#line default
#line hidden
            EndContext();
            BeginContext(3657, 72, true);
            WriteLiteral(" km</td>\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(3730, 20, false);
#line 85 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                                                         Write(item2.KalkisPozisyon);

#line default
#line hidden
            EndContext();
            BeginContext(3750, 69, true);
            WriteLiteral("</td>\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(3820, 19, false);
#line 86 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                                                         Write(item2.VarisPozisyon);

#line default
#line hidden
            EndContext();
            BeginContext(3839, 69, true);
            WriteLiteral("</td>\r\n                                    <td style=\"padding: 5px;\">");
            EndContext();
            BeginContext(3909, 12, false);
#line 87 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                                                         Write(item2.Surucu);

#line default
#line hidden
            EndContext();
            BeginContext(3921, 46, true);
            WriteLiteral("</td>\r\n                                </tr>\r\n");
            EndContext();
#line 89 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"

                            }
                        

#line default
#line hidden
            BeginContext(4027, 717, true);
            WriteLiteral(@"                        <tr>
                            <td style=""padding: 5px;""></td>
                            <td style=""padding: 5px;""></td>
                            <td style=""padding: 5px;""></td>
                            <td style=""padding: 5px;""></td>
                            <td style=""padding: 5px;""></td>
                            <td style=""padding: 5px;""></td>
                            <td style=""padding: 5px;""></td>
                        </tr>
                        <tr style=""background-color:#b9b3b385"">
                            <td style=""text-align: center; padding: 5px;"" colspan=""2""><b>Toplam:</b></td>
                            <td style=""padding: 5px;""><b>");
            EndContext();
            BeginContext(4745, 18, false);
#line 103 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                                                    Write(item.Key.GecenSure);

#line default
#line hidden
            EndContext();
            BeginContext(4763, 68, true);
            WriteLiteral("</b></td>\r\n                            <td style=\"padding: 5px;\"><b>");
            EndContext();
            BeginContext(4832, 46, false);
#line 104 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                                                    Write(item.Value.Sum(x => Convert.ToInt32(x.Mesafe)));

#line default
#line hidden
            EndContext();
            BeginContext(4878, 143, true);
            WriteLiteral(" km</b></td>\r\n                            <td style=\"padding: 5px;\"> </td>\r\n                            <td style=\"padding: 5px;\"><b>Ort. Hız: ");
            EndContext();
            BeginContext(5022, 18, false);
#line 106 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                                                              Write(item.Key.AverageKm);

#line default
#line hidden
            EndContext();
            BeginContext(5040, 22, true);
            WriteLiteral(" km/s <br />Max. Hız: ");
            EndContext();
            BeginContext(5063, 14, false);
#line 106 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
                                                                                                       Write(item.Key.MaxKm);

#line default
#line hidden
            EndContext();
            BeginContext(5077, 246, true);
            WriteLiteral(" km/s </b></td>\r\n                            <td style=\"padding: 5px;\"></td>\r\n                            <td style=\"padding: 5px;\"></td>\r\n                        </tr>\r\n                    </tbody>\r\n                </table>\r\n            </div>\r\n");
            EndContext();
#line 113 "C:\Users\merve\Desktop\VERAWEB\VeraFrontSide\Views\Home\_SeferOzetiRaporuPDF.cshtml"
        }
    

#line default
#line hidden
            BeginContext(5341, 195, true);
            WriteLiteral("\r\n</div>\r\n\r\n<script>\r\n    $(\'#pdfBtn\').click(function () {\r\n\r\n        $(\'#pdfBtn\').hide();\r\n\r\n        setTimeout(() => {\r\n            $(\'#pdfBtn\').show();\r\n        }, 1000);\r\n    });\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Dictionary<VeraDAL.DB.SORKeyClass, List<VeraDAL.DB.SeferOzetiRaporuObjectRepo>>> Html { get; private set; }
    }
}
#pragma warning restore 1591