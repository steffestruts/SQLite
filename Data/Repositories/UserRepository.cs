﻿using Data.Entities;
using Data.Interfaces;
using Microsoft.Data.Sqlite;

namespace Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString = "Data Source=database.db";

    public UserRepository()
    {
        CreateUsersTableIfNotExists();
    }

    public void CreateUsersTableIfNotExists()
    {
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string createUserTableQuery = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                FirstName TEXT NOT NULL,
                LastName TEXT NOT NULL,
                Email TEXT NOT NULL,
                PhoneNumber TEXT NULL
            )";

            using var command = new SqliteCommand(createUserTableQuery, connection);
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public bool Create(UserEntity userEntity)
    {
        try
        {
            string insertQuery = @"
                Insert into Users (FirstName, LastName, Email, PhoneNumber) 
                Values (@FirstName, @LastName, @Email, @PhoneNumber)
            ";

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = new SqliteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@FirstName", userEntity.FirstName);
            command.Parameters.AddWithValue("@LastName", userEntity.LastName);
            command.Parameters.AddWithValue("@Email", userEntity.Email);
            command.Parameters.AddWithValue("@PhoneNumber", userEntity.PhoneNumber);

            command.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public IEnumerable<UserEntity> GetAll()
    {
        try
        {
            var users = new List<UserEntity>();

            string selectQuery = @"SELECT * FROM Users";

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = new SqliteCommand(selectQuery, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read()) 
            {
                users.Add(new UserEntity
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    PhoneNumber = reader.GetString(4)
                });
            }

            return users;
        }
        catch (Exception ex)
        {
            return null!;
        }
    }
}
