<%@ Page Title="Contact" Language="C#" MasterPageFile="~/LibraryMain.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Library.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Your contact page.</h3>
        <address>
            One Microsoft Way<br />
            Redmond, WA 98052-6399<br />
            <abbr title="Phone">P:</abbr>
            425.555.0100
        </address>

        <address>
            <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
            <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
        </address>
    </main>
    <script>
        $(function () {
            setAutocomp();
        });

        function setAutocomp() {
            $("[id$=txgetdata]").keyup(function () {
                let nombre = $(this).val();
                $.ajax({
                    type: "POST",
                    url: "MssplSugg.asmx/GetMemberIDJq",
                    data: JSON.stringify({ "prefixText": nombre }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var dataArray = JSON.parse(data.d);
                        var autocompleteObjects = [];
                        for (var i = 0; i < dataArray.length; i++) {
                            var object = {
                                // Used by jQuery Autocomplete to show
                                // autocomplete suggestions as well as
                                // the text in yourInputTextBox upon selection.
                                // Assign them to a value that you want the user to see.
                                value: dataArray[i].value,
                                label: dataArray[i].label,
                                // Put our own custom id here.
                                // If you want to, you can even put the result object.
                                id: dataArray[i].value
                            };

                            autocompleteObjects.push(object);
                        }
                        // Invoke the response callback.
//                        response(autocompleteObjects);
                        $('[id$=txgetdata]').autocomplete({
                                clearButton: true,
                            source: autocompleteObjects,
                                selectFirst: true,
                            minLength: 1,
                            select: function (event,ui) {
                                console.log(ui.item);
                            }
                            });
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.log('Error1: ' + xhr.responseText);
                        console.log('Error2: ' + textStatus);
                        console.log('Error3: ' + errorThrown);
                    }
                });
            });
        }
</script>
    <asp:UpdatePanel ID="upanel" runat="server">
        <ContentTemplate>
            <asp:TextBox ID="txgetdata" runat="server"  ></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
