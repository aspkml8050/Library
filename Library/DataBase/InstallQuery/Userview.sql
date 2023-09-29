alter VIEW [dbo].[userview]  
AS  
SELECT     TOP 100 PERCENT dbo.CircUserManagement.userid,   
                     (case   
  when dbo.CircUserManagement.middlename=' ' then dbo.CircUserManagement.firstname + ' ' + dbo.CircUserManagement.lastname   
  when dbo.CircUserManagement.lastname=' ' then dbo.CircUserManagement.firstname + ' ' + dbo.CircUserManagement.middlename   
  when dbo.CircUserManagement.middlename=' ' and dbo.CircUserManagement.lastname=' ' then dbo.CircUserManagement.firstname else dbo.CircUserManagement.firstname + ' ' + dbo.CircUserManagement.middlename + ' ' + dbo.CircUserManagement.lastname end) AS name
,   
                      dbo.InstituteMaster.ShortName + '-' + dbo.departmentmaster.departmentname AS departmentname, dbo.departmentmaster.departmentcode,   
                      dbo.CircUserManagement.classname AS classname, dbo.CircUserManagement.gender AS gender,
					  isnull( dbo.CircUserManagement.opac_status,'' ) opac_status,  
                      convert (nvarchar,validupto,106) as validupto ,status,email1,email2,convert (nvarchar,doj,106)as doj ,phone1,phone2,convert (nvarchar,Deactivatedon,106) as Deactivatedon ,FatherName,convert (nvarchar,Dob,106)as DOb,Joinyear,Subjects,
YearSem,Session,MotherName,  
                      Localaddress,Localcity,Localstate,Localpincode,LocalCountry,perAddress,percity,perstate,percountry,perpincode,program_id  
FROM         dbo.CircUserManagement INNER JOIN  
                      dbo.departmentmaster ON dbo.departmentmaster.departmentcode = dbo.CircUserManagement.departmentcode INNER JOIN  
                      dbo.InstituteMaster ON dbo.departmentmaster.institutecode = dbo.InstituteMaster.InstituteCode inner join  
                       dbo.addresstable On addresstable.addid=dbo.CircUserManagement.userid  
  
where dbo.addresstable.Addrelation='User Management'  
ORDER BY dbo.CircUserManagement.firstname, dbo.departmentmaster.departmentname, dbo.CircUserManagement.status