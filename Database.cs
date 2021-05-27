using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace A2SDD
{
    abstract class Database
    {
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;

        //Part of step 2.3.3 in Week 8 tutorial. This method is a gift to you because .NET's approach to converting strings to enums is so horribly broken
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value.Replace(" ", String.Empty));
        }

        /// <summary>
        /// Creates and returns (but does not open) the connection to the database.
        /// </summary>
        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                //Note: This approach is not thread-safe
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        public static List<Publication> LoadPublications(Researcher r)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT pub.doi, title, year, type, cite_as, authors, available FROM publication AS pub, researcher_publication AS respub WHERE pub.doi = respub.doi AND researcher_id = ?id ORDER BY year DESC, title", conn);

                cmd.Parameters.AddWithValue("id", r.ID);
                rdr = cmd.ExecuteReader();
                r.Publications = new List<Publication>();

                while (rdr.Read())
                {
                    r.Publications.Add(new Publication
                    {
                        DOI = rdr.GetString(0),
                        Title = rdr.GetString(1),
                        Year = rdr.GetString(2),
                        Type = ParseEnum<PublicationType>(rdr.GetString(3)),
                        CiteAs = rdr.GetString(4),
                        Authors = rdr.GetString(5),
                        Available = rdr.GetDateTime(6)
                    }) ;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return r.Publications;
        }


        public static List<Position> LoadPosition(Researcher r)
        {

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT level, start, end FROM position WHERE position.id=?id ORDER BY start ASC", conn);

                cmd.Parameters.AddWithValue("id", r.ID);

                rdr = cmd.ExecuteReader();


                r.Positions = new List<Position>();
                while (rdr.Read())
                {
                    r.Positions.Add(new Position
                    {
                        Level = ParseEnum<EmploymentLevel>(rdr.GetString(0)),
                        Start = rdr.GetDateTime(1),
                        End = rdr.IsDBNull(2) ? null : rdr.GetDateTime(2),
                        //End = rdr.GetDateTime(2)
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return r.Positions;
        }


        public static List<Researcher> LoadReseacherListView()
        {
            List<Researcher> researchers = new List<Researcher>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT type, id, given_name, family_name, title, email FROM researcher ORDER BY family_name", conn);

                rdr = cmd.ExecuteReader();

                MySqlCommand PositionResearcher = new MySqlCommand("");

                while (rdr.Read())
                {
                    researchers.Add(new Researcher
                    {
                        EmployeeType = ParseEnum<EmployeeType>(rdr.GetString(0)),
                        ID = rdr.GetInt32(1),
                        GivenName = rdr.GetString(2),
                        FamilyName = rdr.GetString(3),
                        Title = rdr.GetString(4),
                        Email = rdr.GetString(5)

                    });
                }
                conn.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            foreach (Researcher r in researchers)
            {
                r.Positions = LoadPosition(r);
            }

            return researchers;
        }

        public static Researcher LoadReseacherDetailsView(Researcher r)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT type, unit, campus, email, photo, utas_start, current_start FROM researcher WHERE id=?id", conn);

                cmd.Parameters.AddWithValue("id", r.ID);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    r.EmployeeType = ParseEnum<EmployeeType>(rdr.GetString(0));
                    r.Unit = rdr.GetString(1);
                    r.Campus = ParseEnum<Campus>(rdr.GetString(2));
                    r.Email = rdr.GetString(3);
                    r.Photo = rdr.GetString(4);
                    r.Start = rdr.GetDateTime(5);
                    r.CurrentStart = rdr.GetDateTime(6);
                }

            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return r;
        }

        public static Staff LoadStaff(Researcher r)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            Staff changed = new Staff();

            r = LoadReseacherDetailsView(r);

            changed.ID = r.ID;
            changed.GivenName = r.GivenName;
            changed.FamilyName = r.FamilyName;
            changed.Title = r.Title;
            changed.EmployeeType = r.EmployeeType;
            changed.Unit = r.Unit;
            changed.Campus = r.Campus;
            changed.Email = r.Email;
            changed.Photo = r.Photo;
            changed.Start = r.Start;
            changed.CurrentStart = r.CurrentStart;
            

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT level FROM researcher WHERE id=?id", conn);

                cmd.Parameters.AddWithValue("id", r.ID);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    r.CurrentLevel = ParseEnum<CurrentLevel>(rdr.GetString(0));
                }

            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }

            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            changed.Positions = LoadPosition(r);
            changed.Supervisions = LoadSupervisions(changed);

            return changed;
        }

        public static Student LoadStudent(Researcher r)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            Student changed = new Student();

            r = LoadReseacherDetailsView(r);

            changed.ID = r.ID;
            changed.GivenName = r.GivenName;
            changed.FamilyName = r.FamilyName;
            changed.Title = r.Title;
            changed.EmployeeType = r.EmployeeType;
            changed.Unit = r.Unit;
            changed.Campus = r.Campus;
            changed.Email = r.Email;
            changed.Photo = r.Photo; ;
            changed.Start = r.Start;
            changed.CurrentStart = r.CurrentStart;
            changed.Supervisor = new Staff();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT degree, supervisor_id FROM researcher WHERE id=?id", conn);

                cmd.Parameters.AddWithValue("id", r.ID);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    changed.Degree = rdr.GetString(0);
                    changed.Supervisor.ID = rdr.GetInt32(1);
                }

            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return changed;
        }

        /// <summary>
        /// Filter for publications
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Filtered List of publications</returns>

        public static int LoadPublications3YearAVerage(int id, DateTime startYear, DateTime endYear)
        {
            List<Publication> filtered = new List<Publication>();
            int count = 0;

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {

                conn.Open();

                //Do not change this command @David H
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM publication AS pub, researcher_publication AS respub " +
                                                    "WHERE pub.doi = respub.doi AND researcher_id = ?id AND year >= ?start AND year <= ?end", conn);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("start", startYear.Year);
                cmd.Parameters.AddWithValue("end", endYear.Year);


                count = Int32.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return count;
        }

        public static int LoadSupervisionsCount(int id)
        {
            int count = 0;

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM researcher WHERE supervisor_id=?id", conn);

                cmd.Parameters.AddWithValue("id", id);

                count = Int32.Parse(cmd.ExecuteScalar().ToString());

            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return count;
        }

        public static List<Student> LoadSupervisions(Staff s)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT given_name, family_name FROM researcher WHERE supervisor_id=?id ORDER BY family_name", conn);

                cmd.Parameters.AddWithValue("id", s.ID);
                rdr = cmd.ExecuteReader();

                s.Supervisions = new List<Student>();

                while (rdr.Read())
                {
                    s.Supervisions.Add(new Student
                    {
                        GivenName = rdr.GetString(0),
                        FamilyName = rdr.GetString(1),
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return s.Supervisions;
        }

        public static Staff LoadSupervisor(Student s)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT given_name, family_name FROM researcher WHERE id=?id", conn);

                cmd.Parameters.AddWithValue("id", s.Supervisor.ID);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    s.Supervisor = new Staff
                    {
                        GivenName = rdr.GetString(0),
                        FamilyName = rdr.GetString(1)
                    };
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return s.Supervisor;
        }
    }
}
