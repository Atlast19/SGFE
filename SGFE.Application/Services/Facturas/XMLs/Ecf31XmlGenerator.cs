

using SGFE.Application.Interfaces.Facturas;
using SGFE.Domein.Entitys;
using System.Xml.Linq;

namespace SGFE.Application.Services.Facturas.XMLs
{
    public class Ecf31XmlGenerator : IGenerateXMLService
    {
        public string GenerarXml(Factura factura)
        {
            XNamespace ns = "http://dgii.gov.do/ecf";

            var xml = new XDocument(
                new XElement(ns + "eCF",
                    new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),

                    // ENCABEZADO
                    new XElement(ns + "Encabezado",

                        new XElement(ns + "IdDoc",
                            new XElement(ns + "TipoeCF", factura.TiposECF),
                            new XElement(ns + "eNCF", factura.NCF),
                            new XElement(ns + "FechaEmision", factura.FechaEmision.ToString("yyyy-MM-dd")),

                            // CAMPOS CLAVE DGII
                            new XElement(ns + "TipoIngresos", "01"), // 01 = Ingresos por operaciones
                            new XElement(ns + "TipoMoneda", "DOP")
                        ),

                        // EMISOR (HARDCODE - LUEGO DB)
                        new XElement(ns + "Emisor",
                            new XElement(ns + "RNCEmisor", "131234567"),
                            new XElement(ns + "RazonSocial", "Mi Empresa SRL"),
                            new XElement(ns + "NombreComercial", "Mi Empresa"),
                            new XElement(ns + "Direccion", "Santo Domingo"),
                            new XElement(ns + "Telefono", "809-000-0000"),
                            new XElement(ns + "CorreoEmisor", "empresa@test.com"),
                            new XElement(ns + "ActividadEconomica", "6201")
                        ),

                        // RECEPTOR
                        new XElement(ns + "Receptor",
                            new XElement(ns + "RNCReceptor", factura.ClienteDocumento),
                            new XElement(ns + "RazonSocialReceptor", factura.ClienteNombre)
                        )
                    ),

                    // DETALLE
                    new XElement(ns + "Detalle",
                        factura.FacturaDetalles.Select((d, index) =>
                            new XElement(ns + "Item",
                                new XElement(ns + "NumeroLinea", index + 1),
                                new XElement(ns + "Descripcion", d.Descripcion),
                                new XElement(ns + "CantidadItem", d.Cantidad),
                                new XElement(ns + "PrecioUnitarioItem", d.PrecioUnitario),
                                new XElement(ns + "MontoItem", d.Monto),

                                // ITBIS POR ITEM
                                new XElement(ns + "ImpuestosAdicionales",
                                    new XElement(ns + "Impuesto",
                                        new XElement(ns + "TipoImpuesto", "ITBIS"),
                                        new XElement(ns + "MontoImpuesto", d.Itbis)
                                    )
                                )
                            )
                        )
                    ),

                    // TOTALES
                    new XElement(ns + "Totales",
                        new XElement(ns + "MontoGravadoTotal", factura.SubTotal),
                        new XElement(ns + "TotalITBIS", factura.ItbisTotal),
                        new XElement(ns + "MontoTotal", factura.MontoTotal)
                    )
                )
            );

            return xml.ToString();
        }
    }
}
