<%@ Page Title="MusteriSiparis" Language="C#" MasterPageFile="~/Ana.Master" AutoEventWireup="true" CodeBehind="sf_Siparisler.aspx.cs" Inherits="MusteriSiparis.sf_Siparisler" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">       
        $(document).ready(function () {
            function gup(name) {
                name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
                var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
                var results = regex.exec(location.search);
                return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
            };
            $("#nav-log-Siparisler").addClass("active");
            if (gup('op') == "l")
                $("#nav2-log-Siparisler").addClass("active");
            else
                $("#nav2-log-Siparisler").addClass("active");
        });
    </script>

    <script>
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "50%", height: "85%", top: "-80px" });

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
                        <h2 class="font-weight-bold text-center">Siparişler</h2>
                    </div>
                <div class="row">
                    <div class="col-2">
                            <div class="form-group">
                                <label class="font-weight-bold"> Sipariş Numarası</label>
                                <asp:TextBox ID="txt_SiparisNo" runat="server" Style="width: 100%" placeholder="Sipariş numarası" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>

                    <div class="col-2">
                            <div class="form-group">
                                <label>Ürün Türü </label>
                                <asp:DropDownList ID="ddl_UrunTuru" runat="server" Style="width: 100%" CssClass="form-control select2" OnSelectedIndexChanged="ddl_UrunTuru_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    <div class="col-2">
                            <div class="form-group">
                                <label>Ürün </label>
                                <asp:DropDownList ID="ddl_Urun" runat="server" Style="width: 100%" CssClass="form-control select2"></asp:DropDownList>
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
                                <a href="pop_SiparisEkle.aspx" class="btn btn-primary btn-block iframe"><i class="simple-icon-user-follow"></i> Sipariş Ekle
                                </a>
                            </div>
                        </div>
                </div>
                
                <div class="separator mb-5"></div>

        <div class="card">
        
                    <div class="table-responsive">

                        <asp:Repeater ID="rpt_Siparisler" runat="server" OnItemCommand="rpt_Siparisler_ItemCommand">
                            <HeaderTemplate>
                                <table class="table table-striped data-table-standard" id="dataTable" data-order="[[ 1, &quot;desc&quot; ]]" width="100%" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th> Sipariş No </th>
                                            <th> Firma</th>
                                            <th> Ürün Türü / Ürün  </th>
                                            <th> Adet </th>
                                            <th> Fiyat </th>
                                            <th> Toplam Fiyat </th>
                                            <th> Sipariş Tarihi </th>
                                            <th> Durum </th>
                                            <th> İşlem </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                               <tr>
                                   <td>
                                        <%# Eval("siparis_no")%>
                                   </td>
                                   <td>
                                        <%# Eval("firma_adi")%>
                                   </td>
                                   <td>
                                        <%# Eval("tur_adi")%> / <%# Eval("urun_adi")%>
                                   </td>
                                   <td>
                                        <%# Eval("adet")%>
                                   </td>
                                   <td>
                                        <%# Eval("fiyat")%>
                                   </td>
                                   <td>
                                        <%# Eval("toplam_fiyat")%>
                                   </td>
                                   <td>
                                        <%# Eval("siparis_tarihi")%>
                                   </td>
                                   <td>
                                        <%# Eval("durum")%>
                                   </td>
                                   <td>
                                       
                                       <a href='pop_SiparisEkle.aspx?id=<%#Eval("id")%>' data-toggle="tooltip" data-placement="top" title="Güncelle" style="color:white ;background-color:darkblue;" class="btn iframe">
                                                    <i class="iconsminds-file-edit"></i>
                                                </a>

                                       <asp:LinkButton ID="btn_Onayla" Visible="false" CommandArgument='<%#Eval("id")%>' CommandName="onayla" CssClass="btn btn-success" runat="server" data-toggle="tooltip" data-placement="top" title="Siparişi onayla"><i class="simple-icon-check"></i></asp:LinkButton>
                                       <asp:LinkButton ID="btn_IptalEt" Visible="false" CommandArgument='<%#Eval("id")%>' CommandName="iptalet" CssClass="btn btn-danger" runat="server" data-toggle="tooltip" data-placement="top" title="Siparişi iptal et"><i class="simple-icon-trash"></i></asp:LinkButton>
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
            </asp:Content>