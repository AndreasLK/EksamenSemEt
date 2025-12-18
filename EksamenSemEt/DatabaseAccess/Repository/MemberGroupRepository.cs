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
    }
}
