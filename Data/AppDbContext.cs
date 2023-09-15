using MeuTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuTodo.Data
{
    /// <summary>
    /// This class is the data context of this application.
    /// A Data Context is the representation of the database in memory.
    /// This file is used to do a from-to (de-para), like the class "x" will be
    /// the table "y" in the database.
    /// </summary>
    public class AppDbContext : DbContext // -> contains all we need to use the data context properly
    {
        /// <summary>
        /// This DbSet is the representation of the database table in memory
        /// </summary>
        public DbSet<IdModel> Todos { get; set; }

        /// <summary>
        /// This method gives me a DbContextOptions that can be used to set the connection
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");

    }
}