using Dapper;
using DatabaseAccessSem1;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamenSemEt.DatabaseAccess.Repository
{
    public class CertificationRepository
    {
        private readonly IDbConnectionFactory _dbFactory;
        public CertificationRepository(IDbConnectionFactory dbFactory) { _dbFactory = dbFactory; }


        public Certificate CreateGroup(int InstructorID, int CertificationID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"INSERT INTO CertificationGroups 
                        (InstructorID, CertificationID) Values 
                        (@InstructorID, @CertificationID) RETURNING *;";
            return connection.QuerySingle<Certificate>(sql, new { InstructorID = InstructorID, CertificationID = CertificationID });
        }

        public int RemoveGroup(int InstructorID, int CertificationID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"DELETE FROM CertificationGroups 
                        WHERE InstructorID = @InstructorID AND CertificationID = @CertificationID;";
            return connection.Execute(sql, new { InstructorID = InstructorID, CertificationID = CertificationID });
        }

        public IEnumerable<Certificate> GetAll()
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"SELECT * FROM Certifications;";


            return connection.Query<Certificate>(sql);
        }

        public IEnumerable<Certificate> GetByInstructorID(int instructorID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"SELECT c.*
                        FROM Certifications c
                        JOIN CertificationGroups cg ON c.CertificationID = cg.CertificationID
                        WHERE cg.InstructorID = @InstructorID;";

            return connection.Query<Certificate>(sql, new { InstructorID = instructorID });
        }
    }
}
