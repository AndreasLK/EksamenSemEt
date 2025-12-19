using DatabaseAccessSem1.Repository;
using DatabaseAccessSem1;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;


//Ændret af sandra - ved hjælp af Gemini - oprettelse af selve rapporten
namespace EksamenSemEt.Services;

public class ReportingStatistics
{
    private readonly MemberRepository _memberRepository;
    private readonly MemberGroupRepository _memberGroupRepository;

    public ReportingStatistics(MemberRepository memberRepository, MemberGroupRepository memberGroupRepository)
    {
        _memberRepository = memberRepository;
        _memberGroupRepository = memberGroupRepository;
    }

    public void GenerateReport() // metode til at genere rapport med statistik. 
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fileName = $"FithubRapport-{timestamp}.txt";

        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string fullPath = Path.Combine(desktopPath, fileName);

        int activeMembers = _memberRepository.GetActiveMemberCount();

        var sessions = _memberGroupRepository.GetSessionPopularity().ToList();

        var popularSessions = sessions.Take(3).ToList();

        var unpopularSessions = sessions.OrderBy(s => s.ParticipantCount).Take(3).ToList();


        // hentning af data gennem metoden GetBusiestDayOfWeek (travleste dage på ugen).
        var dayStats = _memberGroupRepository.GetBusiestDayOfWeek().ToList();

        // at finde de mest og mindst populære dage overordnet (eller vælg specifikke hold)
        var busiestDay = dayStats.OrderByDescending(d => d.ParticipantCount).First();
        var slowestDay = dayStats.OrderBy(d => d.ParticipantCount).First();

        var sb = new StringBuilder();
        sb.AppendLine("Fitness Center Rapport - statistik");
        sb.AppendLine($"Genereret denne Dato: {DateTime.Now}");
        sb.AppendLine();
        sb.AppendLine($"Antal aktive medlemmer: {activeMembers}");
        sb.AppendLine();
        sb.AppendLine($" TOP {popularSessions.Count} MEST POPULÆRE HOLD");
        foreach (var session in popularSessions)
        {
            sb.AppendLine($" - {session.SessionType}: {session.ParticipantCount} deltagere");
            sb.AppendLine($" - {session.SessionType}: {session.ParticipantPercentage}%");
        }
        sb.AppendLine();

        sb.AppendLine($" TOP {unpopularSessions.Count} MINDST POPULÆRE HOLD");
        foreach (var session in unpopularSessions)
        {
            sb.AppendLine($" - {session.SessionType}: {session.ParticipantCount} deltagere");
        }

        sb.AppendLine();
        sb.AppendLine("--- UGE DAG ANALYSE ---");
        sb.AppendLine($"Den mest populære ugedag (alle hold): {busiestDay.DayOfWeek} ({busiestDay.ParticipantCount} tilmeldinger)");
        sb.AppendLine($"Den mindst populære ugedag (alle hold): {slowestDay.DayOfWeek} ({slowestDay.ParticipantCount} tilmeldinger)");

        try
        {
            File.WriteAllText(fullPath, sb.ToString()); //metode til at skrive selve tekstfilen der skal gemmes på harddisken.
            MessageBox.Show($"Rapport genereret og gemt til:\n{fullPath}", "Genereret Rapport", MessageBoxButtons.OK);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fejl ved oprettelse af rapport: {ex.Message}", "Fejl ved oprettelse af Rapport", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

