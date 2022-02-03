using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
public class PrintingExample
{
    private Font printFont;
    public string textToPrint;

    // The PrintPage event is raised for each page to be printed.
    private void doc_PrintPage(object sender, PrintPageEventArgs ev)
    {
        int leftMargin = 15;//ev.MarginBounds.Left;
        int topMargin = 15;

        var pagebounds = ev.PageBounds;

        var yeah = new Rectangle(leftMargin, topMargin, pagebounds.Width - (leftMargin * 2), pagebounds.Height - (topMargin * 2));
        
        ev.Graphics.DrawString(textToPrint, printFont, Brushes.Black,
               yeah, new StringFormat());
    }

    // Print the file.
    public void Print(string text)
    {
        try
        {
            textToPrint = text;
            printFont = new Font("Arial", 10);
            PrintDocument doc = new PrintDocument();
            
            doc.PrintPage += new PrintPageEventHandler(doc_PrintPage);

            doc.Print();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}