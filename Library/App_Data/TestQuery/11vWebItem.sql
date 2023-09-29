if exists (
select * from sys.views where name='vwebItems'
)
begin
 drop view vwebItems
end
go
/*** first check whether exists with version thru c# ***/

create view vwebItems As
select  
accessionnumber,form,
f.title,f.OriginalPrice,f.OriginalCurrency,f.publishercode,f.PubSource,f.catalogdate1,
f.pages,f.booktype,f.edition,f.isbn,f.issn,
i.Language_Name,f.part,f.eBookURL,f.control008,
f.subject1,f.subject2,f.subject3,
h.firstname Publisher,j.percity PublCity,
g.Item_Type,
k.classnumber,k.booknumber,
l.Subtitle,
replace(m.firstname1+' '+ m.middlename1+' '+m.lastname1,'  ',' ') Author1,
replace(m.firstname2+' '+ m.middlename2+' '+m.lastname2,'  ',' ') Author2,
replace(m.firstname3+' '+ m.middlename3+' '+m.lastname3,'  ',' ') Author3
,m.PersonalName,m.DateAssociated,m.DateofWork,
released,
bookprice,Status,IssueStatus,LoadingDate,CheckStatus,editionyear,Copynumber,pubYear,
biilNo,billDate,catalogdate,program_id,DeptCode,
ItemCategoryCode,/*Loc_id, skipped */a.BookNumber AccnBookNumber,a.VendorId
,b.ItemStatus
,c.Category_LoadingStatus Category
,d.vendorname Vendor, e.percity VendorCity
from bookaccessionmaster a 
join ItemStatusMaster b on a.Status=b.ItemStatusID
join CategoryLoadingStatus c on a.ItemCategoryCode=c.Id
join vendormaster d on a.VendorId=d.vendorid
join AddressTable e on d.vendorcode=e.addid and e.addrelation='vendor'
join BookCatalog f on a.ctrl_no=f.ctrl_no
join Item_Type g on f.booktype=g.Id
join publishermaster h on f.publishercode=h.PublisherId
join Translation_Language i on f.language_id=i.Language_Id
join AddressTable j on h.PublisherId=j.addid and j.addrelation='publisher'
join CatalogData k on f.ctrl_no=k.ctrl_no
join BookConference l on f.ctrl_no=l.ctrl_no
join BookAuthor m on a.ctrl_no=m.ctrl_no
