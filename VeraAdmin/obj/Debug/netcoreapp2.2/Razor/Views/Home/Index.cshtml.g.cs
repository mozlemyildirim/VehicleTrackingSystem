#pragma checksum "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f2a03e6ad55bdc5a5cf70c02a883c9a053f6c5c7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f2a03e6ad55bdc5a5cf70c02a883c9a053f6c5c7", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e98273b5ba9e1a8f6da536bfe2b73f48a9bb8a6", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Home\Index.cshtml"
  
	ViewData["Title"] = "Dashboard";

#line default
#line hidden
            BeginContext(42, 5483, true);
            WriteLiteral(@"
<div class=""text-center"">
	<h1 class=""display-4 ""><b>VERA MOBIL</b></h1> 
</div>

<hr />
<div id=""chartContainer"" style=""height: 450px; width: 100%; margin-top:75px;""></div>
<hr />
<br />
<br />
<div id=""chart2Container"" style=""height: 300px; width: 100%;""></div>
<script>
    window.onload = function () {

        var chart = new CanvasJS.Chart(""chartContainer"", {
            animationEnabled: true,
            zoomEnabled: true,
            theme: ""dark2"",
            title: {
                text: ""Vera Mobil Grafik""
            },
            axisX: {
                title: ""Year"",
                valueFormatString: ""####"",
                interval: 2
            },
            axisY: {
                logarithmic: true, //change it to false
                title: ""Internet Users (Log)"",
                titleFontColor: ""#6D78AD"",
                lineColor: ""#6D78AD"",
                gridThickness: 0,
                lineThickness: 1,
                includeZero: false,
  ");
            WriteLiteral(@"              labelFormatter: addSymbols
            },
            axisY2: {
                title: ""Internet Users"",
                titleFontColor: ""#51CDA0"",
                logarithmic: false, //change it to true
                lineColor: ""#51CDA0"",
                gridThickness: 0,
                lineThickness: 1,
                labelFormatter: addSymbols
            },
            legend: {
                verticalAlign: ""top"",
                fontSize: 16,
                dockInsidePlotArea: true
            },
            data: [{
                type: ""line"",
                xValueFormatString: ""####"",
                showInLegend: true,
                name: ""Log Scale"",
                dataPoints: [
                    { x: 1994, y: 25437639 },
                    { x: 1995, y: 44866595 },
                    { x: 1996, y: 77583866 },
                    { x: 1997, y: 120992212 },
                    { x: 1998, y: 188507628 },
                    { x: 1999, y: 2815376");
            WriteLiteral(@"52 },
                    { x: 2000, y: 414794957 },
                    { x: 2001, y: 502292245 },
                    { x: 2002, y: 665065014 },
                    { x: 2003, y: 781435983 },
                    { x: 2004, y: 913327771 },
                    { x: 2005, y: 1030101289 },
                    { x: 2006, y: 1162916818 },
                    { x: 2007, y: 1373226988 },
                    { x: 2008, y: 1575067520 },
                    { x: 2009, y: 1766403814 },
                    { x: 2010, y: 2023202974 },
                    { x: 2011, y: 2231957359 },
                    { x: 2012, y: 2494736248 },
                    { x: 2013, y: 2728428107 },
                    { x: 2014, y: 2956385569 },
                    { x: 2015, y: 3185996155 },
                    { x: 2016, y: 3424971237 }
                ]
            },
            {
                type: ""line"",
                xValueFormatString: ""####"",
                axisYType: ""secondary"",
                showInL");
            WriteLiteral(@"egend: true,
                name: ""Linear Scale"",
                dataPoints: [
                    { x: 1994, y: 25437639 },
                    { x: 1995, y: 44866595 },
                    { x: 1996, y: 77583866 },
                    { x: 1997, y: 120992212 },
                    { x: 1998, y: 188507628 },
                    { x: 1999, y: 281537652 },
                    { x: 2000, y: 414794957 },
                    { x: 2001, y: 502292245 },
                    { x: 2002, y: 665065014 },
                    { x: 2003, y: 781435983 },
                    { x: 2004, y: 913327771 },
                    { x: 2005, y: 1030101289 },
                    { x: 2006, y: 1162916818 },
                    { x: 2007, y: 1373226988 },
                    { x: 2008, y: 1575067520 },
                    { x: 2009, y: 1766403814 },
                    { x: 2010, y: 2023202974 },
                    { x: 2011, y: 2231957359 },
                    { x: 2012, y: 2494736248 },
                    { x");
            WriteLiteral(@": 2013, y: 2728428107 },
                    { x: 2014, y: 2956385569 },
                    { x: 2015, y: 3185996155 },
                    { x: 2016, y: 3424971237 }
                ]
            }]
        });
        chart.render();

        function addSymbols(e) {
            var suffixes = ["""", ""K"", ""M"", ""B""];

            var order = Math.max(Math.floor(Math.log(e.value) / Math.log(1000)), 0);
            if (order > suffixes.length - 1)
                order = suffixes.length - 1;

            var suffix = suffixes[order];
            return CanvasJS.formatNumber(e.value / Math.pow(1000, order)) + suffix;
        }



        var chart2 = new CanvasJS.Chart(""chart2Container"", {
	animationEnabled: true,
	theme: ""light2"", // ""light1"", ""light2"", ""dark1"", ""dark2""
	title:{
		text: """"
	},
	axisY: {
		title: ""Reserves(MMbbl)""
	},
	data: [{        
		type: ""column"",  
		showInLegend: true, 
		legendMarkerColor: ""grey"",
		legendText: ""MMbbl = one million barrels"",
		dataPoi");
            WriteLiteral(@"nts: [      
			{ y: 300878, label: ""Venezuela"" },
			{ y: 266455,  label: ""Saudi"" },
			{ y: 169709,  label: ""Canada"" },
			{ y: 158400,  label: ""Iran"" },
			{ y: 142503,  label: ""Iraq"" },
			{ y: 101500, label: ""Kuwait"" },
			{ y: 97800,  label: ""UAE"" },
			{ y: 80000,  label: ""Russia"" }
		]
	}]
});
chart2.render();  

    }
</script>

 
 
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
