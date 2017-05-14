cls
break> Active_Learning_Group4_CleanDB.sql 
@ECHO --------------------------Structure----------------- >> Active_Learning_Group4_CleanDB.sql 
type 1_Setup\Struct.sql >> Active_Learning_Group4_CleanDB.sql
@ECHO --------------------------Index--------------------- >> Active_Learning_Group4_CleanDB.sql
type 2_Index\Index.sql >> Active_Learning_Group4_CleanDB.sql
@ECHO --------------------------Function------------------ >> Active_Learning_Group4_CleanDB.sql
type 3_Function\Function.sql >> Active_Learning_Group4_CleanDB.sql
@ECHO --------------------------View---------------------- >> Active_Learning_Group4_CleanDB.sql
type 4_View\View.sql >> Active_Learning_Group4_CleanDB.sql
@ECHO --------------------------SP------------------------ >> Active_Learning_Group4_CleanDB.sql
type 5_SP\SP.sql >> Active_Learning_Group4_CleanDB.sql
@ECHO --------------------------Data---------------------- >> Active_Learning_Group4_CleanDB.sql
type 6_Data\Data.sql >> Active_Learning_Group4_CleanDB.sql
@ECHO --------------------------Others---------------------- >> Active_Learning_Group4_CleanDB.sql
type 7_Others\Patch_DBVersion.sql >> Active_Learning_Group4_CleanDB.sql

@ECHO DONE !

pause
