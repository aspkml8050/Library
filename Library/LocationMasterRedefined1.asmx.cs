using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Library.App_Code.MultipleFramworks
{
    /// <summary>
    /// Summary description for LocationMasterRedefined1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class LocationMasterRedefined1 : System.Web.Services.WebService
    {
        MultipleFramworks.DBIStructure DBI=new MultipleFramworks.DBIStructure();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(EnableSession = true)]
        public string SaveLocationObject(string ObjectName, string Abbr, string Operation, string IdUpdate, string Order)
        {
            string ReturnString = string.Empty;
            // Dim dt As DataTable = DBI.GetDataTable("select * from LocationObject where LocationObject='" & ObjectName & "'", DBI.GetConnectionString(DBI.GetConnectionName()))
            // If dt.Rows.Count > 0 Then
            // ReturnString = "Existed"
            // Return ReturnString
            // End If
            DBI.BeginTransaction(DBI.GetConnectionString(DBI.GetConnectionName()));
            try
            {
                var ord = string.IsNullOrEmpty(Order) ? "0" : Order.Trim();
                if (Operation == "Save")
                {
                    int Id = new int();
                    Id = Convert.ToInt32(DBI.ExecuteScalar("select isnull(max(id),0)+1 from LocationObject", DBI.GetConnectionString(DBI.GetConnectionName())));
                    var Parameter = new ParameterCollection();
                    Parameter.Add("@Id", DbType.Int32, Id.ToString());
                    Parameter.Add("@LocationObject", DbType.String, ObjectName);
                    Parameter.Add("@Abbreviation", DbType.String, Abbr);
                    Parameter.Add("@Inst_Id", DbType.String, "1");
                    Parameter.Add("@OrderNo", DbType.String, ord);
                    DBI.ExecuteProcedure("Insert_LocationObject", Parameter, DBI.GetConnectionString(DBI.GetConnectionName()));
                    ReturnString = "Save";
                }
                else if (Operation == "Update")
                {
                    var iupd = Convert.ToInt32(IdUpdate);
                    if (iupd != -1)
                    {
                        int Id = iupd;// Conversions.ToInteger(IdUpdate);
                        // DBI.ExecuteQueryOnDB("delete from LocationObject where Id=" & Id & "", DBI.GetConnectionString(DBI.GetConnectionName()))
                        var Parameter = new ParameterCollection();
                        Parameter.Add("@Id", DbType.Int32, Id.ToString());
                        Parameter.Add("@LocationObject", DbType.String, ObjectName);
                        Parameter.Add("@Abbreviation", DbType.String, Abbr);
                        Parameter.Add("@Inst_Id", DbType.String, "1");
                        Parameter.Add("@OrderNo", DbType.String, ord);
                        DBI.ExecuteProcedure("Insert_LocationObject", Parameter, DBI.GetConnectionString(DBI.GetConnectionName()));
                        ReturnString = "Update";
                    }
                }
            }
            catch (Exception ex)
            {
                ReturnString = "Existed";
                DBI.RollbackTransaction();
            }
            DBI.CommitTransaction();
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string getTPageCount()
        {
            int count = Convert.ToInt32(DBI.ExecuteScalar("Select count(*) from Mapped_Location", DBI.GetConnectionString(DBI.GetConnectionName())));
            int num = count / 200;
            if (count % 200 != 0)
            {
                num = num + 1;
            }
            return num.ToString();
        }

        [WebMethod(EnableSession = true)]
        public string DeleteLocationObject(string Id)
        {
            string ReturnString = string.Empty;
            var dt = DBI.GetDataTable("select * from Mapped_Location", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                {
                    string gf = dt.Rows[i]["Location_Path"].ToString();
                    string[] fg = gf.Split('-');
                    if (fg.Length > 0)
                    {
                        for (int j = 0, loopTo1 = fg.Length - 1; j <= loopTo1; j++)
                        {
                            string qw = DBI.ExecuteScalar("select LocationObjectId from LocationObject_Items where id=" + fg[j] + "", DBI.GetConnectionString(DBI.GetConnectionName()));
                            if ((qw ?? "") == (Id ?? ""))
                            {
                                ReturnString = "Existed";
                                return ReturnString;
                            }
                        }
                    }
                }
            }
            DBI.BeginTransaction(DBI.GetConnectionString(DBI.GetConnectionName()));
            DBI.ExecuteQueryOnDB("delete from LocationObject where Id=" + Id + "", DBI.GetConnectionString(DBI.GetConnectionName()));
            DBI.CommitTransaction();
            ReturnString = "Success";
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string GetLocationObjectData()
        {
            string ReturnStr = string.Empty;
            var dt = DBI.GetDataTable("select Id,LocationObject,Abbreviation,OrderNo from LocationObject ", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                ReturnStr = "<table class='ForTable'>";
                ReturnStr = ReturnStr + "<tr>";
                ReturnStr = ReturnStr + "<th>" + "Location Object" + "</th>";
                ReturnStr = ReturnStr + "<th>" + "Abbreviation" + "</th>";
                ReturnStr = ReturnStr + "<th>" + "Order No" + "</th>";
                ReturnStr = ReturnStr + "</tr>";
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                {
                    ReturnStr = ReturnStr + "<tr>";
                    ReturnStr = ReturnStr + "<td >" + "<a href=\"" +" JavaScript:EditRecordLocationObject('" + dt.Rows[i]["Id"] + "');" +  "\">" + dt.Rows[i]["LocationObject"] + "</a>" + "</td>";
                    ReturnStr = ReturnStr + "<td >"+ dt.Rows[i]["Abbreviation"].ToString()+ "</td>";
                    ReturnStr = ReturnStr + "<td>" + dt.Rows[i]["OrderNo"].ToString() + "</td>";
                    ReturnStr = ReturnStr + "</tr>";
                }
                ReturnStr = ReturnStr + "</table>";
            }
            return ReturnStr;
        }
        [WebMethod(EnableSession = true)]
        public string EditLocationObjectData(string Id)
        {
            string ReturnString = string.Empty;
            var dt = DBI.GetDataTable("select Id,LocationObject,Abbreviation,OrderNo from LocationObject where id=" + Id + " ", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                ReturnString = dt.Rows[0]["LocationObject"].ToString();
                ReturnString = ReturnString + ","+ dt.Rows[0]["Abbreviation"].ToString();
                ReturnString = ReturnString + "," + dt.Rows[0]["OrderNo"].ToString();
            }
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string GetLocationObjectDataForTab2()
        {
            string ReturnString = string.Empty;
            ReturnString = "<select id='dropLocationObject' style='width:100%;border:1px solid black;' >";
            ReturnString = ReturnString + "<option value='-1'>" + "---Select---" + "</option>";
            var dt = DBI.GetDataTable("select Id,LocationObject from LocationObject ", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                    ReturnString =ReturnString + "<option value='"+ dt.Rows[i]["Id"].ToString()+ "'>"+ dt.Rows[i]["LocationObject"].ToString()+ "</option>";
            }
            ReturnString = ReturnString + "</select>";
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string btnSaveAddItem(string LocationObject, string Item, string Operation, string ItemId)
        {
            string ReturnString = string.Empty;
            DBI.BeginTransaction(DBI.GetConnectionString(DBI.GetConnectionName()));
            var dt = DBI.GetDataTable("select * from LocationObject_Items where LocationObjectItem='" + Item + "'", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                ReturnString = "Existed";
                return ReturnString;
            }
            if (Operation == "Save")
            {
                int Id = Convert.ToInt32(DBI.ExecuteScalar("select isnull(max(Id),0)+1 from LocationObject_Items", DBI.GetConnectionString(DBI.GetConnectionName())));
                var Parameter = new ParameterCollection();
                Parameter.Add("@Id", DbType.Int32, Id.ToString());
                Parameter.Add("@LocationObjectId", DbType.String, LocationObject);
                Parameter.Add("@LocationObjectItem", DbType.String, Item);
                Parameter.Add("@Inst_Id", DbType.String, "1");
                DBI.ExecuteProcedure("Insert_LocationObject_Items", Parameter, DBI.GetConnectionString(DBI.GetConnectionName()));
                ReturnString = "Save";
            }
            else if (Operation == "Update")
            {
                int Id = Convert.ToInt32(ItemId);
                DBI.ExecuteQueryOnDB("delete from LocationObject_Items where Id=" + Id + "", DBI.GetConnectionString(DBI.GetConnectionName()));
                var Parameter = new ParameterCollection();
                Parameter.Add("@Id", DbType.Int32, Id.ToString());
                Parameter.Add("@LocationObjectId", DbType.String, LocationObject);
                Parameter.Add("@LocationObjectItem", DbType.String, Item);
                Parameter.Add("@Inst_Id", DbType.String, "1");
                DBI.ExecuteProcedure("Insert_LocationObject_Items", Parameter, DBI.GetConnectionString(DBI.GetConnectionName()));

                // -----------------------------------nw updating mapped records according to new Name of location object
                var dts = DBI.GetDataTable("select * from mapped_location", DBI.GetConnectionString(DBI.GetConnectionName()));
                if (dts.Rows.Count > 0)
                {
                    for (int z = 0, loopTo = dts.Rows.Count - 1; z <= loopTo; z++)
                    {
                        string Loc_Path = dts.Rows[z]["Location_Path"].ToString();
                        string[] Loc_path_A = Loc_Path.Split('-');
                        if (Loc_path_A.Length > 0)
                        {
                            for (int x = 0, loopTo1 = Loc_path_A.Length - 1; x <= loopTo1; x++)
                            {
                                if (Id == Convert.ToInt32(Loc_path_A[x]))
                                {
                                    DBI.ExecuteQueryOnDB("update Mapped_Location set Location='" + DBI.ExecuteScalar("select dbo.getLocation_String('" + Loc_Path + "')",DBI.GetConnectionString()), DBI.GetConnectionString());
                                }
                            }
                        }
                    }
                }
                ReturnString = "Update";
            }
            DBI.CommitTransaction();

            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string btnSaveAddMultiItem(string LocationObject, string AliasName, string Fromm, string Too)
        {
            string ReturnString = string.Empty;
            int loopLenght = (int)Math.Round(Convert.ToDouble(Too) - Convert.ToDouble(Fromm));
            var dt = DBI.GetDataTable("select LocationObjectItem from LocationObject_Items ", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                {
                    for (int j = 0, loopTo1 = loopLenght; j <= loopTo1; j++)
                    {
                        if (dt.Rows[i]["LocationObjectItem"].ToString() == (AliasName + (Fromm + j)).ToString())
                        {
                            ReturnString = "Existed";
                            return ReturnString;
                        }
                    }
                }
            }

            for (int i = 0, loopTo2 = loopLenght; i <= loopTo2; i++)
            {
                int Id = Convert.ToInt32(DBI.ExecuteScalar("select isnull(max(Id),0)+1 from LocationObject_Items", DBI.GetConnectionString(DBI.GetConnectionName())));
                var parameter = new ParameterCollection();
                parameter.Add("@Id", DbType.Int32, Id.ToString());
                parameter.Add("@LocationObjectId", DbType.String, LocationObject);
                parameter.Add("@LocationObjectItem", DbType.String, (AliasName + (Convert.ToDouble(Fromm) + i)).ToString());
                parameter.Add("@Inst_Id", DbType.String, "1");
                DBI.ExecuteProcedure("Insert_LocationObject_Items", parameter, DBI.GetConnectionString(DBI.GetConnectionName()));
                ReturnString = "Save";
            }
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string GetDataForLoacationItem()
        {
            string ReturnString = string.Empty;
            var dt = DBI.GetDataTable("select LocationObject_Items.LocationObjectItem,LocationObject_Items.Id,LocationObject.LocationObject from LocationObject_Items,LocationObject where LocationObject.Id = LocationObject_Items.LocationObjectId ", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                ReturnString = "<table class='ForTable' ><tr>";
                ReturnString = ReturnString + "<th>" + "Location Object Item" + "</th>";
                ReturnString = ReturnString + "<th>" + "Location Object" + "</th>";
                ReturnString = ReturnString + "<th>" + "Delete" + "</th></tr>";
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                {
                    ReturnString = ReturnString + "<tr><td>" + "<a href=\"" +  "JavaScript:EditRecordLocationObjectItem('" + dt.Rows[i]["Id"].ToString() + "');" +  "\">" + dt.Rows[i]["LocationObjectItem"] + "</a>" + "</td>";
                    ReturnString = ReturnString + "<td>"+ dt.Rows[i]["LocationObject"].ToString()+ "</td>";
                    ReturnString = ReturnString + "<td>" + "<a href=\""  + "JavaScript:DeleteRecordLocationObjectItem('" + dt.Rows[i]["Id"].ToString() + "');" +  "\">" + "Delete" + "</a>" + "</td></tr>";
                }
                ReturnString = ReturnString + "</table>";
            }
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string DeleteRecordLocationObjectItem(string Id)
        {
            string ReturnString = string.Empty;
            var dt = DBI.GetDataTable("select * from Mapped_Location ", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                {
                    string strr = dt.Rows[i]["Location_Path"].ToString();
                    string[] stre = strr.Split('-');
                    if (stre.Length > 0)
                    {
                        for (int j = 0, loopTo1 = stre.Length - 1; j <= loopTo1; j++)
                        {
                            if ((Id ?? "") == (stre[j] ?? ""))
                            {
                                ReturnString = "Existed";
                                return ReturnString;
                            }
                        }
                    }
                }
            }
            DBI.ExecuteQueryOnDB("delete from LocationObject_Items where Id='" + Id + "'", DBI.GetConnectionString(DBI.GetConnectionName()));
            ReturnString = "Delete";
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string EditRecordLocationObjectItem(string Id)
        {
            string ReturnString = string.Empty;
            var dt = DBI.GetDataTable("select * from LocationObject_Items where Id=" + Id + "", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                ReturnString = dt.Rows[0]["LocationObjectId"].ToString();
                ReturnString = ReturnString + ","+ dt.Rows[0]["LocationObjectItem"].ToString();
            }
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string GetLocationObjectDataForTab3()
        {
            string ReturnString = string.Empty;
            var dt = DBI.GetDataTable("select id,LocationObject from LocationObject order by OrderNo", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                ReturnString = "<table width='100%'>";
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                {
                    if (i == 0 | i == 3 | i == 6 | i == 9)
                    {
                        ReturnString = ReturnString + "<tr>";
                    }


                    ReturnString = ReturnString + "<td style='width:33.33%;text-align:left;border:1px solid black;' >";
                    ReturnString = ReturnString + "<fieldset style='width:100%;height:227px'>";
                    ReturnString = ReturnString + "<legend style='color:black'> " + dt.Rows[i]["LocationObject"].ToString() + " </legend>";
                    ReturnString = ReturnString + "<div style='width:100%;height:180px;overflow:auto;'>";
                    ReturnString = ReturnString + "<table class='ForTable' id='PartTable" + dt.Rows[i]["Id"].ToString() + "' >";
                    ReturnString = ReturnString + "<tr>";
                    ReturnString = ReturnString + "<th>" + "<input type='checkbox' id='chkbxMain" + dt.Rows[i]["Id"].ToString() + "' onchange='ChkSelectAllForTab3(" + dt.Rows[i]["Id"].ToString() + ")' /><span>All</span>" + "</th>";
                    ReturnString = ReturnString + "<th>" + "Items" + "</th>";
                    ReturnString = ReturnString + "</tr>";
                    var dt1 = DBI.GetDataTable("select Id,LocationObjectItem from LocationObject_Items where LocationObjectId='" + dt.Rows[i]["Id"].ToString() + "' order by LocationObjectItem ", DBI.GetConnectionString(DBI.GetConnectionName()));
                    if (dt1.Rows.Count > 0)
                    {
                        for (int j = 0, loopTo1 = dt1.Rows.Count - 1; j <= loopTo1; j++)
                        {
                            ReturnString = ReturnString + "<tr>";
                            ReturnString = ReturnString + "<td>" + "<input type='checkbox' id='chkbx" + dt1.Rows[j]["Id"].ToString() + "' />" + "</td>";
                            ReturnString = ReturnString + "<td>" + dt1.Rows[j]["LocationObjectItem"].ToString() + "</td>";
                            ReturnString = ReturnString + "</tr>";
                        }
                    }
                    ReturnString = ReturnString + "</table>";
                    ReturnString = ReturnString + "</div>";
                    ReturnString = ReturnString + "</fieldset>";
                    ReturnString = ReturnString + "</td>";
                    if (i == 2 | i == 5 | i == 8 | i == 11)
                    {
                        ReturnString = ReturnString + "</tr>";
                    }

                }

                ReturnString = ReturnString + "</table>";
            }
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string SelectAllFunction(string Id)
        {
            string ReturnString = string.Empty;
            var dt = DBI.GetDataTable("select Id from LocationObject_Items where LocationObjectId=" + Id + " ", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                {
                    if (string.IsNullOrEmpty(ReturnString))
                    {
                        ReturnString = dt.Rows[i]["Id"].ToString();
                    }
                    else
                    {
                        ReturnString = ReturnString + "," + dt.Rows[i]["Id"].ToString();
                    }
                }
            }
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string MapRecords1()
        {
            string ReturnString = string.Empty;
            var dt = DBI.GetDataTable("select Id from LocationObject", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                {
                    if (string.IsNullOrEmpty(ReturnString))
                    {
                        ReturnString = dt.Rows[i]["Id"].ToString();
                    }
                    else
                    {
                        ReturnString = ReturnString + "," + dt.Rows[i]["Id"].ToString();
                    }
                }
            }
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string MapRecords2(string LocationObject)
        {
            string ReturnString = string.Empty;
            var dt = DBI.GetDataTable("select Id from locationobject_items where LocationObjectId in(" + LocationObject + ")", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                {
                    if (string.IsNullOrEmpty(ReturnString))
                    {
                        ReturnString = dt.Rows[i]["Id"].ToString();
                    }
                    else
                    {
                        ReturnString = ReturnString + "," + dt.Rows[i]["Id"].ToString();
                    }
                }
            }
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string MapRecord3(string[] MapArray)
        {
            string ReturnString = string.Empty;
            var FinalDt = new DataTable();
            var ds = new DataSet();
            if (MapArray.Length > 0)
            {
                string objtIds = string.Empty;
                for (int i = 0, loopTo = MapArray.Length - 1; i <= loopTo; i++)
                {
                    if (string.IsNullOrEmpty(objtIds))
                    {
                        objtIds = MapArray[i];
                    }
                    else
                    {
                        objtIds = objtIds + "," + MapArray[i];
                    }
                }
                var dtObjectID = DBI.GetDataTable("select id from LocationObject order by Cast(ISNULL(OrderNo,0) as bigint)", DBI.GetConnectionString(DBI.GetConnectionName()));
                if (dtObjectID.Rows.Count > 0)
                {
                    for (int i = 0, loopTo1 = dtObjectID.Rows.Count - 1; i <= loopTo1; i++)
                    {
                        var dtt = DBI.GetDataTable("select id from LocationObject_Items where LocationObjectId='" + dtObjectID.Rows[i]["id"].ToString() + "' and id in(" + objtIds + ")", DBI.GetConnectionString(DBI.GetConnectionName()));
                        dtt.TableName = i.ToString();
                        // This will add datatable to dataset only when it does contain some records otherwise it will not include it in dataset for mapping generation
                        if (dtt.Rows.Count > 0)
                        {
                            ds.Tables.Add(dtt.Copy());
                        }
                    }
                }
                if (ds.Tables.Count == 1)
                {
                    FinalDt = ds.Tables[0];
                }
                else
                {
                    for (int i = 1, loopTo2 = ds.Tables.Count - 1; i <= loopTo2; i++)
                    {
                        if (i == 1)
                        {
                            FinalDt = GenerateCombination(ds.Tables[0], ds.Tables[1]);
                        }
                        else
                        {
                            FinalDt = GenerateCombination(FinalDt, ds.Tables[i]);
                        }
                    }
                }
            }
            DBI.BeginTransaction(DBI.GetConnectionString(DBI.GetConnectionName()));
            if (FinalDt.Rows.Count > 0)
            {
                ReturnString = "Duplicate";
                for (int i = 0, loopTo3 = FinalDt.Rows.Count - 1; i <= loopTo3; i++)
                {
                    var dq = DBI.GetDataTable("select * from Mapped_Location where Location_Path='" + FinalDt.Rows[i]["Id"].ToString() + "'", DBI.GetConnectionString(DBI.GetConnectionName()));
                    if (dq.Rows.Count == 0)
                    {
                        int Id = Convert.ToInt32( DBI.ExecuteScalar("select isnull(max(id),0)+1 from Mapped_Location", DBI.GetConnectionString(DBI.GetConnectionName())));
                        var parameter = new ParameterCollection();
                        parameter.Add("@Id", DbType.Int32, Id.ToString());
                        parameter.Add("@Location_Name", DbType.String, "");
                        parameter.Add("@Location_Path", DbType.String, FinalDt.Rows[i]["Id"].ToString());
                        parameter.Add("@Inst_Id", DbType.String, "1");
                        parameter.Add("@Location", DbType.String, DBNull.Value.ToString());
                        DBI.ExecuteProcedure("insert_Mapped_Location", parameter, DBI.GetConnectionString(DBI.GetConnectionName()));
                        ReturnString = "Success";
                    }
                }
            }
            DBI.CommitTransaction();
            return ReturnString;
        }
        public DataTable GenerateCombination(DataTable dt1, DataTable dt2)
        {
            var ReturnDt = new DataTable();
            ReturnDt.Columns.Add("id", typeof(string));
            if (dt1.Rows.Count > 0 & dt2.Rows.Count > 0)
            {
                for (int i = 0, loopTo = dt1.Rows.Count - 1; i <= loopTo; i++)
                {
                    for (int j = 0, loopTo1 = dt2.Rows.Count - 1; j <= loopTo1; j++)
                    {
                        var dr = ReturnDt.NewRow();
                        dr["id"] = dt1.Rows[i]["id"].ToString() + "-" + dt2.Rows[j]["id"].ToString();
                        ReturnDt.Rows.Add(dr);
                        ReturnDt.AcceptChanges();
                    }
                }
            }
            return ReturnDt;
        }
        [WebMethod(EnableSession = true)]
        public string GetMappedRecord(string pageNo)
        {
            return DBI.ExecuteScalar("select dbo.getLocations(" + pageNo + ")", DBI.GetConnectionString(DBI.GetConnectionName()));
        }
        [WebMethod(EnableSession = true)]
        public string CheckAllLocations(string KeywORD)
        {
            var FinalDt = new DataTable();
            FinalDt.Columns.Add("Path");
            FinalDt.Columns.Add("Id");
            FinalDt.Columns.Add("Alias");
            var dt = DBI.GetDataTable("select Location_Path,Id,Location_Name from Mapped_Location ", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                {
                    string OPath;
                    string[] Path;
                    OPath = dt.Rows[i]["Location_Path"].ToString();
                    Path = OPath.Split('-');
                    string stradd = string.Empty;
                    if (Path.Length > 0)
                    {
                        for (int j = 0, loopTo1 = Path.Length - 1; j <= loopTo1; j++)
                        {
                            if (string.IsNullOrEmpty(stradd))
                            {
                                stradd = DBI.ExecuteScalar("select LocationObjectItem from LocationObject_Items where id=" + Path[j] + "", DBI.GetConnectionString(DBI.GetConnectionName()));
                            }
                            else
                            {
                                stradd = stradd + "-" + DBI.ExecuteScalar("select LocationObjectItem from LocationObject_Items where id=" + Path[j] + "", DBI.GetConnectionString(DBI.GetConnectionName()));
                            }
                        }
                        var dr = FinalDt.NewRow();
                        dr["Path"] = stradd;
                        dr["Id"] = dt.Rows[i]["Id"];
                        dr["Alias"] = dt.Rows[i]["Location_Name"];
                        FinalDt.Rows.Add(dr);
                        FinalDt.AcceptChanges();
                    }
                }
            }
            DataRow[] drs = FinalDt.Select("Path like '%" + KeywORD.Trim() + "%'");





            string ReturnString = string.Empty;
            // Dim dt As DataTable = DBI.GetDataTable("select Id from Mapped_Location where ", DBI.GetConnectionString(DBI.GetConnectionName()))
            if (drs.Length > 0)
            {
                for (int i = 0, loopTo2 = drs.Length - 1; i <= loopTo2; i++)
                {
                    if (string.IsNullOrEmpty(ReturnString))
                    {
                        ReturnString = drs[i]["Id"].ToString();
                    }
                    else
                    {
                        ReturnString = ReturnString + "," + drs[i]["Id"].ToString();
                    }
                }
            }
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string UnMapped_Click(string strList)
        {
            string ReturnString = string.Empty;
            if (!string.IsNullOrEmpty(strList))
            {
                DBI.BeginTransaction(DBI.GetConnectionString(DBI.GetConnectionName()));
                DBI.ExecuteQueryOnDB("delete from Mapped_Location where Id in (" + strList + ")", DBI.GetConnectionString(DBI.GetConnectionName()));
                DBI.CommitTransaction();
                ReturnString = "Success";
            }
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string AddAliasName(string data, string id)
        {
            string ReturnString = string.Empty;
            DBI.BeginTransaction(DBI.GetConnectionString(DBI.GetConnectionName()));
            DBI.ExecuteQueryOnDB("update Mapped_Location set Location_Name='" + data + "' where id=" + id + "", DBI.GetConnectionString(DBI.GetConnectionName()));
            DBI.CommitTransaction();
            ReturnString = "Success";
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string GetSuggestion(string PrefixText)
        {
            var dt = DBI.GetDataTable("select top 10 Location_Path,Id,dbo.getLocation_String(Mapped_Location.Location_Path) as Location from Mapped_Location	where dbo.getLocation_String(Mapped_Location.Location_Path) like '%" + PrefixText.Trim() + "%' or Location_Name like '%" + PrefixText.Trim() + "%'", DBI.GetConnectionString(DBI.GetConnectionName()));
            string ReturnString = string.Empty;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                {
                    if (string.IsNullOrEmpty(ReturnString))
                    {
                        ReturnString = "<table width='100%'><tr><td style='width:100%;text-align:left'><div class='suggestion' onclick=" +  "\" SelectRecord('" + dt.Rows[i]["Location"].ToString() + "');" + "\"> " + dt.Rows[i]["Location"] + "[" + dt.Rows[i]["Id"] + "] </div></td></tr>";
                    }
                    else
                    {
                        ReturnString = ReturnString + "<tr><td style='width:100%;text-align:left'><div class='suggestion' onclick=" + "\" SelectRecord('" + dt.Rows[i]["Location"] + "');" + "\"> " + dt.Rows[i]["Location"] + "[" + dt.Rows[i]["Id"] + "]  </div></td></tr>";
                    }
                }
                ReturnString = ReturnString + "</table>";
            }
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string GetSearchedRecords(string PrefixText)
        {
            string ReturnString = string.Empty;
            // Dim dt As DataTable = DBI.GetDataTable("select top 10 Location_Path,Id,Location_Name,dbo.getLocation_String(Mapped_Location.Location_Path) as Location from Mapped_Location	where dbo.getLocation_String(Mapped_Location.Location_Path) like '%" & PrefixText.Trim() & "%'", DBI.GetConnectionString(DBI.GetConnectionName()))
            var dt = DBI.GetDataTable("select top 250 Location_Path,Id,Location_Name,dbo.LocDecode2(Mapped_Location.id) as Location from Mapped_Location	where dbo.LocDecode2(id) like '%" + PrefixText.Trim() + "%'  ", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                ReturnString = "<table class='ForTable'>";
                ReturnString = ReturnString + "<tr><th width='20%'>" + "<input type='checkbox' id='chkbxAllLocation'  onchange='ChkSelectAllForLocation()' /><span>All</span>" + "</th><th width='50%'>" + "Location" + "</th><th width='30%'>" + "Alias" + "</th></tr>";
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                {
                    ReturnString = ReturnString + "<tr>";
                    ReturnString = ReturnString + "<td width='20%'>" + "<input type='checkbox' id='chkbxLocation" + dt.Rows[i]["Id"].ToString() + "'  onchange='ChkSelectForLocation(" + dt.Rows[i]["Id"].ToString() + ")' />" + "</td>";
                    ReturnString = ReturnString + "<td width='50%'>" + dt.Rows[i]["Location"].ToString() + "(" + "<a href='EditMappedLocation(" + dt.Rows[i]["Id"].ToString() + ")'>edit</a>" + ")" + "</td>";
                    ReturnString = ReturnString + "<td width='30%'>" + "<input value='" + dt.Rows[i]["Location_Name"].ToString() + "' type='textbox' id='txtAliasName" + dt.Rows[i]["Id"].ToString() + "' onchange='AddAliasName(" + dt.Rows[i]["Id"].ToString() + ")' " + "</td>";
                    ReturnString = ReturnString + "</tr>";
                }
            }
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string EditMappedLocation(string map_loc_id)
        {
            string ReturnString = string.Empty;
            string Loc_Path = DBI.ExecuteScalar("select Location_Path from Mapped_Location where Id='" + map_loc_id + "'", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (!string.IsNullOrEmpty(Loc_Path))
            {
                string[] LocObjs = Loc_Path.Split('-');
                if (LocObjs.Length > 0)
                {
                    for (int i = 0, loopTo = LocObjs.Length - 1; i <= loopTo; i++)
                    {
                        if (string.IsNullOrEmpty(ReturnString))
                        {
                            ReturnString = LocObjs[i];
                        }
                        else
                        {
                            ReturnString = ReturnString + "," + LocObjs[i];
                        }
                    }
                }
            }
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string btnMapSave_Click1()
        {
            string ReturnString = string.Empty;
            var dt = DBI.GetDataTable("select Id from LocationObject_Items", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0, loopTo = dt.Rows.Count - 1; i <= loopTo; i++)
                {
                    if (string.IsNullOrEmpty(ReturnString))
                    {
                        ReturnString = dt.Rows[i]["Id"].ToString();
                    }
                    else
                    {
                        ReturnString = ReturnString + "," + dt.Rows[i]["Id"].ToString();
                    }
                }
            }
            return ReturnString;
        }
        [WebMethod(EnableSession = true)]
        public string btnMapSave_Click2(string[] ArryTo_Map, string M_LocID_for_Update)
        {
            string ReturnString = string.Empty;
            string loc_obj_id = string.Empty;
            if (ArryTo_Map.Length > 0)
            {
                for (int i = 0, loopTo = ArryTo_Map.Length - 1; i <= loopTo; i++)
                {
                    if (string.IsNullOrEmpty(loc_obj_id))
                    {
                        loc_obj_id = ArryTo_Map[i];
                    }
                    else
                    {
                        loc_obj_id = loc_obj_id + "," + ArryTo_Map[i];
                    }
                }
            }
            string Map_String = string.Empty;
            DataTable dtr;
            var dt = DBI.GetDataTable("select id from LocationObject order by id", DBI.GetConnectionString(DBI.GetConnectionName()));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0, loopTo1 = dt.Rows.Count - 1; i <= loopTo1; i++)
                {
                    dtr = DBI.GetDataTable("select id from LocationObject_Items where LocationObjectId='" + dt.Rows[i]["Id"].ToString() + "' and id in (" + loc_obj_id + ")", DBI.GetConnectionString(DBI.GetConnectionName()));
                    if (dtr.Rows.Count > 0)
                    {
                        if (dtr.Rows.Count > 1)
                        {
                            ReturnString = "Location Sequence is not correct .... !!";
                            goto FinalStep;
                        }
                        else if (string.IsNullOrEmpty(Map_String))
                        {
                            Map_String = dtr.Rows[0]["Id"].ToString();
                        }
                        else
                        {
                            Map_String = Map_String + "-" + dtr.Rows[0]["Id"].ToString();
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(Map_String))
            {
                if (DBI.ExecuteQueryOnDB("update Mapped_Location set	 Location_Path='" + Map_String + "' where id='" + M_LocID_for_Update + "'", DBI.GetConnectionString(DBI.GetConnectionName())) == true)
                {
                    ReturnString = "Updated .....!!";
                    goto FinalStep;
                }
                else
                {
                    ReturnString = "This updation is causing Duplication in records, Can not process request ... !!";
                    goto FinalStep;
                }
            }
            else
            {
                ReturnString = "Error..!! try after resetting page";
                goto FinalStep;
            }

        FinalStep:
            ;

            return ReturnString;
        }
    }
}
