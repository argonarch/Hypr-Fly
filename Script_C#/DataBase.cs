using Godot;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System;
public partial class DataBase : Node2D
{
  public string GetDate(string solicitud)
  {
    string resultado;

    using var connection = new SqliteConnection("Data Source=DataStore/database.db");
    connection.Open();

    var command = connection.CreateCommand();
    command.CommandText = solicitud;
    using var reader = command.ExecuteReader();

    while (reader.Read())
    {
      resultado = reader.GetString(0);
      return resultado;
    }

    return "Failed Query";
  }
  public string[] GetDates(string solicitud, string column)
  {
    List<string> resultados = new List<string>();

    using var connection = new SqliteConnection("Data Source=DataStore/database.db");
    connection.Open();

    var command = connection.CreateCommand();
    command.CommandText = solicitud;
    using var reader = command.ExecuteReader();

    while (reader.Read())
    {
      resultados.Add(Convert.ToString(reader[column]));
    }
    return resultados.ToArray();
  }
}

