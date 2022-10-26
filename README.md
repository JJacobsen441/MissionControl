# MissionControl
  
Login backoffice:  
name - joakimjacobsen_441@hotmail.com  
pass - asdf123456  
  
Ingeniørene fra MissionControl har haft travlt de seneste par dage, men synes de er nået i mål:)  
  
Noter:  
Opgaven blev løst i Umbraco. ville nok have valgt standart WebApi (tror jeg var farvet af stillingbetegnelsen).  
Der er ikke lavet frontend, da alle opgaverne kunne løses som api løsninger.  
Jeg har muligvis misforstået opgaven fra Specs 2 (den var meget løst beskrevet).  
Jeg havde nogle problemer med authentication, så dette gik der meget tid på.  
Arbejds rækkefølgen var således: Spec 4(user api), Spec 1, Spec 2, Spec 4(missionreport api), Spec 3  
Har skrevet kommentarer i koden.  
For at tilgå Api'et: https://insomnia.rest/  
  
Edits:  
FacilityBIZ.GetClosest5min 			- the list should ofcause be ordered by timestamp not distance  
RestResponces.ResultForecast1 		- added location, latitude, longitude and distance  
Project 							- removed class FacilityCentre, uses FacilityDTO instead  
GeneralHelper 						- updated GetIndex  
  
MissionReportBIZ					- CreateMissionReport, checks for user  
MissionReportBIZ					- OpdateMissionReport, checks for user  
DataBaseComposer					- Added ON DELETE CASCADE, for foreignkey missionreport  
DataBaseComposer					- Added ON DELETE CASCADE, for foreignkey missionimages  
CheckHelper							- Added check in CheckID  
Database.Facilitys 					- added ISSlatitude, ISSlongitude (better late than never)  
RestResponces.ResultForecast1 		- added ISSlatitude, ISSlongitude (better late than never)  
UserBIZ.GetUsers					- orderby id  
GetMissinReports.GetMissinReports	- orderby id  
ApiUserController					- updated function names  
ApiReportController					- updated function names  
WeatherController					- updated function names  

CheckHelper							- adjusted checks for inputstrings  
CustomBinderComposer				- added ModelBinder for endpoint mission/missionreport post/put

BasicAuthenticationFilter			- updated to retrieve name, pass from webconfig

GeneralHelper.GetLowestTemps		- added comment

GeneralHelper.GetLowestTemps		- updated algorithm for lowest temps

GeneralHelper.GetLowestTemps		- prettyfied code

GeneralHelper.GetLowestTemps		- adjusted code