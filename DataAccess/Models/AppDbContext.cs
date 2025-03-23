using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<OrdenDigital> OrdenDigitals { get; set; }

    public virtual DbSet<OrdenFisica> OrdenFisicas { get; set; }

    public virtual DbSet<Refresco> Refrescos { get; set; }

    public virtual DbSet<Repartidor> Repartidors { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=pizzamor;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__885457EE7C5C534B");

            entity.ToTable("Cliente");

            entity.HasIndex(e => e.Correo, "UQ__Cliente__2A586E0B14D59F73").IsUnique();

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.ApellidoC)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellidoC");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Domicilio)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("domicilio");
            entity.Property(e => e.NombreC)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombreC");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.UrlImagen)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("urlImagen");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__5295297CCFBA94F1");

            entity.ToTable("Empleado");

            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.ApellidoE)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellidoE");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.NombreE)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombreE");
            entity.Property(e => e.Puesto)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("puesto");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sexo");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__C26AF483DDD23ED6");

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.DescripPizza)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripPizza");
            entity.Property(e => e.IdRefresco).HasColumnName("idRefresco");
            entity.Property(e => e.NombrePizza)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombrePizza");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.TamanioPizza)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tamanioPizza");
            entity.Property(e => e.UrlMenu)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("urlMenu");

            entity.HasOne(d => d.IdRefrescoNavigation).WithMany(p => p.Menus)
                .HasForeignKey(d => d.IdRefresco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Menu__idRefresco__44FF419A");
        });

        modelBuilder.Entity<OrdenDigital>(entity =>
        {
            entity.HasKey(e => e.IdOrdenD).HasName("PK__OrdenDig__57EBDAB36D084184");

            entity.ToTable("OrdenDigital");

            entity.Property(e => e.IdOrdenD).HasColumnName("idOrdenD");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaD)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaD");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdRefresco).HasColumnName("idRefresco");
            entity.Property(e => e.IdRepartidor).HasColumnName("idRepartidor");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.MontoTotal)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("montoTotal");
            entity.Property(e => e.SaborIngredientes)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sabor_ingredientes");
            entity.Property(e => e.TamanioPizza)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tamanioPizza");
            entity.Property(e => e.UrlOrdenD)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("urlOrdenD");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.OrdenDigitals)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrdenDigi__idCli__4D94879B");

            entity.HasOne(d => d.IdRefrescoNavigation).WithMany(p => p.OrdenDigitals)
                .HasForeignKey(d => d.IdRefresco)
                .HasConstraintName("FK__OrdenDigi__idRef__4F7CD00D");

            entity.HasOne(d => d.IdRepartidorNavigation).WithMany(p => p.OrdenDigitals)
                .HasForeignKey(d => d.IdRepartidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrdenDigi__idRep__4E88ABD4");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.OrdenDigitals)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrdenDigi__idSuc__5070F446");
        });

        modelBuilder.Entity<OrdenFisica>(entity =>
        {
            entity.HasKey(e => e.IdOrdenF).HasName("PK__OrdenFis__57EBDAB16DD566A2");

            entity.ToTable("OrdenFisica");

            entity.Property(e => e.IdOrdenF).HasColumnName("idOrdenF");
            entity.Property(e => e.FechaF)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaF");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.IdRefresco).HasColumnName("idRefresco");
            entity.Property(e => e.MontoTotal)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("montoTotal");
            entity.Property(e => e.SaborIngredientes)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sabor_ingredientes");
            entity.Property(e => e.TamanioPizza)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tamanioPizza");
            entity.Property(e => e.UrlOrdenF)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("urlOrdenF");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.OrdenFisicas)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrdenFisi__idEmp__49C3F6B7");

            entity.HasOne(d => d.IdRefrescoNavigation).WithMany(p => p.OrdenFisicas)
                .HasForeignKey(d => d.IdRefresco)
                .HasConstraintName("FK__OrdenFisi__idRef__48CFD27E");
        });

        modelBuilder.Entity<Refresco>(entity =>
        {
            entity.HasKey(e => e.IdRefresco).HasName("PK__Refresco__80FEA81AD640CEF8");

            entity.ToTable("Refresco");

            entity.Property(e => e.IdRefresco).HasColumnName("idRefresco");
            entity.Property(e => e.Marca)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("marca");
            entity.Property(e => e.PrecioR)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("precioR");
            entity.Property(e => e.TamanioR)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("tamanioR");
        });

        modelBuilder.Entity<Repartidor>(entity =>
        {
            entity.HasKey(e => e.IdRepartidor).HasName("PK__Repartid__DC03876A1F583CF8");

            entity.ToTable("Repartidor");

            entity.HasIndex(e => e.Placa, "UQ__Repartid__0C05742533AD09BF").IsUnique();

            entity.Property(e => e.IdRepartidor).HasColumnName("idRepartidor");
            entity.Property(e => e.ApellidoR)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellidoR");
            entity.Property(e => e.NombreR)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombreR");
            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("placa");
            entity.Property(e => e.UrlRepartidor)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("urlRepartidor");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reservas__94D104C864109727");

            entity.Property(e => e.IdReserva).HasColumnName("idReserva");
            entity.Property(e => e.CantidadPersonas).HasColumnName("cantidadPersonas");
            entity.Property(e => e.EstadoReserva)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estadoReserva");
            entity.Property(e => e.FechaReserva)
                .HasColumnType("datetime")
                .HasColumnName("fechaReserva");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__idClie__5535A963");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__idSucu__5629CD9C");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__F707694C2D622B3D");

            entity.ToTable("Sucursal");

            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.TelefonoSucu)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefonoSucu");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
