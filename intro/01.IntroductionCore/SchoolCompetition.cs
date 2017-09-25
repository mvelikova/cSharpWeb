using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.IntroductionCore
{
    public class SchoolCompetition
    {
        static void Main()
        {
            Dictionary<string, HashSet<string>> studentCourses = new Dictionary<string, HashSet<string>>();
            Dictionary<string, int> studentPoints = new Dictionary<string, int>();

            ReadInput(studentPoints, studentCourses);
            PrintResults(studentPoints, studentCourses);
        }

        private static void PrintResults(Dictionary<string, int> studentPoints,
            Dictionary<string, HashSet<string>> studentCourses)
        {
            var orderedStudents = studentPoints
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key);

            foreach (var orderedStudent in orderedStudents)
            {
                var orderedSubjects = studentCourses[orderedStudent.Key].ToArray().OrderBy(s => s);
                Console.WriteLine(
                    $"{orderedStudent.Key}: {orderedStudent.Value} [{string.Join(",", orderedSubjects)}]");
            }
        }

        private static void ReadInput(Dictionary<string, int> studentPoints,
            Dictionary<string, HashSet<string>> studentCourses)
        {
            string cmd = string.Empty;

            while ((cmd = Console.ReadLine()) != "END")
            {
                var tokens = cmd.Split();
                var name = tokens[0];
                var course = tokens[1];
                var points = int.Parse(tokens[2]);

                if (!studentCourses.ContainsKey(name))
                {
                    studentCourses.Add(name, new HashSet<string>());
                    studentPoints.Add(name, 0);
                }

                studentPoints[name] += points;
                studentCourses[name].Add(course);
            }
        }
    }
}