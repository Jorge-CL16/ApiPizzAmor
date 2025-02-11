using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api1.Models;

public partial class ApiPizzeriaContext : DbContext
{
    public ApiPizzeriaContext()
    {
    }

    public ApiPizzeriaContext(DbContextOptions<ApiPizzeriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetallesOrden> DetallesOrdens { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Ingrediente> Ingredientes { get; set; }

    public virtual DbSet<IngredientesExtra> IngredientesExtras { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Ordene> Ordenes { get; set; }

    public virtual DbSet<Puesto> Puestos { get; set; }

    public virtual DbSet<TamanoDePizza> TamanoDePizzas { get; set; }

    public virtual DbSet<TiposDePizza> TiposDePizzas { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    //Se agrego esto
    public virtual DbSet<Refresco> Refrescos { get; set; }
    public virtual DbSet<RefrescosOrdene> RefrescosOrdenes { get; set; }
    //

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost; Database=ApiPizzeria; Integrated Security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__885457EE9C70744D");

            entity.HasIndex(e => e.Telefono, "UQ__Clientes__2A16D94583324A58").IsUnique();

            entity.HasIndex(e => e.Correo, "UQ__Clientes__2A586E0BA40646BA").IsUnique();

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<DetallesOrden>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__Detalles__49CAE2FBDBEEF972");

            entity.ToTable("DetallesOrden");

            entity.Property(e => e.IdDetalle).HasColumnName("idDetalle");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.IdOrden).HasColumnName("idOrden");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioUnitario");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.DetallesOrdens)
                .HasForeignKey(d => d.IdMenu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallesOrden_Menu");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.DetallesOrdens)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallesOrden_Ordenes");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__5295297CC5116CA8");

            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.FechaContratacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fechaContratacion");
            entity.Property(e => e.IdPuesto).HasColumnName("idPuesto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sexo");

            entity.HasOne(d => d.IdPuestoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdPuesto)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Empleados_Puestos");
        });

        modelBuilder.Entity<Ingrediente>(entity =>
        {
            entity.HasKey(e => e.IdIngrediente).HasName("PK__Ingredie__563C0D33C7CE21FF");

            entity.HasIndex(e => e.Nombre, "UQ__Ingredie__72AFBCC6626F4511").IsUnique();

            entity.Property(e => e.IdIngrediente).HasColumnName("idIngrediente");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<IngredientesExtra>(entity =>
        {
            entity.HasKey(e => e.IdExtra).HasName("PK__Ingredie__E06E1F50BC915D0F");

            entity.ToTable("IngredientesExtra");

            entity.Property(e => e.IdExtra).HasColumnName("idExtra");
            entity.Property(e => e.IdIngrediente).HasColumnName("idIngrediente");
            entity.Property(e => e.IdOrden).HasColumnName("idOrden");

            entity.HasOne(d => d.IdIngredienteNavigation).WithMany(p => p.IngredientesExtras)
                .HasForeignKey(d => d.IdIngrediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IngredientesExtra_Ingredientes");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.IngredientesExtras)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IngredientesExtra_Ordenes");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__C26AF4834321F7A8");

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdTamano).HasColumnName("idTamano");
            entity.Property(e => e.IdTipoPizza).HasColumnName("idTipoPizza");
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagenURL");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdTamanoNavigation).WithMany(p => p.Menus)
                .HasForeignKey(d => d.IdTamano)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Menu_TamanoDePizza");

            entity.HasOne(d => d.IdTipoPizzaNavigation).WithMany(p => p.Menus)
                .HasForeignKey(d => d.IdTipoPizza)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Menu_TiposDePizza");

            entity.HasMany(d => d.IdIngredientes).WithMany(p => p.IdMenus)
                .UsingEntity<Dictionary<string, object>>(
                    "MenuIngrediente",
                    r => r.HasOne<Ingrediente>().WithMany()
                        .HasForeignKey("IdIngrediente")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MenuIngredientes_Ingredientes"),
                    l => l.HasOne<Menu>().WithMany()
                        .HasForeignKey("IdMenu")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MenuIngredientes_Menu"),
                    j =>
                    {
                        j.HasKey("IdMenu", "IdIngrediente").HasName("PK__MenuIngr__E7093450BB2B18FB");
                        j.ToTable("MenuIngredientes");
                        j.IndexerProperty<int>("IdMenu").HasColumnName("idMenu");
                        j.IndexerProperty<int>("IdIngrediente").HasColumnName("idIngrediente");
                    });
        });

        modelBuilder.Entity<Ordene>(entity =>
        {
            entity.HasKey(e => e.IdOrden).HasName("PK__Ordenes__C8AAF6F34FF97D86");

            entity.Property(e => e.IdOrden).HasColumnName("idOrden");
            entity.Property(e => e.ActualizadoEn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("actualizadoEn");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creadoEn");
            entity.Property(e => e.FechaOrden)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaOrden");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.MontoTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montoTotal");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Clientes");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Empleados");
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.IdPuesto).HasName("PK__Puestos__ADF48F194818A88F");

            entity.HasIndex(e => e.Nombre, "UQ__Puestos__72AFBCC60AEB88CE").IsUnique();

            entity.Property(e => e.IdPuesto).HasColumnName("idPuesto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TamanoDePizza>(entity =>
        {
            entity.HasKey(e => e.IdTamano).HasName("PK__TamanoDe__8284F1DB7B53790A");

            entity.ToTable("TamanoDePizza");

            entity.HasIndex(e => e.Nombre, "UQ__TamanoDe__72AFBCC6CD9BFF43").IsUnique();

            entity.Property(e => e.IdTamano).HasColumnName("idTamano");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
        });

        modelBuilder.Entity<TiposDePizza>(entity =>
        {
            entity.HasKey(e => e.IdTipoPizza).HasName("PK__TiposDeP__56E573418231663B");

            entity.ToTable("TiposDePizza");

            entity.HasIndex(e => e.Nombre, "UQ__TiposDeP__72AFBCC61C23629E").IsUnique();

            entity.Property(e => e.IdTipoPizza).HasColumnName("idTipoPizza");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Ventas__077D5614AC939DBD");

            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaVenta");
            entity.Property(e => e.IdOrden).HasColumnName("idOrden");
            entity.Property(e => e.MontoTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montoTotal");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Ordenes");
        });
        //Se agrego esto
        modelBuilder.Entity<Refresco>(entity =>
        {
            entity.HasKey(e => e.IdRefresco).HasName("PK__Refresco__B25E6C8BBD60C3A4");

            entity.Property(e => e.IdRefresco).HasColumnName("idRefresco");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Tamano)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tamano");
        });

        modelBuilder.Entity<RefrescosOrdene>(entity =>
        {
            entity.HasKey(e => new { e.IdOrden, e.IdRefresco }).HasName("PK__RefrescosOrdene");

            entity.Property(e => e.IdOrden).HasColumnName("idOrden");
            entity.Property(e => e.IdRefresco).HasColumnName("idRefresco");

            entity.HasOne(d => d.Refresco)
                .WithMany(p => p.RefrescosOrdenes)
                .HasForeignKey(d => d.IdRefresco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefrescosOrdene_Refrescos");

            entity.HasOne(d => d.Ordene)
                .WithMany(p => p.RefrescosOrdenes)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefrescosOrdene_Ordenes");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
