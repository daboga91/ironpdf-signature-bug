// See https://aka.ms/new-console-template for more information
using IronPdf.Signing;
using System.Security.Cryptography.X509Certificates;

var pdf1 = PdfDocument.FromFile("this_is_a_pdf.pdf");
try
{
    var certPath = Path.Combine(["Certificates", "chain.pfx"]);
    var certificate = new X509Certificate2(certPath, "", X509KeyStorageFlags.Exportable);

    var signature = new PdfSignature(certificate)
    {
        SigningContact = "contact",
        SigningLocation = "location",
        SigningReason = "reason",
        SignatureDate = DateTime.Now,
    };

    pdf1.Sign(signature);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

var pdf2 = PdfDocument.FromFile("this_is_a_pdf.pdf");
try
{
    var certPath = Path.Combine("Certificates", "chain.cert.pem");
    var pkPath = Path.Combine("Certificates", "private", "Test_Digital_Signiture.key.pem");
    var certificate = X509Certificate2.CreateFromPemFile(certPath, pkPath);

    var signature = new PdfSignature(certificate)
    {
        SigningContact = "contact",
        SigningLocation = "location",
        SigningReason = "reason",
        SignatureDate = DateTime.Now,
    };

    pdf2.Sign(signature);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
