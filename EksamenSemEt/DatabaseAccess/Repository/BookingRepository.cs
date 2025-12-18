using Dapper;
using DatabaseAccessSem1;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamenSemEt.DatabaseAccess.Repository
{
    public class BookingRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public BookingRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public IEnumerable<BookingViewModel> AdvancedSearch(
            string? memberSearch = null,
            string? instructorSearch = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? sessionTypeID = null,
            int? locationID = null,
            int? maxMembers = null,
            int? amountBooked = null,
            int? minCapacity = null,
            int? maxCapacity = null,
            int? minAvailable = null,
            int limit = 100,
            int offset = 0)
        {
            using var connection = _dbFactory.CreateConnection();

            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append(@"
                SELECT 
                    mg.MemberID,
                    c.FirstName AS MemberFirstName,
                    c.LastName AS MemberLastName,
                    c.PhoneNumber AS MemberPhone,
                    
                    s.SessionID,
                    s.SessionType,
                    s.DateTime AS Date,
                    s.SessionDuration,
                    s.MaxMembers,
                    l.Name AS Location,
                    cert.Name AS SessionTypeName,
                    (SELECT COUNT(*) FROM MemberGroups sub WHERE sub.SessionID = s.SessionID) as BookedCount

                FROM MemberGroups mg
                JOIN Customers c ON mg.MemberID = c.MemberID
                JOIN Sessions s ON mg.SessionID = s.SessionID
                LEFT JOIN Locations l ON s.LocationID = l.LocationID
                LEFT JOIN Certifications cert ON s.SessionType = cert.CertificationID
                
                WHERE 1=1
            ");

            var parameters = new DynamicParameters();
            if (!string.IsNullOrEmpty(memberSearch)) {
                var searchTerms = memberSearch.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                sqlBuilder.Append(" And (");

                for (int i = 0; i < searchTerms.Length; i++)
                {
                    if (i > 0) sqlBuilder.Append(" OR ");

                    string pName = $"@term{i}";
                    parameters.Add(pName, $"%{searchTerms[i]}%");

                    sqlBuilder.Append($@"
                           CAST(mg.MemberID AS NVARCHAR(50)) LIKE {pName}
                        OR c.FirstName LIKE {pName}
                        OR c.LastName LIKE {pName}
                        OR c.PhoneNumber LIKE {pName}
                        OR c.Email LIKE {pName}
                    ");
                }

                sqlBuilder.Append(")");

            }

            if (!string.IsNullOrEmpty(instructorSearch))
            {
                var searchTerms = instructorSearch.Split(' ', StringSplitOptions.RemoveEmptyEntries);

               //hvis instruktør findes 
                sqlBuilder.Append(@" AND EXISTS (
                    SELECT 1 FROM InstructorGroups ig
                    JOIN Instructors i ON ig.InstructorID = i.InstructorID
                    WHERE ig.SessionID = s.SessionID 
                    AND (");

                for (int i = 0; i < searchTerms.Length; i++)
                {
                    if (i > 0) sqlBuilder.Append(" OR ");
                    string pName = $"@iTerm{i}";
                    parameters.Add(pName, $"%{searchTerms[i]}%");

                    sqlBuilder.Append($" (i.FirstName LIKE {pName} OR i.LastName LIKE {pName}) ");
                }

                sqlBuilder.Append("))");
            }

            if (sessionTypeID.HasValue && sessionTypeID.Value > 0)
            {
                sqlBuilder.Append(" AND s.SessionType = @SessionTypeID");
                parameters.Add("SessionTypeID", sessionTypeID);
            }

            if (locationID.HasValue && locationID.Value > 0)
            {
                sqlBuilder.Append(" AND s.LocationID = @LocationID");
                parameters.Add("LocationID", locationID);
            }

            if (startDate.HasValue)
            {
                sqlBuilder.Append(" AND s.DateTime >= @StartDate");
                parameters.Add("StartDate", startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                sqlBuilder.Append(" AND s.DateTime <= @EndDate");
                parameters.Add("EndDate", endDate.Value.Date.AddDays(1));
            }

            if (minCapacity.HasValue && minCapacity.Value > 0) {
                sqlBuilder.Append(" AND s.MaxMembers >= @MinCap");
                parameters.Add("MinCap", minCapacity);
            }

            if (maxCapacity.HasValue && maxCapacity.Value > 0) {
                sqlBuilder.Append(" AND s.MaxMembers <= @MaxCap");
                parameters.Add("MaxCap", maxCapacity);
            }

            if (minAvailable.HasValue && minAvailable.Value > 0) {
                sqlBuilder.Append(@" AND (s.MaxMembers - (SELECT COUNT(*) FROM MemberGroups sub WHERE sub.SessionID = s.SessionID)) >= @MinAvail");
                parameters.Add("MinAvail", minAvailable);
            }



            sqlBuilder.Append(" ORDER BY s.DateTime DESC");
            sqlBuilder.Append(" OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY" );

            parameters.Add("Limit", limit);
            parameters.Add("Offset", offset);

            return connection.Query(sqlBuilder.ToString(), parameters).Select(row =>
            {
                DateTime dt = Convert.ToDateTime(row.Date);
                int duration = Convert.ToInt32(row.SessionDuration ?? 60);
                int booked = Convert.ToInt32(row.BookedCount);
                int max = Convert.ToInt32(row.MaxMembers);
                int sessType = Convert.ToInt32(row.SessionType);

                string startTime = dt.ToString("HH:mm");
                string durationStr = $"{duration} min";
                string availability = $"{booked} / {max}";

                string locationName = (string)row.Location ?? "Ukendt";
                string typeName = (string)row.SessionTypeName ?? "Ukendt";

                return new BookingViewModel
                {
                    SessionID = Convert.ToInt32(row.SessionID),
                    MemberID = Convert.ToInt32(row.MemberID),

                    MemberFirstName = (string)row.MemberFirstName,
                    MemberLastName = (string)row.MemberLastName,
                    MemberPhone = (string)row.MemberPhone,

                    SessionType= typeName,
                    Location = locationName,

                    Date = dt,
                    StartTime = startTime,
                    Duration = durationStr,
                    Availability = availability,

                    InstructorList = new List<Instructor>(),
                    BookedMembersList = new List<Member>()
                };
            });
        }
    }
}
