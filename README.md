## AzureHW

AzureHW is class library application which retrievs data from an api endpoint every minute (time triggered function). There is stored success/failure attempt log in the table and full payload in the blob. Api enpoint and timer is specified in local.settings.json file and it is added to this repository (but it shouldn't be).

You can retrieve attempt log from local storage emulator by this link http://localhost:7071/api/GetLogsFunction 
Logs are retrieved by provading date time from and till, for an example, ?datefrom=04.23.2021&dateto=04.26.2021.

You can retrieve a payload (information about get request and retrieved data) from blob storage by this link http://localhost:7071/api/GetPayloadFunction 
Payload is retrieved by specifying exact time when the payload was saved, for an example, blobName=04.24.2021 17:40:04. 

Expected format for date or blob name is "MM.dd.yyyy HH:mm:ss". 


What could have been done better:

* Interfaces could have been created for similar interfaces (for example, ITableStorage and IBlobStorage - IStorage);
* Exceptions could have been more descriptive, reused when possible;
* Data format needed to be UTC;
* Create tests for all logic;
