# CDatabaseExample

-This repo contains an example C# project that I used to interface with a Microsoft SQL Server Database. 
-It containts two functions that print a Person from the database based on the value passed in. (printPersonId(String personName) && printPersonName(int personId)
-It contains a function to add a Person to the database (addPerson(String name, int age)
-It contains a function to update a user that exists in the database. (updatePersonName(String name, String updatedName))
-It contains a function to delete a Person from the database (deletePerson(int personId, String name))
-Finally it contains a helper function that checks whether a given Person exists in the database. (checkIfPersonExists(SqlConnection connection, String name).
