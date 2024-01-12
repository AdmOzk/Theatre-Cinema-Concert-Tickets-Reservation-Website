using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System;
using webBiletProje.Models;

public class DataContext : DbContext
{
    public DataContext() : base("biletDataConnection")
    {
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<OrderTable> OrderTables { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Orders> Orderss { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // Tablo isimlerini belirleme
        modelBuilder.Entity<Etkinlik>().ToTable("etkinlik");
        modelBuilder.Entity<sehir>().ToTable("sehir");
        modelBuilder.Entity<salon>().ToTable("salon");
        modelBuilder.Entity<etkinlikdetay>().ToTable("etkinlikdetay");


        // Diğer konfigürasyonlar...

        base.OnModelCreating(modelBuilder);
    }

    [DbFunction("webBiletProje", "SelectProcedure")]
    public virtual IQueryable<Appointment> SelectProcedure(DateTime currentDT, int depart)
    {
        var currentDTParameter = new SqlParameter("@currentDT", SqlDbType.DateTime)
        {
            Value = currentDT
        };

        var departParameter = new SqlParameter("@depart", SqlDbType.Int)
        {
            Value = depart
        };

        return Database.SqlQuery<Appointment>("SELECT * FROM ktProcedure(@currentDT, @depart)", currentDTParameter, departParameter).AsQueryable();
    }

    [DbFunction("webBiletProje", "ktProcedure")]
    public virtual IQueryable<Appointment> ktProcedure(DateTime currentDT, int depart)
    {
        var currentDTParameter = new SqlParameter("@currentDT", SqlDbType.DateTime)
        {
            Value = currentDT
        };

        var departParameter = new SqlParameter("@depart", SqlDbType.Int)
        {
            Value = depart
        };

        return Database.SqlQuery<Appointment>("SELECT * FROM ktProcedure(@currentDT, @depart)", currentDTParameter, departParameter).AsQueryable();
    }


    //tiyatro
    [DbFunction("webBiletProje", "TiyatroProcedure")]
    public virtual IQueryable<Appointment> TiyatroProcedure(DateTime currentDT, int depart)
    {
        var currentDTParameter = new SqlParameter("@currentDT", SqlDbType.DateTime)
        {
            Value = currentDT
        };

        var departParameter = new SqlParameter("@depart", SqlDbType.Int)
        {
            Value = depart
        };

        return Database.SqlQuery<Appointment>("SELECT * FROM TiyatroProcedure(@currentDT, @depart)", currentDTParameter, departParameter).AsQueryable();
    }


}