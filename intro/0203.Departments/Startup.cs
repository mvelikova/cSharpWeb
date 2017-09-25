namespace _0203.Departments
{
    class Startup
    {
        static void Main()
        {
            using (var db = new DepartmentsContext())
            {
                DropCreateDatabase(db);
            }
        }

        private static void DropCreateDatabase(DepartmentsContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}