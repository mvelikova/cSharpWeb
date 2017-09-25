namespace _04.StudentsCourses
{
    class Startup
    {
        static void Main()
        {
            using (var db = new StudentsCoursesDb())
            {
                DropCreateDatabase(db);
            }
        }

        private static void DropCreateDatabase(StudentsCoursesDb db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}