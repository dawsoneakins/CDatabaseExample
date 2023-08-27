using CDatabaseExample;
using System.Data.SqlClient;

namespace CDatabaseExample
{
    class Program
    {
        public void printPersonId(String personName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = "Data Source=DESKTOP-5E2DEFN;" +
                        "Initial Catalog=CDatabaseExample;" +
                        "User id=dawso;" +
                        "Password=don;";
                    connection.Open();
                    Console.WriteLine("Connection is Open");

                    String sqlCommand = $"SELECT ID FROM Person WHERE Name=@Name;";

                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                    {
                        command.Parameters.AddWithValue("@Name", personName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"Person ID: {reader["ID"]}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No ID found for selected name");
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        public void printPersonName(int personId)
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = "Data Source=DESKTOP-5E2DEFN;" +
                        "Initial Catalog=CDatabaseExample;" +
                        "User id=dawso;" +
                        "Password=don;";

                connection.Open();

                String commandString = "SELECT Name FROM Person WHERE ID=@ID;";

                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.AddWithValue("@ID", personId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"Person Name: {reader["Name"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No name found for given ID");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void addPerson(String name, int age)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = "Data Source=DESKTOP-5E2DEFN;" +
                        "Initial Catalog=CDatabaseExample;" +
                        "User id=dawso;" +
                        "Password=don;";

                connection.Open();
                Console.WriteLine("Connection is Open");

                String sqlCommand = "INSERT INTO Person (Name, Age) VALUES (@Name, @Age);";

                if (!checkIfPersonExists(connection, name))
                {
                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                    {


                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Age", age);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine("Added the Person to the database");
                        Console.WriteLine($"Rows Affected: {rowsAffected}");
                    }

                }
                else
                {
                    Console.WriteLine("Person already exists in database");
                }
            }
        }

        public void updatePersonName(String name, String updatedName)
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = "Data Source=DESKTOP-5E2DEFN;" +
                            "Initial Catalog=CDatabaseExample;" +
                            "User id=dawso;" +
                            "Password=don;";

                connection.Open();
                Console.WriteLine("Connection has been opened");

                String sqlCommand = "UPDATE Person SET Name = @NewName WHERE Name = @Name;";

                if (checkIfPersonExists(connection, name))
                {
                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                    {
                        command.Parameters.AddWithValue("@NewName", updatedName);
                        command.Parameters.AddWithValue("@Name", name);

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine("Updated Name");
                        Console.WriteLine($"Rows Affected: {rowsAffected}");
                    }
                }
                else
                {
                    Console.WriteLine("Person does not exist");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }



        }

        public void deletePerson(int personId, String name)
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = "Data Source=DESKTOP-5E2DEFN;Initial Catalog=CDatabaseExample;User id=dawso;Password=don;";

                connection.Open();

                String sqlCommand = "DELETE FROM Person WHERE ID=@ID";
                if (checkIfPersonExists(connection, name))
                {
                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                    {
                        command.Parameters.AddWithValue("@ID", personId);

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"Rows Affected: {rowsAffected}");
                        Console.WriteLine("Person has been deleted");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static bool checkIfPersonExists(SqlConnection connection, String name)
        {
            String sqlCommand = "SELECT COUNT(*) FROM Person WHERE Name=@Name;";
            using (SqlCommand command = new SqlCommand(sqlCommand, connection))
            {
                command.Parameters.AddWithValue("@Name", name);

                int count = Convert.ToInt32(command.ExecuteScalar());

                return count > 0;
            }
        }
    }
}

class PrintData
{
    public static void Main(string[] args)
    {
        Program program = new Program();
        String personName = "Don Panini";
        int personId = 0;

        String newPersonName = "Carlos Spicy Weiner";
        int newPersonAge = 30;
        int newPersonId = 4;

        String updatedName = "Sado";

        //program.addPerson(newPersonName, newPersonAge);
        //program.updatePersonName(newPersonName, updatedName);
        program.deletePerson(newPersonId, updatedName);
        program.printPersonName(newPersonId);
    }
}