using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccessSem1.Repository
{
    public class MemberGroupRepository
    {
        private readonly IDbConnectionFactory _dbFactory;
        public MemberGroupRepository(IDbConnectionFactory dbFactory) { _dbFactory = dbFactory; }



        public MemberGroup Create(MemberGroup memberGroup)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"INSERT INTO MemberGroups (MemberID, SessionID) 
                        OUTPUT INSERTED.* Values (@MemberID, @SessionID);";

            return connection.QuerySingle<MemberGroup>(sql, memberGroup);
        }

        public IEnumerable<Session>GetSessions(int memberID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"SELECT s.* FROM Sessions s
                           INNER JOIN MemberGroups mg ON s.SessionID = mg.SessionID
                           WHERE mg.MemberID = @MemberID";

            return connection.Query<Session>(sql, new { MemberID = memberID });

        }

        public IEnumerable<Member> GetMembers(int sessionID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"SELECT c.* FROM Customers c
                           INNER JOIN MemberGroups mg ON c.MemberID = mg.MemberID
                           WHERE mg.SessionID = @SessionID";

            return connection.Query<Member>(sql, new { SessionID = sessionID });

        }

        public IEnumerable<int> GetMembersIDs(int sessionID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"SELECT MemberID FROM MemberGroups
                        WHERE SessionID = @SessionID";

            return connection.Query<int>(sql, new { SessionID = sessionID });

        }
        public int Update(MemberGroup memberGroup)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @" UPDATE MemberGroups
                        SET
                            MemberID = @MemberID,
                            SessionID = @SessionID
                        WHERE GroupingID = @GroupingID";

            return connection.Execute(sql, memberGroup); //Returnere mængden af rækker opdateret (forhåbeligt 1)
        }

        public int Delete(int groupingID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"DELETE FROM MemberGroups
                        WHERE GroupingID = @GroupingID";

            return connection.Execute(sql, new { GroupingID = groupingID }); //Returnere mængden af rækker opdateret (forhåbeligt 1)
        }

        public int DeleteGroup(int memberID, int sessionID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"DELETE FROM MemberGroups 
                        WHERE MemberID = @MemberID AND SessionID = @SessionID;";
            return connection.Execute(sql, new { MemberID = memberID, SessionID = sessionID});
        }

        public bool IsMemberAlreadyBooked(int memberID, int sessionID)
        {
            using var connection = _dbFactory.CreateConnection();
            string sql = @"SELECT COUNT(1) FROM MemberGroups 
                    WHERE MemberID = @MemberID AND SessionID = @SessionID";

            return connection.ExecuteScalar<int>(sql, new { MemberID = memberID, SessionID = sessionID }) > 0;
        }

        // ændret af sandra - Gemini - metode til at få information fra databasen om hvilke hold er mest populære.  
        public IEnumerable<SessionPopularityData> GetSessionPopularity() // IEnumerable = returtype.
        {
            using var connection = _dbFactory.CreateConnection();
            string sql = @"
        SELECT 
            C.Name AS SessionType,
            COUNT(MG.MemberID) AS ParticipantCount,
            ROUND(
                (COUNT(MG.MemberID) * 100.0) / 
                NULLIF((SELECT SUM(s2.MaxMembers) 
                        FROM Sessions s2 
                        WHERE s2.SessionType = S.SessionType), 0), 1) AS ParticipantPercentage
        FROM MemberGroups MG
        INNER JOIN Sessions S ON MG.SessionID = S.SessionID
        INNER JOIN Certifications C ON S.SessionType = C.CertificationID
        GROUP BY C.Name, S.SessionType
        ORDER BY ParticipantPercentage DESC"; //s2 istedet for S for at kende forskel på specifik Session frem for alle
            return connection.Query<SessionPopularityData>(sql);
        }


        // Metode til at finde de mest travle dage på ugen - ændret af sandra - ved hjælp fra gemini.
        public IEnumerable<SessionDayData> GetBusiestDayOfWeek()
        {
            using var connection = _dbFactory.CreateConnection();

            // SQL-forespørgslen bruger SQLite-funktionen strftime('%w') til at finde ugedagen (0=Søndag, 1=Mandag...)
            string sql = @"
        SET DATEFIRST 1; -- Sets Monday as the first day of the week
        SELECT 
            CASE DATEPART(dw, S.DateTime)
                WHEN 1 THEN 'Mandag'
                WHEN 2 THEN 'Tirsdag'
                WHEN 3 THEN 'Onsdag'
                WHEN 4 THEN 'Torsdag'
                WHEN 5 THEN 'Fredag'
                WHEN 6 THEN 'Lørdag'
                ELSE 'Søndag'
            END AS DayOfWeek,
            S.SessionType, 
            COUNT(MG.MemberID) AS ParticipantCount
        FROM MemberGroups MG
        INNER JOIN Sessions S ON MG.SessionID = S.SessionID
        GROUP BY S.SessionType, DATEPART(dw, S.DateTime)
        ORDER BY ParticipantCount DESC";

            return connection.Query<SessionDayData>(sql);
        } // CASE...END: dette oversætter det nummerede resultat (0-6) til læselige danske ugedage.
          // GROUP BY S.SessionType, DayOfWeek: sikrer at deltagerne tælles separat for hvert hold på hver ugedag.
    }
}
