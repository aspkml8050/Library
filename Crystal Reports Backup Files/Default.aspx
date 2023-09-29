<%@ Page Title="Home Page" Language="C#"  AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Library._Default" %>



<!DOCTYPE html>

<html >
<head runat="server">
    <title>Login</title>
          <script type="text/javascript" src="Scripts/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" src="lib/jqueryui/jquery-ui.min.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <link rel="stylesheet" href="lib/bootstrap-icons/font/bootstrap-icons.css" />
    <style  type="text/css">
/*        // X-Small devices (portrait phones, less than 576px)
// No media query for `xs` since this is the default in Bootstrap

// Small devices (landscape phones, 576px and up)
    */
.errinp{
    border-color:red;
}
@media (min-width: 576px) { 
    .container-md{
        width:100%;
    }

}

/* Medium devices (tablets, 768px and up)*/
@media (min-width: 768px) { 
        .container-md{
            margin-top:10%;
        width:100%;
    }

    }

/* Large devices (desktops, 992px and up)*/
@media (min-width: 992px) { 
        .container-md{
            margin-top:12%;
        width:50%;
    }

    }

/* X-Large devices (large desktops, 1200px and up)*/
@media (min-width: 1200px) {  }

    </style>
    <script type="text/javascript">
        $(function () {
        })
        function signIn() {
            var iusr = $('[id$=txUserId]');
            if (iusr.val().trim() == '') {
                iusr.addClass('errinp');
                return false;
            } else {
                iusr.removeClass('errinp');
            }
            var pw = $('[id$=txPassw]');
            if (iusr.val().trim() == '') {
                pw.addClass('errinp');
                return false;
            } else {
                pw.removeClass('errinp');
            }

            $('[id$=btnSignIn]').click();
        }
        function MasterpageMessage(messg, msglevel = 1) {
            MasterMesg(messg, msglevel);

            return false;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-md border rounded ">
            <div class="row">
                <div class="col-md-5 center">
                </div>
                <div class="col-md-2 center">
            <h4>Login</h4>
                </div>
                <div class="col-md-5 center">

                </div>
            </div>
            <div class="row">
                <div class="col-md-12 center">
                    <asp:Label ID="labers" runat="server"  ForeColor ="Red"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5">
                    <h5>User Id</h5>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txUserId" runat="server" CssClass="form-control "  Text="" Width="100%"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <i class="bi  bi-person" ></i>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5">
                    <h5>Password</h5>
                </div>
                <div class="col-md-6 ">
                    <asp:TextBox ID="txPassw" runat="server" CssClass="form-control bi bi-key   " TextMode="Password" Text="" Width="100%"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <i class="bi bi-key"></i>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 ">
                    <button type="button" onclick="return signIn()" class="btn btn-primary"><i class="bi bi-box-arrow-in-right"></i> Sign In </button>
                    <asp:Button id="btnSignIn" runat="server" style="display:none" OnClick="btnSignIn_Click" />
                </div>
                <div class="col-md-6 ">

                </div>
            </div>

        </div>
    </form>

</body>
</html>
