using Dapper;
using EksamenSemEt.DatabaseAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccessSem1.Repository
{
    public class InstructorRepository
    {
        private readonly IDbConnectionFactory _dbFactory;
        public InstructorRepository(IDbConnectionFactory dbFactory) { _dbFactory = dbFactory; }

        public Instructor Create(Instructor instructor)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"INSERT INTO Instructors (FirstName, LastName) 
                           OUTPUT INSERTED.* VALUES (@FirstName, @LastName);";

            return connection.QuerySingle<Instructor>(sql, instructor);
        }

        public IEnumerable<int> GetID(
    string? firstName = null,
    string? lastName = null)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            var sqlBuilder = new StringBuilder("SELECT InstructorID FROM Instructors WHERE 1=1");

            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(firstName))
            {
                sqlBuilder.Append(" AND FirstName LIKE @FirstName");
                parameters.Add("FirstName", $"{firstName}%");
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                sqlBuilder.Append(" AND LastName LIKE @LastName");
                parameters.Add("LastName", $"%{lastName}%");
            }

            return connection.Query<int>(sqlBuilder.ToString(), parameters); // Selve forespørgsel til database
        }

        public IEnumerable<Instructor> broadSearch(string searchString, int? certifcationID = null, int? sessionID = null, int limit = 100)
        {
            using var connection = _dbFactory.CreateConnection();


            //Alt i denne funktion efter denne linje er black magic fuckery.
            //Proceed with caution
            var sqlBuilder = new StringBuilder("SELECT DISTINCT TOP (@Limit) i.* FROM Instructors i");
            var parameters = new DynamicParameters();
            parameters.Add("Limit", limit);

            bool validcert = certifcationID.HasValue && certifcationID.Value > 0;
            if (validcert)
            {
                sqlBuilder.Append(" JOIN CertificationGroups cg ON i.InstructorID = cg.InstructorID");
            }

            bool validSession = sessionID.HasValue && sessionID.Value > 0;
            if (validSession)
            {
                sqlBuilder.Append(" JOIN InstructorGroups ig ON i.InstructorID = ig.InstructorID");
            }

            sqlBuilder.Append(" WHERE 1=1");

            if (validcert)
            {
                sqlBuilder.Append(" AND cg.CertificationID = @CertificationID");
                parameters.Add("CertificationID", certifcationID);
            }

            if (validSession)
            {
                sqlBuilder.Append(" AND ig.SessionID = @SessionID");
                parameters.Add("SessionID", sessionID);
            }



            if (!string.IsNullOrWhiteSpace(searchString)){
                var searchTerms = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < searchTerms.Length; i++) //Kører gennem alle searchTerms
                {
                    string _paramName = $"@term{i}";
                    parameters.Add(_paramName, $"%{searchTerms[i]}%");


                    sqlBuilder.Append($@" AND (
                    FirstName LIKE {_paramName}
                OR LastName LIKE {_paramName}
                OR CAST(i.InstructorID AS NVARCHAR(50)) LIKE {_paramName}
                )");
                }
            }

            return connection.Query<Instructor>(sqlBuilder.ToString(), parameters);
        }

        public IEnumerable<Instructor> GetAll()
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = "SELECT * FROM Instructors";
            return connection.Query<Instructor>(sql); // Selve forespørgsel til database
        }
        
        public Instructor GetByID(int InstructorID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = "SELECT * FROM Instructors WHERE InstructorID = @InstructorID";

            return connection.QuerySingle<Instructor>(sql, new { InstructorID = InstructorID });
        }

        public int Update(Instructor instructor)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @" UPDATE Instructors
                        SET
                            FirstName = @FirstName,
                            LastName = @LastName
                        WHERE InstructorID = @InstructorID";
            return connection.Execute(sql, instructor); //Returnere mængden af rækker opdateret (forhåbeligt 1)
        }

        public int Delete(int instructorID, CertificationRepository certRepo)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            //Delete certs
            string sqlCerts = @"DELETE FROM CertificationGroups
                        WHERE InstructorID = @InstructorID";
            connection.Execute(sqlCerts, new { InstructorID = instructorID });

            //Delete instructor
            string sql = @"DELETE FROM Instructors
                        WHERE InstructorID = @InstructorID";

            return connection.Execute(sql, new { InstructorID = instructorID });
        }

    }
}
