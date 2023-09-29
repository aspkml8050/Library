Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration.ConfigurationManager
Imports Library.App_Code

Public Class CircBookIssue1
    Inherits BaseClass

    '  Dim libobj As New Library.insertLogin
    '  Dim libobj1 As New Library.libGeneralFunctions
    Dim tmpcondition As String
    '  Dim msgLibrary As New Library.messageLibrary()
    Dim message As New DBUTIL.dbUtilities
    Protected Sub cmdClear_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            clearfields()
        Catch ex As Exception
            '            message.PageMesg(ex.Message, Me, DBUTIL.dbUtilities.MsgLevel.Failure)
        End Try
    End Sub

    Protected Sub cmdcheck_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'If Me.txtuseridBIE.Text = String.Empty Then
            '    message.PageMesg(Resources.ValidationResources.EnterMemId.ToString, Me, DBUTIL.dbUtilities.MsgLevel.Warning)
            '    Me.SetFocus(txtuseridBIE)
            '    Exit Sub
            'End If

            'Dim con As New OleDbConnection(retConstr(Session("LibWiseDBConn")))
            'Dim ds As New DataSet
            'Dim cmd As New OleDbCommand
            'Try
            '    con.Open()
            '    cmd.Connection = con
            '    cmd.CommandType = CommandType.Text
            '    cmd.CommandText = "select status from CircUserManagement where CircUserManagement.userid=N'" & Trim(Me.txtuseridBIE.Text.ToString).Replace("'", "''") & "'"
            '    If cmd.ExecuteScalar = "Sleep" Then
            '        '                    message.PageMesg(Resources.ValidationResources.InvOperMemSlp.ToString, Me, DBUTIL.dbUtilities.MsgLevel.Warning)
            '        SetFocus(txtuseridBIE)
            '        Exit Sub
            '    End If

            '    ds = libobj1.PopulateDataset("select CircUserManagement.firstname + ' ' + CircUserManagement.middlename + ' ' + CircUserManagement.lastname as memname, dbo.InstituteMaster.ShortName + '-' + dbo.departmentmaster.departmentname as dept, classname, program_name, fathername from CircUserManagement  INNER JOIN dbo.departmentmaster ON dbo.CircUserManagement.departmentcode = dbo.departmentmaster.departmentcode INNER JOIN dbo.InstituteMaster ON dbo.departmentmaster.institutecode = dbo.InstituteMaster.InstituteCode INNER JOIN dbo.Program_Master ON dbo.CircUserManagement.program_id = dbo.Program_Master.program_id where CircUserManagement.userid=N'" & Trim(Me.txtuseridBIE.Text.ToString).Replace("'", "''") & "'", "Mem", con)
            '    If ds.Tables("Mem").Rows.Count > 0 Then
            '        Me.txtuseridBIE.ReadOnly = True
            '        Me.txtusername.Value = ds.Tables("mem").Rows(0).Item("memname")
            '        Me.txtfathername.Value = ds.Tables("mem").Rows(0).Item("fathername")
            '        Me.txtDept.Value = ds.Tables("mem").Rows(0).Item("dept")
            '        Me.txtMemberGroup.Value = ds.Tables("mem").Rows(0).Item("classname")
            '        Me.txtcourse.Value = ds.Tables("mem").Rows(0).Item("program_name")
            '        Image1.ImageUrl = "imagehiddenform.aspx?uid=" & Trim(txtuseridBIE.Text.ToString)
            '        Me.SetFocus(txtaccno)
            '    Else
            '        message.PageMesg(Resources.ValidationResources.InvMemID.ToString, Me, DBUTIL.dbUtilities.MsgLevel.Warning)
            '        clearfields()
            '    End If
            'Catch ex As Exception
            '    message.PageMesg(ex.Message, Me, DBUTIL.dbUtilities.MsgLevel.Warning)

            'Finally
            '    If con.State = ConnectionState.Open Then
            '        con.Close()
            '    End If
            '    con.Dispose()
            'End Try
        Catch ex As Exception
        End Try
    End Sub

    Sub clearfields()
        Me.txtaccno.Value = String.Empty
        Me.txtcourse.Value = String.Empty
        Me.txtDept.Value = String.Empty
        Me.txtMemberGroup.Value = String.Empty
        Me.txtusername.Value = String.Empty
        Me.txtfathername.Value = String.Empty
        Me.txtuseridBIE.ReadOnly = False
        Me.txtuseridBIE.Text = String.Empty
        Image1.ImageUrl = Nothing
        '   SetFocus(txtuseridBIE)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            message.PageMesg(DateTime.Now.ToString(), Me)
            'cmdClear.CausesValidation = False
            'Me.cmdcheck.CausesValidation = False
            'Me.cmdsubmit.CausesValidation = False
            'If Not IsPostBack Then
            '    hdCulture.Value = Request.Cookies("UserCulture").Value
            '    lblTitle.Text = Request.QueryString("title")

            '    tmpcondition = Request.QueryString("condition")
            '    If tmpcondition = "Y" Then
            '        cmdsubmit.Disabled = False
            '    Else
            '        cmdsubmit.Disabled = True
            '    End If
            'End If
            ' SetFocus(txtuseridBIE)
        Catch ex As Exception
            ' message.PageMesg(ex.Message, Me, DBUTIL.dbUtilities.MsgLevel.Failure)

        End Try
    End Sub

    Protected Sub cmdsubmit_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'If Me.txtuseridBIE.ReadOnly = False Then
            '    '                message.PageMesg(Resources.ValidationResources.ClEToVfyMemID.ToString, Me, DBUTIL.dbUtilities.MsgLevel.Failure)

            '    SetFocus(txtuseridBIE)
            '    Exit Sub
            'End If
            'If Me.txtaccno.Value = String.Empty Then
            '    '                message.PageMesg(Resources.ValidationResources.msgnewmember.ToString, Me, DBUTIL.dbUtilities.MsgLevel.Warning)
            '    SetFocus(txtaccno)
            '    Exit Sub
            'End If
            'Dim con As New OleDbConnection(retConstr(Session("LibWiseDBConn")))
            'Dim com As New OleDbCommand
            'Dim trans As OleDbTransaction
            'con.Open()
            'trans = con.BeginTransaction()
            'com.Connection = con
            'com.Transaction = trans
            'Try
            '    'Validations BEGIN
            '    com.Parameters.Clear()
            '    com.CommandType = CommandType.Text
            '    com.CommandText = "select userid from circuserManagement where userid=N'" & Trim(Me.txtaccno.Value).Replace("'", "''") & "'"
            '    If Not com.ExecuteScalar = String.Empty Then
            '        '                   message.PageMesg(Resources.ValidationResources.SpMemIdExist.ToString, Me, DBUTIL.dbUtilities.MsgLevel.Warning)
            '        SetFocus(txtaccno)
            '        Exit Sub
            '    End If

            '    Try
            '        Execute_update(com, "update circusermanagement set userid='" & Trim(Me.txtaccno.Value) & "',usercode='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '        Execute_update(com, "update circissuemaster set userid='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Catch ex As Exception
            '        Dim str As String = returnvalue("select  currentissuedbooks  from circissuemaster where userid='" & Trim(Me.txtuseridBIE.Text) & "' ", com)
            '        Execute_update(com, "delete from  circissuemaster where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '        Execute_update(com, "update circissuemaster set userid='" & Trim(Me.txtaccno.Value) & "',  currentissuedbooks= currentissuedbooks+ " & Val(str) & "  where userid='" & Trim(Me.txtaccno.Value) & "'")
            '    End Try
            '    Execute_update(com, "update circIssuetransaction set userid='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update CircLostBookentry set userid='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update circpartpayment set user_id='" & Trim(Me.txtaccno.Value) & "' where user_id='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update CircReceiptNo set Member_id='" & Trim(Me.txtaccno.Value) & "' where Member_id='" & Trim(Me.txtuseridBIE.Text) & "'")

            '    Try
            '        Execute_update(com, "update circreceivemaster set userid='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Catch ex As Exception
            '        Dim str As String = returnvalue("select totalfine  from circreceivemaster where userid='" & Trim(Me.txtuseridBIE.Text) & "' ", com)
            '        Execute_update(com, "delete from  circreceivemaster where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '        Execute_update(com, "update circreceivemaster set userid='" & Trim(Me.txtaccno.Value) & "',  totalfine= totalfine + " & Val(str) & "  where userid='" & Trim(Me.txtaccno.Value) & "'")
            '    End Try
            '    Execute_update(com, "update circreceivetransaction set userid='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update circreceivetransactionNDB set userid='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update circReservationtransaction set userid='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update Circularmessageposting set tomemberid='" & Trim(Me.txtaccno.Value) & "' where tomemberid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update  circUserReservations  set userid='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update Current_awairness_service  set member_id='" & Trim(Me.txtaccno.Value) & "' where member_id='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update duelistchildmaster set memberid='" & Trim(Me.txtaccno.Value) & "' where memberid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update fine_calculation set userid='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update Indentmaster set requestercode='" & Trim(Me.txtaccno.Value) & "' where requestercode='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update Journal_IssueNonA set member_id='" & Trim(Me.txtaccno.Value) & "' where member_id='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update Journal_IssueTransaction set userid='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update Journal_ReceiveTransaction set userid='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update JournalReceive_Arrival set Member_id='" & Trim(Me.txtaccno.Value) & "' where Member_id='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update m_member set mem_cd='" & Trim(Me.txtaccno.Value) & "' where mem_cd='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update MemberIssuedetails set userid='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update MemberVisitingRegister set memberid='" & Trim(Me.txtaccno.Value) & "' where memberid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update OpacIndent set requestercode='" & Trim(Me.txtaccno.Value) & "' where requestercode ='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update OverDueReceipt set userid='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update User_Activities set user_id='" & Trim(Me.txtaccno.Value) & "' where user_id='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update Waivepaid set userid='" & Trim(Me.txtaccno.Value) & "' where userid='" & Trim(Me.txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update AddressTable set addid=N'" & Trim(Trim(Me.txtaccno.Value)) & "' where addid=N'" & Trim(txtuseridBIE.Text) & "' and addrelation=N'User Management'")
            '    Execute_update(com, "update library_servicesMaster set member=N'" & Trim(Trim(Me.txtaccno.Value)) & "' where member=N'" & Trim(txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update Journal_Request set Requester_code=N'" & Trim(Trim(Me.txtaccno.Value)) & "' where Requester_code=N'" & Trim(txtuseridBIE.Text) & "'")
            '    Execute_update(com, "update userdetails set memberid=N'" & Trim(Trim(Me.txtaccno.Value)).Replace("'", "''") & "' where userid=N'" & Trim(txtuseridBIE.Text) & "'")

            '    'If Application("Audit") = "Y" Then
            '    '                  libobj.insertLoginFunc(Session("UserName"), lblTitle.Text, Session("session"), Trim(Me.txtuseridBIE.Text) & "^" & Trim(Me.txtaccno.Value), Resources.ValidationResources.bUpdate.ToString, retConstr(Session("LibWiseDBConn")))
            '    'End If

            '    trans.Commit()
            '    '                message.PageMesg(Resources.ValidationResources.OperComSuc.ToString, Me, DBUTIL.dbUtilities.MsgLevel.Success)
            '    clearfields()

            'Catch ex As Exception
            '    trans.Rollback()
            '    'Me.msglabel.Visible = True
            '    'Me.msglabel.Text = ex.Message
            '    message.PageMesg(ex.Message, Me, DBUTIL.dbUtilities.MsgLevel.Failure)

            'Finally
            '    trans.Dispose()
            '    com.Dispose()
            '    If con.State = ConnectionState.Open Then
            '        con.Close()
            '    End If
            '    con.Dispose()
            'End Try
        Catch ex As Exception
            '            message.PageMesg(ex.Message, Me, DBUTIL.dbUtilities.MsgLevel.Failure)

        End Try
    End Sub
    Function returnvalue(ByVal sqlStr As String, ByVal com As OleDbCommand) As String
        Dim str As String = String.Empty
        Try
            com.CommandType = CommandType.Text
            com.CommandText = sqlStr
            str = com.ExecuteScalar().ToString()
            com.Parameters.Clear()
            Return str
        Catch ex As Exception
            Return str
        Finally

        End Try
    End Function
    Private Sub Execute_update(ByVal com As OleDbCommand, ByVal qry As String)
        com.CommandType = CommandType.Text
        com.CommandText = qry
        com.ExecuteNonQuery()
        com.Parameters.Clear()
    End Sub
    'Protected Sub cmdReturn_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Response.Redirect("frmMainControl.aspx?url=AssignNewId.aspx?title=" & lblTitle.Text & "?condition=" & tmpcondition)
    '    Catch ex As Exception
    '        Me.msglabel.Visible = True
    '        Me.msglabel.Text = ex.Message
    '    End Try
    'End Sub

    Protected Sub btntst_Click(sender As Object, e As EventArgs)
        txtaccno.Value = "53423252"
        message.PageMesg(DateTime.Now.ToString, Me)
    End Sub
End Class
