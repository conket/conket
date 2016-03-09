use nihongo_voca
go

delete from ms_uservocabularies;
delete from ms_testresults;
delete from ms_registedvocasets;
delete from ms_paymenthistories;
delete from ms_users;
insert into ms_users(UserName, Password, DisplayName, Status, SystemData, IsAdmin, LoginState, LastVisitedDate) 
values('e1a8cb9d404d967181e2735ecf3477d7', '080397d264f8223eba1081d73c11ae94', 'Ken Chu', '1', '1', '1', null, null);
insert into ms_uservocabularies
(
VocaDetailID
,UserName
,[Level]
, HasLearnt
,Update_Date
,StartDate
,EndDate
,Description)
select 
ms_vocabularydetails.ID
, 'e1a8cb9d404d967181e2735ecf3477d7'
, '10'
,'0'
,null
,'2015-01-01'
,'2016-01-01'
,'' from ms_vocabularydetails;

-- Update numof voca

