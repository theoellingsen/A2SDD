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
            return (T)Enum.Parse(typeof(T), value);
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

        private static DateTime IfNotNull(DateTime t)
		{
            if (t != null)
			{
                return DateTime.Today;
			} else
			{
                return t;
			}
		}

        public static List<Position> LoadPosition(Researcher r)
        {

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT level, start, end FROM position where position.id=?id", conn);

                cmd.Parameters.AddWithValue("id", r.ID);
                
                rdr = cmd.ExecuteReader();


                r.Positions = new List<Position>();
                while (rdr.Read())
                {

                    r.Positions.Add(new Position
                    {
                        Level = ParseEnum<Level>(rdr.GetString(0)),
                        Start = rdr.GetDateTime(1)
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
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT end FROM position where position.id=?id and end IS NOT NULL", conn);

                cmd.Parameters.AddWithValue("id", r.ID);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    r.Positions.Add(new Position
                    {
                        End = rdr.GetDateTime(0)
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

                MySqlCommand cmd = new MySqlCommand("SELECT id, given_name, family_name, title FROM researcher", conn);

                rdr = cmd.ExecuteReader();

                MySqlCommand PositionResearcher = new MySqlCommand("");

                while (rdr.Read())
                {
                    researchers.Add(new Researcher
                    {
                        ID = rdr.GetInt32(0),
                        GivenName = rdr.GetString(1),
                        FamilyName = rdr.GetString(2),
                        Title = rdr.GetString(3),
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
            foreach (Researcher r in researchers)
			{
                r.Positions = LoadPosition(r);
			}

            return researchers;
        }

        public static Researcher LoadReseacherDetailsView(Researcher r)
        {
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdra = null; 
            MySqlDataReader rdrb = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select unit, campus, email, photo, degree, supervisor_id, level, utas_start, current_start " +
                                                    "from researcher " +
                                                    "where researcher_id=?id", conn);

                MySqlCommand PositionResearcher = new MySqlCommand("");

                cmd.Parameters.AddWithValue("id", r.ID);
                rdra = cmd.ExecuteReader();

                while (rdra.Read())
                {
                    r.School = rdra.GetString(5);
                    r.Unit = rdra.GetString(6);
                    r.Campus = ParseEnum<Campus>(rdra.GetString(7));
                    r.Email = rdra.GetString(8);
                    r.Photo = rdra.GetString(9);
                }

                PositionResearcher.Parameters.AddWithValue("id", r.ID);
                rdrb = PositionResearcher.ExecuteReader();

                while (rdrb.Read())
                {
                    r.Positions.Add(new Position 
                    {
                        Level = ParseEnum<Level>(rdrb.GetString(4)),
                        Start = rdrb.GetDateTime(5),
                        End = rdrb.GetDateTime(6)
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdra != null)
                {
                    rdra.Close();
                }
                if (rdrb != null)
                {
                    rdrb.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return r;
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

                MySqlCommand cmd = new MySqlCommand("select count(*) from publication as pub, researcher_publication as respub " +
                                                    "where pub.doi = respub.doi and researcher_id = ?id and year >= ?start and year <= ?end", conn);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("start", startYear);
                cmd.Parameters.AddWithValue("end", endYear);
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

        //Optional part of step 2.3.4 in Week 8 tutorial illustrating that some answers can be obtained by directly querying the database.
        //This is useful if the necessary data has not been loaded into memory yet.
        /* public static int EmployeeTrainingCount(Researcher e, int startYear, int endYear)
         {
             MySqlConnection conn = GetConnection();
             int count = 0;

             try
             {
                 conn.Open();

                 MySqlCommand cmd = new MySqlCommand("select count(*) from publication as pub, researcher_publication as respub " +
                                                     "where pub.doi = respub.doi and researcher_id = ?id and year >= ?start and year <= ?end", conn);
                 cmd.Parameters.AddWithValue("id", e.ID);
                 cmd.Parameters.AddWithValue("start", startYear);
                 cmd.Parameters.AddWithValue("end", endYear);
                 count = Int32.Parse(cmd.ExecuteScalar().ToString());
             }
             catch (MySqlException ex)
             {
                 Console.WriteLine("Error connecting to database: " + ex);
             }
             finally
             {
                 if (conn != null)
                 {
                     conn.Close();
                 }
             }

             return count;
         }

         //This method is now obsolete.
         public static List<Employee> Generate()
         {
             return new List<Employee>() {
                 new Employee { Name = "Jane", ID = 1, Gender = Gender.F },
                 new Employee { Name = "John", ID = 3, Gender = Gender.M },
                 new Employee { Name = "Mary", ID = 7, Gender = Gender.F },
                 new Employee { Name = "Lindsay", ID = 5, Gender = Gender.X },
                 new Employee { Name = "Meilin", ID = 2, Gender = Gender.F }
             };
         } */
    }
}
