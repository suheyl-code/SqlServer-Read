using System.Data.SqlClient;
using System.Text;

var data = ReadFromSQL();
var querry = from d in data
select d;
foreach(var i in querry)
{
    System.Console.WriteLine(i.Adres);
}

static SqlConnection SetSQLConnection()
{

    string? dataSource = default;
    dataSource = @"LenovoThinkbook\SQLEXPRESS";

    string dataBase = "Northwind";
    string connectionString = @"Data Source=" + dataSource + ";Initial Catalog=" + dataBase + ";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    SqlConnection connection = new SqlConnection(connectionString);
    try
    {
        connection.Open();
        Console.WriteLine("\tSQL Connected...");
    }
    catch (Exception e)
    {
        Console.WriteLine("Problem in SQL Connection!" + e.Message);
    }
    return connection;
}

static List<Shipwreck> ReadFromSQL()
{
    List<Shipwreck> listOfItems = new List<Shipwreck>();
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("Select * From dbo.Musteriler");
    string sqlQuery = stringBuilder.ToString();

    using (SqlCommand command = new SqlCommand(sqlQuery, SetSQLConnection()))
    {
        SqlDataReader rdr = command.ExecuteReader();
        while(rdr.Read())
        {
            listOfItems.Add(new Shipwreck
            {
                MusteriID = Convert.ToString(rdr[0]),
                SirketAdi = Convert.ToString(rdr[1]),
                MusteriAdi = Convert.ToString(rdr[2]),
                MusteriUnvani = Convert.ToString(rdr[3]),
                Adres = Convert.ToString(rdr[4])
            });
        }
    }
    return listOfItems;
}