using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DatabaseAccessSem1
{

    //Basically en klasse der ikke (nemt) kan ændres.

    //Navne på variabler skal matche databasen og følger derfor ikke styleguiden

    public record Member
    {
        public int? MemberID { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public DateTime? DateOfBirth { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public required int MemberType { get; init; }
        public required bool Active { get; init; }
    }

    public record Instructor
    {
        public int? InstructorID { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }

        public string FullName => $"{FirstName} {LastName}";
    }

    public record Session
    {
        public int? SessionID { get; init; }
        public required int SessionType { get; init; }
        public required DateTime DateTime { get; init; }
        public int? SessionDuration { get; init; }
        public required int MaxMembers { get; init; }
        public int? LocationID { get; init; }
        public int? SlotsAvailable { get; init; }

        public string? LocationName { get; init; }
        public string? SessionTypeName { get; init; }
    }

    public record MemberGroup
    {
        public int? GroupingID { get; init; }
        public required int MemberID { get; init; }
        public required int SessionID { get; init; }
    }

    public record InstructorGroup
    {
        public int? GroupingID { get; init; }
        public required int InstructorID { get; init; }
        public required int SessionID { get; init; }
    }

    public record MemberTypeOption
    {
        public int? MemberTypeID { get; init; }
        public required string Name { get; init; }
    }

    public record Certificate
    {
        public int? CertificationID { get; init; }
        public required string Name { get; init; }
    }

    public record Location
    {
        public int? LocationID { get; init; }
        public required string Name { get; init; }
    }

    public record MemberViewModel
    {
        public int? MemberID { get; set; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public required int MemberType { get; set; }

        public required bool Active { get; set; }
        public required bool IsBookedOnSession { get; set; }
    }

    public record SessionViewModel
    {
        [Browsable(false)] //SesssionID bliver ikke vist som kolonne
        public int SessionID { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        [Browsable(false)]
        public int MaxMembers { get; set; }
        [Browsable(false)]
        public int BookedCount { get; set; }
        public required string Availability { get; set; }
    }

    public record BookingViewModel
    {
        public int SessionID { get; set; }
        public int? MemberID { get; set; }

        public string MemberFirstName { get; set; } = string.Empty;
        public string MemberLastName { get; set; } = string.Empty ;
        public string MemberPhone { get; set; } = string.Empty;

 
        public string SessionType { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Availability { get; set; } = string.Empty;

        [Browsable(false)]
        public List<Instructor> InstructorList { get; set; } = new();

        [Browsable(false)]
        public List<Member> BookedMembersList { get; set; } = new();
    }

}
