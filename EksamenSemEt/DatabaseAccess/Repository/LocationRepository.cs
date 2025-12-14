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

        public int Delete(int locationID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"DELETE FROM Locations 
                        WHERE LocationID = @LocationID;";
            return connection.Execute(sql, new { LocationID = locationID});
        }
    }
}
