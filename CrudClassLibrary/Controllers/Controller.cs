using CrudClassLibrary.DTOs;
using CrudClassLibrary.Entities;
using CrudClassLibrary.Factories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudClassLibrary.Controllers
{
    internal class Controller
    {
        private NaturalPerson naturalperson;
        private LegalPerson legalperson;
        private PersonDTO personDTO;
        private SqlTransaction trans = null;
        private SqlConnection conn = null;

        public Controller(NaturalPerson naturalperson)
        {
            this.naturalperson = naturalperson;
        }

        public Controller(LegalPerson legalperson)
        {
            this.legalperson = legalperson;
        }

        public Controller(PersonDTO personDTO)
        {
            this.personDTO = personDTO;
        }

        #region Person
        private int InsertPerson(Person p)
        {
            try
            {
                using (SqlCommand stmt = new SqlCommand())
                {
                    /*
                     * TRUNCATE TABLE PERSON
                     * dbcc CHECKIDENT('PERSON', RESEED, 0);
                     */
                    stmt.Connection = this.conn;
                    stmt.Transaction = this.trans;

                    /*
                     * INSERT INTO Person (Name)
                     * OUTPUT INSERTED.Id
                     * VALUES (@Name);
                     */

                    stmt.CommandText = "INSERT INTO Person (Name) OUTPUT INSERTED.Id VALUES (@Name);";

                    // FORMA 01:
                    // stmt.Parameters.AddWithValue("@Name", p.Name);

                    // FORMA 02:
                    stmt.Parameters.Add(new SqlParameter("Name", System.Data.SqlDbType.VarChar)).Value = p.Name;

                    return (int)stmt.ExecuteScalar();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return 1;
            }
        }

        private int UpdatePerson()
        {
            try
            {
                using (SqlCommand stmt = new SqlCommand())
                {
                    stmt.Transaction = this.trans;
                    stmt.Connection = this.conn;
                    stmt.CommandText = "UPDATE PERSON SET NAME = @Name WHERE ID = (SELECT PERSON_ID FROM NATURALPERSON WHERE CPF = @Cpf);";
                    stmt.Parameters.Add(new SqlParameter("Name", System.Data.SqlDbType.VarChar)).Value = naturalperson.Name;
                    stmt.Parameters.Add(new SqlParameter("Cpf", System.Data.SqlDbType.VarChar)).Value = naturalperson.Cpf;
                    int result = stmt.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return -1;
            }
        }

        private int DeletePerson()
        {
            try
            {
                using (SqlCommand stmt = new SqlCommand())
                {
                    stmt.Transaction = this.trans;
                    stmt.Connection = this.conn;
                    stmt.CommandText = "DELETE FROM PERSON WHERE ID = @Id";
                    stmt.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int)).Value = naturalperson.Id;
                    int result = stmt.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return -1;
            }
        }
        #endregion

        #region Natural Person
        private int InsertNaturalPerson()
        {
            try
            {
                using (SqlCommand stmt = new SqlCommand())
                {
                    stmt.Connection = this.conn;
                    stmt.Transaction = this.trans;
                    stmt.CommandText = "INSERT INTO NATURALPERSON (PERSON_ID, CPF) VALUES (@PersonId, @Cpf);";
                    stmt.Parameters.Add(new SqlParameter("PersonId", System.Data.SqlDbType.Int)).Value = naturalperson.Id;
                    stmt.Parameters.Add(new SqlParameter("Cpf", System.Data.SqlDbType.VarChar)).Value = naturalperson.Cpf;
                    return stmt.ExecuteNonQuery();
                }
            }
            catch (Exception error)
            {
                if (trans != null)
                    trans.Rollback();
                Console.WriteLine("Error: {0}", error);
                return -1;
            }
        }

        private int UpdateNaturalPerson()
        {
            try
            {
                // Nenhum outro campo aqui para alterar.
                return 1;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return -1;
            }
        }

        private int DeleteNaturalPerson()
        {
            try
            {
                using (SqlCommand stmt = new SqlCommand())
                {
                    stmt.Transaction = this.trans;
                    stmt.Connection = this.conn;
                    stmt.CommandText = "DELETE FROM NATURALPERSON OUTPUT DELETED.PERSON_ID WHERE NATURALPERSON.CPF = @Cpf;";
                    stmt.Parameters.Add(new SqlParameter("Cpf", System.Data.SqlDbType.VarChar)).Value = naturalperson.Cpf;
                    int result = (int)stmt.ExecuteScalar();
                    return result;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return -1;
            }
        }
        #endregion

        #region Legal Person
        public int InsertLegalPerson()
        {
            return 1;
        }

        public int UpdateLegalPerson()
        {
            return 1;
        }

        public LegalPerson GetLegalPersonByCNPJ()
        {
            return null;
        }
        #endregion

        #region Public Methods
        public int InsertNaturalPersonData()
        {
            this.conn = ConnectionFactory.Instance.Open();
            int id = 0;
            try
            {
                trans = this.conn.BeginTransaction();
                id = this.InsertPerson(naturalperson);
                if (id > 0)
                {
                    naturalperson.Id = id;
                    if (this.InsertNaturalPerson() == 1)
                    {
                        trans.Commit();
                        ConnectionFactory.Instance.Close(this.conn);
                        return 1;
                    }
                    return -1;
                }
                return -1;
            }
            catch (Exception error)
            {
                if (trans != null)
                    trans.Rollback();
                Console.WriteLine("Error: {0}", error);
                return -1;
            }
            finally
            {
                ConnectionFactory.Instance.Close(this.conn);
            }
        }

        public int UpdateNaturalPersonData()
        {
            this.conn = ConnectionFactory.Instance.Open();
            try
            {
                this.trans = this.conn.BeginTransaction();
                if (this.UpdatePerson() == 1)
                {
                    if (this.UpdateNaturalPerson() == 1)
                    {
                        trans.Commit();
                        ConnectionFactory.Instance.Close(this.conn);
                        return 1;
                    }
                    return -1;
                }
                return -1;
            }
            catch (Exception error)
            {
                if (trans != null)
                    trans.Rollback();
                Console.WriteLine("Error: {0}", error);
                return -1;
            }
            finally
            {
                ConnectionFactory.Instance.Close(this.conn);
            }
        }

        public int DeleteNaturalPersonData()
        {
            this.conn = ConnectionFactory.Instance.Open();
            try
            {
                trans = this.conn.BeginTransaction();
                int id = this.DeleteNaturalPerson();
                if (id > 0)
                {
                    naturalperson.Id = id;
                    if (this.DeletePerson() == 1)
                    {
                        this.trans.Commit();
                        ConnectionFactory.Instance.Close(this.conn);
                        return 1;
                    }
                    return -1;
                }
                return -1;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                if (trans != null)
                    trans.Rollback();
                return -1;
            }
            finally
            {
                ConnectionFactory.Instance.Close(this.conn);
            }
        }

        public NaturalPerson GetNaturalPersonByCPF()
        {
            try
            {
                this.conn = ConnectionFactory.Instance.Open();

                using (SqlCommand stmt = new SqlCommand())
                {
                    stmt.Connection = this.conn;
                    stmt.Transaction = this.trans;

                    /*
                     * Criar, pois haverá consulta por CPF:
                     * 
                     * CREATE UNIQUE INDEX IX_NaturalPerson_CPF
                     * ON NaturalPerson(CPF);
                     */
                    stmt.CommandText = "SELECT * FROM PERSON INNER JOIN NATURALPERSON ON PERSON.ID = NATURALPERSON.PERSON_ID WHERE NATURALPERSON.CPF = @Cpf";
                    stmt.Parameters.Add(new SqlParameter("Cpf", System.Data.SqlDbType.VarChar)).Value = naturalperson.Cpf;

                    using (SqlDataReader reader = stmt.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            NaturalPerson np = new NaturalPerson()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Cpf = reader.GetString(4),
                            };
                            return np;
                        }
                        reader.Close();
                    }
                    return null;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return null;
            }
            finally
            {
                ConnectionFactory.Instance.Close(this.conn);
            }
        }

        /*
        *  // IReadOnlyList:
           //   Represents a read-only collection of elements that can be accessed by index.
           //   "Eu vou fornecer uma coleção de resultados, mas quem chamou o método não deve modificar essa coleção."
        */
        public IReadOnlyList<PersonDTO> GetPersonByName()
        {
            try
            {
                this.conn = ConnectionFactory.Instance.Open();

                using (SqlCommand stmt = new SqlCommand())
                {
                    stmt.Connection = this.conn;
                    stmt.Transaction = this.trans;

                    StringBuilder strb = new StringBuilder(@"
                            SELECT
                                P.ID, 
                                P.NAME, 
                                NP.CPF AS DOCUMENT 
                            FROM PERSON P 
                            INNER JOIN NATURALPERSON NP ON P.ID = NP.PERSON_ID
                            WHERE P.NAME LIKE @Name
                            UNION ALL
                            SELECT 
                                P.ID, 
                                P.NAME, 
                                LP.CNPJ AS DOCUMENT 
                            FROM PERSON P 
                            INNER JOIN LEGALPERSON LP ON P.ID = LP.PERSON_ID
                            WHERE P.NAME LIKE @Name
                        ");

                    stmt.CommandText = strb.ToString();
                    stmt.Parameters.Add(new SqlParameter("Name", System.Data.SqlDbType.VarChar)).Value = this.personDTO.Name + "%";

                    using (SqlDataReader reader = stmt.ExecuteReader())
                    {
                        // IReadOnlyList<PersonDTO> people = new List<PersonDTO>();

                        List<PersonDTO> people = new List<PersonDTO>();

                        while (reader.Read())
                        {
                            people.Add(new PersonDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                        }
                        // reader.Close() é opcional aqui pois o 'using' do SqlDataReader já cuida disso, mas não atrapalha.
                        return people;
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return null;
            }
            finally
            {
                ConnectionFactory.Instance.Close(this.conn);
            }
        }
        #endregion
    }
}
