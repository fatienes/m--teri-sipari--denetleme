<%@ Page Title="MusteriSiparis" Language="C#" MasterPageFile="~/Ana.Master" AutoEventWireup="true" CodeBehind="sf_UrunTuru.aspx.cs" Inherits="MusteriSiparis.sf_UrunTuru" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">       
        $(document).ready(function () {
            function gup(name) {
                name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
                var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
                var results = regex.exec(location.search);
                return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
            };
            $("#nav-log-UrunTuru").addClass("active");
            if (gup('op') == "l")
                $("#nav2-log-UrunTuru").addClass("active");
            else
                $("#nav2-log-UrunTuru").addClass("active");
        });
    </script>

    <script>
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "30%", height: "50%", top: "-60px" });

            $(document).on('cbox_open', function () {
                $('body').css({ overflow: 'hidden' });
            }).on('cbox_closed', function () {
                $('body').css({ overflow: '' });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

                <div class="text-center">
                        <h2 class="font-weight-bold text-center">Ürün Türleri</h2>
                    </div>
                <div class="row">
                    <div class="col-3">
                            <div class="form-group">
                                <label class="font-weight-bold"> Ürün Türü</label>
                                <asp:TextBox ID="txt_UrunTuru" runat="server" Style="width: 100%" placeholder="Ürün türü adı" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    <div class="col-lg-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button ID="btn_Ara" runat="server" Text="Ara" CssClass="btn btn-primary btn-block" OnClick="btn_Ara_Click" />
                            </div>
                        </div>

                    <div class="col-lg-4"></div>
                    <div class="col-lg-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <a href="pop_UrunTuruEkle.aspx" class="btn btn-primary btn-block iframe"><i class="simple-icon-user-follow"></i> Ürün Türü Ekle
                                </a>
                            </div>
                        </div>
                </div>
                
                <div class="separator mb-5"></div>

        <div class="card">
        
                    <div class="table-responsive">

                        <asp:Repeater ID="rpt_UrunTuru" runat="server" OnItemCommand="rpt_UrunTuru_ItemCommand">
                            <HeaderTemplate>
                                <table class="table table-striped data-table-standard" id="dataTable" data-order="[[ 1, &quot;desc&quot; ]]" width="100%" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th class="text-center"> Ürün Türü Adı </th>
                                            <th class="text-center"> Durum</th>
                                            <th class="text-center"> İşlem </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                               <tr>
                                   <td class="text-center">
                                        <%# Eval("tur_adi")%>
                                   </td>
                                   <td class="text-center">

                                <b><span  style="color:<%# Eval("aktif_pasif").ToString() == "1" ? "green" : "red"%>"><%# Eval("aktif_pasif").ToString() == "1" ? "Aktif" : "Pasif"%></span></b>

                            </td>
                                   <td class="text-center">
                                       
                                       <a href='pop_UrunTuruEkle.aspx?id=<%#Eval("id")%>' data-toggle="tooltip" data-placement="top" title="Güncelle" style="color:white ;background-color:darkblue;" class="btn iframe">
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
            </asp:Content>