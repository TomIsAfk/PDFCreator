using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Xml.Linq;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace PDFGenerator
{
    internal class PDFClass
    {
        internal bool CreateAndSend(object data)
        {
            // Create a new PdfDocument
            PdfDocument pdfDoc = new PdfDocument();
            pdfDoc.Info.Title = "Sample PDF Letter";

            // Add a page
            PdfPage pdfPage = pdfDoc.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(pdfPage);

            // Define fonts
            XFont headerFont = new XFont("Verdana", 18, XFontStyleEx.Bold);
            XFont bodyFont = new XFont("Verdana", 12, XFontStyleEx.Regular);
            XFont footerFont = new XFont("Verdana", 10, XFontStyleEx.Italic);

            // Draw a logo at the top-right corner
            XImage logo = XImage.FromFile("path_to_logo.png");
            gfx.DrawImage(logo, pdfPage.Width - 150, 20, 130, 50); // Adjust size and position as needed

            // Draw sender's address
            gfx.DrawString("Sender Address Line 1\nSender Address Line 2", bodyFont, XBrushes.Black, new XRect(40, 100, pdfPage.Width, pdfPage.Height), XStringFormats.TopLeft);

            // Draw recipient's address
            gfx.DrawString("Recipient Address Line 1\nRecipient Address Line 2", bodyFont, XBrushes.Black, new XRect(40, 160, pdfPage.Width, pdfPage.Height), XStringFormats.TopLeft);

            // Draw a topic/subject
            gfx.DrawString("Subject: Your Invoice Details", headerFont, XBrushes.Black, new XRect(40, 220, pdfPage.Width, pdfPage.Height), XStringFormats.TopLeft);

            // Draw introductory lines
            gfx.DrawString("Dear Customer,", bodyFont, XBrushes.Black, new XRect(40, 270, pdfPage.Width, pdfPage.Height), XStringFormats.TopLeft);
            gfx.DrawString("Please find the details of your invoice below.", bodyFont, XBrushes.Black, new XRect(40, 300, pdfPage.Width, pdfPage.Height), XStringFormats.TopLeft);

            // Draw a table-like structure for data points
            double yPoint = 350;
            for (int i = 0; i < 5; i++) // Example loop for multiple rows
            {
                gfx.DrawString($"Item {i + 1}", bodyFont, XBrushes.Black, new XRect(40, yPoint, 100, 20), XStringFormats.TopLeft);
                gfx.DrawString($"Description {i + 1}", bodyFont, XBrushes.Black, new XRect(140, yPoint, 200, 20), XStringFormats.TopLeft);
                gfx.DrawString($"$ {(i + 1) * 100}", bodyFont, XBrushes.Black, new XRect(340, yPoint, 100, 20), XStringFormats.TopLeft);
                yPoint += 20;
            }

            // Draw closing lines
            gfx.DrawString("Thank you for your business!", bodyFont, XBrushes.Black, new XRect(40, yPoint + 30, pdfPage.Width, pdfPage.Height), XStringFormats.TopLeft);
            gfx.DrawString("Sincerely,\nYour Company Name", bodyFont, XBrushes.Black, new XRect(40, yPoint + 60, pdfPage.Width, pdfPage.Height), XStringFormats.TopLeft);

            // Draw footer (contact information)
            gfx.DrawString("Contact us: info@yourcompany.com | +1-800-123-456", footerFont, XBrushes.Black, new XRect(40, pdfPage.Height - 50, pdfPage.Width, pdfPage.Height), XStringFormats.TopLeft);

            // Save the document
            string filename = "SampleLetter.pdf";
            pdfDoc.Save(filename);
            pdfDoc.Close();

            // Optionally open the created PDF
            Process.Start(filename);

            return true;
        }
    }
    }
}