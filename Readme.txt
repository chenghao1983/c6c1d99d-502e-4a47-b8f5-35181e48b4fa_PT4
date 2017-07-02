1. Git URL
https://github.com/chenghao1983/c6c1d99d-502e-4a47-b8f5-35181e48b4fa_PT4.git/trunk

2. Azure URL: https://chenghwebapp.azurewebsites.net, https://chenghwebapp-secondary.azurewebsites.net

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
	1. Set upload file location in Web Config
		<!--1 for File System, 2 for Blob-->
		<add key="UploadLocation" value="2" />
	2. Set storage container name
		<add key="StorageContainerName" value="activelearning" />
	3. Web config. Connection string "Active_Learning_Group4" for database
	4. Web config. Connection string "StorageConnectionString" for storage
	5. Make sure the following are in the Appsettings in web.config
		<add key="AllowedFileExtensions" value="jpg,jpeg,png,gif,mp4,ppt,pptx,txt,doc,docx,pdf,xls,xlsx" />
		<add key="VideoFormats" value="mp4" />
    		<add key="UploadPath" value="/Upload/" />
   		<!--AllowedFileSize in MB-->
    		<add key="AllowedFileSize" value="40" />
    		<add key="ChatHistoryCount" value="100" />
