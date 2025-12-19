using System;
using System.Collections.Generic;
using System.Text;
using DatabaseAccessSem1.Repository;
using EksamenSemEt.DatabaseAccess.Repository;

namespace DatabaseAccessSem1.Services
{
	public class BookingService
	{
		private readonly SessionRepository _sessionRepository;
		private readonly MemberGroupRepository _memberGroupRepository;
		private readonly MemberRepository _memberRepository;
		private readonly MemberTypeRepository _memberTypeRepository;


        public BookingService(SessionRepository sessionRepository, MemberGroupRepository memberGroupRepository, MemberRepository memberRepository, MemberTypeRepository memberTypeRepository)
		{
			_memberRepository = memberRepository;
			_sessionRepository = sessionRepository;
			_memberGroupRepository = memberGroupRepository;
			_memberTypeRepository = memberTypeRepository;

		}

		public bool TryBookSession(int memberID, int sessionID)
		{
			try
			{
				Member member = _memberRepository.GetByID(memberID); //Få fulde medlem fra Repo
				if (member == null) // Hvis der en fejl og member bliver null
				{
					MessageBox.Show("Medlem ikke fundet.", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}

				if (!member.Active) //Kontroller aktivitet før booking gennemføres
				{ //Giv fejl hvis inaktiv
					MessageBox.Show("Dit medlemskab er inaktivt.", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}

                var session = _sessionRepository.GetByID(sessionID);
                if (session == null) //Kontroller at hold der gerne vil bookes til også findes
                { //giv fejl hvis hold ikke findes
                    MessageBox.Show("Holdet blev ikke fundet.", "Fejl");
                    return false;
                }

				if (_memberGroupRepository.IsMemberAlreadyBooked(memberID, sessionID)) //Kontroller at medlem ikke allerede er på holdet
                { // Giv fejl hvis medlem er på hold
                    MessageBox.Show("Medlemmet er allerede tilmeldt dette hold.", "Allerede Booket", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                int slotsAvailable = _sessionRepository.GetSlotsAvailable(sessionID); //Kontroller om der er plads på holdet
                if (slotsAvailable <= 0) //giv fejl hvis der ikke er mere plads på holdet
                {
                    MessageBox.Show("Der er ikke flere pladser tilbage på holdet.", "Hold Fyldt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (member.MemberType == 1) // Hvis medlem er type 1 (basis) kontrolleres at medlem ikke er på mere end 2 hold om ugen
				{
					int currentWeeklyBookings = _memberRepository.GetWeeklySessionCount(memberID, session.DateTime); //Baseret på hold tid så man godt kan booke sin uge på forhånd
					if (currentWeeklyBookings >= 2)
					{ //giv fejl hvis basis medlem har for mange hold
                        MessageBox.Show("Basis-medlemmer kan kun booke 2 hold om ugen.", "Grænse nået");
                        return false;
                    }
				}

				var memberGroup = new MemberGroup //opret MemberGroup object til oprettelse af booking
				{
					MemberID = memberID,
					SessionID = sessionID
				};
				_memberGroupRepository.Create(memberGroup); //gem selve bookingen til databasen

				MessageBox.Show(
					$"Booking gennemført\n\nHold: {session.SessionType}\nTidspunkt: {session.DateTime:dd-MM-yyyy HH:mm}",
					"Succes",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);

				return true; //Session Booket successfuldt
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("UNIQUE constraint failed") || ex.Message.Contains("PRIMARY KEY"))
				{
					MessageBox.Show("Medlemmet er allerede tilmeldt dette hold.", "Dublet", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show($"Fejl ved booking: {ex.Message}", "System Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				return false; // Fejl under booking
			}
		}

	}
}