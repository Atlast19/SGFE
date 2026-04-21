using Microsoft.EntityFrameworkCore;
using SGFE.Domein.Entitys.Configurations;

namespace SGFE.Domein.Entitys;

public partial class SGFEContext : DbContext
{
    public SGFEContext()
    {
        
    }

    public SGFEContext(DbContextOptions<SGFEContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CertificadosDigitale> CertificadosDigitales { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ConsultasEstado> ConsultasEstados { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EnviosDGII> EnviosDGIIs { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; }

    public virtual DbSet<LogsSistema> LogsSistemas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SecuenciasNCF> SecuenciasNCFs { get; set; }

    public virtual DbSet<TiposECF> TiposECFs { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-BM8BQQ3\\SQLEXPRESS;Initial Catalog=SGFE;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CertificadosDigitaleConfiguration());
        modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        modelBuilder.ApplyConfiguration(new ConsultasEstadoConfiguration());
        modelBuilder.ApplyConfiguration(new EmpresaConfiguration());
        modelBuilder.ApplyConfiguration(new EnviosDGIIConfiguration());
        modelBuilder.ApplyConfiguration(new FacturaConfiguration());
        modelBuilder.ApplyConfiguration(new FacturaDetalleConfiguration());
        modelBuilder.ApplyConfiguration(new LogsSistemaConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new SecuenciasNCFConfiguration());
        modelBuilder.ApplyConfiguration(new TiposECFConfiguration());
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
