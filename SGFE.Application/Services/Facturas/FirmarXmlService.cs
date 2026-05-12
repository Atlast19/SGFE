using SGFE.Application.Interfaces.Facturas;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace SGFE.Application.Services.Facturas
{
    public class FirmarXmlService : IFirmaXmlService
    {
        public string FirmarXml(string xml, string rutaCertificado, string password)
        {
            // 1. Cargar XML
            var xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(xml);

            // 2. Cargar certificado (.p12)
            var cert = new X509Certificate2(
                rutaCertificado,
                password,
                X509KeyStorageFlags.MachineKeySet |
                X509KeyStorageFlags.Exportable
            );

            // Validación clave privada
            if (!cert.HasPrivateKey)
                throw new Exception("El certificado no tiene clave privada");

            // 3. Crear SignedXml
            var signedXml = new SignedXml(xmlDoc)
            {
                SigningKey = cert.GetRSAPrivateKey()
            };

            // 4. Referencia (firma TODO el documento)
            var reference = new Reference();
            reference.Uri = "";

            // Transformación enveloped (IMPORTANTE)
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());

            // Canonicalización
            reference.AddTransform(new XmlDsigC14NTransform());

            signedXml.AddReference(reference);

            // 5. Información del certificado
            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(cert));
            signedXml.KeyInfo = keyInfo;

            // 6. Firmar
            signedXml.ComputeSignature();

            // 7. Obtener nodo <Signature>
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // 8. Insertar firma en el XML (dentro de eCF)
            xmlDoc.DocumentElement.AppendChild(
                xmlDoc.ImportNode(xmlDigitalSignature, true)
            );

            // 9. Retornar XML firmado
            return xmlDoc.OuterXml;
        }
    }
}
