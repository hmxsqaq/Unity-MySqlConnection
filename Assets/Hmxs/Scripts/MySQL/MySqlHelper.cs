using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using UnityEngine;

namespace Hmxs.Scripts.MySQL
{
    public static class MySqlHelper
    {
        private static MySqlConnection _connection;
        private static MySqlCommand _command;
        private static MySqlDataReader _reader;

        public static MySqlConnection CreateConnection(string server, string database, string uid, string password)
        {
            var connectionString = "SERVER=" + server + ";" +
                                   "DATABASE=" + database + ";" +
                                   "UID=" + uid + ";" +
                                   "PASSWORD=" + password + ";";

            _connection = new MySqlConnection(connectionString);
            return _connection;
        }

        public static void CloseConnection() => _connection.Close();

        public static void ExecuteNonQuery(string query)
        {
            if (_command is { Connection: { State: ConnectionState.Open } })
                _command.Connection.Close();

            _command = new MySqlCommand(query, _connection);
            if (_command.Connection.State != ConnectionState.Open)
                _command.Connection.Open();
            _command.ExecuteNonQuery();
        }

        public static List<List<string>> ExecuteQueryList(string query)
        {
            if (_command is { Connection: { State: ConnectionState.Open } })
                _command.Connection.Close();

            _command = new MySqlCommand(query, _connection);
            if (_command.Connection.State != ConnectionState.Open)
                _command.Connection.Open();
            _reader = _command.ExecuteReader();
            var result = new List<List<string>>();
            while (_reader.Read())
            {
                var row = new List<string>();
                for (var i = 0; i < _reader.FieldCount; i++)
                    row.Add(_reader[i].ToString());
                result.Add(row);
            }
            return result;
        }

        public static DataSet ExecuteQueryData(string query)
        {
            if (_connection.State != ConnectionState.Open) return null;

            var data = new DataSet();
            var sqlDataAdapter = new MySqlDataAdapter(query, _connection);
            sqlDataAdapter.Fill(data);
            return data;
        }

        public static DataSet GetTableData(string table)
        {
            var data = new DataSet();
            if (_connection.State == ConnectionState.Open)
            {
                var command = $"SELECT * FROM {table}";
                var sqlDataAdapter = new MySqlDataAdapter(command, _connection);
                sqlDataAdapter.Fill(data, table);
            }
            return data;
        }

        public static List<string> GetTableStringList(string table)
        {
            var data = new DataSet();
            if (_connection.State == ConnectionState.Open)
            {
                var command = $"SELECT * FROM {table}";
                var sqlDataAdapter = new MySqlDataAdapter(command, _connection);
                sqlDataAdapter.Fill(data, table);
            }

            var dataTable = data.Tables[table];
            var results = new List<string>();
            foreach (DataRow row in dataTable.Rows)
            {
                var result = "";
                foreach (DataColumn column in dataTable.Columns)
                {
                    result += row[column] + " ";
                }
                results.Add(result);
            }
            return results;
        }
    }
}