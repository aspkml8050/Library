<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProgressControl.ascx.vb" Inherits="ProgressControl" %>
<style type="text/css">
    .TopProg
{
    top:90px;
    position: fixed;
    left:45%;
    background-color:Yellow;
    border-left:solid 1px gray;
    border-right:solid 1px gray;
    border-bottom:solid 1px gray;
    border-radius:0px 0px 4px 4px;
    padding:5px;
    z-index:350;
    font-family:Cambria;
    font-size:small;
}
</style>
<div class="TopProg">
                <img style="padding-right:5px;" src="img/progress.gif" />Processing...
            </div>