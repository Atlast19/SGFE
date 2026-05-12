using SGFE.Application.Interfaces.Facturas;
using SGFE.Application.Services.Facturas.XMLs;


namespace SGFE.Application.Services.Facturas
{
    public class GenerateXMLFactory 
    {
        public IGenerateXMLService Generator(string TiposEcf) 
        {
            return TiposEcf switch
            {
                "31" => new Ecf31XmlGenerator(),
                "32" => new Ecf32XmlGenerator(),
                _ => throw new Exception("Tipo e-CF no soportado")
            };
        }
    }
}
