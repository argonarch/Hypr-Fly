using Godot;
using LiteDB;
using System.Collections.Generic;
using System.IO;


public class Customer
{
  public int Id { get; set; }
  public string Context { get; set; }
  public string Command { get; set; }
  public string Icon { get; set; }
  public int Type { get; set; }
}

public class LiteData
{
  public static string HyprFlyDB = Globals.HyprFlyDir + "/HyprFly.db";
  public static void TestDataBB()
  {
    using (var db = new LiteDatabase(HyprFlyDB))
    {
      // Get customer collection
      var col = db.GetCollection<Customer>("customers");

      var customers = col.Find(Query.And(Query.EQ("Type", 1), Query.EQ("Context", "prueba")));

      // Mostrar el contenido de cada documento
      foreach (var customer in customers)
      {
        GD.Print($"ID: {customer.Id}");
        GD.Print($"Command: {customer.Command}");
        GD.Print($"Icon: {customer.Icon}");
        GD.Print($"Type: {customer.Type}");
        GD.Print($"Context: {customer.Context}");
      }
    }
  }
  public static void RestoreBackup()
  {
    OS.Execute("mkdir", new string[] { "-p", Globals.HyprFlyDir });
    string filePath = Globals.HyprFlyDir + "/BackupDB.csv";

    // Lista para almacenar los datos
    List<string[]> rows = new List<string[]>();

    // Leer el archivo CSV
    using (StreamReader sr = new StreamReader(filePath))
    {
      string line;
      while ((line = sr.ReadLine()) != null)
      {
        // Separar la línea en columnas usando ',' como delimitador
        string[] columns = line.Split(';');
        rows.Add(columns);
      }
    }
    // Mostrar los datos leídos
    foreach (var row in rows)
    {
      var customer = new Customer
      {
        Command = row[0],
        Icon = row[1],
        Context = row[2],
        Type = row[3].ToInt(),
      };
      using (var db = new LiteDatabase(HyprFlyDB))
      {
        var col = db.GetCollection<Customer>("customers");
        col.Insert(customer);
      }
    }
    GD.Print("Backup restored");
  }

  public static Customer[] GetItems(string context)
  {
    using (var db = new LiteDatabase(HyprFlyDB))
    {
      List<Customer> ListCustumers = new List<Customer>();
      var col = db.GetCollection<Customer>("customers");

      var customers = col.Find(Query.Or(Query.And(Query.EQ("Type", 1), Query.EQ("Context", context)),
                                        Query.And(Query.EQ("Type", 2), Query.EQ("Context", context))));
      foreach (var customer in customers)
      {
        ListCustumers.Add(customer);
      }
      return ListCustumers.ToArray();
    }
  }

  public static string GetIconCenter(string context)
  {
    using (var db = new LiteDatabase(HyprFlyDB))
    {
      var col = db.GetCollection<Customer>("customers");

      var customer = col.FindOne(Query.And(Query.EQ("Type", 3), Query.EQ("Context", context)));

      return customer.Icon;
    }
  }
}
