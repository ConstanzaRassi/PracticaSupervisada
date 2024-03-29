﻿using System;
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
        public virtual DbSet<Barrio> Barrios { get; set; }
        public virtual DbSet<Clase> Clases { get; set; }
        public virtual DbSet<ClaseAlumno> ClaseAlumnos { get; set; }
        public virtual DbSet<CuerpoEjercicio> CuerpoEjercicios { get; set; }
        public virtual DbSet<Disciplina> Disciplinas { get; set; }
        public virtual DbSet<Ejercicio> Ejercicios { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<FormasPago> FormasPagos { get; set; }
        public virtual DbSet<Plan> Planes { get; set; }
        public virtual DbSet<Profesor> Profesores { get; set; }
        public virtual DbSet<Rutina> Rutinas { get; set; }
        public virtual DbSet<RutinasEjercicio> RutinasEjercicios { get; set; }
        public virtual DbSet<Sexo> Sexos { get; set; }
        public virtual DbSet<Sucursal> Sucursales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("User ID=postgres;Password=programacion; Server=localhost; Database=vitamove; Integrated Security=true; Pooling=true");
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

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FecNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fec_nacimiento");

                entity.Property(e => e.IdSexo).HasColumnName("id_sexo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .HasColumnName("nombre");

                entity.Property(e => e.Pass)
                    .HasMaxLength(50)
                    .HasColumnName("pass");

                entity.Property(e => e.Vencimiento)
                    .HasColumnType("date")
                    .HasColumnName("vencimiento");

                entity.HasOne(d => d.IdSexoNavigation)
                    .WithMany(p => p.Alumnos)
                    .HasForeignKey(d => d.IdSexo)
                    .HasConstraintName("alumnos_id_sexo_fkey");
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

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.HoraDesde)
                    .HasMaxLength(10)
                    .HasColumnName("hora_desde");

                entity.Property(e => e.HoraHasta)
                    .HasMaxLength(10)
                    .HasColumnName("hora_hasta");

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
                entity.HasKey(e => new { e.IdClase, e.IdAlumno, e.Fecha })
                    .HasName("clase_alumnos_pkey");

                entity.ToTable("clase_alumnos");

                entity.Property(e => e.IdClase).HasColumnName("id_clase");

                entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.ClaseAlumnos)
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("clase_alumnos_id_alumno_fkey");
            });

            modelBuilder.Entity<CuerpoEjercicio>(entity =>
            {
                entity.HasKey(e => e.IdEj)
                    .HasName("cuerpo_ejercicios_pkey");

                entity.ToTable("cuerpo_ejercicios");

                entity.Property(e => e.IdEj)
                    .ValueGeneratedNever()
                    .HasColumnName("id_ej");

                entity.Property(e => e.Ej1)
                    .HasMaxLength(50)
                    .HasColumnName("ej1");

                entity.Property(e => e.Ej2)
                    .HasMaxLength(50)
                    .HasColumnName("ej2");

                entity.Property(e => e.Ej3)
                    .HasMaxLength(50)
                    .HasColumnName("ej3");
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

                entity.Property(e => e.CodPago).HasColumnName("cod_pago");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");

                entity.Property(e => e.IdPlan).HasColumnName("id_plan");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.HasOne(d => d.CodPagoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.CodPago)
                    .HasConstraintName("facturas_cod_pago_fkey");

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
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasKey(e => e.IdPlan)
                    .HasName("planes_pkey");

                entity.ToTable("planes");

                entity.Property(e => e.IdPlan).HasColumnName("id_plan");

                entity.Property(e => e.CantMeses).HasColumnName("cant_meses");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Precio).HasColumnName("precio");
            });

            modelBuilder.Entity<Profesor>(entity =>
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

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FecNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fec_nacimiento");

                entity.Property(e => e.IdSexo).HasColumnName("id_sexo");

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

                entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");

                entity.Property(e => e.IdDisciplina).HasColumnName("id_disciplina");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.Rutinas)
                    .HasForeignKey(d => d.IdAlumno)
                    .HasConstraintName("rutinas_id_alumno_fkey");

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

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.IdSucursal)
                    .HasName("sucursales_pkey");

                entity.ToTable("sucursales");

                entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .HasColumnName("direccion");

                entity.Property(e => e.Estado).HasColumnName("estado");

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
