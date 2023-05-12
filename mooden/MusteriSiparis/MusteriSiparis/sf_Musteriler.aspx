<%@ Page Title="MusteriSiparis" Language="C#" MasterPageFile="~/Ana.Master" AutoEventWireup="true" CodeBehind="sf_Musteriler.aspx.cs" Inherits="MusteriSiparis.sf_Musteriler" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">       
        $(document).ready(function () {
            function gup(name) {
                name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
                var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
                var results = regex.exec(location.search);
                return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
            };
            $("#nav-log-Musteriler").addClass("active");
            if (gup('op') == "l")
                $("#nav2-log-Musteriler").addClass("active");
            else
                $("#nav2-log-Musteriler").addClass("active");
        });
    </script>

    <script>
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "80%", height: "75%", top: "-100px" });

            $(document).on('cbox_open', function () {
                $('body').css({ overflow: 'hidden' });
            }).on('cbox_closed', function () {
                $('body').css({ overflow: '' });
            });
        });

        function ExportToExcel(tableid, wsadi, dsadi) {
            var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
            var textRange; var j = 0;
            tab = document.getElementById(tableid);//.getElementsByTagName('table'); // id of table
            if (tab == null) {
                return false;
            }
            if (tab.rows.length == 0) {
                return false;
            }
            for (j = 0; j < tab.rows.length; j++) {
                tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";//tab_text=tab_text+"</tr>";
            }

            tab_text = tab_text + "</table>";
            tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
            tab_text = tab_text.replace(/<a[^>]*>|<\/a>/g, "");//remove if u want links in your table
            tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
            tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params
            tab_text = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>' + wsadi + '</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--> <meta charset="utf-8"></head><body>' + tab_text + '</body></html>';

            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");

            if (!dsadi.endsWith(".xls"))
                dsadi = dsadi + ".xls";

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
            {
                txtArea1.document.open("txt/html", "replace");
                txtArea1.document.write(tab_text);
                txtArea1.document.close();
                txtArea1.focus();
                sa = txtArea1.document.execCommand("SaveAs", true, dsadi + ".xls");
            }
            else {
                //other browser not tested on IE 11
                //sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
                try {
                    var blob = new Blob([tab_text], { type: "application/vnd.ms-excel" });
                    window.URL = window.URL || window.webkitURL;
                    link = window.URL.createObjectURL(blob);
                    a = document.createElement("a");
                    a.download = dsadi;
                    a.href = link;
                    document.body.appendChild(a);
                    a.click();
                    document.body.removeChild(a);
                } catch (e) {
                }
            }
            return false;
            //return (sa);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

                <div class="text-center">
                        <h2 class="font-weight-bold text-center">Müşteriler</h2>
                    </div>
                <div class="row">
                    <div class="col-2">
                            <div class="form-group">
                                <label class="font-weight-bold"> Müşteri</label>
                                <asp:TextBox ID="txt_MusteriAdi" runat="server" Style="width: 100%" placeholder="Müşteri Adı" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    <div class="col-2">
                            <div class="form-group">
                                <label>Şehir </label>
                                <asp:DropDownList ID="ddl_Sehir" runat="server" Style="width: 100%" CssClass="form-control select2" OnSelectedIndexChanged="ddl_Sehir_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    <div class="col-2">
                            <div class="form-group">
                                <label>İlçe </label>
                                <asp:DropDownList ID="ddl_Ilce" runat="server" Style="width: 100%" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                    <div class="col-lg-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button ID="btn_Ara" runat="server" Text="Ara" CssClass="btn btn-primary btn-block" OnClick="btn_Ara_Click" />
                            </div>
                        </div>
                    <div class="col-lg-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button ID="btn_excelolarakver" runat="server" Text="Excel"
                                    CssClass="btn btn-success btn-block" OnClick="btn_excelolarakver_Click" />
 </div>
                        </div>

                    <div class="col-lg-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <a href="pop_MusteriEkle.aspx" class="btn btn-primary btn-block iframe"><i class="simple-icon-user-follow"></i> Müşteri Ekle
                                </a>
                            </div>
                        </div>
                </div>
                
                <div class="separator mb-5"></div>

        <div class="card">
        
                    <div class="table-responsive">
                          <div style ="overflow-x:auto">
                        <asp:Repeater ID="rpt_Musteriler" runat="server" OnItemCommand="rpt_Musteriler_ItemCommand">
                            <HeaderTemplate>
                              
                                <table class="table table-striped data-table-standard" id="dataTable" data-order="[[ 1, &quot;desc&quot; ]]" width="100%" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th class="text-center"> Firma </th>
                                            <th class="text-center"> Adı Soyadi </th>
                                            <th class="text-center"> Şehir / İlçe</th>
                                            <th class="text-center"> Durum </th>
                                            <th class="text-center"> İşlem </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                               <tr>
                                   <td class="text-center">
                                        <%# Eval("firma_adi")%> 
                                   </td>
                                   <td class="text-center">
                                        <%# Eval("musteri_adi")%> <%# Eval("musteri_soyadi")%>
                                   </td>
                                   <td class="text-center">
                                        <%# Eval("sehir_adi")%> / <%# Eval("ilce_adi")%>
                                   </td>
                                   <td class="text-center">

                                <b><span  style="color:<%# Eval("aktif_pasif").ToString() == "1" ? "green" : "red"%>"><%# Eval("aktif_pasif").ToString() == "1" ? "Aktif" : "Pasif"%></span></b>

                            </td>
                                   <td class="text-center">
                                       
                                       <a href='pop_MusteriEkle.aspx?id=<%#Eval("id")%>' data-toggle="tooltip" data-placement="top" title="Güncelle" style="color:white ;background-color:darkblue;" class="btn iframe">
                                                    <i class="iconsminds-file-edit"></i>
                                                </a>

                                       <asp:LinkButton ID="btn_Aktif" Visible="false" CommandArgument='<%#Eval("id")%>' CommandName="aktif" CssClass="btn btn-success" runat="server" data-toggle="tooltip" data-placement="top" title="Aktif"><i class="simple-icon-check"></i></asp:LinkButton>
                                       <asp:LinkButton ID="btn_Pasif" Visible="false" CommandArgument='<%#Eval("id")%>' CommandName="pasif" CssClass="btn btn-danger" runat="server" data-toggle="tooltip" data-placement="top" title="Pasif"><i class="simple-icon-trash"></i></asp:LinkButton>
                                   </td>
                               </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    
                     
                        </div>
                    </div>
    </div>
    </div>
            </asp:Content>