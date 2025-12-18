using System;
using System.Collections.Generic;
using System.Text;
using DatabaseAccessSem1.Repository;
using EksamenSemEt.DatabaseAccess.Repository;
using static System.Collections.Specialized.BitVector32;

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
				var member = _memberRepository.GetByID(memberID);
				if (member == null)
				{
					MessageBox.Show("Medlem ikke fundet.", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}

				if (!member.Active)
				{
					MessageBox.Show("Dit medlemskab er inaktivt.", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}

                var session = _sessionRepository.GetByID(sessionID);
                if (session == null)
                {
                    MessageBox.Show("Holdet blev ikke fundet.", "Fejl");
                    return false;
                }

				if (_memberGroupRepository.IsMemberAlreadyBooked(memberID, sessionID)){
                    MessageBox.Show("Medlemmet er allerede tilmeldt dette hold.", "Allerede Booket", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (member.MemberType == 0)
				{
					int currentWeeklyBookings = _memberRepository.GetWeeklySessionCount(memberID, session.DateTime);
					if (currentWeeklyBookings >= 2)
					{
                        MessageBox.Show("Basis-medlemmer kan kun booke 2 hold om ugen.", "Grænse nået");
                        return false;
                    }
				}


				int slotsAvailable = _sessionRepository.GetSlotsAvailable(sessionID);
				if (slotsAvailable <= 0)
				{
					MessageBox.Show("Der er ikke flere pladser tilbage på holdet.", "Hold Fyldt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}

				var memberGroup = new MemberGroup
				{
					MemberID = memberID,
					SessionID = sessionID
				};
				_memberGroupRepository.Create(memberGroup);

				MessageBox.Show(
					$"Booking gennemført\n\nHold: {session.SessionType}\nTidspunkt: {session.DateTime:dd-MM-yyyy HH:mm}",
					"Succes",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);

				return true;
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
				return false;
			}
		}

	}
}