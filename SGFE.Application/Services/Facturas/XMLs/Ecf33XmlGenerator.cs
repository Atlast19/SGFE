using SGFE.Application.Interfaces.Facturas;
using SGFE.Domein.Entitys;
using System.Xml.Linq;

namespace SGFE.Application.Services.Facturas.XMLs
{
    public class Ecf33XmlGenerator : IGenerateXMLService
    {
        public string GenerarXml(Factura factura)
        {
            XNamespace ns = "http://dgii.gov.do/ecf";

            var xml = new XDocument(
                new XElement(ns + "eCF",
                    new XAttribute(XNamespace.Xmlns + "xsi",
                        "http://www.w3.org/2001/XMLSchema-instance"),

                    // ENCABEZADO
                    new XElement(ns + "Encabezado",

                        new XElement(ns + "IdDoc",
                            new XElement(ns + "TipoeCF", "33"),
                            new XElement(ns + "eNCF", factura.NCF),
                            new XElement(ns + "FechaEmision",
                                factura.FechaEmision.ToString("yyyy-MM-dd")),

                            new XElement(ns + "TipoIngresos", "01"),
                            new XElement(ns + "TipoMoneda", "DOP")
                        ),

                        // EMISOR
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

                    // INFORMACION REFERENCIA
                    // FACTURA AFECTADA
                    new XElement(ns + "InformacionReferencia",

                        // eNCF ORIGINAL
                        new XElement(ns + "NCFModificado", "aja aja" /*factura.NCFReferencia*/), // verificar si se agrega a la base de datos

                        // FECHA DOCUMENTO ORIGINAL
                        new XElement(ns + "FechaNCFModificado", "aja aja" /*factura.FechaFacturaReferencia?.ToString("yyyy-MM-dd")*/), // tambien

                        // MOTIVO
                        new XElement(ns + "CodigoModificacion", "02"),

                        new XElement(ns + "RazonModificacion", "Aumento en el valor facturado")
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
