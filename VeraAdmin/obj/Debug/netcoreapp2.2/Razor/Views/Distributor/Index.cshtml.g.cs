#pragma checksum "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cae07adcac0b0367c4d866707934f38affed8fe8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Distributor_Index), @"mvc.1.0.view", @"/Views/Distributor/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Distributor/Index.cshtml", typeof(AspNetCore.Views_Distributor_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cae07adcac0b0367c4d866707934f38affed8fe8", @"/Views/Distributor/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e98273b5ba9e1a8f6da536bfe2b73f48a9bb8a6", @"/Views/_ViewImports.cshtml")]
    public class Views_Distributor_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DistributorViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\Index.cshtml"
  
	ViewData["Title"] = "Index";
	Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(111, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 7 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\Index.cshtml"
  
	var activeUser = ViewBag.ActiveUser as VeraDAL.Entities.User;


#line default
#line hidden
            BeginContext(186, 557, true);
            WriteLiteral(@"



<div class=""row  "" style=""padding: 15px !important; "">
	<div class=""portlet light bordered"" >
        <div class=""portlet-title"">
            <div class=""caption font-dark"">
                <div class=""col-md-12 col-xs-12"">
                    <i class=""icon-settings font-dark""></i>
                    <span class=""caption-subject bold uppercase""><b>Bayi Bilgileri</b></span>
                </div>
            </div>


            <div class=""tools"">    </div>
            <div class=""col-md-12 col-xs-12"">

                <br />
");
            EndContext();
#line 30 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\Index.cshtml"
                  
                    if (activeUser.UserTypeId == 3)
                    {

#line default
#line hidden
            BeginContext(839, 238, true);
            WriteLiteral("                        <button onclick=\"ShowDistributorModal()\" class=\"btn btn-lg btn-info rounded-2 table-group-action-submit\">\r\n                            <i class=\"fa fa-plus\"></i> Bayi Ekle      \r\n                        </button>\r\n");
            EndContext();
#line 36 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\Index.cshtml"
                    }   /*user restriction*/
                

#line default
#line hidden
            BeginContext(1142, 1219, true);
            WriteLiteral(@"                <h4 style="" margin-top: 15px; margin-bottom: 15px;"">Arama Kriterleri</h4>
            </div>
     

            <div id=""id"" value=""0"" style=""display:none;""></div>
            <div id=""userId"" value=""0"" style=""display:none;""></div>


            <div class=""col-md-12"">
                <div class=""row"" style=""margin-top: 15px; margin-bottom: 15px;"">

                    <div class=""col-md-1"" style=""margin-top:15px;"">
                        <label style=""margin-bottom: 0px !important; font-size: 15px;""><b>Bayi Adı</b>:</label>
                    </div>
                    <div class=""col-md-2"" style=""margin-top:15px;"">
                        <input id=""bayiAdi"" class=""table-group-action-input form-control input-inline input-small input-sm"" style=""width: 100% !important;"" />
                    </div>
                    <div class=""col-md-1"" style=""margin-top:15px;"">
                        <button onclick=""getList()"" class=""btn btn-primary rounded-3 table-group-action-submi");
            WriteLiteral("t\">\r\n                            <i class=\"fa fa-check\"></i> Listele\r\n                        </button>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n");
            EndContext();
            BeginContext(2385, 367, true);
            WriteLiteral(@"        <div class=""portlet-body"" id=""tableContainer"">

        </div>

 		<div class=""portlet-body"" id=""loadingContainer"" style=""display: none; margin-top: 15px; margin-bottom: 15px;"">
			<center>
				<img style=""width: 100px;"" src=""https://thumbs.gfycat.com/ObviousQuarrelsomeIntermediateegret-size_restricted.gif"" />
			</center>
		</div>
	</div>
</div> ");
            EndContext();
            BeginContext(2777, 972, true);
            WriteLiteral(@"

<div class=""modal fade"" id=""exampleModalCenter"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalCenterTitle"" aria-hidden=""true"">
	<div class=""modal-dialog modal-dialog-centered modal-lg"" role=""document"">
		<div class=""modal-content modal-lg"">
			<div class=""modal-header"">
				<h3 class=""modal-title text-center modal-lg"" id=""exampleModalLongTitle""><b>Bayi Bilgileri</b></h3>
				<button style=""margin-top: -18px !important;"" type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
					<span aria-hidden=""true"">&times;</span>
				</button>
			</div>
			<div class=""modal-body modal-lg"">
                <div class=""form-body"">
                    <div class=""col-md-4 col-sm-12"" style=""margin-top:15px;"">
                        <div class=""form-group"">
                            <label> İş Ortağı </label>
                            <select id=""companyPartnerId"" class=""form-control input-sm"">
                                ");
            EndContext();
            BeginContext(3749, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cae07adcac0b0367c4d866707934f38affed8fe88594", async() => {
                BeginContext(3767, 11, true);
                WriteLiteral("Seçiniz ...");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3787, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 93 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\Index.cshtml"
                                 foreach (var item in Model.CompanyPartners)
                                {

#line default
#line hidden
            BeginContext(3902, 36, true);
            WriteLiteral("                                    ");
            EndContext();
            BeginContext(3938, 44, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cae07adcac0b0367c4d866707934f38affed8fe810341", async() => {
                BeginContext(3964, 9, false);
#line 95 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\Index.cshtml"
                                                        Write(item.Name);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#line 95 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\Index.cshtml"
                                       WriteLiteral(item.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3982, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 96 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\Index.cshtml"
                                }

#line default
#line hidden
            BeginContext(4019, 1037, true);
            WriteLiteral(@"
                            </select>
                        </div>

                        <div class=""form-group"">
                            <label>* Bayi Adi</label>
                            <input id=""name"" type=""text"" class=""form-control input-sm"" />
                        </div>

                        <div class=""form-group"">
                            <label>Bayi Giriş Tarihi</label>
                            <div class=""input-group input-medium"" style=""width:100% !important;"">
                                <input id=""entranceDate"" type=""date"" value="""" style=""height:30px;"" class=""form-control"">
                            </div>
                        </div>

                        <div class=""form-group"">
                            <label>Bayi Çıkış Tarihi</label>
                            <div class=""input-group input-medium"" style=""width:100% !important;"">
                                <input id=""exitDate"" type=""date"" value="""" style=""height:30px;"" class=""for");
            WriteLiteral("m-control\">\r\n");
            EndContext();
            BeginContext(5337, 291, true);
            WriteLiteral(@"                            </div>
                        </div>


                        <div class=""form-group"">
                            <label> Aktiflik </label>
                            <select id=""activity"" class=""form-control input-sm"">
                                ");
            EndContext();
            BeginContext(5628, 32, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cae07adcac0b0367c4d866707934f38affed8fe814051", async() => {
                BeginContext(5646, 5, true);
                WriteLiteral("Aktif");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5660, 34, true);
            WriteLiteral("\r\n                                ");
            EndContext();
            BeginContext(5694, 32, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cae07adcac0b0367c4d866707934f38affed8fe815460", async() => {
                BeginContext(5712, 5, true);
                WriteLiteral("Pasif");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5726, 3063, true);
            WriteLiteral(@"
                            </select>
                        </div>

                        <div class=""form-group"">
                            <label>Telefon 1</label>
                            <input id=""phone1"" type=""text"" class=""form-control input-sm"" />
                        </div>
                    </div>
                    <div class=""col-md-4 col-sm-12"" style=""margin-top:15px;"">

                        <div class=""form-group"">
                            <label>Telefon 2</label>
                            <input id=""phone2"" type=""text"" class=""form-control input-sm"" />
                        </div>

                        <div class=""form-group"">
                            <label>Fax</label>
                            <input id=""fax"" type=""text"" class=""form-control input-sm"" />
                        </div>

                        <div class=""form-group"">
                            <label> Sehir</label>
                            <input id=""city"" type=""text"" ");
            WriteLiteral(@"class=""form-control input-sm"" />
                        </div>
                        <div class=""form-group"">
                            <label>Bayi Yetkilisi</label>
                            <input id=""personInCharge"" type=""text"" class=""form-control input-sm"" />
                        </div>

                        <div class=""form-group"">
                            <label>Bayi Kodu</label>
                            <input id=""code"" type=""text"" class=""form-control input-sm"" />
                        </div>

                        <div class=""form-group"">
                            <label>Adres</label>
                            <input id=""address"" type=""text"" class=""form-control input-sm"" />
                        </div>
                    </div>
                    <div class=""col-md-4 col-sm-12"" style=""margin-top:15px;"">

                        <div class=""form-group"">
                            <label> * Kullanıcı Kodu </label>
                            <input id");
            WriteLiteral(@"=""userCode"" type=""text"" class=""form-control input-sm"" />
                        </div>

                        <div class=""form-group"">
                            <label> * Kullanıcı Şifresi </label>
                            <input id=""userPassword"" type=""password"" class=""form-control input-sm"" />
                        </div>


                        <div class=""form-group"">
                            <label> * Kullanıcı Adı </label>
                            <input id=""userName"" type=""text"" class=""form-control input-sm"" />
                        </div>


                        <div class=""form-group"">
                            <label> * Kullanıcı Soyadı </label>
                            <input id=""userSurname"" type=""text"" class=""form-control input-sm"" />
                        </div>


                        <div class=""form-group"">
                            <label>  Kullanıcı E-Mail </label>
                            <input id=""userEmail"" type=""email""");
            EndContext();
            BeginWriteAttribute("placeholder", " placeholder=\"", 8789, "\"", 8821, 3);
            WriteAttributeValue("", 8803, "example", 8803, 7, true);
            WriteAttributeValue("", 8810, "@", 8810, 1, true);
            WriteAttributeValue("", 8812, "gmail.com", 8812, 9, true);
            EndWriteAttribute();
            BeginContext(8822, 678, true);
            WriteLiteral(@" class=""form-control input-sm"" />
                        </div>

                        <div class=""form-group"">
                            <label>  Kullanıcı Telefonu </label>
                            <input id=""userPhone"" type=""text"" class=""form-control input-sm"" />
                        </div>
                    </div>

                    <div class=""modal-footer"" style=""text-align: unset;"">
                        <button type=""button"" onclick=""SaveDistributor();"" style=""float:right; margin-left:35%; margin-top:23px;"" class=""btn btn-primary"">Kaydet</button>
                    </div>
                </div>
			</div>
		</div>
	</div>
</div> ");
            EndContext();
            BeginContext(9526, 1905, true);
            WriteLiteral(@"

<script>
	function ShowDistributorModal() {
		$(""#exampleModalCenter input[type='text']"").val('');
		$(""#exampleModalCenter select"").val('0');
		$(""#exampleModalCenter input[type='checkbox']"").prop(""checked"", false);
		$(""#exampleModalCenter input[type='date']"").val('');
		$('#exampleModalCenter').modal('show');
	}
	 $(document).ready(function () {
        getList(true);
	 });
    function SaveDistributor() { 
		debugger;
        var id = $(""#id"").val();
		var name = $(""#name"").val();
		var companyPartnerId = $(""#companyPartnerId"").val();
		var entranceDate = $(""#entranceDate"").val();
		var exitDate = $(""#exitDate"").val();
		var activity = $(""#activity"").val();
		var phone1 = $(""#phone1"").val();
		var phone2 = $(""#phone2"").val();
		var fax = $(""#fax"").val();
		var city = $(""#city"").val();
		var personInCharge = $(""#personInCharge"").val();
		var code = $(""#code"").val();
		var address = $(""#address"").val();
		var userCode = $(""#userCode"").val();
		var userId = $(""#userId"").val();");
            WriteLiteral(@"
		var userPassword = $(""#userPassword"").val();
		var userName = $(""#userName"").val();
		var userSurname = $(""#userSurname"").val();
		var userEmail = $(""#userEmail"").val();
		var userPhone = $(""#userPhone"").val(); 
        if (name == """" || userCode == """" || userPassword == """" || userName == """" || userSurname == """" ) {
            swal('Hata!', 'Lütfen yıldızlı(*) alanları boş bırakmayınız!!', 'error');
            return;
		}
		
		if (userPhone!="""" && isNaN(userPhone)) {
			swal('Hata!', 'Lütfen telefon numarasını doğru giriniz..', 'error');
			return;
		}
		if (phone1!="""" && isNaN(phone1)) {
			swal('Hata!', 'Lütfen telefon numarasını doğru giriniz..', 'error');
			return;
		}
		if (phone2!="""" && isNaN(phone2)) {
			swal('Hata!', 'Lütfen telefon numarasını doğru giriniz..', 'error');
			return;
		}
		if (userEmail != """" && !(userEmail.includes('");
            EndContext();
            BeginContext(11432, 190, true);
            WriteLiteral("@\'))) { \r\n\t\t\tswal(\'Hata!\', \'Lütfen E-mail\"i doğru giriniz...\', \'error\');\r\n\t\t\treturn;\r\n\t\t}\r\n\tvar canSave = false; \r\n\t\tif (userCode.length >= 3) {\r\n\t\t\tif (id == 0) {\r\n\t\t\t\t$.ajax({\r\n\t\t\t\t\turl: \'");
            EndContext();
            BeginContext(11623, 45, false);
#line 273 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\Index.cshtml"
                     Write(Url.Action("CheckIfUserExist", "Distributor"));

#line default
#line hidden
            EndContext();
            BeginContext(11668, 542, true);
            WriteLiteral(@"',
					type: 'POST',
					dataType: 'json',
					async: false,
					data: {
						""_userCode"": userCode
					},
					success: function (data) {
						if (data) {
							swal('Hata!', 'User içeride var', 'error');
							canSave = false;
						}
						else {
							canSave = true;
						}
					},
					error: function (err) {
						swal('Hata!', 'Lütfen geçerli yetkili kodu giriniz !', 'error');
					}
				});
			}
			else {
				canSave = true;
			}
		} 
		if (!canSave) {
			return;
		} 
		$.ajax({ 
			url: '");
            EndContext();
            BeginContext(12211, 44, false);
#line 302 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\Index.cshtml"
             Write(Url.Action("SaveDistributor", "Distributor"));

#line default
#line hidden
            EndContext();
            BeginContext(12255, 1089, true);
            WriteLiteral(@"',
			type:'POST',
			dataType: 'json',
			data: {
				""_id"": id,
				""_name"": name,
				""_companyPartnerId"": companyPartnerId,
				""_entranceDate"": entranceDate,
				""_exitDate"": exitDate,
				""_activity"": activity,
				""_phone1"": phone1,
				""_phone2"": phone2,
				""_fax"": fax,
				""_city"": city,
				""_personInCharge"": personInCharge,
				""_code"": code,
				""_address"": address,
				 ""_userId"": userId,
				""_userCode"": userCode,
				""_userPassword"": userPassword,
				""_userName"": userName,
				""_userSurname"": userSurname,
				""_userEmail"": userEmail,
				""_userPhone"": userPhone
			},
			success: function (data) {
			if (data) {
				console.log(data);
				      swal('Başarılı!', 'Bayi Kaydedildi!', 'success')
                        .then((value) => {
                            window.location.reload();
                        });
					}
				}
			});
	} 
	function getList(isDefault = false) {
		$(""#tableContainer"").html('');
        $(""#loadingContainer"").show();
		 bayi");
            WriteLiteral("Adi = $(\'#bayiAdi\').val();\r\n         $.ajax({\r\n            url: \'");
            EndContext();
            BeginContext(13345, 47, false);
#line 343 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\Index.cshtml"
             Write(Url.Action("GetDistributorList", "Distributor"));

#line default
#line hidden
            EndContext();
            BeginContext(13392, 387, true);
            WriteLiteral(@"',
            type: 'POST',
            dataType: 'html',
            data: {
              _bayiAdi : (isDefault ? """" : bayiAdi)
            },
			 success: function (data) {
                 $(""#loadingContainer"").hide();
                $(""#tableContainer"").html(data);
            }
        });
	} 
	function GetDistributorByIdForDisplaying(id) {
		$.ajax({
				url: '");
            EndContext();
            BeginContext(13780, 60, false);
#line 357 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\Index.cshtml"
                 Write(Url.Action("GetDistributorByIdForDisplaying", "Distributor"));

#line default
#line hidden
            EndContext();
            BeginContext(13840, 1528, true);
            WriteLiteral(@"',
				type: 'POST',
				dataType: 'json',
				data: {
					""_id"": id
			 },
			success: function (data) {
			console.log(data);
					$(""#exampleModalCenter"").modal('show');
					$(""#id"").val(data[""Id""]);
					$(""#name"").val(data[""Name""]);
					$(""#companyPartnerId"").val(data[""CompanyPartnerId""]);
					$(""#entranceDate"").val((data[""EntranceDate""] == null ? """" : data[""EntranceDate""].split('T')[0]));
					$(""#exitDate"").val((data[""ExitDate""] == null ? """" : data[""ExitDate""].split('T')[0]));
					$(""#activity"").val(data[""Activity""]==true?1:0);
					$(""#phone1"").val(data[""Phone1""]);
					$(""#phone2"").val(data[""Phone2""]);
					$(""#fax"").val(data[""Fax""]);
					$(""#city"").val(data[""City""]);
					$(""#personInCharge"").val(data[""PersonInCharge""]);
					$(""#code"").val(data[""Code""]);
					$(""#address"").val(data[""Address""]);
					$(""#userCode"").val(data[""UserCode""]);
					$(""#userPassword"").val(data[""UserPassword""]);
					$(""#userName"").val(data[""UserName""]);
					$(""#userSurname"").val(data[""UserSu");
            WriteLiteral(@"rname""]); ;
					$(""#userEmail"").val(data[""UserEmail""]); ;
					$(""#userPhone"").val(data[""UserTelephone""]); ;
					$(""#userId"").val(data[""UserId""]); ;
           		 }
			});
	} 
	function DeleteDistributor(id) {
			swal({
			title: ""Emin misiniz?"",
			text: ""Bu bayiyi silerseniz geri alamayacaksınız!"",
			icon: ""warning"",
			buttons: [
				'Hayır,iptal et!',
				'Evet , eminim!'
			],
			dangerMode: true,
		}).then(function (isConfirm) {
			if (isConfirm) {
			$.ajax({
				url: '");
            EndContext();
            BeginContext(15369, 46, false);
#line 402 "C:\Users\merve\Desktop\VERAWEB\VeraAdmin\Views\Distributor\Index.cshtml"
                 Write(Url.Action("DeleteDistributor", "Distributor"));

#line default
#line hidden
            EndContext();
            BeginContext(15415, 399, true);
            WriteLiteral(@"',
				type: 'POST',
				dataType: 'json',
				data: {
				""_id"": id
			 },
			success: function (data) {
			    if (data) {
                    swal('Successful!', 'Bayi Silindi!', 'success')
                        .then((value) => {
                            window.location.reload();
                        });
                }
           }
			});
			}
		}) 
	} 
</script> ");
            EndContext();
            BeginContext(15833, 648, true);
            WriteLiteral(@"

<script>
	function getOffset(el) {
		var _x = 0;
		var _y = 0;
		while (el && !isNaN(el.offsetLeft) && !isNaN(el.offsetTop)) {
			_x += el.offsetLeft - el.scrollLeft;
			_y += el.offsetTop - el.scrollTop;
			el = el.offsetParent;
		}
		return { top: _y, left: _x };
	}

	function showSubMenu(elem) {
		$(""#rightClickMenuContainer"").fadeIn(250);
		var objOffset = getOffset(elem);
		var height = $(""#rightClickMenu"").height();
		var toptop = 0;
		$(""#rightClickMenu"").css(""margin-top"", (objOffset.top - parseInt($(""#rightClickMenu"").height())));

		$(""#rightClickMenu"").css(""margin-left"", objOffset.left + 20);
	}
</script> ");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DistributorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
