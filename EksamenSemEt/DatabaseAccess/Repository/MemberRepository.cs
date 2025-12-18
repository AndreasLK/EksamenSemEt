using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccessSem1.Repository
{
    public class MemberRepository
    {
        private readonly IDbConnectionFactory _dbFactory;
        public MemberRepository(IDbConnectionFactory dbFactory) {_dbFactory = dbFactory;}



        public Member Create(Member member) 
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"INSERT INTO Customers 
                        (FirstName, LastName, DateOfBirth, Email, PhoneNumber, MemberType, Active) Values 
                        (@FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber, @MemberType, @Active) RETURNING *;";

            return connection.QuerySingle<Member>(sql, member);
        }

        public IEnumerable<int> GetID(
    string? firstName = null,
    string? lastName = null,
    DateTime? dateOfBirth = null,
    string? email = null,
    string? phoneNumber = null,
    int? memberType = null,
    bool? active = null)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
                                                                  // 1. Start with a basic query that selects ALL columns so the Member object can be filled
            var sqlBuilder = new StringBuilder("SELECT MemberID FROM Customers WHERE 1=1");

            // 2. Create a container for your safe parameters
            var parameters = new DynamicParameters();

            // 3. Add filters only if they are provided
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

            if (dateOfBirth.HasValue)
            {
                sqlBuilder.Append(" AND DateOfBirth = @DateOfBirth");
                parameters.Add("DateOfBirth", dateOfBirth);
            }

            if (!string.IsNullOrEmpty(email))
            {
                sqlBuilder.Append(" AND Email LIKE @Email");
                parameters.Add("Email", $"%{email}%");
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                sqlBuilder.Append(" AND PhoneNumber LIKE @PhoneNumber");
                parameters.Add("PhoneNumber", phoneNumber + "%");
            }

            if (memberType.HasValue)
            {
                sqlBuilder.Append(" AND MemberType = @MemberType");
                parameters.Add("MemberType", memberType);
            }

            if (active.HasValue)
            {
                sqlBuilder.Append(" AND Active = @Active");
                parameters.Add("Active", active);
            }


            return connection.Query<int>(sqlBuilder.ToString(), parameters); // Selve forespørgsel til database
        }
        public IEnumerable<Member> broadSearch(string searchString, int?sesionID = null, bool? excludeInactive = false,  int limit = 100, int offset = 0)
        {
            using var connection = _dbFactory.CreateConnection();

            var sqlBuilder = new StringBuilder("SELECT * FROM Customers WHERE 1=1");
            var parameters = new DynamicParameters();

            if (excludeInactive.HasValue && excludeInactive.Value == true) sqlBuilder.Append(" AND Active = 1"); 
            if (sesionID.HasValue) {
                sqlBuilder.Append(" AND MemberID IN (SELECT MemberID FROM MemberGroups WHERE SessionID = @SessionID)");
                parameters.Add("SessionID", sesionID.Value);
            }


            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var searchTerms = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < searchTerms.Length; i++) //Kører gennem alle searchTerms
                {
                    string _paramName = $"@term{i}";
                    parameters.Add(_paramName, $"%{searchTerms[i]}%");


                    sqlBuilder.Append($@" AND (
                    FirstName LIKE {_paramName}
                OR LastName LIKE {_paramName}
                OR CAST(PhoneNumber AS TEXT) LIKE {_paramName}
                OR CAST(MemberID AS TEXT) LIKE {_paramName}
                )");
                }
            }






            sqlBuilder.Append(" ORDER BY MemberID");
            sqlBuilder.Append(" LIMIT @Limit OFFSET @Offset");
            parameters.Add("Limit", limit);
            parameters.Add("Offset", offset);

            return connection.Query<Member>(sqlBuilder.ToString(), parameters);
        }

        public IEnumerable<Member> GetAll()
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = "SELECT * FROM Customers";
            return connection.Query<Member>(sql); // Selve forespørgsel til database
        }

        public Member GetByID(int memberID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = "SELECT * FROM Customers WHERE MemberID = @MemberID";

            return connection.QuerySingle<Member>(sql, new { MemberID = memberID });
        }

        public int Update(Member member)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @" UPDATE Customers
                        SET
                            FirstName = @FirstName,
                            LastName = @LastName,
                            DateOfBirth = @DateOfBirth,
                            Email = @Email,
                            PhoneNumber = @PhoneNumber,
                            MemberType = @MemberType,
                            Active = @Active
                        WHERE MemberID = @MemberID";
            return connection.Execute(sql, member); //Returnere mængden af rækker opdateret (forhåbeligt 1)
        }

        public int Delete(int memberID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"DELETE FROM Customers
                        WHERE MemberID = @MemberID";

            return connection.Execute(sql, new { MemberID = memberID });
        }



		//Metode til tælle hvor mange hold et medlem har tilmeldt sig i den nuværrende uge. - Med hjælp fra chatten.
		public int GetWeeklySessionCount(int memberId)
		{
            using var connection = _dbFactory.CreateConnection();

            DateTime today = DateTime.Now.Date;

            int dayOfWeek = (int)today.DayOfWeek;
            if (dayOfWeek == 0) dayOfWeek = 7;

            DateTime startOfWeek = today.AddDays(1 - dayOfWeek);

            DateTime endOfWeek = startOfWeek.AddDays(7);

            string sql = @"
        SELECT COUNT(*) 
        FROM MemberGroups mg
        INNER JOIN Sessions s ON mg.SessionID = s.SessionID
        WHERE mg.MemberID = @MemberID
        AND s.DateTime >= @StartOfWeek
        AND s.DateTime < @EndOfWeek";

            return connection.ExecuteScalar<int>(sql, new
            {
                MemberID = memberId,
                StartOfWeek = startOfWeek,
                EndOfWeek = endOfWeek
            });

        }

		public bool IsActive(int memberId)
		{
			using var connection = _dbFactory.CreateConnection();

			string sql = @"SELECT Active 
                   FROM Customers 
                   WHERE MemberID = @MemberID";

			return connection.ExecuteScalar<bool>(sql, new { MemberID = memberId });
		}
	}


}
