1. Git URL
https://github.com/chenghao1983/9EA0F9C3-119A-4D20-9D56-E444CBEC5925_Active_Learning_Group4

2. Azure URL: chh1983web.cloudapp.net
Accounts
Username: student1  	Password: 1qazxsw2!@QW
Username: student2  	Password: 1qazxsw2!@QW
Username: instructor1 	Password: 1qazxsw2!@QW
Username: instructor2 	Password: 1qazxsw2!@QW
Username: admin1 	Password: 1qazxsw2!@QW


3.Database creation Guide
	1. Create MSSQL DB, schema name: Active_Learning_Group4
	2. Run DB/Active_Learning_Group4_CleanDB.sql to create fresh database	

4. Solution configuration Guide
	1. Create Upload the root and write permission
	2. Web config. Connection string
	3. Make sure the following are in the Appsettings in web.config
	<add key="AllowedFileExtensions" value="mp4,ppt,pptx,txt,doc,docx,pdf,xls,xlsx" />
	<add key="VideoFormats" value="mp4"/>
	<add key="UploadPath" value="/Upload/" />
	<add key="AllowedFileSize" value="40" />
	<add key="ChatHistoryCount" value="100" />





Notes: chat history is now available :)