using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Pdfa;
using PDF_A.Properties;
using pdftron.PDF.PDFA;

namespace PDF_A
{
    public partial class FormMain : Form
    {
        private string _inputDir;
        private string _outputDir;
        readonly Dictionary<string, string> _prefix = new Dictionary<string, string>();

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            IEnumerable<string[]> profixList = File.ReadLines("prefiks.txt", Encoding.UTF8).Select(f => f.Split(';'));

            string type = IniFile.ReadIni("settings", "type");

            switch (type)
            {
                case "operaty":
                    
                    foreach (string[] prefix in profixList)
                    {
                        _prefix.Add("-" + prefix[0] + ".", prefix[1]);
                        _prefix.Add("-" + prefix[0] + "_", prefix[1]);
                        _prefix.Add("_" + prefix[0] + ".", prefix[1]);
                        _prefix.Add("_" + prefix[0] + "_", prefix[1]);
                        _prefix.Add("-" + prefix[0] + "-", prefix[1]);
                    }
                    break;

                case "katastralne":

                    foreach (string[] prefix in profixList)
                    {
                        _prefix.Add(prefix[0] + ".", prefix[1]);
                        _prefix.Add(prefix[0] + "_", prefix[1]);
                        _prefix.Add(prefix[0] + "-", prefix[1]);
                    }
                    break;
            }

            if (!File.Exists("PDF_A.ini"))
            {
                IniFile.SaveIni("settings", "type", "operaty");

                IniFile.SaveIni("metadane", "Title", "");
                IniFile.SaveIni("metadane", "Author", "");
                IniFile.SaveIni("metadane", "Subject", "");
                IniFile.SaveIni("metadane", "Keywords", "");

                IniFile.SaveIni("metadane_dod", "podstawa_prawna", "");
            }

            textBoxInputDir.Text = IniFile.ReadIni("directory", "lastInputDir");
            textBoxOutputDir.Text = IniFile.ReadIni("directory", "lastOutputDir");
        }

        private void ButtonInputDir_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.ShowNewFolderButton = false;
                fbd.SelectedPath = IniFile.ReadIni("directory", "lastInputDir");

                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    _inputDir = fbd.SelectedPath;
                    textBoxInputDir.Text = _inputDir;
                    IniFile.SaveIni("directory", "lastInputDir", _inputDir);
                }
            }
        }

        private void ButtonOutputDir_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.ShowNewFolderButton = true;
                fbd.SelectedPath = IniFile.ReadIni("directory", "lastOutputDir");

                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    _outputDir = fbd.SelectedPath;
                    textBoxOutputDir.Text = _outputDir;
                    IniFile.SaveIni("directory", "lastOutputDir", _outputDir);
                }
            }
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            string title = IniFile.ReadIni("metadane", "Title");
            string author = IniFile.ReadIni("metadane", "Author");
            string subject = IniFile.ReadIni("metadane", "Subject");
            string keywords = IniFile.ReadIni("metadane", "Keywords");

            string podstawaPrawna = IniFile.ReadIni("metadane_dod", "podstawa_prawna");

            List<string> inputFolders = new List<string> {_inputDir};
            inputFolders.AddRange(Directory.GetDirectories(_inputDir, "*", SearchOption.AllDirectories).ToList());

            Dictionary<string, int> validationErrors = new Dictionary<string, int>();
            Dictionary<string, int> prefiksErrors = new Dictionary<string, int>();
            Dictionary<string, string> otherErrors = new Dictionary<string, string>();

            foreach (string inputFolder in inputFolders)
            {
                string outputFolder = inputFolder.Replace(_inputDir, _outputDir);

                Directory.CreateDirectory(outputFolder);

                string[] inputFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

                foreach (string inputFile in inputFiles)
                {
                    string outputFile = Path.Combine(outputFolder, Path.GetFileName(inputFile) ?? throw new InvalidOperationException());

                    PdfDocument srcPdf = new PdfDocument(new PdfReader(inputFile));

                    PdfOutputIntent rgbIntent = new PdfOutputIntent("Custom", 
                        "", 
                        "http://www.color.org",
                        "sRGB IEC61966-2.1", 
                        new MemoryStream(Resources.sRGB_CS_profile));

                    PdfADocument outputPdf = new PdfADocument(new PdfWriter(outputFile), PdfAConformanceLevel.PDF_A_3B, rgbIntent);

                    try
                    {
                        srcPdf.CopyPagesTo(1, srcPdf.GetNumberOfPages(), outputPdf);
                    }
                    catch (Exception exception)
                    {
                        srcPdf.Close();

                        Document document = new Document(outputPdf); 
                        document.Add(new Paragraph());
                        document.Close();
                        outputPdf.Close();

                        otherErrors.Add(inputFile, exception.Message);

                        continue;
                    }

                    srcPdf.Close();

                    PdfDocumentInfo info = outputPdf.GetDocumentInfo();

                    info.SetCreator("GN PDF_A Creator");

                    info.SetTitle(title);
                    info.SetAuthor(author);
                    info.SetSubject(subject);
                    info.SetKeywords(keywords);

                    info.SetMoreInfo(@"Podstawa prawna", podstawaPrawna);

                    string file = Path.GetFileName(outputFile);

                    Dictionary<string, string> rodzajDokumentu = _prefix.Where(p => file.ToUpper().Contains(p.Key.ToUpper())).ToDictionary(x => x.Key, x => x.Value);

                    if (rodzajDokumentu.Count != 1)
                    {
                        prefiksErrors.Add(inputFile, rodzajDokumentu.Count);
                        info.SetMoreInfo(@"Rodzaj Dokumentu", "");  
                    }
                    else
                    {
                        info.SetMoreInfo(@"Rodzaj Dokumentu", rodzajDokumentu.Values.ElementAt(0));  
                    }
                        
                    info.SetMoreInfo("software", @"The file was created by the PDF_A program using iText 7 library");
                    info.SetMoreInfo("license", @"This program is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.");
                    info.SetMoreInfo("copyright", @"Copyright (C) 2019 GISNET");

                    for (int i = 0; i <= outputPdf.GetNumberOfPdfObjects(); i++)
                    {
                        PdfObject pdfObject = outputPdf.GetPdfObject(i);

                        if (pdfObject != null && pdfObject.IsStream())
                        {
                            PdfStream pdfStream = (PdfStream)pdfObject;

                            PdfObject subtype = pdfStream.Get(PdfName.Subtype);

                            if (Equals(subtype, PdfName.Image) && pdfStream.ContainsKey(PdfName.Interpolate))
                            {
                                pdfStream.Remove(PdfName.Interpolate);    
                            }
                        }
                    }

                    try
                    {
                        outputPdf.Close();
                    }
                    catch (Exception exception)
                    {
                        otherErrors.Add(inputFile, exception.Message);

                        continue;
                    }

                    using (PDFACompliance pdfA = new PDFACompliance(false, outputFile, null, PDFACompliance.Conformance.e_Level3B, null, 10, false))
                    {
                        int errCnt = pdfA.GetErrorCount();

                        if (errCnt > 0)
                        {
                            validationErrors.Add(outputFile, errCnt);    
                        }
                    }
                }
            }

            using (StreamWriter errorFile = new StreamWriter(new FileStream("errors_validation.txt", FileMode.Create)))
            {
                foreach (var error in validationErrors)
                {
                    errorFile.WriteLine(error.Key + ": " + error.Value);
                }
            }

            using (StreamWriter errorFile = new StreamWriter(new FileStream("errors_prefix.txt", FileMode.Create)))
            {
                foreach (var error in prefiksErrors)
                {
                    errorFile.WriteLine(error.Key + ": " + error.Value);
                }
            }

            using (StreamWriter errorFile = new StreamWriter(new FileStream("errors_other.txt", FileMode.Create)))
            {
                foreach (var error in otherErrors)
                {
                    errorFile.WriteLine(error.Key + ": " + error.Value);
                }
            }

            MessageBox.Show(@"Przetwarzanie zakończono.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TextBoxInputDir_TextChanged(object sender, EventArgs e)
        {
            _inputDir = textBoxInputDir.Text;
            IniFile.SaveIni("directory", "lastInputDir", _inputDir);
        }

        private void TextBoxOutputDir_TextChanged(object sender, EventArgs e)
        {
            _outputDir = textBoxOutputDir.Text;
            IniFile.SaveIni("directory", "lastOutputDir", _outputDir);
        }
    }
}
