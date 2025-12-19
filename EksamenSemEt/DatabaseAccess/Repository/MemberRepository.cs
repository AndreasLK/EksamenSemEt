using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccessSem1.Repository
{
    public class MemberRepository //Lavet af Andreas, Sandra, Jesper
    {
        private readonly IDbConnectionFactory _dbFactory;
        public MemberRepository(IDbConnectionFactory dbFactory) {_dbFactory = dbFactory;}



        public Member Create(Member member) 
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"INSERT INTO Customers (FirstName, LastName, DateOfBirth, Email, PhoneNumber, MemberType, Active) 
                    OUTPUT INSERTED.* Values (@FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber, @MemberType, @Active);";

            return connection.QuerySingle<Member>(sql, member);
        }

        public IEnumerable<Member> broadSearch(string searchString, int? sesionID = null, bool? excludeInactive = false, int limit = 100, int offset = 0)
        {
            using var connection = _dbFactory.CreateConnection(); //opret forbindelse. Forbindelse afsluttes når metoden er færdig med at køre pga. "Using"

            var sqlBuilder = new StringBuilder("SELECT * FROM Customers WHERE 1=1"); //Base case til søgning (finder alle medlemmer)
            var parameters = new DynamicParameters();

            if (excludeInactive.HasValue && excludeInactive.Value == true) sqlBuilder.Append(" AND Active = 1"); //Skal inaktive medlemmer udelukkes

            if (sesionID.HasValue)
            { //Søges der angående et specifikt hold
                sqlBuilder.Append(" AND MemberID IN (SELECT MemberID FROM MemberGroups WHERE SessionID = @SessionID)");
                parameters.Add("SessionID", sesionID.Value);
            }


            if (!string.IsNullOrWhiteSpace(searchString)) //Hvis der søges på andet end bare mellemrum
            {
                var searchTerms = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries); //Del string op ved mellemrum, giver individuelle søgestrings

                for (int i = 0; i < searchTerms.Length; i++) //Kører gennem alle searchTerms
                {
                    string _paramName = $"@term{i}";
                    parameters.Add(_paramName, $"%{searchTerms[i]}%"); //Tilføj søge betingelser som Parameter


                    sqlBuilder.Append($@" AND ( 
                    FirstName LIKE {_paramName}
                OR LastName LIKE {_paramName} 
                OR PhoneNumber LIKE {_paramName}
                OR CAST(MemberID AS NVARCHAR(50)) LIKE {_paramName}
                )"); // Søger efter om aktuelle søge string (fx "Søren" er enten Fornavn, efternavn, tlf nr, eller medlemsid)
                }
            }

            sqlBuilder.Append(" ORDER BY MemberID"); //Skal altid sortere efter noget når der kan hentes data i bider
            sqlBuilder.Append(" OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY"); //basically hent de første x linjer efter linje y (typisk 0)
            parameters.Add("Limit", limit);
            parameters.Add("Offset", offset);

            return connection.Query<Member>(sqlBuilder.ToString(), parameters); //Selve forespørgsel
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
                        WHERE MemberID = @MemberID"; //Opdater kunde med kundeID til at have de nye data fra indsatte objekt
            return connection.Execute(sql, member); //Returnere mængden af rækker opdateret (forhåbeligt 1)
        }

        public int Delete(int memberID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"DELETE FROM Customers
                        WHERE MemberID = @MemberID"; // Sletter alle medlemmer med det givende ID

            return connection.Execute(sql, new { MemberID = memberID }); //Returnere mængden af rækker der er ændret ( Forhåbeligt 1)
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
                                                                  
            var sqlBuilder = new StringBuilder("SELECT MemberID FROM Customers WHERE 1=1");

            var parameters = new DynamicParameters();

            //Tilføj filtrer hvis de har en værdi
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

        





		//Metode til tælle hvor mange hold et medlem har tilmeldt sig i den nuværrende uge. - Med hjælp fra chatten.
		public int GetWeeklySessionCount(int memberId, DateTime targetDate)
		{
            using var connection = _dbFactory.CreateConnection();

            DateTime baseDate = targetDate.Date;

            int dayOfWeek = (int)baseDate.DayOfWeek;
            if (dayOfWeek == 0) dayOfWeek = 7;

            DateTime startOfWeek = baseDate.AddDays(1 - dayOfWeek);

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

        public int GetActiveMemberCount()
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"SELECT COUNT(*) FROM Customers WHERE Active = @IsActive";
            return connection.QuerySingle<int>(sql, new { IsActive = true });
        }
    }


}
