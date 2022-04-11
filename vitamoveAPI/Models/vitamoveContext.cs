using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace vitamoveAPI.Models
{
    public partial class vitamoveContext : DbContext
    {
        public vitamoveContext()
        {
        }

        public vitamoveContext(DbContextOptions<vitamoveContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumnos { get; set; }
        public virtual DbSet<AlumnoRutina> AlumnoRutinas { get; set; }
        public virtual DbSet<Barrio> Barrios { get; set; }
        public virtual DbSet<Clase> Clases { get; set; }
        public virtual DbSet<ClaseAlumno> ClaseAlumnos { get; set; }
        public virtual DbSet<DetallesPago> DetallesPagos { get; set; }
        public virtual DbSet<Disciplina> Disciplinas { get; set; }
        public virtual DbSet<Ejercicio> Ejercicios { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<FormasPago> FormasPagos { get; set; }
        public virtual DbSet<Plane> Planes { get; set; }
        public virtual DbSet<Profesore> Profesores { get; set; }
        public virtual DbSet<Rutina> Rutinas { get; set; }
        public virtual DbSet<RutinasEjercicio> RutinasEjercicios { get; set; }
        public virtual DbSet<Sexo> Sexos { get; set; }
        public virtual DbSet<Sucursale> Sucursales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("User ID=postgres; Password=programacion; Server=localhost; Database=vitamove; Integrated Security=true; Pooling=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Spanish_Argentina.1252");

            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.IdAlumno)
                    .HasName("alumnos_pkey");

                entity.ToTable("alumnos");

                entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .HasColumnName("apellido");

                entity.Property(e => e.Dni)
                    .HasMaxLength(50)
                    .HasColumnName("dni");

                entity.Property(e => e.FecNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fec_nacimiento");

                entity.Property(e => e.IdSexo).HasColumnName("id_sexo");

                entity.Property(e => e.Imagen).HasColumnName("imagen");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdSexoNavigation)
                    .WithMany(p => p.Alumnos)
                    .HasForeignKey(d => d.IdSexo)
                    .HasConstraintName("alumnos_id_sexo_fkey");
            });

            modelBuilder.Entity<AlumnoRutina>(entity =>
            {
                entity.HasKey(e => new { e.IdAlumno, e.IdRutina })
                    .HasName("alumno_rutinas_pkey");

                entity.ToTable("alumno_rutinas");

                entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");

                entity.Property(e => e.IdRutina).HasColumnName("id_rutina");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.AlumnoRutinas)
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("alumno_rutinas_id_alumno_fkey");

                entity.HasOne(d => d.IdRutinaNavigation)
                    .WithMany(p => p.AlumnoRutinas)
                    .HasForeignKey(d => d.IdRutina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("alumno_rutinas_id_rutina_fkey");
            });

            modelBuilder.Entity<Barrio>(entity =>
            {
                entity.HasKey(e => e.IdBarrio)
                    .HasName("barrios_pkey");

                entity.ToTable("barrios");

                entity.Property(e => e.IdBarrio).HasColumnName("id_barrio");

                entity.Property(e => e.Barrio1)
                    .HasMaxLength(30)
                    .HasColumnName("barrio");
            });

            modelBuilder.Entity<Clase>(entity =>
            {
                entity.HasKey(e => e.IdClase)
                    .HasName("clases_pkey");

                entity.ToTable("clases");

                entity.Property(e => e.IdClase).HasColumnName("id_clase");

                entity.Property(e => e.Cupo).HasColumnName("cupo");

                entity.Property(e => e.DiaSemana).HasColumnName("dia_semana");

                entity.Property(e => e.HoraDesde).HasColumnName("hora_desde");

                entity.Property(e => e.HoraHasta).HasColumnName("hora_hasta");

                entity.Property(e => e.IdDisciplina).HasColumnName("id_disciplina");

                entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");

                entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");

                entity.HasOne(d => d.IdDisciplinaNavigation)
                    .WithMany(p => p.Clases)
                    .HasForeignKey(d => d.IdDisciplina)
                    .HasConstraintName("clases_id_disciplina_fkey");

                entity.HasOne(d => d.IdProfesorNavigation)
                    .WithMany(p => p.Clases)
                    .HasForeignKey(d => d.IdProfesor)
                    .HasConstraintName("clases_id_profesor_fkey");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.Clases)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("clases_id_sucursal_fkey");
            });

            modelBuilder.Entity<ClaseAlumno>(entity =>
            {
                entity.HasKey(e => new { e.IdClase, e.IdAlumno })
                    .HasName("clase_alumnos_pkey");

                entity.ToTable("clase_alumnos");

                entity.Property(e => e.IdClase).HasColumnName("id_clase");

                entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.ClaseAlumnos)
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("clase_alumnos_id_alumno_fkey");

                entity.HasOne(d => d.IdClaseNavigation)
                    .WithMany(p => p.ClaseAlumnos)
                    .HasForeignKey(d => d.IdClase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("clase_alumnos_id_clase_fkey");
            });

            modelBuilder.Entity<DetallesPago>(entity =>
            {
                entity.HasKey(e => new { e.IdFactura, e.CodPago })
                    .HasName("detalles_pago_pkey");

                entity.ToTable("detalles_pago");

                entity.Property(e => e.IdFactura).HasColumnName("id_factura");

                entity.Property(e => e.CodPago).HasColumnName("cod_pago");

                entity.Property(e => e.Importe).HasColumnName("importe");

                entity.Property(e => e.Recargo).HasColumnName("recargo");

                entity.HasOne(d => d.CodPagoNavigation)
                    .WithMany(p => p.DetallesPagos)
                    .HasForeignKey(d => d.CodPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detalles_pago_cod_pago_fkey");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.DetallesPagos)
                    .HasForeignKey(d => d.IdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detalles_pago_id_factura_fkey");
            });

            modelBuilder.Entity<Disciplina>(entity =>
            {
                entity.HasKey(e => e.IdDisciplina)
                    .HasName("disciplinas_pkey");

                entity.ToTable("disciplinas");

                entity.Property(e => e.IdDisciplina).HasColumnName("id_disciplina");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Ejercicio>(entity =>
            {
                entity.HasKey(e => e.IdEjercicio)
                    .HasName("ejercicios_pkey");

                entity.ToTable("ejercicios");

                entity.Property(e => e.IdEjercicio).HasColumnName("id_ejercicio");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura)
                    .HasName("facturas_pkey");

                entity.ToTable("facturas");

                entity.Property(e => e.IdFactura).HasColumnName("id_factura");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");

                entity.Property(e => e.IdPlan).HasColumnName("id_plan");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdAlumno)
                    .HasConstraintName("facturas_id_alumno_fkey");

                entity.HasOne(d => d.IdPlanNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdPlan)
                    .HasConstraintName("facturas_id_plan_fkey");
            });

            modelBuilder.Entity<FormasPago>(entity =>
            {
                entity.HasKey(e => e.CodPago)
                    .HasName("formas_pago_pkey");

                entity.ToTable("formas_pago");

                entity.Property(e => e.CodPago).HasColumnName("cod_pago");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.PorcRecargo).HasColumnName("porc_recargo");
            });

            modelBuilder.Entity<Plane>(entity =>
            {
                entity.HasKey(e => e.IdPlan)
                    .HasName("planes_pkey");

                entity.ToTable("planes");

                entity.Property(e => e.IdPlan).HasColumnName("id_plan");

                entity.Property(e => e.CantMeses).HasColumnName("cant_meses");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Precio).HasColumnName("precio");
            });

            modelBuilder.Entity<Profesore>(entity =>
            {
                entity.HasKey(e => e.IdProfesor)
                    .HasName("profesores_pkey");

                entity.ToTable("profesores");

                entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .HasColumnName("apellido");

                entity.Property(e => e.Dni)
                    .HasMaxLength(50)
                    .HasColumnName("dni");

                entity.Property(e => e.FecNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fec_nacimiento");

                entity.Property(e => e.IdSexo).HasColumnName("id_sexo");

                entity.Property(e => e.Imagen).HasColumnName("imagen");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdSexoNavigation)
                    .WithMany(p => p.Profesores)
                    .HasForeignKey(d => d.IdSexo)
                    .HasConstraintName("profesores_id_sexo_fkey");
            });

            modelBuilder.Entity<Rutina>(entity =>
            {
                entity.HasKey(e => e.IdRutina)
                    .HasName("rutinas_pkey");

                entity.ToTable("rutinas");

                entity.Property(e => e.IdRutina).HasColumnName("id_rutina");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdDisciplina).HasColumnName("id_disciplina");

                entity.HasOne(d => d.IdDisciplinaNavigation)
                    .WithMany(p => p.Rutinas)
                    .HasForeignKey(d => d.IdDisciplina)
                    .HasConstraintName("rutinas_id_disciplina_fkey");
            });

            modelBuilder.Entity<RutinasEjercicio>(entity =>
            {
                entity.HasKey(e => new { e.IdRutina, e.IdEjercicio })
                    .HasName("rutinas_ejercicios_pkey");

                entity.ToTable("rutinas_ejercicios");

                entity.Property(e => e.IdRutina).HasColumnName("id_rutina");

                entity.Property(e => e.IdEjercicio).HasColumnName("id_ejercicio");

                entity.Property(e => e.Repeticiones).HasColumnName("repeticiones");

                entity.Property(e => e.Tiempo)
                    .HasMaxLength(30)
                    .HasColumnName("tiempo");

                entity.HasOne(d => d.IdEjercicioNavigation)
                    .WithMany(p => p.RutinasEjercicios)
                    .HasForeignKey(d => d.IdEjercicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rutinas_ejercicios_id_ejercicio_fkey");

                entity.HasOne(d => d.IdRutinaNavigation)
                    .WithMany(p => p.RutinasEjercicios)
                    .HasForeignKey(d => d.IdRutina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rutinas_ejercicios_id_rutina_fkey");
            });

            modelBuilder.Entity<Sexo>(entity =>
            {
                entity.HasKey(e => e.IdSexo)
                    .HasName("sexos_pkey");

                entity.ToTable("sexos");

                entity.Property(e => e.IdSexo).HasColumnName("id_sexo");

                entity.Property(e => e.Sexo1)
                    .HasMaxLength(20)
                    .HasColumnName("sexo");
            });

            modelBuilder.Entity<Sucursale>(entity =>
            {
                entity.HasKey(e => e.IdSucursal)
                    .HasName("sucursales_pkey");

                entity.ToTable("sucursales");

                entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");

                entity.Property(e => e.Dirección)
                    .HasMaxLength(50)
                    .HasColumnName("dirección");

                entity.Property(e => e.IdBarrio).HasColumnName("id_barrio");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdBarrioNavigation)
                    .WithMany(p => p.Sucursales)
                    .HasForeignKey(d => d.IdBarrio)
                    .HasConstraintName("sucursales_id_barrio_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
