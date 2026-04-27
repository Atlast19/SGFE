using Microsoft.EntityFrameworkCore;
using SGFE.Domein.Entitys;

namespace SGFE.Percistence.Contexts.Context;

public partial class SGFEContext : DbContext
{
    public SGFEContext()
    {
    }

    public SGFEContext(DbContextOptions<SGFEContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CertificadosDigital> CertificadosDigitales { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Configuracion> Configuracions { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EnvioDGII> EnviosDGIIs { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<SecuenciaNCF> SecuenciaNCFs { get; set; }

    public virtual DbSet<TipoECF> TiposECFs { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-BM8BQQ3\\SQLEXPRESS;Initial Catalog=SGFE;Integrated Security=True;Encrypt=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Domein.Entitys.Configurations.CertificadosDigitaleConfiguration());
        modelBuilder.ApplyConfiguration(new Domein.Entitys.Configurations.ClienteConfiguration());
        modelBuilder.ApplyConfiguration(new Domein.Entitys.Configurations.ConfiguracionConfiguration());
        modelBuilder.ApplyConfiguration(new Domein.Entitys.Configurations.EmpresaConfiguration());
        modelBuilder.ApplyConfiguration(new Domein.Entitys.Configurations.EnviosDGIIConfiguration());
        modelBuilder.ApplyConfiguration(new Domein.Entitys.Configurations.FacturaConfiguration());
        modelBuilder.ApplyConfiguration(new Domein.Entitys.Configurations.FacturaDetalleConfiguration());
        modelBuilder.ApplyConfiguration(new Domein.Entitys.Configurations.LogConfiguration());
        modelBuilder.ApplyConfiguration(new Domein.Entitys.Configurations.RoleConfiguration());
        modelBuilder.ApplyConfiguration(new Domein.Entitys.Configurations.SecuenciaNCFConfiguration());
        modelBuilder.ApplyConfiguration(new Domein.Entitys.Configurations.TiposECFConfiguration());
        modelBuilder.ApplyConfiguration(new Domein.Entitys.Configurations.UsuarioConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
