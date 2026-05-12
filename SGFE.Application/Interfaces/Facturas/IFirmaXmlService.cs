
namespace SGFE.Application.Interfaces.Facturas
{
    public interface IFirmaXmlService
    {
        string FirmarXml(string xml, string rutaCertificado, string password);
    }
}
