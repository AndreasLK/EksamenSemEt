using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace DatabaseAccessSem1.Repository
{
    public class SessionRepository
    {
        private readonly IDbConnectionFactory _dbFactory;
        public SessionRepository(IDbConnectionFactory dbFactory) { _dbFactory = dbFactory; }



        public Session Create(Session session)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"INSERT INTO Sessions 
                        (SessionType, DateTime, SessionDuration, MaxMembers, LocationID) Values 
                        (@SessionType, @DateTime, @SessionDuration, @MaxMembers, @LocationID) RETURNING *;";

            return connection.QuerySingle<Session>(sql, session);
        }

        public IEnumerable<Session> Search(
            int? sessionType = null,
            DateTime? dateTimeStart = null,
            DateTime? dateTimeEnd = null,
            int? sessionDurationStart = null,
            int? sessionDurationEnd = null,
            int? maxMembers = null,
            int? minMembers = null,
            int? locationID = null,
            int? minSlots = null)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
                                                                  // 1. Start with a basic query that selects ALL columns so the Member object can be filled
            var sqlBuilder = new StringBuilder(@"SELECT s.*, 
                                                (s.MaxMembers - (SELECT COUNT(*) FROM MemberGroups mg WHERE mg.SessionID = s.SessionID)) 
                                                AS SlotsAvailable
                                                FROM Sessions s 
                                                WHERE 1=1"); //Søger efter alle linjer hvor 1=1 (som er alle) og tilføjer senere mere præcise instruktioner

            // 2. Create a container for your safe parameters
            var parameters = new DynamicParameters();

            if (sessionType.HasValue)
            {
                sqlBuilder.Append(" AND SessionType = @SessionType");
                parameters.Add("SessionType", sessionType);
            }
            if (dateTimeStart.HasValue)
            {
                sqlBuilder.Append(" AND DateTime >= @DateTimeStart");
                parameters.Add("DateTimeStart", dateTimeStart);
            }
            if (dateTimeEnd.HasValue)
            {
                sqlBuilder.Append(" AND DateTime <= @DateTimeEnd");
                parameters.Add("DateTimeEnd", dateTimeEnd);
            }
            if (sessionDurationStart.HasValue)
            {
                sqlBuilder.Append(" AND SessionDuration >= @SessionDurationStart");
                parameters.Add("SessionDurationStart", sessionDurationStart);
            }
            if (sessionDurationEnd.HasValue)
            {
                sqlBuilder.Append(" AND SessionDuration < @SessionDurationEnd");
                parameters.Add("SessionDurationEnd", sessionDurationEnd);
            }
            if (maxMembers.HasValue)
            {
                sqlBuilder.Append(" AND MaxMembers <= @MaxMembers");
                parameters.Add("MaxMembers", maxMembers);
            }
            if (minMembers.HasValue)
            {
                sqlBuilder.Append(" AND MaxMembers >= @MinMembers");
                parameters.Add("MinMembers", minMembers);
            }
            if (locationID.HasValue)
            {
                sqlBuilder.Append(" AND LocationID = @LocationID");
                parameters.Add("LocationID", locationID);
            }
            if (minSlots.HasValue)
            {
                sqlBuilder.Append(@" AND (s.MaxMembers - (SELECT COUNT(*) FROM MemberGroups mg WHERE mg.SessionID = s.SessionID)) >= @MinSlots");
                parameters.Add("MinSlots", minSlots);
            }

            return connection.Query<Session>(sqlBuilder.ToString(), parameters); // Selve forespørgsel til database

        }

        public void RemoveAllByType(int sessionType)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"DELETE FROM Sessions
                        WHERE SessionType = @SessionType";
            connection.Execute(sql, new { SessionType = sessionType });
        }

        public IEnumerable<Session> GetAll()
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = "SELECT * FROM Sessions";
            return connection.Query<Session>(sql); // Selve forespørgsel til database
        }

        public Session GetByID(int sessionID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = "SELECT * FROM Sessions WHERE SessionID = @SessionID;";

            return connection.QuerySingle<Session>(sql, new { SessionID =  sessionID});
        }

        public int GetSlotsAvailable(int sessionID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"
                        SELECT (
                            s.MaxMembers - (SELECT COUNT(*) FROM MemberGroups 
                            WHERE SessionID = s.SessionID)
                            ) 
                        FROM Sessions s
                        WHERE s.SessionID = @SessionID";

            return connection.ExecuteScalar<int>(sql, new { SessionID = sessionID});
        }

        public int GetMemberCount(int sessionID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"
                        SELECT COUNT(*) 
                        FROM MemberGroups 
                        WHERE SessionID = @SessionID";
            return connection.ExecuteScalar<int>(sql, new { SessionID = sessionID });
        }

        public IEnumerable<Session> GetSessionsByMember(int memberID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"SELECT   s.*,
                                    c.Name as SessionTypeName,
                                    l.Name as LocationName
                            FROM Sessions s
                            JOIN MemberGroups mg ON s.SessionID = mg.SessionID
                            LEFT JOIN Certifications c ON s.SessionType = c.CertificationID
                            LEFT JOIN Locations l ON s.LocationID = l.LocationID
                            
                            WHERE mg.MemberID = @MemberID;";

            return connection.Query<Session>(sql, new { MemberID = memberID });
        }

        public IEnumerable<Session> GetSessionsByInstructor(int instructorID)
        {
            using var connection = _dbFactory.CreateConnection();

            var sql = @"
                SELECT s.*, 
                       c.Name as SessionTypeName,
                       l.Name as LocationName
                FROM Sessions s
                JOIN InstructorGroups ig ON s.SessionID = ig.SessionID
                LEFT JOIN Certifications c ON s.SessionType = c.CertificationID
                LEFT JOIN Locations l ON s.LocationID = l.LocationID

                WHERE ig.InstructorID = @InstructorID;";

            return connection.Query<Session>(sql, new { InstructorID = instructorID });
        }


        public int Update(Session session)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @" UPDATE Sessions
                        SET
                            SessionType = @SessionType,
                            DateTime = @DateTime,
                            SessionDuration = @SessionDuration,
                            MaxMembers = @MaxMembers,
                            LocationID = @LocationID
                        WHERE SessionID = @SessionID";
            return connection.Execute(sql, session); //Returnere mængden af rækker opdateret (forhåbeligt 1)
        }

        public int Delete(int sessionID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"DELETE FROM Sessions
                        WHERE SessionID = @SessionID";

            return connection.Execute(sql, new { SessionID = sessionID });
        }

    }
}
