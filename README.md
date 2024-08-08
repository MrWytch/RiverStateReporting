# RiverStateReporting

A school project.

A razor pages application for monitoring of rivers water level.
The application uses a SQL databes to store data.
Data is sent to the application by stations deployed over US rivers. Data sending can be simulated using e.g. Postman (HTTP POST request).
Data is saved in database via API (included in the project).

Data model (JSON format):

{
"stationid":1,                            // int
"value":75,                               // int 1-999 = river's water level in cm
"timestamp":"2024-02-02-T1212:30:00"      // DateTime
}


Before first use of the application run the migrations. After opening the application, follow the prompts on the home page.
