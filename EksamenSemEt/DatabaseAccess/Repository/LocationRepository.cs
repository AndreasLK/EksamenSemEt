using Dapper;
using DatabaseAccessSem1;
using DatabaseAccessSem1.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamenSemEt.DatabaseAccess.Repository
{
    public class LocationRepository
    {
        private readonly IDbConnectionFactory _dbFactory;
        public LocationRepository(IDbConnectionFactory dbFactory) { _dbFactory = dbFactory; }




        public Location Create(Location location)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"INSERT INTO Locations 
                        (Name) Values 
                        (@Name) RETURNING *;";
            return connection.QuerySingle<Location>(sql, location);
        }

        public IEnumerable<Location> GetAll()
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"SELECT * FROM Locations;";
            return connection.Query<Location>(sql);
        }

        public Location Update(Location location)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"UPDATE Locations SET 
                        Name = @Name
                        WHERE LocationID = @LocationID RETURNING *;";
            return connection.QuerySingle<Location>(sql, location);
        }

        public int Delete(int locationID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"DELETE FROM Locations 
                        WHERE LocationID = @LocationID;";
            return connection.Execute(sql, new { LocationID = locationID});
        }

        public IEnumerable<Location> BroadSearch(string searchString)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            if (string.IsNullOrWhiteSpace(searchString)) return GetAll();

            var searchTerms = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var sqlBuilder = new StringBuilder("SELECT * FROM Locations WHERE 1=1");
            var parameters = new DynamicParameters();

            for (int i = 0; i < searchTerms.Length; i++)
            {
                var paramName = $"@term{i}";
                sqlBuilder.Append(
                                    $@" AND (
                                    CAST(LocationID AS Text) LIKE {paramName}
                                    OR Name LIKE {paramName}
                                    )");
                parameters.Add(paramName, $"%{searchTerms[i]}%");
            }

            return connection.Query<Location>(sqlBuilder.ToString(), parameters);
        }
    }
}
