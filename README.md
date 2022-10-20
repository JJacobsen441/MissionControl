# MissionControl
  
Login backoffice:  
name - joakimjacobsen_441@hotmail.com  
pass - asdf123456  

Ingeniørene fra MissionControl har haft travlt de seneste par dage, men synes de er nået i mål:)  

Noter:  
Opgaven blev løst i Umbraco. ved nærmere omtanke havde det måske været bedre med et standart WebApi.  
Der er ikke lavet frontend, da alle opgaverne kunne løses som api løsninger.  
Jeg har muligvis misforstået opgaven fra Specs 2 (den var meget løst beskrevet).  
Jeg havde nogle problemer med authentication, så dette gik der meget tid på.  
Arbejds rækkefølgen var således: Spec 4(user api), Spec 1, Spec 2, Spec 4(missionreport api), Spec 3  
Har skrevet kommentarer i koden.  
For at tilgå Api'et: https://insomnia.rest/

Edits:
FacilityBIZ.GetClosest5min 		- the list should ofcause be ordered by timestamp not distance
RestResponces.ResultForecast1 	- added location, latitude, longitude and distance
Project 						- removed class FacilityCentre, uses FacilityDTO instead
GeneralHelper 					- updated GetIndex