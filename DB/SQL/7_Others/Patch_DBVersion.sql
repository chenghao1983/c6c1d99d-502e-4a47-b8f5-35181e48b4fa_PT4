-- DB version --

delete from DBVersion
go

insert into DBVersion ( DBVersion, CreateDT) values ('Schema revision 1.4.1', '2016-04-22')
go